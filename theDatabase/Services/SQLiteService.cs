using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
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
        public Guid SystemGUID
        {
            get
            { // Diese GUID wird für bspw. die globalen Regionen Datensätze verwendet 
                return new Guid("9D796C23-FC90-4379-85FF-D0CAB55E1D75");
            }
        }
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
        public IQueryResult BackupDatabase()
        {
            IQueryResult info = new QueryResult();

            // Check Name 
            if (string.IsNullOrEmpty(SQLiteService.Database))
            {
                info.Status = "FAIL";
                info.Message = "Datenbankdatei wurde nicht benannt";
                return info;
            }

            // Vollständiger Pfad zur Quell-Datenbank 
            string filePath = Path.Combine(SQLiteService.ContentRootPath, "Database", SQLiteService.Database);
            if (!File.Exists(filePath))
            {
                info.Status = "FAIL";
                info.Message = "Datenbankdatei existiert nicht mehr";
                return info;
            }

            // Backup Ordner
            string backupPath = Path.Combine(SQLiteService.ContentRootPath, "Database", "Backup");

            try
            {
                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }

                string timeStamp = DateTime.Now.ToString("'D-'yyyy-MM-dd'-U-'HH-mm-ss");
                string backupFile = $"Backup-{timeStamp}.db";
                string backupFilePath = Path.Combine(backupPath, backupFile);

                // Offizielle SQLite Backup-Methode über EF Core Context & Microsoft.Data.Sqlite
                using (SQLiteContext ctx = GetContext(isCreationMode: false))
                {
                    // Erforderliche Verbindungen für Quell- und Ziel-Datenbank
                    var sourceConnection = (SqliteConnection)ctx.Database.GetDbConnection();

                    if (sourceConnection.State != System.Data.ConnectionState.Open)
                    {
                        sourceConnection.Open();
                    }

                    // Ziel-Verbindung zur Backup-Datei aufbauen
                    string destinationConnectionString = new SqliteConnectionStringBuilder
                    {
                        DataSource = backupFilePath
                    }.ConnectionString;

                    using (var destinationConnection = new SqliteConnection(destinationConnectionString))
                    {
                        destinationConnection.Open();

                        // Offizieller SQLite Online-Backup-Befehl
                        sourceConnection.BackupDatabase(destinationConnection);
                    }
                }

                info.Status = "OK";
                info.Message = $"Backup erfolgreich erstellt: {backupFile}";
            }
            catch (Exception ex)
            {
                info.Status = "FAIL";
                info.Message = $"Fehler beim Erstellen des Backups: {ex.Message}";
            }

            return info;
        }

        public async Task<IQueryResult> CompressDatabase()
        {
            IQueryResult info = new QueryResult { Status = "OK", Message = "" };

            try
            {
                using (SQLiteContext ctx = GetContext())
                {
                    // Temporäre Dateien im RAM halten für schnelleres VACUUM
                    await ctx.Database.ExecuteSqlRawAsync("PRAGMA temp_store = MEMORY;");

                    // VACUUM ausführen
                    await ctx.Database.ExecuteSqlRawAsync("VACUUM;");

                    // WAL-File optional aufräumen/verkleinern
                    await ctx.Database.ExecuteSqlRawAsync("PRAGMA wal_checkpoint(TRUNCATE);");
                }

                info.Status = "OK";
                info.Message = "Migration wurde durchgeführt";
                return info;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                // Error 
                info.Status = "Fehler";
                info.Message = "" + ex.Message;
                return info;
            }
        }
        public async Task<IQueryResult> OptimizeDatabase()
        {
            IQueryResult info = new QueryResult { Status = "OK", Message = "" };

            try
            {
                // Context 
                using (SQLiteContext ctx = GetContext())
                {
                    // Aktualisiert die Abfrage-Statistiken in sqlite_stat1/sqlite_stat4
                    await ctx.Database.ExecuteSqlRawAsync("ANALYZE;");

                    // Wendet von ANALYZE gesammelte Statistiken auf die aktuelle Verbindung an
                    await ctx.Database.ExecuteSqlRawAsync("PRAGMA optimize;");
                }

                info.Status = "OK";
                info.Message = "Migration wurde durchgeführt";
                return info;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                // Error 
                info.Status = "Fehler";
                info.Message = "" + ex.Message;
                return info;
            }
        }

        // Pages 
        public async Task<int> GetUsedPageCount()
        {
            try
            {
                using (SQLiteContext ctx = GetContext())
                {
                    var connection = ctx.Database.GetDbConnection();

                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "PRAGMA page_count;";
                        object? result = await command.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result).ToSecureInt();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FAIL: {ex.Message}");
            }

            return 0;
        }
        public async Task<int> GetNotUsedPageCount()
        {
            try
            {
                using (SQLiteContext ctx = GetContext())
                {
                    var connection = ctx.Database.GetDbConnection();

                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "PRAGMA freelist_count;";
                        object? result = await command.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result).ToSecureInt();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FAIL: {ex.Message}");
            }

            return 0;
        }
        public async Task<int> GetRecordCount()
        {
            try
            {
                using (SQLiteContext ctx = GetContext())
                {
                    // Nutzung von EF Core CountAsync für eine saubere, asynchrone SQL-COUNT-Abfrage
                    return await ctx.tbl_CON_Content.CountAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FAIL: {ex.Message}");
            }

            return 0;
        }

        // CRUD 
        public async Task<IQueryResult> Create(IQueryParameter query)
        {
            IQueryResult info = new QueryResult { Status = "OK", Message = "" };

            // GUID 
            if (query.GUID == Guid.Empty)
            {
                query.GUID = Guid.NewGuid();
            }

            // Master 
            Guid master = GeneralMasterGUID;

            // ID 
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
                CreatedBy = query.UserGUID,
                Creator = query.UserName,
                EditedAt = DateTime.Now,
                EditedBy = query.UserGUID,
                Editor = query.UserName
            };

            // Query generieren
            FormattableString fq = Query.CreateQuery(input, mtd);

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    await ctx.Database.ExecuteSqlAsync(fq);

                    var dbitem = await (from c in ctx.tbl_CON_Content
                                        where c.GUID == query.GUID
                                        select c).FirstOrDefaultAsync();

                    if (dbitem != null)
                    {
                        // Creator 
                        dbitem.Metadata.CreatedAt = DateTime.Now;
                        dbitem.Metadata.CreatedBy = query.UserGUID;
                        dbitem.Metadata.Creator = query.UserName;

                        // Editor 
                        dbitem.Metadata.EditedAt = DateTime.Now;
                        dbitem.Metadata.EditedBy = query.UserGUID;
                        dbitem.Metadata.Editor = query.UserName;

                        dbitem.Matchcode = query.Title;
                    }

                    // SAVE 
                    await ctx.SaveChangesAsync();
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
                    // Item Delete 
                    var rowsAffected = await ctx.Database.ExecuteSqlAsync(query);

                    if (rowsAffected == 0)
                    {
                        result.Status = "FAIL";
                        result.Message = "Datensatz wurde nicht gefunden.";
                    }

                    // Connection Delete 
                    await ctx.tbl_TEC_Relation
                            .Where(conn => conn.ParentGUID == dto.GUID || conn.ChildGUID == dto.GUID)
                                .ExecuteDeleteAsync();
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

            // Query 
            FormattableString sql = query.IsPersonalizedQuery
                ? Query.GetPersonalAssignedItems(query)
                : Query.GetItems(query);

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    var dbItems = await ctx.tbl_CON_Content
                        .FromSql(sql)
                        .AsNoTracking()
                        .ToListAsync();

                    result.Items = dbItems.OfType<IDTO>().ToList();
                }

            }
            catch (Exception ex)
            {
                result.Status = "FAIL";
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<IQueryResult> Search(IQueryParameter query)
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

            // Excluded ItemTypes 
            query.ItemTypeExcludes = new List<string>() { 
                "Principal", "User", "Permission", 
                "App", "ItemType", "Field", "Slot", "Board" };

            FormattableString sql = Query.Search(query);

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    var dbItems = await ctx.tbl_CON_Content
                        .FromSql(sql)
                              .AsNoTracking()
                        .ToListAsync();

                    result.Items = dbItems.OfType<IDTO>().ToList();
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

            if (query.Validate() == false)
            {
                result.Status = "FAIL";
                result.Message = "Query not conform";
                return result;
            }

            if (DatabaseExists == false)
            {
                result.Status = "FAIL";
                result.Message = "Database not exists";
                return result;
            }

            try
            {
                await using var ctx = GetContext();

                var item = await ctx.tbl_CON_Content
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x =>
                        EF.Functions.Collate(x.GUID, "NOCASE") == query.GUID &&
                        EF.Functions.Collate(x.MasterGUID, "NOCASE") == query.MasterGUID);

                if (item != null)
                {
                    result.Items = new List<IDTO> { (IDTO)item };
                }
                else
                {
                    result.Items = new List<IDTO>();
                }
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

                        // Matchcode 
                        var matchdata = new Dictionary<string, string>
                        {
                            { "Title", dbitem.Title ?? string.Empty },
                            { "Content", dbitem.Content ?? string.Empty }
                        };

                        if (dbitem.Properties != null)
                        {
                            foreach (ItemProperty ip in dbitem.Properties)
                            {
                                if (!string.IsNullOrEmpty(ip.Property))
                                {
                                    matchdata[ip.Property] = ip.Value ?? string.Empty;
                                }
                            }
                        }

                        // Erzeugt ein valides JSON: {"Title":"Task","Content":"","Status":"abgeschlossen",...}
                        dbitem.Matchcode = JsonSerializer.Serialize(matchdata);
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

        public async Task<IQueryResult> ChangeItemType(IDTO dto, string newItemType)
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
                        dbitem.ItemType = newItemType;
                        dto.ItemType = newItemType;

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
        public async Task<IQueryResult> GetRelatedItems(IDTO dto, string ItemType,
                          AssociationTyp Typ = AssociationTyp.Association)
        {
            IQueryResult info = new QueryResult
            {
                Status = "OK",
                Message = "",
                Items = new List<IDTO>()
            };

            if (dto == null)
            {
                info.Status = "FAIL";
                info.Message = "Das übergebene DTO oder dessen GUID ist null.";
                return info;
            }

            string relationTypeStr = Typ.ToSecureEnumString();

            try
            {

                //if (Typ == AssociationTyp.Children ||
                //   Typ == AssociationTyp.Parallels ||
                //   Typ == AssociationTyp.Related ||
                //   Typ == AssociationTyp.UserImage ||
                //   Typ == AssociationTyp.Default)
                //{
                //    info.Items = await GetChildren(dto, ItemType, Typ);
                //}
                //else if (Typ == AssociationTyp.Parents)
                //{
                //    info.Items = await GetParents(dto, ItemType, Typ);
                //}
                //else if (Typ == AssociationTyp.Association)
                //{
                //    info.Items = await GetAll(dto, ItemType, Typ);
                //}
                //else if (Typ == AssociationTyp.Connection)
                //{
                //    info.Items = await GetAllConnected(dto, ItemType, Typ);
                //}
                //else
                //{
                //}
                info.Items = await GetAll(dto, ItemType, Typ);
            }
            catch (Exception ex)
            {
                info.Status = "FAIL";
                info.Message = ex.Message;
            }

            return info;
        }

        // Helper 
        private async Task<List<IDTO>> GetChildren(IDTO dto, string ItemType, AssociationTyp typ)
        {
            List<IDTO> resultList = new List<IDTO>();

            try
            {
                using SQLiteContext ctx = GetContext();

                var query = await (from relation in ctx.tbl_TEC_Relation
                                   join content in ctx.tbl_CON_Content
                                     on relation.ChildGUID equals content.GUID // Definiert die Child Beziehung 
                                   where relation.ParentGUID == dto.GUID // Definiert die von Oben abwärts Richtung 
                                      && content.ItemType == ItemType
                                      && (relation.RelationType == typ.ToSecureEnumString())
                                   select new
                                   {
                                       Content = content,
                                       RelationType = relation.RelationType
                                   }).ToListAsync();

                var dbResult = query.ToList();


                foreach (var item in dbResult)
                {
                    if (item.Content is IDTO dtoItem)
                    {
                        dtoItem.RelationType = item.RelationType;
                        resultList.Add(dtoItem);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return resultList;
        }
        private async Task<List<IDTO>> GetParents(IDTO dto, string ItemType, AssociationTyp typ)
        {
            List<IDTO> resultList = new List<IDTO>();
            try
            {
                using SQLiteContext ctx = GetContext();

                var query = await (from relation in ctx.tbl_TEC_Relation
                                   join content in ctx.tbl_CON_Content
                                     on relation.ParentGUID equals content.GUID
                                   where relation.ChildGUID == dto.GUID
                                      && content.ItemType == ItemType
                                      && (relation.RelationType == typ.ToSecureEnumString())
                                   select new
                                   {
                                       Content = content,
                                       RelationType = relation.RelationType
                                   }).ToListAsync();

                var dbResult = query.ToList();

                foreach (var item in dbResult)
                {
                    if (item.Content is IDTO dtoItem)
                    {
                        dtoItem.RelationType = item.RelationType;
                        resultList.Add(dtoItem);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return resultList;
        }

        private async Task<List<IDTO>> GetAll_obsolete(IDTO dto, string ItemType, AssociationTyp typ)
        {
            List<IDTO> resultList = new List<IDTO>();

            try
            {
                using SQLiteContext ctx = GetContext();

                var query = await (from content in ctx.tbl_CON_Content

                                   // Suche Relationen, wo Content das Parent ist und dto.GUID das Child
                                   join pr in ctx.tbl_TEC_Relation
                                     on new { Parent = content.GUID, Child = dto.GUID }
                                 equals new { Parent = pr.ParentGUID, Child = pr.ChildGUID } into parentRelations
                                   from pr in parentRelations.DefaultIfEmpty()

                                       // Suche Relationen, wo Content das Child ist und dto.GUID das Parent
                                   join cr in ctx.tbl_TEC_Relation
                                     on new { Child = content.GUID, Parent = dto.GUID }
                                 equals new { Child = cr.ChildGUID, Parent = cr.ParentGUID } into childRelations
                                   from cr in childRelations.DefaultIfEmpty()

                                       // Es dürfen nur Einträge geladen werden, die mindestens eine Treffer-Relation haben
                                   where (pr != null || cr != null)
                                      && (string.IsNullOrEmpty(ItemType) || content.ItemType == ItemType)

                                   select new
                                   {
                                       Content = content,
                                       RelationType = pr != null ? pr.RelationType : cr.RelationType
                                   }).Distinct().ToListAsync();

                foreach (var item in query)
                {
                    if (item.Content is IDTO dtoItem)
                    {
                        dtoItem.RelationType = item.RelationType;
                        resultList.Add(dtoItem);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return resultList.DistinctBy(se => se.GUID).ToList();
        }
        public async Task<List<IDTO>> GetAll(IDTO dto, string ItemType, AssociationTyp typ)
        {
            List<IDTO> resultList = new List<IDTO>();

            try
            {
                using SQLiteContext ctx = GetContext();

                bool ignoreRelationType = typ == AssociationTyp.NULL;
                string relationTypeString = typ.ToString();

                var query = await (from content in ctx.tbl_CON_Content.AsNoTracking()

                                       // Parent-Suche (Content = Parent, dto = Child)
                                   join pr in ctx.tbl_TEC_Relation.AsNoTracking().Where(se => ignoreRelationType || se.RelationType == relationTypeString)
                                     on new { Parent = content.GUID, Child = dto.GUID }
                                 equals new { Parent = pr.ParentGUID, Child = pr.ChildGUID } into parentRelations
                                   from pr in parentRelations.DefaultIfEmpty()

                                       // Child-Suche (Content = Child, dto = Parent)
                                   join cr in ctx.tbl_TEC_Relation.AsNoTracking().Where(se => ignoreRelationType || se.RelationType == relationTypeString)
                                     on new { Child = content.GUID, Parent = dto.GUID }
                                 equals new { Child = cr.ChildGUID, Parent = cr.ParentGUID } into childRelations
                                   from cr in childRelations.DefaultIfEmpty()

                                       // Filter: Mindestens eine RelationTreffer & ItemType-Prüfung
                                   where (pr != null || cr != null)
                                      && (string.IsNullOrEmpty(ItemType) || content.ItemType == ItemType)

                                   select new
                                   {
                                       Content = content,
                                       RelationType = pr != null ? pr.RelationType : cr.RelationType
                                   }).Distinct().ToListAsync();

                foreach (var item in query)
                {
                    if (item.Content is IDTO dtoItem)
                    {
                        dtoItem.RelationType = item.RelationType;
                        resultList.Add(dtoItem);
                    }
                }


                //using SQLiteContext ctx = GetContext();

                //var query = await (from content in ctx.tbl_CON_Content

                //                   // Suche Relationen, wo Content das Parent ist und dto.GUID das Child
                //                   join pr in ctx.tbl_TEC_Relation.Where(se => se.RelationType == typ.ToString())
                //                     on new { Parent = content.GUID, Child = dto.GUID }
                //                 equals new { Parent = pr.ParentGUID, Child = pr.ChildGUID } into parentRelations
                //                   from pr in parentRelations.DefaultIfEmpty()

                //                       // Suche Relationen, wo Content das Child ist und dto.GUID das Parent
                //                   join cr in ctx.tbl_TEC_Relation.Where(se => se.RelationType == typ.ToString())
                //                     on new { Child = content.GUID, Parent = dto.GUID }
                //                 equals new { Child = cr.ChildGUID, Parent = cr.ParentGUID } into childRelations
                //                   from cr in childRelations.DefaultIfEmpty()

                //                       // Es dürfen nur Einträge geladen werden, die mindestens eine Treffer-Relation haben
                //                   where (pr != null || cr != null)
                //                      && (string.IsNullOrEmpty(ItemType) || content.ItemType == ItemType)

                //                   select new
                //                   {
                //                       Content = content,
                //                       RelationType = pr != null ? pr.RelationType : cr.RelationType
                //                   }).Distinct().ToListAsync();

                //foreach (var item in query)
                //{
                //    if (item.Content is IDTO dtoItem)
                //    {
                //        dtoItem.RelationType = item.RelationType;
                //        resultList.Add(dtoItem);
                //    }
                //}
            }
            catch (Exception ex)
            {

            }

            return resultList.DistinctBy(se => se.GUID).ToList();
        }

        //public async Task<IQueryResult> GetRelatedItemsObsolete(IDTO dto, string ItemType,
        //                    AssociationTyp Typ = AssociationTyp.Association)
        //{
        //    IQueryResult info = new QueryResult { Status = "OK", Message = "" };
        //    // Helper.DirectionByAssociation(ast) 
        //    RelationDirection direction = Helper.DirectionByAssociation(Typ);
        //    try
        //    {
        //        await using (SQLiteContext ctx = GetContext())
        //        {
        //            // Richtungsbedingung vorab und kompakt kapseln
        //            var query = from content in ctx.tbl_CON_Content
        //                        join relation in ctx.tbl_TEC_Relation
        //                        on 1 equals 1 // Expliziter JOIN für flexible WHERE-Bedingung
        //                        where content.ItemType == ItemType
        //                           && (Typ == AssociationTyp.NULL ||
        //                                Typ == AssociationTyp.Association ||
        //                                relation.RelationType == Typ.ToSecureEnumString())
        //                           && (
        //                               (direction != RelationDirection.Parents && relation.ParentGUID == dto.GUID && relation.ChildGUID == content.GUID) ||
        //                               (direction != RelationDirection.Children && relation.ChildGUID == dto.GUID && relation.ParentGUID == content.GUID)
        //                              )
        //                        select new { content, relation.RelationType };

        //            // Asynchrone Datenbankabfrage ohne unnötige In-Memory-Konvertierung
        //            var dbResult = await query.ToListAsync();

        //            List<IDTO> result = dbResult.ConvertAll(x =>
        //            {
        //                IDTO dtoItem = (IDTO)x.content;
        //                dtoItem.RelationType = x.RelationType;
        //                return dtoItem;
        //            });

        //            info.Items = result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        info.Status = "FAIL";
        //        info.Message = ex.Message;
        //    }

        //    return info;
        //}
        public async Task<IQueryResult> Assign(IDTO parent, IDTO child, AssociationTyp Typ = AssociationTyp.Association)
        {
            IQueryResult info = new QueryResult { Status = "OK", Message = "" };

            try
            {
                await using (SQLiteContext ctx = GetContext())
                {
                    // check 
                    var result = await (from query in ctx.tbl_TEC_Relation
                                        where query.ParentGUID == parent.GUID
                                        && query.ChildGUID == child.GUID
                                        && query.RelationType == Typ.ToSecureEnumString()
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
                    item.RelationType = Typ.ToSecureEnumString();
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
                                        where (query.ParentGUID == parent.GUID && query.ChildGUID == child.GUID) ||
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