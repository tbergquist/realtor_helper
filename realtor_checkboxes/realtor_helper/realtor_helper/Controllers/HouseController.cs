using realtor_helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realtor_helper.Controllers
{
    public class HouseController : Controller
    {
        // GET: House
        public ActionResult Index()
        {
            var model =
                from r in _lists
                orderby r.Id
                select r;

            return View(model);
        }

        // GET: House/Details/5
        public ActionResult Details(int id)
        {
            var listing = _lists.Single(r => r.Id == id);

            return View(listing);
        }

        // GET: House/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: House/Create
       [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(HouseListing house)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", house);
            }

            _lists.Add(house);

            return RedirectToAction("Index");
        }

        // GET: House/Edit/5
        public ActionResult Edit(int id)
        {
            var listing = _lists.Single(r => r.Id == id);

            return View(listing);
        }

        // POST: House/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var listing = _lists.Single(r => r.Id == id);
            if (TryUpdateModel(listing))
            {
                return RedirectToAction("Index");
            }
            return View(listing);
        }

        // GET: House/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: House/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Believe I may need this in the future

                return RedirectToAction("Index");
            }
            catch
            {
                // ReSharper disable once Mvc.ViewNotResolved
                return View();
            }
        }

        static List<HouseListing> _lists = new List<HouseListing>
        {
            new HouseListing
            {
                Id = 012345,
                Address = "1011 Rogers Circle, Carrollton, GA 30116",
                Price = 323000.75,
                Description = "Country Living at its best but only minutes from all " + 
               "of the amenities of modern living.  This lovely 4 BR/2 full bath 1900 " +
                "sq ft ranch style home sits on 32 pristine acres with a good balance of " +
                "open pastures, mixed woods, 400 ft paved driveway and electric front gate. " +
                "Home features a Full Length Screened-in Back Porch, Granite Countertops in Kitchen " +
                "and Baths, Custom Tiled Baths/Showers, and cozy woodstove in the living room. " +
                "The Sculpted, 25,000 gal. Gunite/Saltwater pool with 3 waterfalls provide a wonderful " +
                "venue for entertaining or the perfect oasis for relaxing at the end of a long day. " +
                "There is a 2 car detached garage and workshop with half-bath, along with a 3-stall bar"
            },

            new HouseListing
            {
                Id = 780436,
                Address = "138 West Valley Street, Temple, GA 30179",
                Price = 139000.92,
                Description = "138 West Valley Street in Temple, GA is a cute 3 bedroom/2.5 Bath home " +
                "in a nice, quiet neighborhood. The Hardwood Flooring and the Fireplace in the Family " +
                "Room highlight the lower level of this home. While upstairs, you will appreciate a " +
                "Master Bathroom featuring a Double Vanity along with a Separate Tub & Shower."
            },

            new HouseListing
            {
                Id = 987123,
                Address = "22 Clark Avenue, Carrollton, GA 30116",
                Price = 272345.25,
                Description = "Take advantage of this wonderful opportunity! Beautiful home located in " +
                "the country with over 3600 square feet and 90% completed. This home features an eat-in " +
                "kitchen with granite countertops, high ceilings, exposed wood beams, hardwood floors " +
                "throughout and sits on almost 50 acres. Additionally, a 2800 square feet shop that sits " +
                "on a concrete slab is only steps away."
            },

            new HouseListing
            {
                Id = 384841,
                Address = "901 Scout Road, Carrollton, GA 30117",
                Price = 172600.52,
                Description = "A must see in Carrollton City! 4 Bedroom, 2.5 Bath home just a minute away " +
                "from the Carrollton Greenbelt near Lakeshore Recreational Center. Many nice upgrades; " +
                "tankless water heater; whole house surge protector; gas logs in a real brick fireplace; " +
                "handicap accessible ramp & hand rails in bathrooms; Large rear deck; new counter tops in " +
                "kitchen & new ceramic tiles in Bathrooms; New Pain inside and out. HVAC only 2 years old. "

            }
        };
    }
}
