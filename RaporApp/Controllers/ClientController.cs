using Core.Abstract;
using Core.Helpers;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RaporApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        public ClientController(IHttpService httpService)
        {
            HttpService = httpService;
        }
        private readonly IHttpService HttpService;


        [HttpGet]
        [Route("rapor-request")]
        public async Task<IActionResult> GetRaporRequest(Guid iletisimBilgiTipiId, string icerik)
        {
            ServiceOutput<RaporRequestModel> output = await HttpService.GetRaporRequest( iletisimBilgiTipiId,  icerik);

            return await ActionOutput<RaporRequestModel>.GenerateAsync(200, true,  data: output.Data);
        }
    }
}
