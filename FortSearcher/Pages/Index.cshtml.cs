using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required]
        public string User_input { get; set; }

        public string Error_message { get; set; }



        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                try
                {


                    var client = new RestClient("https://api.fortnitetracker.com/v1/profile/pc/" + User_input);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Postman-Token", "5f230875-c646-4f22-979d-a84ccbc17b6f");
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Authorization", "Basic Og==");
                    request.AddHeader("TRN-Api-Key", "0ba3f7e3-a655-4ece-97ce-19ccd40b4147");
                    request.RequestFormat = DataFormat.Json;
                    IRestResponse response = client.Execute(request);


                    RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                    var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);
                    var x = JSONObj["epicUserHandle"];

                    return RedirectToPage("profile", new { user_input = User_input });

                }
                catch (Exception)
                {
                    Error_message = "This user does not exist";

                    return Page();
                }

            }
            else
            {
                return Page();
            }
        }

    }
}
