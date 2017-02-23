using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;


namespace realtor_helper.Models
{
    public class HouseListing
    {
        public int Id { get; set; }
        public string Address { get; set; }
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage="Price cannot be less than or equal to zero.")]
        public double Price { get; set; }
        public string Description { get; set; } 
        public string AgentName { get; set; }  
        
        public int Agent_ID { get; set; }
        public string ListOfFeatures { get; set; }
                
        public virtual IList<Feature> Features { get; set; }
        public virtual Agent Agent { get; set; }
       
    }
}