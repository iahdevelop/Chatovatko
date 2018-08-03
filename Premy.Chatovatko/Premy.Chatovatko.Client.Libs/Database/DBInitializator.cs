﻿using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.UserData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Resources;
using Premy.Chatovatko.Client.Libs.Database.SqlScripts;
using Premy.Chatovatko.Libs.Logging;

namespace Premy.Chatovatko.Client.Libs.Database
{
    public class DBInitializator :ILoggable
    {
        private readonly IClientDatabaseConfig config;
        private readonly Logger logger;
        public DBInitializator(IClientDatabaseConfig config, Logger logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public void DBDelete()
        {
            File.Delete(config.DatabaseAddress);
            logger.Log(this, "Database deleted");
        }

        public void DBInit()
        {
            if (File.Exists(config.DatabaseAddress))
            {
                DBDelete();
            }

            using (var context = new SqlClientContext(config))
            {
                try
                {
                    ResourceManager resources = new ResourceManager(typeof(Resource));
                    string createScript = (string)resources.GetObject("sqlBuild");

                    context.Database.ExecuteSqlCommand(createScript);
                }
                catch(Exception ex)
                {
                    DBDelete();
                    throw new ChatovatkoException(this, ex, "Initialization of database failed");
                }
            }
            logger.Log(this, "Database initialized");

        }

        public void DBEnsureCreated()
        {
            if (File.Exists(config.DatabaseAddress))
            {
                logger.Log(this, "Database exists already");
            }
            else
            {
                DBInit();
            }
        }

        public string GetLogSource()
        {
            return "Database initializator";
        }
    }
}
