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
    public class BattleController : ControllerBase
    {
        //private static readonly UserInfoDto LoggedOutUser = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };

        private readonly ILogger<CardController> logger;
        private readonly ICardService cardService;

        public BattleController(ILogger<CardController> logger,
            ICardService cardService)
        {
            this.logger = logger;
            this.cardService = cardService;
        }

        

        [HttpPost]
        public async Task<ApiResponse> Perform(List<BattleCard> cards, int srcPos, int tarPos)
        {
            var srcCard = await cards.Where(x => x.Position == srcPos).FirstOrDefaultAsync();
            var tarCard = await cards.Where(x => x.Position == tarPos).FirstOrDefaultAsync();
           
            if (srcCard == null ||
                srcCard.AtkType != AtkType.ALL && srcCard.AtkType != AtkType.RANDOM && tarCard == null)
            var cards = await this.cardService.GetAll();
            return new ApiResponse(Status200OK, "Get All Cards Successful", cards);
        }
    }
}
