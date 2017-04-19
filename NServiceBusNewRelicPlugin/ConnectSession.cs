using Microsoft.Management.Infrastructure;
using System;
using System.Collections.Generic;

namespace NServiceBusNewRelicPlugin
{
    class ConnectSession
    {
        private CimSession session;

        public ConnectSession(CimSession session) {
            this.session = session;
        }

        public List<Metric> getNServiceBusMetrics()
        {
            var serviceBusData = session.QueryInstances(@"root\cimv2", "WQL", "Select * FROM Win32_PerfFormattedData_NServiceBus_NServiceBus WHERE Name=\"acl.command\"");
            List<Metric> metrics = new List<Metric>();

            foreach (CimInstance md in serviceBusData)
            {
                NServiceMetrics nServiceMetrics = new NServiceMetrics();
                foreach (var serviceMetrics in nServiceMetrics.lstNServiceMetrics)
                {
                    Metric metric = new Metric();
                    metric.metricName = serviceMetrics.Value.metricName;
                    metric.unit = serviceMetrics.Value.metricUnit;
                    metric.value = long.Parse(md.CimInstanceProperties[serviceMetrics.Key].Value.ToString());
                    metrics.Add(metric);
                }
            }
            return metrics;
        }

    }
}
