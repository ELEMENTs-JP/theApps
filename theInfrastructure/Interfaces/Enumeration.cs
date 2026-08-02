using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public enum InputType
    {
        Text,
        TextArea,
        Password,
        Email,
        Number,
        Integer,
        Decimal,
        Tel,
        Url,
        Search,
        Date,
        Time,
        DateTimeLocal,
        Month,
        Week,
        Color,
        Range,
        Hidden
    }

    public enum DatabaseTyp
    {
        NULL = 0,
        Master = 1,
        Client = 2,
    }

}
