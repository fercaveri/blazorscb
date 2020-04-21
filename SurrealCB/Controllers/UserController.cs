using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SurrealCB.Data;
using SurrealCB.Data.Dto.Account;
using SurrealCB.Data.Model;
using SurrealCB.Server.Misc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SurrealCB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private static readonly UserInfoDto LoggedOutUser = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };

        private readonly ILogger<CardController> logger;
        private readonly IUserService userService;
        private readonly ICardService cardService;
        private readonly SCBDbContext repository;

        public UserController(ILogger<CardController> logger, IUserService userService,
            ICardService cardService, SCBDbContext repository )
        {
            this.logger = logger;
            this.userService = userService;
            this.cardService = cardService;
            this.repository = repository;
        }

        [HttpGet("get")]
        public async Task<ApiResponse> GetUserProfile()
        {
            var user = await this.userService.GetUser(await (this.userService.GetUserId()));
            var profileQuery = from userProf in this.repository.UserProfiles
                               where userProf.UserId == user.Id
                               select userProf;

            UserProfileDto userProfile = new UserProfileDto();
            if (!await profileQuery.AnyAsync())
            {
                userProfile = new UserProfileDto
                {
                    UserId = user.Id
                };
            }
            else
            {
                UserProfile profile = await profileQuery.FirstAsync();
                userProfile.Gold = user.Gold;
                userProfile.Exp = user.Exp;
                userProfile.IsNavOpen = profile.IsNavOpen;
                userProfile.LastPageVisited = profile.LastPageVisited;
                userProfile.IsNavMinified = profile.IsNavMinified;
                userProfile.UserId = user.Id;
            }
            return new ApiResponse(Status200OK, "Get All Cards Successful", userProfile);
        }

        [HttpGet("user")]
        public async Task<ApiResponse> GetUserCards()
        {
            var userCards = await this.userService.GetUserCards();
            return new ApiResponse(Status200OK, "Get All Cards Successful", userCards);
        }

        [HttpGet("user")]
        public async Task<ApiResponse> GetUserRunes()
        {
            var runes = await this.userService.GetUserRunes();
            return new ApiResponse(Status200OK, "Get Runes Successful", runes);
        }
    }
}
