﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace dydutility
{
    [DataContract]
    class BonusCommand
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Response { get; set; }
    }
}
