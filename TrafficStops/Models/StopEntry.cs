using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrafficStops.Models
{
    public class StopEntry
    {
        //"ID","TYPE_OF_STOP","CITATION_CONTROL_NUMBER","ACTIVITY RESULTS","OFFICER_GENDER","OFFICER_RACE","OFFICER_AGE_RANGE","ACTIVITY_DATE","ACTIVITY_TIME",
        //"ACTIVITY_LOCATION","ACTIVITY_DIVISION","ACTIVITY_BEAT","DRIVER_GENDER","DRIVER_RACE","DRIVER_AGE_RANGE","NUMBER OF PASSENGERS",
        //"WAS_VEHCILE_SEARCHED","REASON_FOR_SEARCH"
        public int Id { get; set; }
        public string TypeOfStop { get; set; }
        public string CitationControlNumber { get; set; }
        public string ActivityResults { get; set; }
        public string OfficerGender { get; set; }
        public string OfficerRace { get; set; }
        public string OfficerAgeRange { get; set; }
        public DateTime? ActivityDate { get; set; }
        public TimeSpan? ActivityTime { get; set; }
        public string ActivityLocation { get; set; }
        public string ActivityDivision { get; set; }
        public string ActivityBeat { get; set; }
        public string DriverGender { get; set; }
        public string DriverRace { get; set; }
        public string DriverAgeRange { get; set; }
        public int NumberOfPassengers { get; set; }
        public bool? WasVehicleSearched { get; set; }
        public string ReasonForSearch { get; set; }

    }
}

