using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserService userService;
        private readonly ICardService cardService;
        private readonly SCBDbContext repository;

        public CardController(ILogger<CardController> logger, IUserService userService,
            ICardService cardService, SCBDbContext repository )
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
            var playerCard = await this.repository.PlayerCards.FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse(Status200OK, "Get All Cards Successful", playerCard);
        }
    }
}
