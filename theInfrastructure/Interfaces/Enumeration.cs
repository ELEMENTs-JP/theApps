using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public enum FieldTyp
    {
        Text,
        TextArea,
        Hidden,


        Number,
        Integer,
        Decimal,
        
        Password,
        Email,
        Url,
        
        DateTimeLocal,
        Date,
        Time,
        
        Month,
        Week,
        Tel,
        
        Search,
        Color,
        Range,
        
    }

    public enum DatabaseTyp
    {
        NULL = 0,
        Master = 1,
        Client = 2,
    }

    public enum LoadingPosition
    { 
        NULL = 0,
        Head = 1,
        Body = 2,
    }

}
