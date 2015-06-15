using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
    [DataContract]
    public class CardUpdateActionData
    {
        [JsonProperty("listBefore")]
        [DataMember(Name = "listBefore")]
        public List ListBefore { get; set; }

        [JsonProperty("listAfter")]
        [DataMember(Name = "listAfter")]
        public List ListAfter { get; set; }

        [JsonProperty("card")]
        [DataMember(Name = "card")]
        public Card Card { get; set; }
    }
}
