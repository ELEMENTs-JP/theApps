using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public interface IQueryContext
    {
        bool IsLoading { get; set; } 
        IItemType ItemType { get; set; }
        IDTO RelatedItem { get; set; }
        IDTO Item { get; set; }
        List<IDTO> Items { get; set; }
        Task Search();
        Task Load(string ID);
        Task RelatedItems();
        void Init(ISqlDatabaseService sql);
        Task<Guid> Create(string title);
        Task Delete(IDTO dto);
        Task Update(IDTO dto);
        Task Assign(IDTO dto);
        Task Remove(IDTO dto);
    }
   
}
