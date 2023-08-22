using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class Company
    {
        [Key]
        public int Company_ID { get; set; }

       
        public string CompanyName { get; set; }

        public string Description { get; set; }

      
        public DateTime StartDate { get; set; }


        [JsonIgnore]
        public User User { get; set; }


    }
}
