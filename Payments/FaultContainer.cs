using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Payments
{
    [DataContract]
    public class FaultContainer
    {
        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}