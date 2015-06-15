using System;
using System.Collections.Generic;

namespace Chello.Core
{
    public class CardUpdateService : TrelloApiBase
    {
        public CardUpdateService(string authKey, string authToken)
            : base(authKey, authToken)
        {
        }

        public IEnumerable<CardUpdateAction> ForBoard(string boardId, object args)
        {
            string queryString = BuildQueryString(args);

            return GetRequest<List<CardUpdateAction>>("/boards/{0}/actions?{1}", boardId, queryString);
        }

        public IEnumerable<CardUpdateAction> ForBoard(string boardId)
        {
            return ForBoard(boardId, null);
        }

        public IEnumerable<CardUpdateAction> ForBoardSince(string boardId, DateTime sinceDate)
        {
            return ForBoard(boardId, new { since = sinceDate });
        }

        public IEnumerable<CardUpdateAction> ForCard(string cardId, object args)
        {
            string queryString = BuildQueryString(args);

            return GetRequest<List<CardUpdateAction>>("/cards/{0}/actions?{1}", cardId, queryString);
        }

        public IEnumerable<CardUpdateAction> ForCard(string cardId)
        {
            return ForCard(cardId, null);
        }

        public IEnumerable<CardUpdateAction> ForCardSince(string cardId, DateTime sinceDate)
        {
            return ForCard(cardId, new { since = sinceDate });
        }
    }
}