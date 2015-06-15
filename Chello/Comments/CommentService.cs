using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	public class CommentService : TrelloApiBase
	{
		public CommentService(string authKey, string authToken)
			: base(authKey, authToken)
		{
		}

		public IEnumerable<CommentAction> ForBoard(string boardId, object args)
		{
            string queryString = "filter=commentCard&" + BuildQueryString(args);

			return GetRequest<List<CommentAction>>("/boards/{0}/actions?{1}", boardId, queryString);
		}

        public IEnumerable<CommentAction> ForBoard(string boardId)
        {
            return ForBoard(boardId, null);
        }
	}
}
