using System;
using System.Collections.Generic;
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
        SystemConfiguration Configuration { get; set; }
        string GetLabel(string de, string en, string es);
    }

    public interface ISecurityService
    {
        IDTO Principal { get; set; }
        IDTO User { get; set; }
        Task SetUser(Guid GUID);
    }

    public interface IAppService
    {
        IApp App { get; set; }
        List<IApp> AllApps { get; set; }
        Task SetApp(IApp app);
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

        Task<IQueryResult> Assign(IDTO parent, IDTO child);
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
