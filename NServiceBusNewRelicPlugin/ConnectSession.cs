using Microsoft.Management.Infrastructure;
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
            var serviceBusData = session.QueryInstances(@"root\cimv2", "WQL", "Select * FROM Win32_PerfFormattedData_NServiceBus_NServiceBus");
            List<Metric> metrics = new List<Metric>();

            Metric totalMessagesPulled = new Metric();
            totalMessagesPulled.metricName = "Total Number Of Messages Pulled From Input Queue";
            totalMessagesPulled.unit = "Messages/Sec";
            totalMessagesPulled.value = 0;
            Metric totalMessageFailers = new Metric();
            totalMessageFailers.metricName = "Total Number Of Message Failures";
            totalMessageFailers.unit = "Messages/Sec";
            totalMessageFailers.value = 0;
            Metric totalMessagesProcessed = new Metric();
            totalMessagesProcessed.metricName = "Total Number Of Messages Fully Processed";
            totalMessagesProcessed.unit = "Messages/Sec";
            totalMessagesProcessed.value = 0;

            foreach (CimInstance md in serviceBusData)
            {
                string name = md.CimInstanceProperties["Name"].Value.ToString();
                NServiceMetrics nServiceMetrics = new NServiceMetrics();
                foreach (var serviceMetrics in nServiceMetrics.lstNServiceMetrics)
                {
                    Metric metric = new Metric();
                    metric.metricName = name + "/" + serviceMetrics.Value.metricName;
                    metric.unit = serviceMetrics.Value.metricUnit;
                    metric.value = long.Parse(md.CimInstanceProperties[serviceMetrics.Key].Value.ToString());
                    metrics.Add(metric);
                    switch (serviceMetrics.Value.metricName)
                    {
                        case "Messages/Number Of Messages Failures":
                            totalMessageFailers.value += metric.value;
                            break;
                        case "Messages/Number Of Messages Pulled From Input Queue":
                            totalMessagesPulled.value += metric.value;
                            break;
                        case "Messages/Number Of Messages Fully Processed":
                            totalMessagesProcessed.value += metric.value;
                            break;
                        default:
                            break;
                    }
                }
            }
            metrics.Add(totalMessageFailers);
            metrics.Add(totalMessagesProcessed);
            metrics.Add(totalMessagesPulled);
            return metrics;
        }

    }
}
