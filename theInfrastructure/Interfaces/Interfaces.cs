using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace theInfrastructure
{
 

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

        // Validation 
        string Message { get; set; }
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
        event PropertyChangedEventHandler PropertyChanged;
        SystemConfiguration Configuration { get; set; }
    }

    public interface IAppService
    {
        IApp App { get; set; }
        List<IApp> AllApps { get; set; }
        IItemType ItemType { get; set; }
        IDTO Page { get; set; }
        Task SetApp(IApp app);
        Task SetItemType(IItemType it);
        Task SetPage(IDTO page);
        Task AppByItemType(IItemType it);
        LayoutConfiguration Configuration { get; set; }


    }
    public interface ISqlDatabaseService
    {
        bool DatabaseExists { get; }
        string DefaultDatabasePath { get; }
        Guid MasterGUID { get; }
        IQueryResult CreateDatabase();
        IQueryResult DeleteDatabase();

        Task<IQueryResult> Create(IQueryParameter query);
        Task<IQueryResult> GetItems(IQueryParameter query);
        Task<IQueryResult> GetItem(IQueryParameter query);
        Task<IQueryResult> Delete(IDTO dto);
        Task<IQueryResult> Update(IDTO dto);
        Task<IQueryResult> ChangeItemType(IDTO dto, string newItemType);

        Task<IQueryResult> Assign(IDTO parent, IDTO child, string associationTyp = "Association");
        Task<IQueryResult> Remove(IDTO parent, IDTO child);
        Task<IQueryResult> GetRelatedItems(IDTO dto, string ItemType, string Typ = "Association");

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
