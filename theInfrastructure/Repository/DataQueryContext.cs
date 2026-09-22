using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public interface IQueryContext
    {
        Guid ID { get; set; }
        bool IsLoading { get; set; } 
        IItemType ItemType { get; set; }
        IDTO RelatedItem { get; set; }
        IDTO Item { get; set; }
        List<IDTO> Items { get; set; }
        IFilterParameter Filter { get; set; }
        Task Search();
        Task Load(string ID);
        Task<List<IDTO>> RelatedItems(string itemType, AssociationTyp association = AssociationTyp.Association);
        void Init(ISqlDatabaseService sql, ISecurityService sec);
        Task<Guid> Create(string title);
        Task Delete(IDTO dto);
        Task Update(IDTO dto);
        Task ChangeItemType(IDTO dto, string newItemType);
        Task Assign(IDTO dto, AssociationTyp association = AssociationTyp.Association);
        Task Remove(IDTO dto);
        Task Clear();
        List<string> ValuesByColumn(string column);
        List<IField> Fields { get; }
    }
   
}
