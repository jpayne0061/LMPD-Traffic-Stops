using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrafficStops.Models;
using System.Web.Script.Serialization;
using ServiceStack.Text;

namespace TrafficStops.Controllers
{
    public class JsonTableApiController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public JsonTableApiController()
        {

            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public string GetTableData([FromUri]StopVars query)
        {
            query.SetToEmptyString();

            var allStops = _context.StopEntries.Where(s => s.DriverAgeRange != null)
                                                .Where(s => s.DriverGender != null)
                                                .Where(s => s.DriverRace != null)
                                                .Where(s => s.OfficerGender != null)
                                                .Where(s => s.OfficerRace != null)
                                                .Where(s => s.OfficerAgeRange != null)
                                                .Where(s => s.ActivityResults != null)
                                                .Where(s => s.DriverAgeRange != "")
                                                .Where(s => s.DriverGender != "")
                                                .Where(s => s.DriverRace != "")
                                                .Where(s => s.OfficerGender != "")
                                                .Where(s => s.OfficerRace != "")
                                                .Where(s => s.OfficerAgeRange != "")
                                                .Where(s => s.ActivityResults != "")
                                                .ToList();

            var shortList = allStops.Take(20);

            //string csvString = CsvSerializer.SerializeToCsv<dynamic>(shortList);


            string output = new JavaScriptSerializer().Serialize(shortList);

            //byte[] csvBytes = System.Text.Encoding.Unicode.GetBytes(csvString);

            //return File(csvBytes, "text/csv", "foo.csv");


            return output;
        }



    }
}
