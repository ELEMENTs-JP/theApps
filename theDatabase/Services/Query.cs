using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using theInfrastructure;
using theInfrastructure;

namespace theDatabase
{
    public partial class Query
    {
        // CREATE 
    
        public static FormattableString CreateQuery(IQueryParameter input, Metadata mtd)
        {
            // !!! ACHTUNG hier niemals eine Berücksichtigung von Meta oder Query... 
            // Die Datensätze müssen immer sauber injitiert werden 
            string sql = string.Empty;

            string meta = Helper.Serialize<Metadata>(mtd);

            string matchcode = input.Title.ToSecureString() + " " + input.Content.ToSecureString();
            string properties = "[]";
            
            return $@" INSERT INTO tbl_CON_Content ( [GUID], [MasterGUID], 
                                                     [ID], [Title], 
                                                     [Content], [Matchcode], 
                                                     [ItemType], [Metadata], [Properties]) 
                                VALUES (
                                {input.GUID}, {input.MasterGUID}, 
                                {input.ID.ToSecureString()}, 
                                
                                {input.Title.ToSecureString()}, 
                                {input.Content.ToSecureString()}, 
                                {matchcode}, 

                                {input.ItemType}, 
                                {meta}, {properties})";

        }
        public static FormattableString GetItems(IQueryParameter query)
        {
            // Parameter 
            object[] parameters = Array.Empty<object>();

            // Query 
            string sql = "SELECT * FROM tbl_CON_Content WHERE ItemType = '"+ query.ItemType +"' ";

            // Matchcode 
            if (!string.IsNullOrEmpty(query.Matchcode))
            {
                sql += " AND Matchcode LIKE {0} COLLATE NOCASE ";
                parameters = new object[] { $"%{query.Matchcode}%" };
            }

            // Query 
            return FormattableStringFactory.Create(sql, parameters);
        }
  
        public static FormattableString GetItem(IQueryParameter query)
        {
            object[] parameters = Array.Empty<object>();
            var sql = "SELECT * FROM tbl_CON_Content";

            sql += " WHERE GUID = {0} COLLATE NOCASE AND MasterGUID = {1} COLLATE NOCASE";
            parameters = new object[] { query.GUID, query.MasterGUID };

            return FormattableStringFactory.Create(sql, parameters);
        }
        public static FormattableString DeleteItem(IDTO dto)
        {
            string guid = dto.GUID.ToSecureString();

            return $@" DELETE FROM tbl_CON_Content WHERE GUID = {guid} COLLATE NOCASE";
        }

       
    
    }
}
