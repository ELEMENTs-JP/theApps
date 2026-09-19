using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net;
//using System.Net.Mail;
//using System.Net.Mime;
using System.Text;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

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

        public bool IsValid()
        {
            if (IsEMailActive == false)
                return false;

            if (string.IsNullOrEmpty(Server))
                return false;

            if (string.IsNullOrEmpty(Port))
                return false;

            if (string.IsNullOrEmpty(User))
                return false;

            if (string.IsNullOrEmpty(Password))
                return false;

            return true;
        }

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
                html += $"<div style='padding-top:15px; padding-bottom:15px; text-align: center;'><a href='{link}'>Link zum Setzen eines Passwortes</a></div>";
                html += $"<div style='padding-top:15px; padding-bottom:15px; text-align: center;'>Alternativ: {link}</div>";
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
                var sb = new System.Text.StringBuilder();

                // HEAD 
                sb.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                sb.Append("<head>");
                sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
                sb.Append("<title></title>");
                sb.Append("</head>");

                // BODY 
                sb.Append("<body style=\"margin:0; padding:0; background-color:#eee; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%;\">");

                // MAIN OUTER CONTAINER (Verhindert Rand-Probleme & steuert Hintrgrund)
                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#eee\" style=\"background-color:#eee;\">");
                sb.Append("<tr>");
                sb.Append("<td align=\"center\" style=\"padding:25px 15px;\">"); // Padding ersetzt margin

                // INNER CONTENT BOX
                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"565\" bgcolor=\"#ffffff\" style=\"background-color:#ffffff; border:1px solid #ddd; border-radius:10px; width:565px;\">");
                sb.Append("<tr>");
                sb.Append("<td align=\"left\" valign=\"top\" style=\"padding:10px; font-family:Verdana, Arial, sans-serif; font-size:14px; color:#555555;\">");

                sb.Append(innerHTML);

                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                // UN订阅 / ABBESTELLEN
                if (userGUID != Guid.Empty && !string.IsNullOrEmpty(mailConfig.UrlToDeactivateMailReceiving))
                {
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"565\" style=\"width:565px;\">");
                    sb.Append("<tr>");
                    sb.Append("<td align=\"left\" style=\"padding:20px; font-family:Verdana, Arial, sans-serif; font-size:11px; color:#bbbbbb;\">");
                    sb.Append("<span>Wenn Sie Nachrichten nicht mehr erhalten möchten, können Sie diese </span>");
                    sb.Append("<a href=\"").Append(mailConfig.UrlToDeactivateMailReceiving).Append("/").Append(userGUID).Append("\" style=\"color:#bbbbbb; font-weight:bold; text-decoration:underline;\">hier abbestellen</a>.");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }

                // HINWEISE
                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"565\" style=\"width:565px;\">");
                sb.Append("<tr>");
                sb.Append("<td align=\"left\" style=\"padding:0 20px 20px 20px; font-family:Verdana, Arial, sans-serif; font-size:11px; color:#aaaaaa; line-height:1.4;\">");
                sb.Append("<strong>Hinweis: </strong>");
                sb.Append("Wir sind bemüht, Ihnen ausschließlich E-Mails zu senden, die dem Schutz Ihrer Sicherheit dienen bzw. vertraglich oder technisch notwendig sind. ");
                sb.Append("Falls Sie mit einer unserer E-Mails nicht einverstanden sind, wenden Sie sich bitte an ");
                sb.Append("<a href=\"mailto:info@...?subject=Deactivate-Mail\" style=\"color:#aaaaaa; text-decoration:underline;\">info@...</a>. ");
                sb.Append("Bitte haben Sie Verständnis, dass wir aus verschiedenen technischen Gründen oder Gründen, die Ihrer Internetsicherheit dienen, ");
                sb.Append("den Versand aller oder bestimmter E-Mails nicht einstellen können.");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                // FOOTER / GRUSSFORMEL
                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"565\" style=\"width:565px;\">");
                sb.Append("<tr>");
                sb.Append("<td align=\"left\" style=\"padding:0 20px 20px 20px; font-family:Verdana, Arial, sans-serif; font-size:11px; color:#bbbbbb; line-height:1.4;\">");
                sb.Append("<strong style=\"color:#555555;\">Mit freundlichen Grüßen</strong><br />");
                sb.Append("<div>...</div>");
                sb.Append("<div>...</div>");
                sb.Append("<div>...</div>");
                sb.Append("<div>Mail: <a href=\"mailto:info@...?subject=Info\" style=\"color:#bbbbbb; text-decoration:underline;\">info@...</a></div>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                // CLOSING TAGS
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</body>");
                sb.Append("</html>");

                return sb.ToString();

            }
            catch (Exception ex)
            {

            }

            return string.Empty;
        }

    }

    public static partial class Helper
    {
        // Mailkit 
        public static async Task SendSmtpMailKitAsync(string to, string topic, string message, EMailType mailtype,
                    SMTPConfiguration config, string filename = "",  string template = "mail",
                        string link = "", string code = "", string inlineTitle = "",
                            List<MailFileAttachment> attachments = null, Guid userGUID = default)
        {
            // Validierung
            if (config == null || !config.IsEMailActive || string.IsNullOrWhiteSpace(to) || !to.Contains("@"))
                return;

            if (config.IsValid() == false)
                return;

            // Sender 
            string from = config.User;

            // Mail-Inhalt generieren
            string innerHTML = GetHtmlByMailType(topic, message, mailtype, link, code);
            string fullHTML = GetEmptyHtmlMailTemplate(innerHTML, topic, config, userGUID);

            try
            {
                var email = new MimeMessage();

                // Absender & Empfänger
                string senderAddress = string.IsNullOrWhiteSpace(from) ? config.User : from;
                email.From.Add(new MailboxAddress("the App Tools", senderAddress));
                email.To.Add(MailboxAddress.Parse(to));

                // Betreff
                email.Subject = !string.IsNullOrEmpty(topic) ? topic : "Mail";

                // BodyBuilder verwaltet Text, HTML und Anhänge
                var builder = new BodyBuilder
                {
                    HtmlBody = fullHTML
                };

                // 1. Datei-Anhang (Pfad)
                if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                {
                    builder.Attachments.Add(filename);
                }

                // 2. Automatischer Widerruf-Anhang bei bestimmten E-Mail-Typen
                if (mailtype == EMailType.InvoiceMail || mailtype == EMailType.OrderMail)
                {
                    string wdrlink = "./files/Widerrufserklaerung.docx";
                    if (File.Exists(wdrlink))
                    {
                        builder.Attachments.Add(wdrlink);
                    }
                }

                // 3. Dynamische In-Memory Anhänge
                if (attachments != null && attachments.Count > 0)
                {
                    foreach (var file in attachments)
                    {
                        if (file?.Array == null)
                            continue;

                        // ContentType parsen oder Fallback nutzen
                        if (!MimeKit.ContentType.TryParse(file.ContentType, out var parsedContentType))
                        {
                            parsedContentType = new MimeKit.ContentType("application", "octet-stream");
                        }

                        builder.Attachments.Add(file.FileName, file.Array, parsedContentType);
                    }
                }

                email.Body = builder.ToMessageBody();

                // Versand-Logik via MailKit
                await ExecuteSendAsync(config, email);
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung / Logging
            }
        }

        private static async Task ExecuteSendAsync(SMTPConfiguration config, MimeMessage message)
        {
            try
            {
                await SendWithPortAsync(config, config.Port.ToSecureInt(), message);
            }
            catch
            {
                // Fallback-Port versuchen (z. B. 465 statt 587 oder umgekehrt)
                int fallbackPort = (config.Port.ToSecureInt() == 587) ? 465 : 587;
                await SendWithPortAsync(config, fallbackPort, message);
            }
        }

        private static async Task SendWithPortAsync(SMTPConfiguration config, int port, MimeMessage message)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();

            // SSL/TLS-Option basierend auf Port und Config festlegen
            SecureSocketOptions socketOptions;
            if (!config.SSL)
            {
                socketOptions = SecureSocketOptions.None;
            }
            else if (config.Port.ToSecureInt() == 465)
            {
                socketOptions = SecureSocketOptions.SslOnConnect;
            }
            else
            {
                socketOptions = SecureSocketOptions.StartTls;
            }

            // Verbindung aufbauen
            await client.ConnectAsync(config.Server, config.Port.ToSecureInt(), socketOptions);

            // Authentifizieren (falls Anmeldedaten vorhanden sind)
            if (!string.IsNullOrEmpty(config.User) && !string.IsNullOrEmpty(config.Password))
            {
                await client.AuthenticateAsync(config.User, config.Password);
            }

            // E-Mail senden & Verbindung trennen
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }



}
