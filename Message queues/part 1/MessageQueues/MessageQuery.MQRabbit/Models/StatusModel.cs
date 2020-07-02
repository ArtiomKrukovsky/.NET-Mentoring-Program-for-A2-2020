using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQuery.MQRabbit.Models
{
    public class StatusModel
    {
        public int MaxMessageSize { get; set; }

        public string CurrentStatus { get; set; }
    }
}
