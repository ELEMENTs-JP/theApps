using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public enum ItemTypeTyp
    {
        NULL = 0,

        Item = 1,
        File = 2,
    }
    public enum Orientation
    {
        NULL = 0,
        Vertical = 1,
        Horizontal = 2,
        Center = 3,
    }
    public enum TabsLayout
    {
        NULL = 0,

        Regular = 1,
        Buttons = 2,
    }

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
        Empty,
        Search,
        Check,
        Menu,
        Config,
        Connect,
        Disconnect,
        Neu,
        File,
        Corner,
        Password,
        Home,
    }
    public enum ValueBindingTyp
    {
        NULL = 0,
        Title = 1,
        Description = 2,
        Property = 3,
    }
    public enum DefaultFieldTypes
    {
        NULL = 0,

        Description = 1,
        Performance = 2,
        File = 3,
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
        Html,
        DropDown,
        CheckBox,
        Heading,

        Priority,
        Status,
        Progress,

        Number,
        Integer,
        Decimal,
        Money,
        
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
        FieldTyp, // EditBox FieldTyp 
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
