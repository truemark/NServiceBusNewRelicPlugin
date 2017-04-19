using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBusNewRelicPlugin
{
    class NServiceMetrics
    {
        public Dictionary<String, ValuePair> lstNServiceMetrics = new Dictionary<string, ValuePair>();

        public NServiceMetrics() {
            //lstNServiceMetrics.Add("Name", new ValuePair { metricName = "Name", metricUnit = "Name" });
            lstNServiceMetrics.Add("CriticalTime", new ValuePair { metricName = "Critical Time", metricUnit = "Sec" });
            lstNServiceMetrics.Add("NumberofmsgsfailuresPersec", new ValuePair { metricName = "Number Of Messages Failures", metricUnit = "Per/Sec" });
            lstNServiceMetrics.Add("NumberofmsgspulledfromtheinputqueuePersec", new ValuePair { metricName = "Number Of Messages Pulled From Input Queue", metricUnit = "Per/Sec" });
            lstNServiceMetrics.Add("NumberofmsgssuccessfullyprocessedPersec", new ValuePair { metricName = "Number Of Messages Fully Processed", metricUnit = "Per/Sec" });
            lstNServiceMetrics.Add("SLAviolationcountdown", new ValuePair { metricName = "SLA Voilation Countdown", metricUnit = "Voilations" });
        }
    }

    struct ValuePair {
        public string metricName;
        public string metricUnit;
    }
}
