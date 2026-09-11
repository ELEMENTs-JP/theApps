using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public interface INotificationService
    {
        event Action<IDTO, NotificationType, IDTO> OnNotify;
        void NotifyChanged(IDTO item, NotificationType typ, IDTO user);
    }

    public class NotificationService : INotificationService
    {
        // C#-Event für Änderungen
        public event Action<IDTO, NotificationType, IDTO> OnNotify;

        // Person B ruft diese Methode nach Speichern in der DB auf
        public void NotifyChanged(IDTO item, NotificationType typ, IDTO user)
        {
            OnNotify?.Invoke(item, typ, user);
        }
    }

    public enum NotificationType
    {
        // Requests / Benachrichtigungen
        Update, Delete
    }
}
