using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONLINE_SHOPPING.BO.Models
{
    public class SmsSentDataModel
    {
        public string Phone { get; set; }
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
        public int SmsType { get; set; }
        public bool SentStatus { get; set; }


    }
}