using System;
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
    public class MapController : ControllerBase
    {
        //private static readonly UserInfoDto LoggedOutUser = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };

        private readonly ILogger<CardController> logger;
        private readonly IMapService mapService;
        private readonly ICardService cardService;

        public MapController(ILogger<CardController> logger, IMapService mapService,
            ICardService cardService)
        {
            this.logger = logger;
            this.mapService = mapService;
            this.cardService = cardService;
        }

        [HttpGet("all")]
        public async Task<ApiResponse> GetAll()
        {
            var maps = await this.mapService.GetAll();
            return new ApiResponse(Status200OK, "Get All Maps Successful", maps);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetAll(int id)
        {
            var map = await this.mapService.GetById(id);
            return new ApiResponse(Status200OK, "Get All Maps Successful", map);
        }
    }
}
