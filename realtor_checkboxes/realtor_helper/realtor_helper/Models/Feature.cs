using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace realtor_helper.Models
{
    public class Feature
    {
        public int FeatureID { get; set; }
        public string Name { get; set; }

        public virtual HouseListing HouseListing { get; set; }
        public virtual Agent Agent { get; set; }
    }
}