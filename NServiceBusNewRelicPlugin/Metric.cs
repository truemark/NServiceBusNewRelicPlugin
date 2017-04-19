using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBusNewRelicPlugin
{
    class Metric
    {
        public string metricName { get; set; }
        public string unit { get; set; }
        public long value { get; set; }
    }
}
