using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace theInfrastructure
{
    public class FieldBuilder
    {
        public FieldBuilder() { }

        public static FieldBuilder Create() { return new FieldBuilder(); }

        public IField BuildField(FieldTyp fieldTyp)
        {
            foreach(FieldTyp field in Enum.GetValues(typeof(FieldTyp)))
            {
                if (field == fieldTyp)
                {
                    IField f = new Field();
                    f.Title = field.ToString();
                    f.Typ = field;
                    f.Column = field.ToString();
                    f.CSS = "col";
                    return f;
                }
            }

            return null;
        }
    }
}
