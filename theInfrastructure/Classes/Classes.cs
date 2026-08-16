using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace theInfrastructure
{
    public static class Sections
    {
        // SectionOutlet, SectionContent 
        public static readonly object ModalOutlet = new();
    }
    public interface IDataValue
    {
        IField Field { get; set; }
        string Value { get; set; }

    }
    public class DataValue : IDataValue
    {
        public DataValue(IField field, string value)
        {
            Field = field;
            Value = value;
        }

        public IField Field { get; set; }
        public string Value { get; set; } = string.Empty;
    }



    public class QueryResult : IQueryResult
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public Guid GUID { get; set; } = Guid.Empty;

        public List<IDTO> Items { get; set; } = new();

        public void Validate()
        {
            if (Status != "OK")
            {
                Console.WriteLine("FAIL: " + Message);
            }
        }
    }
    public class ItemHistory
    {
        public static ItemHistory Empty(string value) => new ItemHistory { OldValue = value, NewValue = string.Empty, ChangeDate = DateTime.Now };
        public string OldValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
        public DateTime ChangeDate { get; set; } = DateTime.Now;

    }
    public class ItemProperty
    {
        public static ItemProperty Empty(string value) => new ItemProperty { Property = value, Value = string.Empty };
        public string Property { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    public class Metadata
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        public DateTime EditedAt { get; set; } = DateTime.Now.Date;
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public Guid EditedBy { get; set; } = Guid.Empty;
        public string Creator { get; set; } = string.Empty;
        public string Editor { get; set; } = string.Empty;

    }
    public class QueryParameter : IQueryParameter
    {
        // Identify 
        public Guid GUID { get; set; } = Guid.NewGuid();
        public Guid MasterGUID { get; set; } 
        public string ID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Matchcode { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public static IQueryParameter Default(string title)
        {
            IQueryParameter qp = new QueryParameter()
            {
                GUID = Guid.NewGuid(),
                Title = title
            };

            return qp;
        }

        // Validation 
        public string Message { get; set; } = string.Empty;
        public bool Validate()
        {
            if (this.MasterGUID == Guid.Empty)
            {
                this.Message = "Master GUID is Empty";
                return false;
            }


            return true;
        }
    }



}
