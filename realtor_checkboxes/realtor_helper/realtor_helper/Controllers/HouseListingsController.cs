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
using realtor_helper.ViewModels;
using System.Data.Entity.Infrastructure;

namespace realtor_helper.Controllers
{
    public class HouseListingsController : Controller
    {
        private DAL.RealtorContext db = new DAL.RealtorContext();

        // GET: HouseListings
        public ActionResult Index()
        {              
            return View(db.Houses.ToList());
        }


        // GET: HouseListings/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseListing houseListing = db.Houses.Find(id);
            if (houseListing == null)
            {
                return HttpNotFound();
            }

            return View(houseListing);
        }       

        //// GET: HouseListings/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: HouseListings/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Address,Price,Description,Agent_ID,AgentName,Feature")] HouseListing houseListing)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Houses.Add(houseListing);               
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(houseListing);
        //}

        public ActionResult Create()
        {
            var houselisting = new HouseListing();
            houselisting.Features = new List<Feature>();
            PopulateAssignedCourseData(houselisting);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Address, Price, Description, AgentName, Feature, HouseFeature")]HouseListing houselisting, string[] selectedFeatures)
        {
            if (selectedFeatures != null)
            {
                houselisting.Features = new List<Feature>();
                foreach (var feature in selectedFeatures)
                {
                    var featureToAdd = db.Features.Find(int.Parse(feature));
                    houselisting.Features.Add(featureToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Houses.Add(houselisting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(houselisting);
            return View(houselisting);
        }


        // GET: HouseListings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // HouseListing houseListing = db.Houses.Find(id);
           

           HouseListing houseListing = db.Houses
                .Include(i => i.Agent)
                .Include(i => i.Features)        
                .Where(i => i.Id == id)
                .Single();
            PopulateAssignedCourseData(houseListing);
            if (houseListing == null)
            {
                return HttpNotFound();
            }
            return View(houseListing);
        }

        private void PopulateAssignedCourseData(HouseListing houseListing)
        {
            var allFeatures = db.Features;
            var featureNames = new HashSet<int>(houseListing.Features.Select(c => c.FeatureID));
            var viewModel = new List<HouseFeature>();
            foreach (var feature in allFeatures)
            {
                    viewModel.Add(new HouseFeature
                    {
                        FeatureID = feature.FeatureID,
                        FeatureName = feature.Name,                       
                        Assigned = featureNames.Contains(feature.FeatureID)
                    });  //ViewBag.Features = viewModel;

            }
            ViewBag.Features = viewModel;
        }

        // POST: HouseListings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Address,Price,Description,Agent_ID,AgentName,Feature")] HouseListing houseListing)
        //{
        //    RealtorContext context = new RealtorContext();
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(houseListing).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }            
        //    return View(houseListing);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedFeatures)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var featureToUpdate = db.Houses
               .Include(i => i.Agent)
               .Include(i => i.Features)              
               .Where(i => i.Id == id)
               .Single();

            if (TryUpdateModel(featureToUpdate, "",
               new string[] { "Id", "Address", "Price", "Description", "AgentName", "Feature", "HouseFeature" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(featureToUpdate.AgentName))
                    {
                        featureToUpdate.Agent = null;
                    }

                    UpdateHouseFeatures(selectedFeatures, featureToUpdate);                   
                    db.SaveChanges();

                    return RedirectToAction("Index");

                }

                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedCourseData(featureToUpdate);
            return View(featureToUpdate);
        }

        private void UpdateHouseFeatures(string[] selectedFeatures, HouseListing featureToUpdate)
        {
            if (selectedFeatures == null)
            {
                featureToUpdate.Features = new List<Feature>();
                return;
            }

            var selectedFeaturesHS = new HashSet<string>(selectedFeatures);
            var houseFeatures = new HashSet<int> (featureToUpdate.Features.Select(c => c.FeatureID));
            foreach (var feature in db.Features)
            {
                if (selectedFeaturesHS.Contains(feature.FeatureID.ToString()))
                {
                    if (!houseFeatures.Contains(feature.FeatureID))
                    {
                        featureToUpdate.Features.Add(feature);
                    }
                }
                else
                {
                    if (houseFeatures.Contains(feature.FeatureID))
                    {
                       featureToUpdate.Features.Remove(feature);
                       // featureToUpdate.Features.Add(feature);
                    }
                }
            }
        }

        // GET: HouseListings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            HouseListing houseListing = db.Houses.Find(id);
            if (houseListing == null)
            {
                return HttpNotFound();
            }
            return View(houseListing);
        }

        // POST: HouseListings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    HouseListing houseListing = db.Houses.Find(id);
        //    db.Houses.Remove(houseListing);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseListing houselisting = db.Houses
              .Include(i => i.Agent)
              .Include(i => i.Features)
              .Where(i => i.Id == id)
              .Single();

           // DeleteEmployee(houselisting);
           db.Houses.Remove(houselisting);
            db.SaveChanges();
            //var agent = db.Houses
            //    .Where(d => d.Id == id)
            //    .FirstOrDefault();
                        
           // db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public string DeleteHouse(HouseListing listing)
        {
            if (listing != null)
            {
                using (RealtorContext dataContext = new RealtorContext())
                {
                    int no = Convert.ToInt32(listing.Id);
                    var houseList = dataContext.Houses.Where(x => x.Id == no).FirstOrDefault();
                    dataContext.Houses.Remove(houseList);
                    dataContext.SaveChanges();
                    return "House Deleted";
                }
            }
            else
            {
                return "Invalid House";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<Feature> CreateBaseFeatureList()
        {
            var features = new List<Feature>
            {
                new Feature { FeatureID=25, Name="Attic"},
                new Feature { FeatureID=35, Name="Front Porch" },
                new Feature { FeatureID=45, Name="Basement" },
                new Feature { FeatureID=55, Name="Fence" }
            };


            return features;


        }
    }
}
