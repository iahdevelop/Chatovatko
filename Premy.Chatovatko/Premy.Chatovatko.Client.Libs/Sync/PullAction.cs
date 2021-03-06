using Premy.Chatovatko.Client.Libs.ClientCommunication;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Sync
{
    public class PullAction : IAction, ILoggable
    {
        private readonly Action reconnect;
        private readonly Func<Connection> getConnection;
        private readonly Logger logger;
        private readonly SettingsCapsula settings;

        public PullAction(Func<Connection> getConnection, Action reconnect, Logger logger, SettingsCapsula settings)
        {
            this.reconnect = reconnect;
            this.getConnection = getConnection;
            this.logger = logger;
            this.settings = settings;
        }

        public string GetLogSource()
        {
            return "Automatized pull action";
        }

        public IAction GetNext()
        {
            return new PullAction(getConnection, reconnect, logger, settings);
        }

        public bool Run()
        {
            try
            {
                return getConnection().Pull() > 0;
            }
            catch (Exception ex)
            {
                logger.LogException(this, ex);
                reconnect();
            }
            return false;
        }
    }
}
