using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
    [DataContract]
    public class CardUpdate : ITrelloEntity
    {
        [JsonProperty("id")]
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [JsonProperty("idCard")]
        [DataMember(Name = "idCard")]
        public string IdCard { get; set; }
    }
}
