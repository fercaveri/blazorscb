using AutoMapper;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
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
using SurrealCB.Data.Repository;

namespace SurrealCB.Server.Services
{
    public interface IApiLogService
    {
        Task Log(ApiLogItem apiLogItem);
        Task<ApiResponse> Get();
        Task<ApiResponse> GetByApplictionUserId(int applicationUserId);
    }

    public class ApiLogService : IApiLogService
    {
        private readonly IRepository _db;
        //private readonly DbContextOptionsBuilder<SCBDbContext> _optionsBuilder;
        private readonly IMapper _autoMapper;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSession _userSession;

        public ApiLogService(IConfiguration configuration, IRepository db, IMapper autoMapper, IHttpContextAccessor httpContextAccessor, 
            IUserSession userSession)
        {
            _db = db;
            _autoMapper = autoMapper;
            //_userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userSession = userSession;

            // Calling Log from the API Middlware results in a disposed ApplicationDBContext. This is here to build a DB Context for logging API Calls
            // If you have a better solution please let me know.
            //_optionsBuilder = new DbContextOptionsBuilder<SCBDbContext>();

            //if (Convert.ToBoolean(configuration["BlazorBoilerplate:UsePostgresServer"] ?? "false"))
            //{
            //    _optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            //}
            //else if (Convert.ToBoolean(configuration["BlazorBoilerplate:UseSqlServer"] ?? "false"))
            //{
                //_optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); //SQL Server Database
            //}
            //else
            //{
            //    _optionsBuilder.UseSqlite($"Filename={configuration.GetConnectionString("SqlLiteConnectionFileName")}");  // Sql Lite / file database
            //}
        }

        public async Task Log(ApiLogItem apiLogItem)
        {
            if (apiLogItem.ApplicationUserId != 0)
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
                apiLogItem.ApplicationUserId = 0;
            }

            //using (SCBDbContext _dbContext = new SCBDbContext(_optionsBuilder.Options, _userSession))
            //{
                await _db.SaveAsync(apiLogItem);
            //}
        }

        public async Task<ApiResponse> Get()
        {
            return new ApiResponse(Status200OK, "Retrieved Api Log", await _autoMapper.ProjectTo<ApiLogItemDto>(_db.Query<ApiLogItem>()).ToListAsync());
        }

        public async Task<ApiResponse> GetByApplictionUserId(int applicationUserId)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved Api Log", await _autoMapper.ProjectTo<ApiLogItemDto>(_db.Query<ApiLogItem>().Where(a => a.ApplicationUserId == applicationUserId)).ToListAsync());
            }
            catch (Exception ex)
            {
                return new ApiResponse(Status400BadRequest, ex.Message);
            }
        }
    }
}
