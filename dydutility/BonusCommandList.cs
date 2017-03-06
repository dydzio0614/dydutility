using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace dydutility
{
    [DataContract]
    class BonusCommandList
    {
        [DataMember]
        public List<BonusCommand> CommandList;
    }
}
