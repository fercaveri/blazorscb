using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurrealCB.Data;
using SurrealCB.Server.Misc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SurrealCB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        //private static readonly UserInfoDto LoggedOutUser = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };

        private readonly ILogger<CardController> logger;
        private readonly ICardService cardService;

        public CardController(ILogger<CardController> logger,
            ICardService cardService)
        {
            this.logger = logger;
            this.cardService = cardService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            var cards = await this.cardService.GetAll();
            return new ApiResponse(Status200OK, "Get All Cards Successful", cards);
        }
    }
}
