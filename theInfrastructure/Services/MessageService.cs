using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public interface IMessagingBusService
    {
        bool IsInit();
        void InitService(ISqlDatabaseService sql);

        event Action<AppMessage>? OnMessage;

        void Publish(AppMessage msg);

        // Bequemer Überladungs-Helfer
        void Publish(string id, BusAction action, object? obj = null, Action<AppMessage>? reply = null);
    }

    public class MessagingBusService : IMessagingBusService
    {
        private ISqlDatabaseService _sql;

        public void InitService(ISqlDatabaseService sql)
        {
            _sql = sql;
        }

        public bool IsInit()
        {
            if (_sql == null)
                return false;

            return true;
        }


        public event Action<AppMessage>? OnMessage;

        public void Publish(AppMessage msg)
        {
            OnMessage?.Invoke(msg);
        }

        // Bequemer Überladungs-Helfer
        public void Publish(string id, BusAction action, object? obj = null, Action<AppMessage>? reply = null)
        {
            Publish(new AppMessage(id, action, obj, reply));
        }

    }

    public enum BusAction
    {
        // Requests / Benachrichtigungen
        Refresh,  Select, Info, Search
    }

    public record AppMessage(
        string Id,
        BusAction Action,
        object? Payload = null,
        Action<AppMessage>? Reply = null
    );


}
