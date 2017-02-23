using Microsoft.Ajax.Utilities;
using realtor_helper.Controllers;
using realtor_helper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebGrease.Css.Extensions;

namespace realtor_helper.DAL
{
    public class RealtorInitializer : DropCreateDatabaseAlways<RealtorContext>
    {       
        protected override void Seed(RealtorContext context)
        {
            // base.Seed(context);
            //var features = new List<Feature>
            //{
            //    new Feature { FeatureID=25, Name="Attic"},
            //    new Feature { FeatureID=35, Name="Porch" },
            //    new Feature { FeatureID=45, Name="Swimming Pool" },
            //    new Feature { FeatureID=55, Name="Fence" }
            //};

            //features.ForEach(s => context.Features.Add(s));
            //context.SaveChanges();

            //var agents = new List<Agent>
            //{
            //    new Agent { ID=1, AgentName="Joe Smith", PhoneNumber="(770) 834-8444", Email="jsmith@gmail.com"},
            //    new Agent { ID=2, AgentName="Shannon Poe", PhoneNumber="(678) 993-3553", Email="shpoe@gmail.com" },
            //    new Agent { ID=3, AgentName="Albert Ricks", PhoneNumber="(678) 775-6776", Email="aRick@yahoo.com" },
            //    new Agent { ID=4, AgentName="Amy Denney", PhoneNumber="(404) 334-0909", Email="adenney@gmail.com" }
            //};

            //agents.ForEach(s => context.Agents.Add(s));
            //context.SaveChanges();
            HouseListingsController listing = new HouseListingsController();
            var houses = new List<HouseListing>
            {

                new HouseListing
            {
                Id = 1,
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
                "There is a 2 car detached garage and workshop with half-bath, along with a 3-stall bar",
                Agent_ID = 1, AgentName = "Joe Smith", Features=listing.CreateBaseFeatureList()

            },

            new HouseListing
            {
                Id = 2,
                Address = "138 West Valley Street, Temple, GA 30179",
                Price = 139000.92,
                Description = "138 West Valley Street in Temple, GA is a cute 3 bedroom/2.5 Bath home " +
                "in a nice, quiet neighborhood. The Hardwood Flooring and the Fireplace in the Family " +
                "Room highlight the lower level of this home. While upstairs, you will appreciate a " +
                "Master Bathroom featuring a Double Vanity along with a Separate Tub & Shower.",
                Agent_ID = 2, AgentName = "Shannon Poe", Features=listing.CreateBaseFeatureList()
            },

            new HouseListing
            {
                Id = 3,
                Address = "22 Clark Avenue, Carrollton, GA 30116",
                Price = 272345.25,
                Description = "Take advantage of this wonderful opportunity! Beautiful home located in " +
                "the country with over 3600 square feet and 90% completed. This home features an eat-in " +
                "kitchen with granite countertops, high ceilings, exposed wood beams, hardwood floors " +
                "throughout and sits on almost 50 acres. Additionally, a 2800 square feet shop that sits " +
                "on a concrete slab is only steps away." , Agent_ID = 3, AgentName = "Albert Ricks", Features=listing.CreateBaseFeatureList()

        },

            new HouseListing
            {
                Id = 4,
                Address = "901 Scout Road, Carrollton, GA 30117",
                Price = 172600.52,
                Description = "A must see in Carrollton City! 4 Bedroom, 2.5 Bath home just a minute away " +
                "from the Carrollton Greenbelt near Lakeshore Recreational Center. Many nice upgrades; " +
                "tankless water heater; whole house surge protector; gas logs in a real brick fireplace; " +
                "handicap accessible ramp & hand rails in bathrooms; Large rear deck; new counter tops in " +
                "kitchen & new ceramic tiles in Bathrooms; New Pain inside and out. HVAC only 2 years old. ",
                Agent_ID = 4, AgentName = "Amy Denney", Features=listing.CreateBaseFeatureList()

        }
        };

            houses.ForEach(s => context.Houses.Add(s));           
            context.SaveChanges();

            var features = new List<Feature>
            {
                new Feature { FeatureID=25, Name="Attic"},
                new Feature { FeatureID=35, Name="Porch" },
                new Feature { FeatureID=45, Name="Swimming Pool" },
                new Feature { FeatureID=55, Name="Fence" }
            };

            features.ForEach(s => context.Features.Add(s));
            context.SaveChanges();

            var agents = new List<Agent>
            {
                new Agent { ID=1, AgentName="Joe Smith", PhoneNumber="(770) 834-8444", Email="jsmith@gmail.com"},
                new Agent { ID=2, AgentName="Shannon Poe", PhoneNumber="(678) 993-3553", Email="shpoe@gmail.com" },
                new Agent { ID=3, AgentName="Albert Ricks", PhoneNumber="(678) 775-6776", Email="aRick@yahoo.com" },
                new Agent { ID=4, AgentName="Amy Denney", PhoneNumber="(404) 334-0909", Email="adenney@gmail.com" }
            };

            agents.ForEach(s => context.Agents.Add(s));
            context.SaveChanges();

            /* var realty = (from h in houses
                join ag in agents on h.Agent_ID equals ag.ID
                where h.Agent_ID == ag.ID
                          select new
                {
                    Id = h.Id,
                    Address = h.Address,
                    Price = h.Price,
                    Description = h.Description,
                    AgentName = ag.AgentName,
                    
                }).ToList(); */

            // var realty = from h in houses
            //             join ag in agents on h.Id equals ag.ID
            //             join Agent in agents on h.Agent_ID equals Agent.ID
            //             where h.Agent_ID == 1
            //             select new { Id = h.Id, AgentName = ag.AgentName, Address = h.Address, Price = h.Price };

            //var result = realty.ToList();

            context.SaveChanges();

        }      
    
        }
    }
