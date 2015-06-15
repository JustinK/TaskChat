using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class Notification : ITrelloEntity
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("unread")]
		[DataMember(Name = "unread")]
		public bool Unread { get; set; }

		[JsonProperty("type")]
		[DataMember(Name = "type")]
		public string Type { get; set; }

		[JsonProperty("date")]
		public DateTime Date { get; set; }

		[JsonProperty("data")]
		[DataMember(Name = "data")]
		public string Data { get; set; }

		[JsonProperty("idMemberCreator")]
		[DataMember(Name = "idMemberCreator")]
		public string IdMemberCreator { get; set; }

        //Needed for Test Service to make sure date formats are handled the same way
        //as they are in the Trello API -> "yyyy-MM-ddTHH:mm:ss.fffZ"
        [DataMember(Name = "date")]
        private string DateForSerialization { get; set; }

        [OnSerializing]
        void OnSerializing(StreamingContext ctx)
        {
            DateForSerialization = Date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext ctx)
        {
            //Used to prevent null exceptions in the event date isn't supplied
            DateForSerialization = "1900-01-01T00:00:00.000Z";
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            Date = DateTime.ParseExact(DateForSerialization, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }
	}
}