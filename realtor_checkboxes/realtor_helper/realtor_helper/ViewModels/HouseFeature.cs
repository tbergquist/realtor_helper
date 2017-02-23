using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace realtor_helper.ViewModels
{
    public class HouseFeature
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public bool Assigned { get; set; }
    }
}