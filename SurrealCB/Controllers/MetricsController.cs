using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using Microsoft.Extensions.Logging;
using SurrealCB.Data;
using SurrealCB.Data.Dto.Account;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;
using SurrealCB.Server.Misc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SurrealCB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        //private static readonly UserInfoDto LoggedOutUser = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };

        private readonly ILogger<CardController> logger;
        private readonly IUserService userService;
        private readonly ICardService cardService;
        private readonly IRepository repository;
        private readonly IMetricService metricService;

        public MetricsController(ILogger<CardController> logger, IUserService userService,
            ICardService cardService, IRepository repository, IMetricService metricService )
        {
            this.logger = logger;
            this.userService = userService;
            this.cardService = cardService;
            this.repository = repository;
            this.metricService = metricService;
        }

        [HttpGet("level")]
        public async Task<ApiResponse> DoMetrics()
        {
            for (var i = 0; i < 4000; i++)
            {
                var random = new Random();
                var level = random.Next(1, 9);
                await this.metricService.MakeLevelMetricBalanced(level);
            }
            for (var i = 0; i < 8000; i++)
            {
                var random = new Random();
                var level = random.Next(1, 9);
                await this.metricService.MakeLevelMetricRarity(level, Data.Enum.Rarity.COMMON);
            }
            for (var i = 0; i < 4000; i++)
            {
                var random = new Random();
                var level = random.Next(1, 9);
                await this.metricService.MakeLevelMetricRarity(level, Data.Enum.Rarity.RARE);
            }
            for (var i = 0; i < 2000; i++)
            {
                var random = new Random();
                var level = random.Next(1, 9);
                await this.metricService.MakeLevelMetricRarity(level, Data.Enum.Rarity.SPECIAL);
            }
            for (var i = 0; i < 500; i++)
            {
                var random = new Random();
                var level = random.Next(1, 9);
                await this.metricService.MakeLevelMetricRarity(level, Data.Enum.Rarity.LEGENDARY);
            }
            await this.repository.GetSession().FlushAsync();
            return new ApiResponse(Status200OK, "Finished");
        }
    }
}
