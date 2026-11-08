using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public interface IApp
    {
        string Title { get; set; } // Titel der Anwendung 
        string Name { get; set; } // interner Name der Anwendung 
        AppTyp Typ { get; set; } // Typisierung der Anwendung (Custom, Personalized, Predefined, System)
        string Description { get; set; } // Beschreibung der Anwendung
        string Group { get; set; } // Gruppierung in der Navigation // Allgemeine Typisierung
        bool IsNavigation { get; set; } // wird im App Selector // Navigation angezeigt oder nicht 
        bool IsActive { get; set; } // legt fest ob die App aktiv ist und verwendet werden kann (Feature on off) 
        Task<List<IItemType>> GetItemTypes(
            AssociationTyp ast = AssociationTyp.Association);
        Task<List<IDTO>> GetPages(AssociationTyp ast = AssociationTyp.Association);
        Task<List<IDTO>> GetBoards(AssociationTyp ast = AssociationTyp.Association);
        Task<List<ComponentDefinition>> GetDashboardComponents(string typ = "");

    }
    public class BaseApp : IApp
    {
        public BaseApp()
        {

        }
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public AppTyp Typ { get; set; } = AppTyp.System;
        public string Description { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public bool IsNavigation { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public virtual async Task<List<IItemType>> GetItemTypes(
            AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            return ItemTypes;
        }
        public virtual async Task<List<IDTO>> GetPages(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IDTO> Pages = new();

            return Pages;
        }

        public virtual async Task<List<IDTO>> GetBoards(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IDTO> Pages = new();

            return Pages;
        }

        //public virtual async Task<List<IDTO>> GetBoards()
        //{
        //    List<IDTO> Boards = new();

        //    return Boards;
        //}

        public virtual async Task<List<ComponentDefinition>> GetDashboardComponents(string typ = "")
        {
            List<ComponentDefinition> comps = new List<ComponentDefinition>();

            return comps;
        }

        public override string ToString()
        {
            return ((string.IsNullOrEmpty(Title)) ? Name : Title);
        }
    }

    public enum AppTyp
    { 
        NULL = 0,
        Custom = 1, // CMS Designer App bzw. Public Application 
        Personalized = 2, // Personalisierte App (z.B. RUNer, TASKler, etc.)
        Predefined = 3, // Vorgefertigte App (z.B. OKRler, etc.)
        System = 4, // System App (z.B. Security, Farm, Search, Admin, etc.)
    }
}
