using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrafficStops.Models;


namespace TrafficStops.ViewModels
{
    public class LookUpViewModel
    {
        public DropDownListValues Races { get; set; }
        public DropDownListValues DriverAges { get; set; }
        public DropDownListValues OfficerAges { get; set; }
        public DropDownListValues Genders { get; set; }

        public StopVars StopVars { get; set; }

    }
}