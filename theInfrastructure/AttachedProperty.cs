using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace theInfrastructure
{


    // 1. Storage für die Zusatzdaten definieren
    public static class AttachedProperty
    {
        private static readonly ConditionalWeakTable<object, Dictionary<string, object?>> _storage = new();

        public static void SetAttachedProperty(this IDTO target, string propertyName, object? value)
        {
            var properties = _storage.GetOrCreateValue(target);
            properties[propertyName] = value;
        }

        public static T? GetAttachedProperty<T>(this IDTO target, string propertyName)
        {
            if (_storage.TryGetValue(target, out var properties) &&
                properties.TryGetValue(propertyName, out var value) &&
                value is T typedValue)
            {
                return typedValue;
            }
            return default;
        }
    }
}

//// 2. Verwendung mit DTO und SQL-Join-Daten
//var dto = new UserDto { Id = 1, Name = "Max Mustermann" };

//// Wert aus SQL-Join anheften
//dto.SetAttachedProperty("JoinedRoleName", "Administrator");

//// Wert abrufen
//string? role = dto.GetAttachedProperty<string>("JoinedRoleName");

