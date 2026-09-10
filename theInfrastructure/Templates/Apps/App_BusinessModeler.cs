using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_BusinessModeler : TemplateApp, ITemplateApp
    {
        public TApp_BusinessModeler()
        {
            
        }

        public override AppInfo GetApp()
        {
            return new AppInfo("BusinessMODELer", "the Business MODELer");
        }
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("BusinessModel", "Business Model"));
            types.Add(new ItemTypeInfo("ValueProposition", "Value Proposition"));
            
            types.Add(new ItemTypeInfo("KeyActivities", "Key Activities"));
            types.Add(new ItemTypeInfo("KeyPartner", "Key Partners"));
            types.Add(new ItemTypeInfo("KeyRessources", "Key Resources"));
            
            types.Add(new ItemTypeInfo("CustomerRelationships", "Customer Relationships"));
            types.Add(new ItemTypeInfo("Channels", "Channels"));
            types.Add(new ItemTypeInfo("CustomerSegments", "Customer Segments"));
            
            types.Add(new ItemTypeInfo("CostStructure", "Cost Structure"));
            types.Add(new ItemTypeInfo("RevenueStreams", "Revenue Streams"));
            
            return types;
        }
        public override List<ItemTypeConnection> GetItemTypeConnections()
        {
            List<ItemTypeConnection> cons = new List<ItemTypeConnection>();

            cons.Add(new ItemTypeConnection("BusinessModel", "ValueProposition")); // Parent -> Child 
            
            cons.Add(new ItemTypeConnection("BusinessModel", "KeyActivities")); // Parent -> Child 
            cons.Add(new ItemTypeConnection("BusinessModel", "KeyPartner")); // Parent -> Child 
            cons.Add(new ItemTypeConnection("BusinessModel", "KeyRessources")); // Parent -> Child 

            cons.Add(new ItemTypeConnection("BusinessModel", "CustomerRelationships")); // Parent -> Child 
            cons.Add(new ItemTypeConnection("BusinessModel", "Channels")); // Parent -> Child 
            cons.Add(new ItemTypeConnection("BusinessModel", "CustomerSegments")); // Parent -> Child 

            cons.Add(new ItemTypeConnection("BusinessModel", "CostStructure")); // Parent -> Child 
            cons.Add(new ItemTypeConnection("BusinessModel", "RevenueStreams")); // Parent -> Child 

            return cons;
        }
        public override List<FieldInfo> GetFields(string ItemType)
        {
            List<FieldInfo> fields = new List<FieldInfo>();

            if (ItemType == "Template")
            {
                fields.Add(new FieldInfo("Template", "string"));
                fields.Add(new FieldInfo("Template", "string"));
            }

            return fields;
        }
    }
}
