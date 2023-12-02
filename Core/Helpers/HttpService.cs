using Core.Abstract;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class HttpService: IHttpService
    {
        private readonly string baseURL = "https://localhost:7018/api/";

        public async Task<ServiceOutput<RaporRequestModel>> GetRaporRequest(Guid iletisimBilgiTipiId, string icerik)
        {
            string apiUrl = baseURL + $"Rapor/get-request?iletisimBilgiTipiId={iletisimBilgiTipiId}&icerik={icerik}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                   
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceOutput<RaporRequestModel>>(content);

                        if (result != null )
                        {
                            return result;
                        }
                        else
                        {
                            return new ServiceOutput<RaporRequestModel> { Data = new RaporRequestModel() };
                        }
                    }
                    else
                    {
                        var error = response.StatusCode;
                        return new ServiceOutput<RaporRequestModel> { Data = new RaporRequestModel() };
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    return new ServiceOutput<RaporRequestModel> { Data = new RaporRequestModel() };
                }
            }
        }
    }
}
