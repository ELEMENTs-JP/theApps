using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using theInfrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace theDatabase
{
    public class SQLiteService : ISqlDatabaseService
    {
        // Fields 
        public static string Database { get; set; } = "Default.db";
        public static string ContentRootPath = string.Empty;
        public static Guid GeneralMasterGUID = new Guid("30C61E17-DBD3-4FF1-8BD6-612834D328D9");
        public Guid MasterGUID { get { return SQLiteService.GeneralMasterGUID; } }

        // Properties 
        public Guid SystemGUID {
            get { // Diese GUID wird für bspw. die globalen Regionen Datensätze verwendet 
                return new Guid("9D796C23-FC90-4379-85FF-D0CAB55E1D75"); } }
        public DatabaseTyp DatabaseTyp 
        { 
            get 
            {
                if (SQLiteService.Database.ToLower().Contains("default"))
                {
                    return DatabaseTyp.Master;
                }

                return DatabaseTyp.Client;
            } 
        }
        public bool DatabaseExists
        {
            get
            {
                string fullFilePath = Path.Combine(SQLiteService.ContentRootPath, "Database", SQLiteService.Database);
                return File.Exists(fullFilePath);
            }
        }
        public string DefaultDatabasePath
        {
            get
            {
                return Path.Combine(SQLiteService.ContentRootPath, "Database", SQLiteService.Database);
            }
        }

        // CTR 
        public SQLiteService()
        {

        }
        public SQLiteService(string contentrootpath)
        {
            SQLiteService.ContentRootPath = contentrootpath;
        }

        // Context 
        public static SQLiteContext GetContext(bool integrated = false, bool isCreationMode = false)
        {
            string ffp = Path.Combine(SQLiteService.ContentRootPath, "Database", SQLiteService.Database);
            if (isCreationMode == false)
            {
                if (!File.Exists(ffp))
                {
                    return null;
                }
            }

            try
            {
                return new SQLiteContext(ffp, integrated);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Erstellen des Context: " + ex.Message);
                return null;
            }
        }

        // Administration 
        public IQueryResult CreateDatabase()
        {

            IQueryResult info = new QueryResult();
            info.Status = "";
            info.Message = "";

            // Check Name 
            if (string.IsNullOrEmpty(SQLiteService.Database))
            {
                // Error 
                info.Status = "FAIL";
                info.Message = "Datenbankdatei wurde nicht benannt";
                return info;
            }


            // vollständige Pfad zur Datenbank 
            string filePath = Path.Combine(SQLiteService.ContentRootPath, "Database", SQLiteService.Database);
            if (string.IsNullOrEmpty(filePath))
            {
                // Error 
                info.Status = "FAIL";
                info.Message = "Datenbankpfad wurde nicht gesetzt";
                return info;
            }

            // File 
            if (File.Exists(filePath))
            {
                // Error 
                info.Status = "FAIL";
                info.Message = "Datenbankdatei existiert bereits";
                return info;
            }

            // Directory 
            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }

            try
            {
                // Context 
                using (SQLiteContext ctx = GetContext(isCreationMode: true))
                {
                    // Create 
                    ctx.Database.EnsureCreated();
                    ctx.Dispose();
                }

                if (!File.Exists(filePath))
                {
                    // NOT Exists 
                    info.Status = "FAIL";
                    info.Message = "Datenbank konnte nach Erstellungsversuch nicht erstellt werden.";
                    return info;
                }

                // Status 
                info.Status = "OK";
                info.Message = "Datenbank wurde erstellt";

                return info;
            }
            catch (Exception ex)
            {
                // Error 
                string msg = ex.Message;
                info.Status = "Fehler";
                info.Message = "" + ex.Message;
                return info;
            }
        }
        public IQueryResult DeleteDatabase()
        {
            IQueryResult info = new QueryResult();
            info.Status = "";
            info.Message = "";

            // Check Name 
            if (string.IsNullOrEmpty(SQLiteService.Database))
            {
                // Error 
                info.Status = "FAIL";
                info.Message = "Datenbankdatei wurde nicht benannt";
                return info;
            }

            // vollständige Pfad zur Datenbank 
            string filePath = Path.Combine(SQLiteService.ContentRootPath, "Database", SQLiteService.Database);
            if (string.IsNullOrEmpty(filePath))
            {
                // Error 
                info.Status = "FAIL";
                info.Message = "Datenbankpfad wurde nicht gesetzt";
                return info;
            }

            // File 
            if (!File.Exists(filePath))
            {
                // Error 
                info.Status = "FAIL";
                info.Message = "Datenbankdatei existiert nicht mehr";
                return info;
            }


            try
            {
                string shmPath = $"{filePath}-shm";
                string walPath = $"{filePath}-wal";

                // Context 
                using (SQLiteContext ctx = GetContext())
                {
                    // Datenbank schließen... 
                    var connection = ctx.Database.GetDbConnection();

                    // Falls die Verbindung offen ist, schließen
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }

                    // Kritisch für SQLite: Den Connection Pool leeren, damit die Dateihandles freigegeben werden
                    if (connection is Microsoft.Data.Sqlite.SqliteConnection sqliteConn)
                    {
                        Microsoft.Data.Sqlite.SqliteConnection.ClearPool(sqliteConn);
                    }
                }

                // GC kurz anstoßen, um verbleibende File-Handles final freizugeben
                GC.Collect();
                GC.WaitForPendingFinalizers();

                File.Delete(filePath);

                // 3. WAL und SHM Dateien löschen, falls vorhanden
                if (File.Exists(shmPath))
                {
                    File.Delete(shmPath);
                }

                if (File.Exists(walPath))
                {
                    File.Delete(walPath);
                }

                // File 
                if (!File.Exists(filePath))
                {
                    // Error 
                    info.Status = "OK";
                    info.Message = "Datenbankdatei wurde gelöscht";
                    return info;
                }
            }
            catch (Exception ex)
            {
                info.Status = "FAIL";
                info.Message = ex.Message;
            }

            // File 
            if (File.Exists(filePath))
            {
                // Error 
                info.Status = "FAIL";
                info.Message = "Datenbankdatei existiert noch immer";
                return info;
            }

            return info;
        }

        // CRUD 
        public async Task<IQueryResult> Create(IQueryParameter query)
        {
            IQueryResult info = new QueryResult { Status = "OK", Message = "" };

            if (query.GUID == Guid.Empty)
            {
                query.GUID = Guid.NewGuid();
            }
            Guid master = GeneralMasterGUID;
            string id = SqlHelper.GenerateID(query.ItemType, master);

            // Datensatz 
            IQueryParameter input = new QueryParameter
            {
                GUID = query.GUID,
                MasterGUID = master,
                ID = id,
                Title = query.Title,
                Content = query.Content,
                ItemType = query.ItemType
            };

            // Metadaten 
            Metadata mtd = new Metadata 
            {
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                Creator = "Batman",
                EditedAt = DateTime.Now,
                EditedBy = Guid.NewGuid(),
                Editor = "Batman"
            };

            // Query generieren
            FormattableString fq = Query.CreateQuery(input, mtd);

            try
            {
                using (SQLiteContext ctx = GetContext())
                {
                    await ctx.Database.ExecuteSqlAsync(fq);
                }

                // Item GUID 
                info.GUID = query.GUID;

                // Message 
                info.Message = "Eintrag erfolgreich erstellt.";
            }
            catch (Exception ex)
            {
                info.Status = "FAIL";
                info.Message = ex.Message;
            }

            return info;
        }
        public async Task<IQueryResult> Delete(IDTO dto)
        {
            IQueryResult result = new QueryResult { Status = "OK", Message = "" };
            FormattableString query = Query.DeleteItem(dto);

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    var rowsAffected = await ctx.Database.ExecuteSqlAsync(query);

                    if (rowsAffected == 0)
                    {
                        result.Status = "FAIL";
                        result.Message = "Datensatz wurde nicht gefunden.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = "FAIL";
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<IQueryResult> GetItems(IQueryParameter query)
        {
            IQueryResult result = new QueryResult { Status = "OK", Message = "" };

            // Query Validation 
            if (query.Validate() == false)
            {
                result.Status = "FAIL";
                result.Message = "Query not conform";
                return result;
            }

            // Datenbank 
            if (DatabaseExists == false)
            {
                result.Status = "FAIL";
                result.Message = "Database not exists";
                return result;
            }
                
            FormattableString sql = Query.GetItems(query);

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    var dbItems = await ctx.tbl_CON_Content
                        .FromSql(sql)
                        .ToListAsync();

                    result.Items = dbItems.Cast<IDTO>().ToList();
                }
            }
            catch (Exception ex)
            {
                result.Status = "FAIL";
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<IQueryResult> GetItem(IQueryParameter query)
        {
            IQueryResult result = new QueryResult { Status = "OK", Message = "" };

            // Query Validation 
            if (query.Validate() == false)
            {
                result.Status = "FAIL";
                result.Message = "Query not conform";
                return result;
            }

            // Datenbank 
            if (DatabaseExists == false)
            {
                result.Status = "FAIL";
                result.Message = "Database not exists";
                return result;
            }

            FormattableString sql = Query.GetItem(query);

            try
            {
                await using var ctx = GetContext();

                IQueryable<tbl_CON_Content> queryable = ctx.tbl_CON_Content;

                queryable = queryable.Where(x =>
                    EF.Functions.Collate(x.GUID, "NOCASE") == query.GUID &&
                    EF.Functions.Collate(x.MasterGUID, "NOCASE") == query.MasterGUID);

                var items = await queryable.AsNoTracking().ToListAsync();

                result.Items = items.Cast<IDTO>().ToList();
            }
            catch (Exception ex)
            {
                result.Status = "FAIL";
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<IQueryResult> Update(IDTO dto)
        {
            IQueryResult result = new QueryResult { Status = "OK", Message = "" };
      
            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    var dbitem = await (from query in ctx.tbl_CON_Content
                                        where query.GUID == dto.GUID
                                        select query).FirstOrDefaultAsync();

                    if (dbitem != null)
                    {
                        dbitem.Title = dto.Title;
                        dbitem.Content = dto.Content;

                        dbitem.Metadata.EditedAt = DateTime.Now;
                        dbitem.Metadata.EditedBy = Guid.Empty;
                        dbitem.Metadata.Editor = "Hono Lulu";

                        // CONTENT 
                        dbitem = (tbl_CON_Content)Helper.MapProperties(dto, dbitem);
                    }

                    // SAVE 
                    await ctx.SaveChangesAsync();
                    
                    // Close 
                    ctx.Database.CloseConnection();
                    ctx.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Status = "FAIL";
                result.Message = ex.Message;
            }

            return result;
        }

        // Relation 
        public async Task<IQueryResult> GetRelatedItems(IDTO dto, string ItemType, string Typ = "Association")
        {
            IQueryResult info = new QueryResult { Status = "OK", Message = "" };

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    var query = from content in ctx.tbl_CON_Content
                                where ctx.tbl_TEC_Relation.Any(relation =>
                                    (
                                        (relation.ParentGUID == dto.GUID && relation.ChildGUID == content.GUID) ||
                                        (relation.ChildGUID == dto.GUID && relation.ParentGUID == content.GUID)
                                    )
                                    && (relation.RelationType.Equals(Typ))
                                    && (content.ItemType.Equals(ItemType))
                                )
                                select content;

                    List<IDTO> result = query.ToList().ConvertAll(c => (IDTO)c);

                    info.Items = result;
                }
            }
            catch (Exception ex)
            {
                info.Status = "FAIL";
                info.Message = ex.Message;
            }

            return info;
        }
        public async Task<IQueryResult> Assign(IDTO parent, IDTO child)
        {
            string associationType = "Association";

            IQueryResult info = new QueryResult { Status = "OK", Message = "" };

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    // check 
                    var result = await (from query in ctx.tbl_TEC_Relation
                                        where query.ParentGUID == parent.GUID
                                        && query.ChildGUID == child.GUID
                                        && query.RelationType == associationType
                                        select query).FirstOrDefaultAsync();

                    // Exists already 
                    if (result != null)
                    {
                        info.Status = "FAIL";
                        info.Message = "Verbindung existiert bereits";
                        return info;
                    }

                    // NULL -> NEW -> CREATE 
                    tbl_TEC_Relation item = new tbl_TEC_Relation();

                    // GUIDs 
                    item.MasterGUID = parent.MasterGUID;
                    item.ParentGUID = parent.GUID;
                    item.ChildGUID = child.GUID;

                    // Types 
                    item.ParentItemType = parent.ItemType;
                    item.ChildItemType = child.ItemType;

                    // Association 
                    item.RelationType = associationType;
                    item.Comment = "";

                    // Dates 
                    item.From = DateTime.Now.Date;
                    item.To = DateTime.Now.Date;

                    // Add 
                    ctx.tbl_TEC_Relation.Add(item);
                    await ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                info.Status = "FAIL";
                info.Message = ex.Message;
            }

            info.Status = "OK";
            info.Message = string.Empty;

            return info;
        }
        public async Task<IQueryResult> Remove(IDTO parent, IDTO child)
        {
            string associationType = "Association";

            IQueryResult info = new QueryResult { Status = "OK", Message = "" };

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    // check 
                    var result = await (from query in ctx.tbl_TEC_Relation
                                        where  (query.ParentGUID == parent.GUID && query.ChildGUID == child.GUID) ||
                                                (query.ParentGUID == child.GUID && query.ChildGUID == parent.GUID)
                                        select query).FirstOrDefaultAsync();

                                        // && query.RelationType == associationType

                    // does not exists 
                    if (result == null)
                    {
                        info.Status = "FAIL";
                        info.Message = "Verbindung existiert nicht";
                        return info;
                    }

                    // Exists already 
                    if (result != null)
                    {
                        // Remove 
                        ctx.tbl_TEC_Relation.Remove(result);
                        await ctx.SaveChangesAsync();

                        info.Status = "OK";
                        info.Message = string.Empty;
                        return info;
                    }
                }
            }
            catch (Exception ex)
            {
                info.Status = "FAIL";
                info.Message = ex.Message;
            }

            return info;
        }
    }
}