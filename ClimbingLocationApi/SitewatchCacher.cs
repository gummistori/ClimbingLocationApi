using ClimbingLocationApi.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace ClimbingLocationApi
{
    public class SitewatchCacher : ISitewatchCacher
    {
        private DateTime lastUpdateTime = DateTime.MinValue;
        private readonly double maxDataAgeMinutes = 3.0;
        private static HttpClient webClient = new HttpClient();
        private readonly string username = "";
        private readonly string password = "";
        private readonly Credentials credentials;
        private static DateTime lastTokenTime = DateTime.MinValue;
        private string token = "";
        private ResponseModel currentInfo;
        private readonly string loginUrl = "https://www.sitewatch.is/api/v1/Auth/Login";

        public SitewatchCacher()
        {
            lastTokenTime = DateTime.MinValue;
            this.currentInfo = new ResponseModel();
            this.credentials = new Credentials(username, password);
            System.Diagnostics.Debug.WriteLine("Created class");
        }

        public async Task<Mob> GetCurrentPos()
        {
            // add check if to old

            await this.UpdateData();
            if (this.currentInfo == null)
            {
                Console.WriteLine("currentInfo is null");
                return null;
            }

            if (this.currentInfo.Vehicles.Count > 0)
            {
                return this.currentInfo.Vehicles[0];
            }
            System.Diagnostics.Debug.WriteLine("Returning nothing");
            return null;
        }

        private async Task<string> GetToken()
        {
            HttpClient client = new HttpClient();

            var cont = JsonSerializer.Serialize(credentials);
            var content = new StringContent(cont, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(loginUrl, content);
            string contents = await response.Content.ReadAsStringAsync();
            lastTokenTime = DateTime.Now;

            return contents;
        }

        private async Task<ResponseModel> UpdateData()
        {

            if ((DateTime.Now - lastTokenTime).TotalMinutes >= 60.0)
            {
                System.Diagnostics.Debug.WriteLine("Renewing token, token is 60 minutes old or older. " + lastTokenTime.ToString());
                System.Diagnostics.Debug.WriteLine((DateTime.Now - lastTokenTime).TotalMinutes);
                this.token = await GetToken();
                System.Diagnostics.Debug.WriteLine(token);
            }
            
            // check if time to update
            if ((DateTime.Now - this.lastUpdateTime).TotalMinutes >= this.maxDataAgeMinutes)
            {
                System.Diagnostics.Debug.WriteLine("Fetching new data, mad data age exceeded, " + lastUpdateTime, this.lastUpdateTime);
                try
                {
                    RequestModel requestModel = new RequestModel();
                    webClient.DefaultRequestHeaders.Clear();
                    webClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.token);
                    try
                    {
                        var res = await webClient.PostAsync("https://www.sitewatch.is/api/v1/Vehicle/ShowVehicles", JsonContent.Create(requestModel));
                        var cont =  await res.Content.ReadAsStringAsync();
                        JsonSerializerOptions settings = new JsonSerializerOptions();
                        settings.PropertyNameCaseInsensitive = true;
                        // settings.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Always;
                        this.currentInfo =  JsonSerializer.Deserialize<ResponseModel>(cont, settings);

                        this.lastUpdateTime = DateTime.Now;
                        Console.WriteLine(this.lastUpdateTime.ToLongTimeString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                } catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    return new ResponseModel();
                }
            
            } // end if time to update

            System.Diagnostics.Debug.WriteLine("Returning without fetching.. " + this.lastUpdateTime.ToLongTimeString());
            return this.currentInfo;
        }
    }
}
