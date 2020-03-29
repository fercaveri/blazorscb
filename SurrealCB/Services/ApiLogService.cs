﻿using AutoMapper;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using SurrealCB.Server.Misc;
using SurrealCB.Data;
using Microsoft.AspNetCore.Http;
using SurrealCB.Data.Dto;
using SurrealCB.Data.Model;
using SurrealCB.Data.Shared;

namespace SurrealCB.Server.Services
{
    public interface IApiLogService
    {
        Task Log(ApiLogItem apiLogItem);
        Task<ApiResponse> Get();
        Task<ApiResponse> GetByApplictionUserId(Guid applicationUserId);
    }

    public class ApiLogService : IApiLogService
    {
        private readonly SCBDbContext _db;
        private readonly DbContextOptionsBuilder<SCBDbContext> _optionsBuilder;
        private readonly IMapper _autoMapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSession _userSession;

        public ApiLogService(IConfiguration configuration, SCBDbContext db, IMapper autoMapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, 
            IUserSession userSession)
        {
            _db = db;
            _autoMapper = autoMapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userSession = userSession;

            // Calling Log from the API Middlware results in a disposed ApplicationDBContext. This is here to build a DB Context for logging API Calls
            // If you have a better solution please let me know.
            _optionsBuilder = new DbContextOptionsBuilder<SCBDbContext>();

            //if (Convert.ToBoolean(configuration["BlazorBoilerplate:UsePostgresServer"] ?? "false"))
            //{
            //    _optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            //}
            //else if (Convert.ToBoolean(configuration["BlazorBoilerplate:UseSqlServer"] ?? "false"))
            //{
                _optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); //SQL Server Database
            //}
            //else
            //{
            //    _optionsBuilder.UseSqlite($"Filename={configuration.GetConnectionString("SqlLiteConnectionFileName")}");  // Sql Lite / file database
            //}
        }

        public async Task Log(ApiLogItem apiLogItem)
        {
            if (apiLogItem.ApplicationUserId != Guid.Empty)
            {
                //TODO populate _userSession??

                //var currentUser = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                //UserSession userSession = new UserSession();
                //if (currentUser != null)
                //{
                //    userSession = new UserSession(currentUser.Result);
                //}
            }
            else
            {
                apiLogItem.ApplicationUserId = null;
            }

            using (SCBDbContext _dbContext = new SCBDbContext(_optionsBuilder.Options, _userSession))
            {
                _dbContext.ApiLogs.Add(apiLogItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ApiResponse> Get()
        {
            return new ApiResponse(Status200OK, "Retrieved Api Log", await _autoMapper.ProjectTo<ApiLogItemDto>(_db.ApiLogs).ToListAsync());
        }

        public async Task<ApiResponse> GetByApplictionUserId(Guid applicationUserId)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved Api Log", await _autoMapper.ProjectTo<ApiLogItemDto>(_db.ApiLogs.Where(a => a.ApplicationUserId == applicationUserId)).ToListAsync());
            }
            catch (Exception ex)
            {
                return new ApiResponse(Status400BadRequest, ex.Message);
            }
        }
    }
}
