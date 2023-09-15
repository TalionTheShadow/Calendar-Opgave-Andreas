using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Calendar_Opgave_Andreas;
using Calendar_Opgave_Andreas.Shared;
using Newtonsoft.Json.Linq;

namespace Calendar_Opgave_Andreas.Pages
{
    public partial class Index
    {
        static public string apiKey = "94357e89-9b96-496e-a130-b5e0b83dea08";
        static public string country = "DK";
        static int year = 2022;
        static string BaseApiUrl = $"https://holidayapi.com/v1/holidays?key={apiKey}&country={country}&year={year}";

      
        
        public class Dag
        {
            public DateTime? Dato { get; set; }
            public string? Navn { get; set; }
        }

        public List<Dag>? helligdag { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }

        async Task<List<Dag>> GetHelligdag()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage svar = await client.GetAsync(BaseApiUrl);
                if (svar.IsSuccessStatusCode)
                {
                    string json = await svar.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(json);
                    JArray? holidaysArray = jsonObject["holidays"] as JArray;
                    if (holidaysArray != null)
                    {
                        var holidayList = new List<Dag>();
                        foreach (JObject holidayObject in holidaysArray)
                        {
                            string? navn = holidayObject["navn"]?.ToString();
                            DateTime? dato = holidayObject["dato"]?.ToObject<DateTime?>();
                            Dag holiday = new Dag()
                            {
                                Navn = navn,
                                Dato = dato
                            };
                            holidayList.Add(holiday);
                        }
                        return holidayList;
                    }
                    else
                    {

                    }

                }
            }
                    return new List<Dag>();

        }
    }
}