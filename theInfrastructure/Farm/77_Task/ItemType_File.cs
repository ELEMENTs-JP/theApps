using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_File : BaseItemType, IItemType
    {
        public ItemType_File()
        {
            Name = "File";
            Typ = ItemTypeTyp.File;
        }

        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            // Fields.Add(new Field() { Title = "Pfad", Typ = FieldTyp.Text, Column = "FullFilePath", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Size in Byte", Typ = FieldTyp.Text, Column = "FileSizeInByte", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Size in KB", Typ = FieldTyp.Text, Column = "FileSizeInKB", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Size in MB", Typ = FieldTyp.Text, Column = "FileSizeInMB", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Erweiterung", Typ = FieldTyp.Text, Column = "FileExtension", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }

    }
}
