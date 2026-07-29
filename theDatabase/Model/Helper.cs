using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using theInfrastructure;

namespace theDatabase
{
    public static class SqlHelper
    {
        public static string GenerateID(string itemType, Guid masterGUID)
        {
            string id = "" + Helper.GenerateShort(itemType) + "-";

            if (masterGUID == Guid.Empty)
                return id;

            try
            {
                List<string> ids = new List<string>();

                // Context 
                using (SQLiteContext context = SQLiteService.GetContext())
                {
                    // Setting 
                    ids = (from item in context.tbl_CON_Content
                           where item.ItemType == itemType
                                //&& item.MasterGUID == masterGUID
                                && item.ID != null
                                && item.ID != string.Empty
                           select item.ID).ToList();

                    // Close and Dispose 
                    context.Database.CloseConnection();
                    context.Dispose();
                }
                int max = 0;
                foreach (string text in ids)
                {
                    int number = Helper.ExtractNumber(text);
                    if (number > max)
                    {
                        max = number;
                    }
                }

                return id + (max + 1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FAIL : " + ex.Message);
            }

            return id;
        }

    }
}
