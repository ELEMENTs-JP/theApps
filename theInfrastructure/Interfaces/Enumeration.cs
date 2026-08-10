using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public enum StaticPosition
    {
        NULL = 0,

        Relative,
        ScreenCenter,
        MessageBox,

        TopLeft,
        TopMiddle,
        TopRight,

        MiddleLeft,
        MiddleMiddle,
        MiddleRight,

        BottomLeft,
        BottomMiddle,
        BottomRight,

    }
    public enum ModalFormSize { Small, Normal, Large, Extra, Full }
    public enum Icon
    { 
        NULL = 0,
        Empty = 1,
        Search = 2,
        Check = 3,
        Menu = 4,
        Config = 5,
    }
    public enum ValueBindingTyp
    { 
        NULL = 0,
        Title = 1,
        Description = 2,
        Property = 3,
    }
    public enum DeviceDisplay
    { 
        NULL = 0,
        Phone = 1,
        Tablet = 2,
        Desktop = 3,
    }
    public enum FieldTyp
    {
        Text,
        TextArea,
        DropDown,
        CheckBox,
        Heading,

        Number,
        Integer,
        Decimal,
        
        Password,
        Email,
        Url,
        
        DateTime,
        Date,
        Time,
        
        Month,
        Week,
        Tel,
        
        Search,
        Color,
        Range,
        
        Hidden,
        HR,
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
