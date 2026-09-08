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
            Title = "Dateien";
            Typ = ItemTypeTyp.File;
            this.Order = 6;
            Description = "Mit dem Dateimanagement fügen Sie relevante Dokumente und Dateien direkt an eine Aufgabe an, wodurch Sie dezentrale Informationssilos auflösen, allen Beteiligten den sofortigen Zugriff auf benötigte Unterlagen ermöglichen und den Suchaufwand im operativen Tagesgeschäft nachhaltig minimieren.";
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            // Files 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.File));

            // Description 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Description));

            return Fields;
        }

    }
}
