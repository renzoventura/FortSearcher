using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using SimpleJson;

namespace FortSearcher.Pages
{
    public class profileModel : PageModel
    {
        public string Username { get; private set; }



        public dynamic Total_wins { get; private set; }

        public dynamic Total_kills { get; private set; }

        public dynamic Total_matches { get; private set; }

        public dynamic Kill_death_ratio { get; private set; }

        public void OnGet(string user_input)
        {

            dynamic user_stats = callAPI(user_input);
            //total_matches_object = json[7];
            //total_matches_object = total_matches_object["value"];
            ////List<string> lifetimeresult = JSONObj["lifeTimeStats"].Split(new char[] { ',' }).ToList();
            foreach (dynamic x in user_stats)
            {
                if (x["key"] == "Matches Played")
                {
                    Total_matches = x;
                }
                if (x["key"] == "Wins")
                {
                    Total_wins = x;
                }
                if (x["key"] == "Kills")
                {
                    Total_kills = x;
                }
                if (x["key"] == "K/d")
                {
                    Kill_death_ratio = x;
                }


            }


        }


        public dynamic callAPI(string userinput)
        {
            var client = new RestClient("https://api.fortnitetracker.com/v1/profile/pc/" + userinput);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "5f230875-c646-4f22-979d-a84ccbc17b6f");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic Og==");
            request.AddHeader("TRN-Api-Key", "0ba3f7e3-a655-4ece-97ce-19ccd40b4147");
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);


            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);
            Username = JSONObj["epicUserHandle"];

            string lifetimestats_string = JSONObj["lifeTimeStats"];
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(lifetimestats_string);

            return json;
        }
    }
}