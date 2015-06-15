using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskRankSite.Models
{
    public class GroupSession
    {
        // Dictionary of sessions, indexed by session ID
        public static Dictionary<int, GroupSession> Sessions { get; set; }

        static GroupSession()
        {
            Sessions = new Dictionary<int, GroupSession>();
        }

        /// <summary>
        /// Creates a new session with a unique ID and adds it to the collection of ongoing Sessions.
        /// </summary>
        public GroupSession()
        {
            Id = GroupSession.GetId();
            IsStale = false;
            VoteValues = new[] { "1", "2", "3", "5", "8", "12", "24" };
            Tasks = new List<object>();
            Members = new List<Member>();

            Sessions.Add(this.Id, this);
        }

        public static int GetId()
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            int newSession = rand.Next(Sessions.Count < 100 ? 100 : Sessions.Count * Sessions.Count);
            // Get a new session ID and make sure it's not already in use.
            while (Sessions.ContainsKey(newSession))
            {
                newSession = rand.Next(Sessions.Count * Sessions.Count);
            }

            return newSession;
        }

        public GroupSession(IEnumerable<string> voteValues)
            : this()
        {
            VoteValues = voteValues;
        }

        // Each setter should broadcast the change to the group, and maybe
        // include a hash of the data so users can detect when they're out of sync (v2 feature ;) )
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> VoteValues { get; set; }
        public object CurrentTask { get; set; }
        public IList<object> Tasks { get; set; }
        public IList<Member> Members { get; set; }
        public Member TeamLead { get; set; }
        public bool IsStale { get; set; }
    }

    public class Member
    {
        public string Name;
    }
}