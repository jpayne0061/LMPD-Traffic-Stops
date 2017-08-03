using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TrafficStops.Models;
using TrafficStops.ViewModels;


namespace TrafficStops.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {

            _context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LookUp() {

            var lookUpModel = new LookUpViewModel();
            lookUpModel.Races = new DropDownListValues();
            lookUpModel.Genders = new DropDownListValues();
            lookUpModel.OfficerAges = new DropDownListValues();
            lookUpModel.DriverAges = new DropDownListValues();

            lookUpModel.Races.Values = new List<string>() { "WHITE", "BLACK", "HISPANIC", "ASIAN" };
            lookUpModel.Genders.Values = new List<string>() { "M", "F" };
            lookUpModel.OfficerAges.Values = new List<string>() { "21 - 30", "31 - 40", "40 - 50", "50 - 60", "OVER 60" };
            lookUpModel.DriverAges.Values = new List<string>() { "16 - 19", "20 - 25", "26 - 30", "31 - 40", "41 - 50", "51 - 60", "OVER 60" };

            return View(lookUpModel);
        }


        //public ActionResult Download(StopVars query) {

        //    query.SetToEmptyString();

        //    var allStops = _context.StopEntries.Where(s => s.DriverAgeRange != null)
        //                                        .Where(s => s.DriverGender != null)
        //                                        .Where(s => s.DriverRace != null)
        //                                        .Where(s => s.OfficerGender != null)
        //                                        .Where(s => s.OfficerRace != null)
        //                                        .Where(s => s.OfficerAgeRange != null)
        //                                        .Where(s => s.ActivityResults != null)
        //                                        .Where(s => s.DriverAgeRange != "")
        //                                        .Where(s => s.DriverGender != "")
        //                                        .Where(s => s.DriverRace != "")
        //                                        .Where(s => s.OfficerGender != "")
        //                                        .Where(s => s.OfficerRace != "")
        //                                        .Where(s => s.OfficerAgeRange != "")
        //                                        .Where(s => s.ActivityResults != "")
        //                                        .ToList();

        //    var queryCountWandC = allStops.Where(s => s.DriverGender.Contains(query.GenderDriver))
        //                            .Where(s => s.OfficerGender.Contains(query.GenderOfficer))
        //                            .Where(s => s.DriverRace.Contains(query.RaceDriver))
        //                            .Where(s => s.OfficerRace.Contains(query.RaceOfficer))
        //                            .Where(s => s.DriverAgeRange.Contains(query.AgeDriver))
        //                            .Where(s => s.OfficerAgeRange.Contains(query.AgeOfficer))
        //                            .Where(s => s.ActivityResults.Contains(""))
        //                            .ToList();



        //    //var shortList = allStops.Take(20);

        //    string csvString = CsvSerializer.SerializeToCsv<StopEntry>(queryCountWandC);


        //    //string output = new JavaScriptSerializer().Serialize(shortList);

        //    byte[] csvBytes = System.Text.Encoding.Unicode.GetBytes(csvString);

        //    return File(csvBytes, "text/csv", "foo.csv");
        //}

        public ActionResult Download(StopVars query)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["firstDBString"].ToString();
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.LMPD_STOPS WHERE OfficerRace LIKE @officerRace AND OfficerAgeRange LIKE @officerAgeRange AND OfficerGender LIKE @officerGender AND DriverRace LIKE @driverRace AND DriverGender LIKE @driverGender AND DriverAgeRange LIKE @driverAgeRange", conn);
                cmd.Parameters.Add(new SqlParameter("officerRace", "%" + query.RaceOfficer + "%"));
                cmd.Parameters.Add(new SqlParameter("officerAgeRange", "%" + query.AgeOfficer + "%"));
                cmd.Parameters.Add(new SqlParameter("officerGender", "%" + query.GenderOfficer + "%"));
                cmd.Parameters.Add(new SqlParameter("driverRace", "%" + query.RaceDriver + "%"));
                cmd.Parameters.Add(new SqlParameter("driverAgeRange", "%" + query.AgeDriver + "%"));
                cmd.Parameters.Add(new SqlParameter("driverGender", "%" + query.GenderDriver + "%"));

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        StringBuilder sb = new StringBuilder();

                        IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                          Select(column => column.ColumnName);
                        sb.AppendLine(string.Join(",", columnNames));

                        foreach (DataRow row in dt.Rows)
                        {
                            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                            sb.AppendLine(string.Join(",", fields));
                        }

                        byte[] csvBytes = System.Text.Encoding.Unicode.GetBytes(sb.ToString());

                        return File(csvBytes, "text/csv", "foo.csv");

                    }

                }

            }


        }
    }
}