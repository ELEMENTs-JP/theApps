using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // File 
    public class FileNotification
    {
        public string FullFilePath { get; set; }
        public Guid FileGUID { get; set; }
        public string OriginalFileName { get; set; }
        public decimal FileSizeInByte { get; set; }
        public decimal FileSizeInKB { get; set; }
        public decimal FileSizeInMB { get; set; }
        public string FileTextContent { get; set; }
        public string FileExtension { get; set; }
        public string Content { get; set; }
    }
    public class FileNotificationService
    {
        public event Func<FileNotification, Task> Notification;

        public void Notify(FileNotification notification)
        {
            if (Notification != null)
            {
                Notification.Invoke(notification);
            }
        }
    }
}
