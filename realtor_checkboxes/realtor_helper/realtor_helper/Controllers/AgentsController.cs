using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using realtor_helper.DAL;
using realtor_helper.Models;

namespace realtor_helper.Controllers
{
    public class AgentsController : Controller
    {
        private DAL.RealtorContext db = new DAL.RealtorContext();

        // GET: Agents
        public ActionResult Index()
        {
            return View(db.Agents.ToList());
        }

        // GET: Agents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            //agent.AgentHouses.Add(GetAgentHouses(id));
            string houseAddress = GetAgentHouses(id);
            //List<string> list = new List<string>();
            //list.Add(houseAddress);
            agent.AgentHouses = houseAddress;
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // GET: Agents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AgentName,PhoneNumber,Email,House")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Agents.Add(agent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agent);
        }

        // GET: Agents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AgentName,PhoneNumber,Email,House")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agent);
        }

        // GET: Agents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agent agent = db.Agents.Find(id);
            db.Agents.Remove(agent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public string GetAgentHouses(int? id)
        {
            Agent agentId = db.Agents.Find(id);
            HouseListing houseListing = db.Houses.Find(id);
            List<string> agenthouses = new List<string>();
            if(agentId.ID == 0 || agentId.ID.Equals(null))
            {
                return "Please contact me if you need an agent.";
            }

            if (agentId.ID == houseListing.Agent_ID)
            {
                string address = houseListing.Address;
                // Agent agents = new Agent();
                //agenthouses.Add(address);
                return address;
            }
            
            if(agentId.AgentName.Equals(houseListing.AgentName))
            {
                    string address = houseListing.Address;
                    return address;
            }
            

            return "Please contact me if you need an agent.";
           
        }
    }
}
