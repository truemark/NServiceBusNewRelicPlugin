using NewRelic.Platform.Sdk;
using NewRelic.Platform.Sdk.Utils;
using System;
using System.Collections.Generic;

namespace NServiceBusNewRelicPlugin
{
    class NServiceBusAgentFactory : AgentFactory
    {
        Logger logger = Logger.GetLogger("NServiceBusAgentFactory");

        public override Agent CreateAgentWithConfiguration(IDictionary<string, object> properties)
        {
            string name = (string)properties["name"];
            if (string.IsNullOrEmpty(name))
            {
                logger.Error("Agent Name may not be null or empty.");
                throw new Exception("Agent Name may not be null or empty.");
            }
            string host = (string)properties["host"];
            if (string.IsNullOrEmpty(host))
            {
                logger.Error("Host may not be null or empty.");
                throw new Exception("Host may not be null or empty.");
            }
            string domain = (string)properties["domain"];
            if (string.IsNullOrEmpty(domain))
            {
                logger.Error("Domain may not be null or empty.");
                throw new Exception("Domain may not be null or empty.");
            }
            string username = (string)properties["username"];
            if (string.IsNullOrEmpty(username))
            {
                logger.Error("Username may not be null or empty.");
                throw new Exception("Username may not be null or empty.");
            }
            string password = (string)properties["password"];
            if (string.IsNullOrEmpty(password))
            {
                logger.Error("Password may not be null or empty.");
                throw new Exception("Password may not be null or empty.");
            }
            return new NServiceBusAgent(name, host, domain, username, password);
        }
    }
}