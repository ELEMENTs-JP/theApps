using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public enum SecurityFunction
    {
        NULL = 0,
        Create = 1,
        Read = 2,
        Update = 3,
        Delete = 4,
    }
    public enum AssociationTyp
    {
        NULL = 0,
        Default = 1,
        Children = 2,
        Parents = 3,
        Parallels = 4,
        Related = 5,
        Association = 6,

        // EXTRA 
        UserImage = 11,
    }

    public enum KpiSize
    {
        NULL = 0,

        SMALL = 1,
        MEDIUM = 2,
        LARGE = 3,
    }
    public enum HierarchyMode
    {
        NULL = 0,

        Strict = 1, // lädt immer wieder den gleichen ItemType als hierarchisches Unterelement 
        Related = 2,  // Lädt mit dem itemType verbundene ItemTypes als hierarchische Unterlement 
    }
    public enum TextFormat
    {
        NULL = 0,

        // Dateien 
        MB = 11,
        KB = 12,
        Byte = 13,

        // Regular 
        Text = 21,
        Integer = 22,
        Decimal = 23,
    }
    public enum RelevantPropertyType
    {
        NULL = 0,

        Date = 11,

    }
    public enum ItemTypeTyp
    {
        NULL = 0,

        Item = 1,
        File = 2,
        Appointment = 3,
        Hierarchy = 4,
        Image = 5,
        User = 6,
        Audio = 7,
        Note = 8,
        Comment = 9,
        Information = 10,
        Link = 11,
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
        Filter,
        Config,
        Help,
        Connect,
        Disconnect,
        Link,
        Top, Down, Equal,
        Neu,
        File,
        Corner,
        Password,
        User,
        Admin,
        Principal,
        Home,
        Delete,
        Archiv,
        Papierkorb,
        Download,
        Private,
        Public,
        Setting,
        Grib,
        Play,
        Stop,
        Previous,
        Next,

        // Layout 
        Max,
        Min,
        FullScreen,
        Layout,
        COL12,
        COL66,
        COL363,
        COL444,
        COL57,
        COL75,
        Controls,
        // True False
        YES,
        NO,
        Invariant,

        // Prioritäten 
        Prio_Highest,
        Prio_Higher,
        Prio_High,
        Prio_Middle,
        Prio_Low,
        Prio_Lower,
        Prio_Lowest,

        // Status 
        Status_New,
        Status_inPlan,
        Status_inPreparation,
        Status_inWork,
        Status_onHold,
        Status_Finished,
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
        Appointment = 4,
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
        TextBlock,
        Text,
        TextArea,
        Html,
        DropDown, // Text Drop Down Auswahl 
        Select, // Items Selection // Connection 
        CheckBox,
        Heading,

        Priority,
        Status,
        Progress,
        User,

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
        ItemTypeTyp, // Typ des ItemTyp (Item, File, etc.)
        ItemTypeList, // Liste der verfügbaren ItemTypes 
        AppList, // Liste aller CMS Apps im System 
        FunctionList, // Liste der Funktionen die allow oder deny werden können 
        AssociationTyp, // Typ der Verbindungen von Datensätzen (Association Type bei Relation) 
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
