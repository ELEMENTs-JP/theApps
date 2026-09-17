using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace theInfrastructure
{
    public interface IFilterParameter
    {
        string Matchcode { get; set; }
        List<KeyValuePair<string, string>> Parameters { get; set; }

        string SortColumn { get; set; }
        ListSortDirection? Direction { get; set; }

        string GroupColumn { get; set; }
    }
    public interface IAssignableItems
    {
        // Legt die zugeordneten Einträge bei der Selection Box fest 
        // um auf die Assigned Items in der AddBox zugreifen zu können 
        List<IDTO> AssignedItems { get; set; }
    }
    public interface IQueryParameter
    {
        // Identify 
        Guid GUID { get; set; }
        Guid MasterGUID { get; set; }
        string ID { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        string Matchcode { get; set; }
        string ItemType { get; set; }
        Guid UserGUID { get; set; } 
        string UserName { get; set; } 
        bool IsPersonalizedQuery { get; set; }

        // Validation 
        string Message { get; set; }
        IEnumerable<string> ItemTypeExcludes { get; set; }
        bool Validate();
    }

    public interface ILocalizationService
    {
        LocaleConfiguration Configuration { get; set; }
        string GetLabel(string de, string en, string es);
        string GetLabel(string de, string en);
        string DefaultFilePath { get; set; } 
    }

    public interface ISecurityService
    {
        IDTO Principal { get; set; }
        IDTO User { get; set; }
        Task SetUser(Guid GUID);
        Task RefreshUser();
        List<IDTO> Permissions { get; set; }
        Task<bool> HasAppPermission(IApp theApp);
        Task<bool> HasItemTypePermission(IItemType itemType, SecurityFunction seFunc);
        event PropertyChangedEventHandler PropertyChanged;
        SystemConfiguration Configuration { get; set; }
        SMTPConfiguration Mail { get; set; }
        Task Logoff();
    }
    public interface ISearchService
    {
        List<IDTO> Store { get; set; }
        Task<List<IDTO>> Search(string matchcode);
        int CountByItemType(string itemtype);
        List<string> ValuesByColumn(string itemType, string column);
        Task Init();
    }

    public interface IAppService
    {
        IApp App { get; set; }
        List<IApp> AllApps { get; set; }
        List<IDTO> AllPages { get; set; }
        IItemType ItemType { get; set; }
        IDTO Page { get; set; }
        Task SetApp(IApp? app);
        Task SetItemType(IItemType it);
        Task SetPage(IDTO page);
        Task SetPage(Guid GUID);
        Task<IApp?> AppByItemType(IItemType? it);
        Task<IApp?> AppByPage(IDTO? page);
        LayoutConfiguration Configuration { get; set; }


    }
    public interface ISqlDatabaseService
    {
        bool DatabaseExists { get; }
        string DefaultDatabasePath { get; }
        Guid MasterGUID { get; }
        IQueryResult CreateDatabase();
        IQueryResult DeleteDatabase();
        IQueryResult BackupDatabase();

        // Optimization 
        Task<IQueryResult> CompressDatabase();
        Task<IQueryResult> OptimizeDatabase();
  

        // Pages 
        Task<int> GetUsedPageCount();
        Task<int> GetNotUsedPageCount();
        Task<int> GetRecordCount();


        Task<IQueryResult> Create(IQueryParameter query);
        Task<IQueryResult> GetItems(IQueryParameter query);
        Task<IQueryResult> Search(IQueryParameter query);
        Task<IQueryResult> GetItem(IQueryParameter query);
        Task<IQueryResult> Delete(IDTO dto);
        Task<IQueryResult> Update(IDTO dto);
        Task<IQueryResult> ChangeItemType(IDTO dto, string newItemType);

        Task<IQueryResult> Assign(IDTO parent, IDTO child, AssociationTyp Typ = AssociationTyp.Association);
        Task<IQueryResult> Remove(IDTO parent, IDTO child);
        Task<IQueryResult> GetRelatedItems(
            IDTO dto, string ItemType, AssociationTyp Typ = AssociationTyp.Association);

    }
    public interface IQueryResult
    {
        string Status { get; set; }
        string Message { get; set; }
        List<IDTO> Items { get; set; }
        Guid GUID { get; set; }
        void Validate();
    }

    /// <summary>
    /// Simple Element
    /// </summary>
    public interface ISE
    {
        Guid GUID { get; set; } 
        Guid MasterGUID { get; set; } 
        string? ID { get; set; }
        string Title { get; set; }
        string? Content { get; set; }
     
    }
    public interface IMetadata
    {
        Guid GUID { get; set; }

        Guid MasterGUID { get; set; }

        string? ID { get; set; }

        string ItemType { get; set; }
        Metadata Metadata { get; set; }
    }
    public interface IDTO
    {

        Guid GUID { get; set; }

        Guid MasterGUID { get; set; }

        string? ID { get; set; }

        string ItemType { get; set; }

        string Title { get; set; }

        string? Content { get; set; }

        string? Matchcode { get; set; }

        string this[string propertyName] { get; set; }
        string? RelationType { get; set; }
        Metadata Metadata { get; set; }
    }
    public interface IRelationDTO
    {
        // Identify 
        Guid MasterGUID { get; set; }

        // Identify 
        Guid ParentGUID { get; set; }
        Guid ChildGUID { get; set; }

        // Typ 
        string ChildItemType { get; set; }
        string ParentItemType { get; set; }

        // Relation 
        string RelationType { get; set; }

        // Comment 
        string Comment { get; set; }

        // Date 
        DateTime From { get; set; }
        DateTime To { get; set; }

        // Helper 
        bool Validate();

    }
}
