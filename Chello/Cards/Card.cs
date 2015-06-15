using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class Card : ITrelloEntity
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[JsonProperty("desc")]
		[DataMember(Name = "desc")]
		public string Desc { get; set; }

		[JsonProperty("idBoard")]
		[DataMember(Name = "idBoard")]
		public string IdBoard { get; set; }

		[JsonProperty("idList")]
		[DataMember(Name = "idList")]
		public string IdList { get; set; }

		[JsonProperty("closed")]
		[DataMember(Name = "closed")]
		public bool Closed { get; set; }

		[JsonProperty("labels")]
		[DataMember(Name = "labels")]
		public System.Collections.Generic.List<Label> Labels { get; set; }

		[DataContract]
		public class Label
		{
		    [JsonProperty("color")]
		    [DataMember(Name = "color")]
		    public string Color { get; set; }
		    [JsonProperty("name")]
		    [DataMember(Name = "name")]
		    public string Name { get; set; }
		}
	}
}
