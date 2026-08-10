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
        Task RelatedItems(string itemType);
        void Init(ISqlDatabaseService sql);
        Task<Guid> Create(string title);
        Task Delete(IDTO dto);
        Task Update(IDTO dto);
        Task Assign(IDTO dto);
        Task Remove(IDTO dto);
    }
   
}
