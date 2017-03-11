using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrafficStops.Models
{
    public class StopVars
    {
        public int Id { get; set; }

        [Display(Name = "Race")]
        public string RaceDriver { get; set; }

        [Display(Name = "Gender")]
        public string GenderDriver { get; set; }

        [Display(Name = "Age")]
        public string AgeDriver { get; set; }

        [Display(Name="Race")]
        public string RaceOfficer { get; set; }

        [Display(Name = "Gender")]
        public string GenderOfficer { get; set; }

        [Display(Name = "Age")]
        public string AgeOfficer { get; set; }

        public string Result { get; set; }

        public void SetToEmptyString()
        {
            if(RaceDriver == null)
            {
                RaceDriver = "";
            }
            if (GenderDriver == null)
            {
                GenderDriver = "";
            }
            if (AgeDriver == null)
            {
                AgeDriver = "";
            }
            if (RaceOfficer == null)
            {
                RaceOfficer = "";
            }
            if (GenderOfficer == null)
            {
                GenderOfficer = "";
            }
            if (AgeOfficer == null)
            {
                AgeOfficer = "";
            }
            if (Result == null)
            {
                Result = "";
            }

        }

    }
}