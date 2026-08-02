using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public enum Icon
    { 
        NULL = 0,
        Empty = 1,
        Search = 2,
        Check = 3,
    }
    public enum FieldTyp
    {
        Text,
        TextArea,
        Select,


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
        
        Hidden,
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
