﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurrealCB.Data;
using SurrealCB.Data.Model;
using SurrealCB.Server.Misc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SurrealCB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        //private static readonly UserInfoDto LoggedOutUser = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };

        private readonly ILogger<CardController> logger;
        private readonly IBattleService battleService;
        private readonly ICardService cardService;
        private readonly IUserService userService;
        private readonly SCBDbContext repository;

        public BattleController(ILogger<CardController> logger, IBattleService battleService,
            ICardService cardService, IUserService userService, SCBDbContext repository)
        {
            this.logger = logger;
            this.battleService = battleService;
            this.cardService = cardService;
            this.userService = userService;
            this.repository = repository;
        }

        [HttpGet("start/{enemyId}")]
        public async Task<ApiResponse> StartBattle(int enemyId)
        {
            var enemy = await this.repository.Enemies.FirstOrDefaultAsync(x => x.Id == enemyId);
            var battleCards = new List<BattleCard>();
            var random = new Random();
            for (var i = 0; i < 4; i++)
            {
                var pcard = enemy.Cards[random.Next(enemy.Cards.Count - 1)];
                battleCards.Add(new BattleCard(pcard)
                {
                    Position = i + 4
                });
            }
            var userCards = await this.userService.GetUserCards();
            for (var i = 0; i < 4; i++)
            {
                var pcard = userCards[random.Next(userCards.Count - 1)];
                battleCards.Add(new BattleCard(pcard)
                {
                    Position = i
                });
            }

            return new ApiResponse(Status200OK, "Cards updated successfully", battleCards);
        }

        [HttpPost("next")]
        public async Task<ApiResponse> NextTurn([FromBody]List<BattleCard> cards)
        {
            var nextPosition = await this.battleService.NextTurn(cards);

            return new ApiResponse(Status200OK, "Cards updated successfully", new BattleStatus { Cards = cards, NextPosition = nextPosition});
        }

        [HttpPost("perform")]
        public async Task<ApiResponse> Perform([FromBody]List<BattleCard> cards, int srcPos, int tarPos)
        {
            var srcCard = cards.Where(x => x.Position == srcPos).FirstOrDefault();
            var tarCard = cards.Where(x => x.Position == tarPos).FirstOrDefault();

            if (srcCard == null ||
                srcCard.PlayerCard.Card.AtkType != AtkType.ALL && srcCard.PlayerCard.Card.AtkType != AtkType.RANDOM && tarCard == null)
            {
                return new ApiResponse(Status400BadRequest, "Wrong request");
            }
            await this.battleService.PerformAttack(srcCard, tarCard);

            return new ApiResponse(Status200OK, "Cards updated successfully", new BattleStatus { Cards = cards});
        }
    }
}
