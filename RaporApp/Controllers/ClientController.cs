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

        public ClientController(IHttpService httpService, ILogger<ClientController> logger, IMessageService messageService)
        {
            HttpService = httpService;
            Logger = logger;
            MessageService = messageService;
        }
        private readonly IHttpService HttpService;
        private readonly ILogger<ClientController> Logger;
        private readonly IMessageService MessageService;


        [HttpGet]
        [Route("rapor-request")]
        public async Task<IActionResult> GetRaporRequest(Guid iletisimBilgiTipiId, string icerik)
        {
            MessageService.SendMessage(new RaporStatus { Status = "hazırlanıyor" });

            ServiceOutput<RaporRequestModel> output = await HttpService.GetRaporRequest(iletisimBilgiTipiId, icerik);
            if (output.Status)
                MessageService.SendMessage(new RaporStatus { Status = "tamamlandı" });

            return await ActionOutput<RaporRequestModel>.GenerateAsync(200, true, data: output.Data);
        }
    }
}
