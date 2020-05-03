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
            for (var i = 0; i < 1000; i++)
            {
                var random = new Random();
                var level = random.Next(1, 9);
                await this.metricService.MakeLevelMetric(level);
            }
            //await this.repository.SaveChangesAsync();
            return new ApiResponse(Status200OK, "Finished");
        }
    }
}
