using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using Microsoft.Extensions.Logging;
using SurrealCB.Data;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;
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
        private readonly IUserService userService;
        private readonly ICardService cardService;
        private readonly IRepository repository;

        public CardController(ILogger<CardController> logger, IUserService userService,
            ICardService cardService, IRepository repository )
        {
            this.logger = logger;
            this.userService = userService;
            this.cardService = cardService;
            this.repository = repository;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            var cards = await this.cardService.GetAll();
            return new ApiResponse(Status200OK, "Get All Cards Successful", cards);
        }

        [HttpGet("user")]
        public async Task<ApiResponse> GetUserCards()
        {
            var userCards = await this.userService.GetUserCards();
            return new ApiResponse(Status200OK, "Get All Cards Successful", userCards);
        }

        [HttpGet("player/{id}")]
        public async Task<ApiResponse> GetPlayerCard(int id)
        {
            var playerCard = await this.repository.Query<PlayerCard>().FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse(Status200OK, "Get All Cards Successful", playerCard);
        }

        [HttpGet("player/levelboost/{cardid}/{boostid}")]
        public async Task<ApiResponse> ActivateLevelBoost(int cardid, int boostid)
        {
            var playerCard = await this.repository.Query<PlayerCard>().FirstOrDefaultAsync(x => x.Id == cardid);
            var levelBoost = await this.repository.Query<LevelBoost>().FirstOrDefaultAsync(x => x.Id == boostid);
            try
            {
                await this.cardService.ActivateLevelBoost(playerCard, levelBoost);
            }
            catch (ApiException ex)
            {
                return new ApiResponse(ex.StatusCode, ex.Message);
            }
            return new ApiResponse(Status200OK, "Level Boost Applied", playerCard);
        }
    }
}
