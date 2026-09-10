using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace theInfrastructure
{
    public class SMTPConfiguration
    {
        public bool IsEMailActive { get; set; } = false;
        public string Server { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public bool SSL { get; set; } = true;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UrlToDeactivateMailReceiving { get; set; } = string.Empty;


        public void Save()
        {
            SMTPConfiguration config = this as SMTPConfiguration;

            Serializer.Save<SMTPConfiguration>(config, "smtp.config");
        }
        public static SMTPConfiguration Load()
        {
            return Serializer.Load<SMTPConfiguration>("smtp.config");
        }

    }

    public enum EMailType
    {
        NULL = 0,

        RegistrationWelcomeMail = 1,
        LoginMail = 2,
        RequestNewPassword = 3,
        PasswordChange = 4,

        OrderMail = 11,
        InvoiceMail = 12,

        ContactMail = 21,

        Notification = 31,
    }
    public class MailFileAttachment
    {
        public string ContentType { get; set; } = "application/pdf";
        public string FileName { get; set; } = string.Empty;
        public byte[] Array { get; set; } = null;
    }
  
    

    public static partial class Helper
    {

        public static async Task SendSmtpMailAsync(string to, string topic, string message, EMailType mailtype,
                SMTPConfiguration config, string filename = "", string from = "", string template = "mail",
                    string link = "", string code = "", string inlineTitle = "",
                        List<MailFileAttachment> attachments = null, Guid userGUID = default)
        {
            // Validierung
            if (config == null || string.IsNullOrWhiteSpace(to) || !to.Contains("@"))
                return;

            if (string.IsNullOrEmpty(config.Server) || string.IsNullOrEmpty(config.Port) || !int.TryParse(config.Port, out int port))
                return;

            // Mail-Inhalt generieren
            string innerHTML = GetHtmlByMailType(topic, message, mailtype, link, code);
            string fullHTML = GetEmptyHtmlMailTemplate(innerHTML, topic, config, userGUID);

            using var msg = new MailMessage();
            try
            {
                msg.From = new MailAddress(from, "the App Tools", Encoding.UTF8);
                msg.To.Add(to);
                msg.IsBodyHtml = true;

                // --- Umlaut-Fixing: Explizites UTF-8 Encoding setzen ---
                msg.SubjectEncoding = Encoding.UTF8;
                msg.BodyEncoding = Encoding.UTF8;
                msg.HeadersEncoding = Encoding.UTF8;

                msg.Subject = !string.IsNullOrEmpty(topic) ? topic : "Mail";
                msg.Body = fullHTML;

                // 1. Datei-Anhang (Pfad)
                if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                {
                    var data = new Attachment(filename, MediaTypeNames.Application.Octet);
                    var disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(filename);
                    disposition.ModificationDate = File.GetLastWriteTime(filename);
                    disposition.ReadDate = File.GetLastAccessTime(filename);
                    msg.Attachments.Add(data);
                }

                // 2. Automatischer Widerruf-Anhang bei bestimmten E-Mail-Typen
                if (mailtype == EMailType.InvoiceMail || mailtype == EMailType.OrderMail)
                {
                    string wdrlink = "./files/Widerrufserklaerung.docx";
                    if (File.Exists(wdrlink))
                    {
                        var wdl = new Attachment(wdrlink, MediaTypeNames.Application.Octet);
                        var disposition = wdl.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(wdrlink);
                        disposition.ModificationDate = File.GetLastWriteTime(wdrlink);
                        disposition.ReadDate = File.GetLastAccessTime(wdrlink);
                        msg.Attachments.Add(wdl);
                    }
                }

                // 3. Dynamische In-Memory Anhänge
                if (attachments != null && attachments.Count > 0)
                {
                    foreach (var file in attachments)
                    {
                        if (file?.Array == null)
                            continue;

                        var contentType = new ContentType(file.ContentType);
                        var contentStream = new MemoryStream(file.Array);
                        var fa = new Attachment(contentStream, contentType);

                        var disposition = fa.ContentDisposition;
                        disposition.FileName = file.FileName;
                        disposition.CreationDate = DateTime.Now;
                        disposition.ModificationDate = DateTime.Now;
                        disposition.ReadDate = DateTime.Now;

                        msg.Attachments.Add(fa);
                    }
                }

                // Versand-Logik
                await ExecuteSendAsync(config, port, msg);
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung / Logging
            }
        }

        private static async Task ExecuteSendAsync(SMTPConfiguration config, int port, MailMessage msg)
        {
            try
            {
                using var client = CreateSmtpClient(config, port);
                await client.SendMailAsync(msg);
            }
            catch
            {
                int fallbackPort = (port == 587) ? 465 : 587;
                using var fallbackClient = CreateSmtpClient(config, fallbackPort);
                await fallbackClient.SendMailAsync(msg);
            }
        }

        private static SmtpClient CreateSmtpClient(SMTPConfiguration config, int port)
        {
            return new SmtpClient(config.Server, port)
            {
                Credentials = new NetworkCredential(config.User, config.Password),
                EnableSsl = config.SSL
            };
        }

        public static string GetHtmlByMailType(string header, string message, EMailType mailType, string link = "", string code = "")
        {
            string html = string.Empty;

            // LOGIN 
            if (mailType == EMailType.LoginMail)
            {
                html += "<div style='padding:15px;'>";
                html += "<div style=''><h2 style=''>Anmeldung</h2></div>";

                html += "<div style='padding-top:10px;padding-bottom:5px;'><strong>" + message + "</strong></div>";

                html += "<div style='padding-top:5px;margin-top:5px;'>";
                html += "<span>Wir möchten Sie darüber informieren, dass mit Ihren Anmeldedaten ein Anmeldung am System stattgefunden hat. </span>";
                html += "<span>Wenn Sie diese Anmeldung durchgeführt haben, besteht Ihrerseits kein Handlungsbedarf. </span>";
                html += "<span>Sollten Sie sich nicht vor kurzem angemeldet haben, dann möchten wir sie bitten umgehend ihr Passwort zu ändern. </span>";
                html += "<span>Es ist wichtig, dass sie ihre Anmeldedaten sicher aufbewahren. </span>";
                html += "</div>";

                html += "</div>";
            }

            // REGISTRATION 
            else if (mailType == EMailType.RegistrationWelcomeMail)
            {
                html += "<div style='padding:10px;'>";
                html += "<div style='text-align: center;'><h2 style='text-align: center;'>Abschluss der Registrierung</h2></div>";
                html += "<div style='text-align: center;'><h1 style='text-align: center; font-weight:light;'>the Apps Plattform</h1></div>";

                html += "<div style='text-align: center; padding-top:20px;padding-bottom:20px;'>" + message + "</div>";

                html += "<div style='text-align: center; padding-top:20px;padding-bottom:30px;'><a href='" + link + "'>Link zur Bestätigung der Registrierung</a></div>";

                html += "<div style='text-align: center; background-color:#ddd; border-radius: 0.6rem; padding:15px;'><strong>" + code + "</strong></div>";
                html += "</div>";
            }

            // Request New Password 
            else if (mailType == EMailType.RequestNewPassword)
            {
                html += "<div style='padding:15px;'>";
                html += "<div style='text-align: center;'><h2 style='text-align: center;'>Neues Passwort setzen</h2></div>";
                html += "<div style='padding-top:15px; padding-bottom:15px; text-align: center;'><a href='" + link + "'>Link zum Setzen eines Passwortes</a></div>";
                html += "<div style='padding:15px; text-align: center;'>" + message + "</div>";
                html += "</div>";
            }

            // Contact 
            else if (mailType == EMailType.ContactMail)
            {
                string path = $"{System.IO.Directory.GetCurrentDirectory()}{@"\wwwroot\mail\ContactMe.html"}";
                if (System.IO.File.Exists(path))
                {
                    html += System.IO.File.ReadAllText(path);
                    html = html.Replace("#message#", message);
                }
                else
                {
                    html += "<div style='padding:10px;'>";
                    html += "<div><h2>Kontakt E-Mail</h2></div>";
                    html += "<div style='padding:10px;'>" + message + "</div>";
                    html += "</div>";
                }
            }

            // Password Change 
            else if (mailType == EMailType.PasswordChange)
            {
                html += "<div style='padding:10px;'>";
                html += "<div><h2>Änderung Ihres Passworts</h2></div>";
                html += "<div style='padding:10px;'>" + message + "</div>";
                html += "</div>";
            }

            // Order 
            else if (mailType == EMailType.OrderMail)
            {
                html += "<div style='padding:10px;'>";
                html += "<div><h2>Ihre Bestellung</h2></div>";
                html += "<div><p>Vielen Dank, dass Sie sich für eines unserer Produkte entschieden haben.</p></div>";
                html += "<div><p>In Ihrem Konto erhalten Sie eine Übersicht über Ihre Bestellung.</p></div>";
                html += "<div><p>Sie erhalten in Kürze eine Rechnung per PayPal bzw. Stripe zugesendet.</p></div>";
                html += "<div><p>" + message + "</p></div>";
                html += "</div>";
            }

            // Invoice 
            else if (mailType == EMailType.InvoiceMail)
            {
                string path = "./Templates/InvoiceMail.html";
                if (System.IO.File.Exists(path))
                {
                    html += System.IO.File.ReadAllText(path);
                }
                else
                {
                    html += "<div style='padding:10px;'>";
                    html += "<div><h2>Ihre Rechnung</h2></div>";
                    html += "<div><p>Vielen Dank, dass Sie sich für unsere Produkte entschieden haben.</p></div>";
                    html += "<div style='padding:10px;'>" + message + "</div>";
                    html += "</div>";
                }
            }

            // Notification 
            else if (mailType == EMailType.Notification)
            {
                html += "<div style='padding:10px;'>";
                html += "<div><h2>Benachrichtigung</h2></div>";
                html += "<div><p>Ein Datensatz wurde erzeugt, aktualisiert oder gelöscht.</p></div>";
                html += "<div style='padding:10px;'>" + message + "</div>";
                html += "</div>";
            }

            // Message 
            else
            {
                html += "<div style='padding:2rem;'>";
                html += "<div>" + message + "</div>";
                html += "</div>";
            }

            return html;
        }

        public static string GetEmptyHtmlMailTemplate(string innerHTML, string title, SMTPConfiguration mailConfig, Guid userGUID = new Guid())
        {
            try
            {
                string htmlFrame = string.Empty;

                // HEAD 
                htmlFrame += "<!DOCTYPE html>";
                htmlFrame += "<html>";
                htmlFrame += "<head>";
                htmlFrame += "<meta charset='utf-8' />";

                htmlFrame += "<title></title>";

                // STYLE 
                htmlFrame += "<style>";

                htmlFrame += "html, body ";
                htmlFrame += " { ";
                htmlFrame += " font-family: Verdana, Arial, Helvetica, sans-serif !important; ";
                htmlFrame += " background-color: #eee !important; ";
                htmlFrame += " text-align:left; ";
                htmlFrame += " } ";

                htmlFrame += " table, tr, td, th { ";
                htmlFrame += " padding:2px!important; ";
                htmlFrame += " padding-left: 5px!important; ";
                htmlFrame += " padding-right: 5px!important; ";
                htmlFrame += " } ";

                htmlFrame += " .text-primary { color: #1c75ca; } ";
                htmlFrame += " .colorBlue { color: #1c75ca; } ";
                htmlFrame += " .colorDeep { color: #333; } ";
                htmlFrame += " .colorDark { color: #555; } ";
                htmlFrame += " .colorGrey { color: #aaa; } ";
                htmlFrame += " .colorMuted { color: #bbb; } ";

                htmlFrame += " </style> ";
                htmlFrame += " </head> ";

                // BODY 
                htmlFrame += "<body class='colorDark' ";
                htmlFrame += " style='overflow:hidden; " +
                                " margin:0; " +
                                " padding:0; " +
                                " -webkit-text-size-adjust:none; " +
                                " -ms-text-size-adjust:none; ' ";
                htmlFrame += " topmargin='0' " +
                                " marginwidth='0' " +
                                " marginheight='0' " +
                                " leftmargin='0' " +
                                " bgcolor='#f9f9f9'> ";

                // FRAME 
                htmlFrame += "<table style='overflow:hidden;' class='colorDark' width='100%' cellspacing='0' cellpadding='0' border='0'>";
                htmlFrame += "<tr>";
                htmlFrame += "<td align='center' style='overflow: hidden;'>";


                // table -> INNER HTML -> CONTENT Rahmen 
                htmlFrame += "<table class='colorDark' ";
                htmlFrame += " style=' " +
                            " border:1px solid #ddd; " +
                            " border-radius: 0.65rem; " +
                            " overflow:hidden; " +
                            " '";
                htmlFrame += " bgcolor='#ffffff' width='565' cellspacing='0' cellpadding='0' border='0'> ";

 
                htmlFrame += "<tr>";
                htmlFrame += "<td class='colorDark' ";
                htmlFrame += " style='height:760px;  overflow:hidden;  vertical-align:top;  text-align:left;  padding:0.5rem;'>";

                htmlFrame += innerHTML;

                htmlFrame += "</td>";
                htmlFrame += "</tr>";


                htmlFrame += "</table>";



                if (userGUID != Guid.Empty)
                {
                    // Empty 
                    if (!string.IsNullOrEmpty(mailConfig.UrlToDeactivateMailReceiving))
                    {
                        // Table 
                        htmlFrame += "<table style='overflow:hidden;' width='575' cellspacing='0' cellpadding='0' border='0'>";
                        htmlFrame += "<tr>";
                        htmlFrame += "<td class='colorMuted' style='font-size:11px; padding:2rem;'>";
                        htmlFrame += "<span>Wenn Sie Nachrichten nicht mehr erhalten möchten, können Sie diese </span>";

                        htmlFrame += "<span style='padding:5px;'><a style='font-weight:bold;' " +
                                    " href = '" + mailConfig.UrlToDeactivateMailReceiving + "/" + userGUID + "'> hier abbestellen</a>.</span>";

                        htmlFrame += "</td>";
                        htmlFrame += "</tr>";
                        htmlFrame += "</table>";
                    }
                }

                // Hinweise per Mail  
                htmlFrame += "<table style='overflow:hidden;' width='575' cellspacing='0' cellpadding='0' border='0'>";
                htmlFrame += "<tr>";
                htmlFrame += "<td style=' padding:2rem; '>";
                htmlFrame += "<p class='colorGrey'>";
                htmlFrame += "<small>";
                htmlFrame += "<strong>Hinweis: </strong>";
                htmlFrame += "<span> Wir sind bemueht, Ihnen ausschliesslich E-Mails zu senden </span>";
                htmlFrame += "<span> die dem Schutze Ihrer Sicherheit dienen bzw.vertraglich oder technisch notwendig sind. </span>";
                htmlFrame += "<span> Falls Sie mit einer unserer E-Mails nicht einverstanden sind, </span>";
                htmlFrame += "<span> wenden Sie sich bitte an <a class='colorGrey' href = 'mailto:info@...?subject=Deactivate-Mail'>...</a> </span>";
                htmlFrame += "<span> Bitte haben Sie Verstaendnis, dass wir aus verschiedenen teschnichen Gruenden </span>";
                htmlFrame += "<span> oder Gruenden die Ihrer Internetsicherheit dienen, </span>";
                htmlFrame += "<span> den Versand aller oder bestimmter E-Mails nicht einstellen koennen. </span>";
                htmlFrame += "</small>";
                htmlFrame += "</p>";
                htmlFrame += "</td>";
                htmlFrame += "</tr>";
                htmlFrame += "</table>";



                // Table 
                htmlFrame += "<table style='overflow:hidden;' width='575' cellspacing='0' cellpadding='0' border='0'>";
                htmlFrame += "<tr>";
                htmlFrame += "<td class='colorMuted' style='font-size:11px; padding:2rem;'>";
                htmlFrame += "<strong>Mit freundlichen Gruessen</strong>";
                htmlFrame += "<div>...</div>";
                htmlFrame += "<div>...</div>";
                htmlFrame += "<div>...</div>";
                htmlFrame += "<div>Mail: <a href = 'mailto:info@...?subject=Info'> info@...</a></div>";
                htmlFrame += "</td>";
                htmlFrame += "</tr>";
                htmlFrame += "</table>";

                // Frame 
                htmlFrame += "</td>";
                htmlFrame += "</tr>";
                htmlFrame += "</table>";

                // Body 
                htmlFrame += "</body>";
                htmlFrame += "</html>";


                return htmlFrame;

            }
            catch (Exception ex)
            {

            }

            return string.Empty;
        }

    }
}
