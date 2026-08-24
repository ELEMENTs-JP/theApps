using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public interface IQueryContext
    {
        Guid ID { get; set; }
        bool IsLoading { get; set; } 
        string Matchcode { get; set; }
        IItemType ItemType { get; set; }
        IDTO RelatedItem { get; set; }
        IDTO Item { get; set; }
        List<IDTO> Items { get; set; }
        Task Search();
        Task Load(string ID);
        Task<List<IDTO>> RelatedItems(string itemType, string association = "Association");
        void Init(ISqlDatabaseService sql);
        Task<Guid> Create(string title);
        Task Delete(IDTO dto);
        Task Update(IDTO dto);
        Task ChangeItemType(IDTO dto, string newItemType);
        Task Assign(IDTO dto, string association = "Association");
        Task Remove(IDTO dto);
    }
   
}
