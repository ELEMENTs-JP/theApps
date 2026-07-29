using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theInfrastructure;

namespace theDatabase
{
    public class tbl_TEC_Relation : IRelationDTO
    {
        // https://docs.microsoft.com/de-de/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt 

        // Identify 
        [Required]
        public Guid MasterGUID { get; set; }

        // Parent 
        [Required]
        public Guid ParentGUID { get; set; }
        public string ParentItemType { get; set; } = string.Empty;

        // Child 
        [Required]
        public Guid ChildGUID { get; set; }
        public string ChildItemType { get; set; } = string.Empty;

        // Type 
        [Required]
        [MaxLength(50)]
        public string RelationType { get; set; } = "Relation";

        // Date 
        public DateTime From { get; set; } = DateTime.Now.Date;
        public DateTime To { get; set; } = DateTime.Now.Date;

        // Kommentar 
        [MaxLength(4000)]
        public string Comment { get; set; } = string.Empty;

        // Methods 
        public bool Validate()
        {
            // Check 
            #region Check
            if (MasterGUID == Guid.Empty)
            {
                throw new Exception("Master GUID not set");
            }

            if (ParentGUID == Guid.Empty)
            {
                throw new Exception("GUID not set");
            }

            if (ChildGUID == Guid.Empty)
            {
                throw new Exception("GUID not set");
            }

            //if (string.IsNullOrEmpty(AppType))
            //{
            //    throw new Exception("AppType not set");
            //}

            if (string.IsNullOrEmpty(ParentItemType))
            {
                throw new Exception("ParentItemType not set");
            }
            if (string.IsNullOrEmpty(ChildItemType))
            {
                throw new Exception("ChildItemType not set");
            }
            if (string.IsNullOrEmpty(RelationType))
            {
                throw new Exception("RelationType not set");
            }
            #endregion

            return true;
        }

    }
}
