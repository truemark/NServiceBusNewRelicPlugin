using NewRelic.Platform.Sdk;
using NewRelic.Platform.Sdk.Utils;
using System;
using System.Collections.Generic;

namespace NServiceBusNewRelicPlugin
{
    class NServiceBusAgent : Agent
    {
        Logger logger = Logger.GetLogger("NServiceBusAgent");

        private String host, domain, username, password, agentName;

        public override string Guid
        {
            get
            {
                return "com.truemark.newrelic.NServiceBus";
            }
        }

        public override string Version
        {
            get
            {
                return "1.0.0";
            }
        }

        public override string GetAgentName()
        {
            return this.agentName;
        }

        public NServiceBusAgent(String agentName, String host, String domain, String username, String password)
        {
            this.agentName = agentName;
            this.host = host;
            this.domain = domain;
            this.username = username;
            this.password = password;
        }

        public override void PollCycle()
        {
            try
            {
                ConnectSession connection = ConnectionUtil.connect(this.host, this.domain, this.username, this.password);
                List<Metric> metrics = connection.getNServiceBusMetrics();
                logger.Debug("Reporting metrics for : " + this.host);
                int count = 0;
                foreach (var metric in metrics)
                {
                    logger.Debug("Reporting metric: {0}, unit: {1}, value: {2}", metric.metricName, metric.unit, metric.value);
                    //     ReportMetric(metric.metricName, metric.unit, metric.value);
                    count++;
                }
                logger.Debug("Reported {0} Metrics.", count);
            }
            catch (Exception e)
            {
                logger.Error("Error getting metrics for: {0} : {1}", this.host, e.Message);
            }
        }
    }
}