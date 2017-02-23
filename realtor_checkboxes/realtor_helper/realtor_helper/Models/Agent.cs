using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace realtor_helper.Models
{
    public class Agent
    {
        public int ID { get; set; }
        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }
        [Required(ErrorMessage = "Please provide a PhoneNumber")]
        [Display(Name = "Work Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AgentHouses { get; set; }

        public virtual ICollection<HouseListing> House { get; set; }            
    }
}