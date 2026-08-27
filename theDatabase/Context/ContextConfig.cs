using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace theDatabase
{
    public class ContextConfig
    {
        public string DatabaseFileName = "DEFAULT.db;"; // DEFAULT Cache=Shared;Mode=ReadWriteCreate;

        public ContextConfig()
        { }

        public static ContextConfig Default(string dbFileName = "DEFAULT.db;") // Cache=Shared;Mode=ReadWriteCreate;
        {
            ContextConfig config = new ContextConfig();

            config.DatabaseFileName = dbFileName;

            return config;
        }

    }

}
