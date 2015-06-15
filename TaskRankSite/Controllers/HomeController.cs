using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskRankSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id=-1)
        {
            if (id == -1)
            {
                return View("CreateOrJoin");
            }
            else if (TaskRankSite.Models.GroupSession.Sessions.Count == 0)
            {
                TaskRankSite.Models.GroupSession.Sessions.Add(1234, new TaskRankSite.Models.GroupSession() { Id = 1234 });
            }

            if (TaskRankSite.Models.GroupSession.Sessions.ContainsKey(id))
            {
                ViewBag.Session = TaskRankSite.Models.GroupSession.Sessions[id];
                return View("Index");
            }
            else
            {
                // This should probobly notify the user that the ID they passed in didn't work
                return View("CreateOrJoin");
            }
        }

        public ActionResult GroupSession()
        {
            return View("Index");
        }

        public ActionResult Create(string Title, string Name)
        {
            TaskRankSite.Models.GroupSession newSession = new TaskRankSite.Models.GroupSession();

            newSession.Title = Title;
            newSession.TeamLead = new Models.Member() { Name = Name };
            newSession.Members.Add(newSession.TeamLead);
            
            ViewBag.Session = newSession;

            return View("Index");

        }

        public ActionResult Join(int Id, string Name)
        {
            if (TaskRankSite.Models.GroupSession.Sessions.ContainsKey(Id))
            {
                TaskRankSite.Models.GroupSession.Sessions[Id].Members.Add(new TaskRankSite.Models.Member() { Name = Name });

                ViewBag.Session = TaskRankSite.Models.GroupSession.Sessions[Id];

                return View("Index");
            }
            else
            {
                return View("CreateOrJoin");
            }
        }
    }
}
