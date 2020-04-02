using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SurrealCB.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SurrealCB.Data
{
    public interface ISeeder
    {
        Task SeedAsync();
    }

    public class Seeder : ISeeder
    {
        private readonly SCBDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger _logger;

        private Card[] cards;
        private Map[] maps;

        public Seeder(
            SCBDbContext context,
            ILogger<Seeder> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            this.cards = this.FillCards();
            this.maps = this.FillMaps();
        }

        public virtual async Task SeedAsync()
        {
            //Apply EF Core migration scripts
            await MigrateAsync();

            //Seed users and roles
            await SeedASPIdentityCoreAsync();

            //Seed clients and Api
            await SeedIdentityServerAsync();

            await SeedGameData();
        }

        private async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            await Task.CompletedTask;
        }

        private async Task SeedASPIdentityCoreAsync()
        {
            //if (!await _context.Users.AnyAsync())
            //{
            //    //Generating inbuilt accounts
            //    const string adminRoleName = "Administrator";
            //    const string userRoleName = "User";

            //    await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
            //    await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

            //    await CreateUserAsync("admin", "admin123", "Admin", "Blazor", "Administrator", "admin@blazoreboilerplate.com", "+1 (123) 456-7890", new string[] { adminRoleName });
            //    await CreateUserAsync("user", "user123", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });

            //    _logger.LogInformation("Inbuilt account generation completed");
            //}
            //else
            //{
            //    const string adminRoleName = "Administrator";

            //    IdentityRole<Guid> adminRole = await _roleManager.FindByNameAsync(adminRoleName);
            //    var AllClaims = ApplicationPermissions.GetAllPermissionValues().Distinct();
            //    var RoleClaims = (await _roleManager.GetClaimsAsync(adminRole)).Select(c => c.Value).ToList();
            //    var NewClaims = AllClaims.Except(RoleClaims);
            //    foreach (string claim in NewClaims)
            //    {
            //        await _roleManager.AddClaimAsync(adminRole, new Claim(ClaimConstants.Permission, claim));
            //    }
            //    var DeprecatedClaims = RoleClaims.Except(AllClaims);
            //    var roles = await _roleManager.Roles.ToListAsync();
            //    foreach (string claim in DeprecatedClaims)
            //    {
            //        foreach (var role in roles)
            //        {
            //            await _roleManager.RemoveClaimAsync(role, new Claim(ClaimConstants.Permission, claim));
            //        }
            //    }
            //}
            await Task.CompletedTask;
        }

        private async Task SeedGameData()
        {
            //ApplicationUser user = await _userManager.FindByNameAsync("user");

            //if (!_context.UserProfiles.Any())
            //{
            //    UserProfile userProfile = new UserProfile
            //    {
            //        UserId = user.Id,
            //        ApplicationUser = user,
            //        Count = 2,
            //        IsNavOpen = true,
            //        LastPageVisited = "/dashboard",
            //        IsNavMinified = false,
            //        LastUpdatedDate = DateTime.Now
            //    };
            //    _context.UserProfiles.Add(userProfile);
            //}

            if (!_context.Cards.Any())
            {
                _context.Cards.AddRange(
                   this.cards
                );
            }

            if (!_context.Maps.Any())
            {
                _context.Maps.AddRange(
                   this.maps
                );
            }

            //if (!_context.ApiLogs.Any())
            //{
            //    _context.ApiLogs.AddRange(
            //    new ApiLogItem
            //    {
            //        RequestTime = DateTime.Now,
            //        ResponseMillis = 30,
            //        StatusCode = 200,
            //        Method = "Get",
            //        Path = "/api/seed",
            //        QueryString = "",
            //        RequestBody = "",
            //        ResponseBody = "",
            //        IPAddress = "::1",
            //        ApplicationUserId = user.Id
            //    },
            //    new ApiLogItem
            //    {
            //        RequestTime = DateTime.Now,
            //        ResponseMillis = 30,
            //        StatusCode = 200,
            //        Method = "Get",
            //        Path = "/api/seed",
            //        QueryString = "",
            //        RequestBody = "",
            //        ResponseBody = "",
            //        IPAddress = "::1",
            //        ApplicationUserId = user.Id
            //    }
            //);
            //}

            _context.SaveChanges();
            await Task.CompletedTask;
        }

        private async Task SeedIdentityServerAsync()
        {
            //if (!await _configurationContext.Clients.AnyAsync())
            //{
            //    _logger.LogInformation("Seeding IdentityServer Clients");
            //    foreach (var client in IdentityServerConfig.GetClients())
            //    {
            //        _configurationContext.Clients.Add(client.ToEntity());
            //    }
            //    _configurationContext.SaveChanges();
            //}
            //if (!await _configurationContext.IdentityResources.AnyAsync())
            //{
            //    _logger.LogInformation("Seeding IdentityServer Identity Resources");
            //    foreach (var resource in IdentityServerConfig.GetIdentityResources())
            //    {
            //        _configurationContext.IdentityResources.Add(resource.ToEntity());
            //    }
            //    _configurationContext.SaveChanges();
            //}
            //if (!await _configurationContext.ApiResources.AnyAsync())
            //{
            //    _logger.LogInformation("Seeding IdentityServer API Resources");
            //    foreach (var resource in IdentityServerConfig.GetApiResources())
            //    {
            //        _configurationContext.ApiResources.Add(resource.ToEntity());
            //    }
            //    _configurationContext.SaveChanges();
            //}
            await Task.CompletedTask;
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            //if ((await _roleManager.FindByNameAsync(roleName)) == null)
            //{
            //    if (claims == null)
            //        claims = new string[] { };

            //    string[] invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null).ToArray();
            //    if (invalidClaims.Any())
            //        throw new Exception("The following claim types are invalid: " + string.Join(", ", invalidClaims));

            //    IdentityRole<Guid> applicationRole = new IdentityRole<Guid>(roleName);

            //    var result = await _roleManager.CreateAsync(applicationRole);

            //    IdentityRole<Guid> role = await _roleManager.FindByNameAsync(applicationRole.Name);

            //    foreach (string claim in claims.Distinct())
            //    {
            //        result = await _roleManager.AddClaimAsync(role, new Claim(ClaimConstants.Permission, ApplicationPermissions.GetPermissionByValue(claim)));

            //        if (!result.Succeeded)
            //        {
            //            await _roleManager.DeleteAsync(role);
            //        }
            //    }
            //}
            await Task.CompletedTask;
        }

        private async Task<ApplicationUser> CreateUserAsync(string userName, string password, string firstName, string fullName, string lastName, string email, string phoneNumber, string[] roles)
        {
            //var applicationUser = _userManager.FindByNameAsync(userName).Result;

            //if (applicationUser == null)
            //{
            //    applicationUser = new ApplicationUser
            //    {
            //        UserName = userName,
            //        Email = email,
            //        PhoneNumber = phoneNumber,
            //        FullName = fullName,
            //        FirstName = firstName,
            //        LastName = lastName,
            //        EmailConfirmed = true
            //    };

            //    var result = _userManager.CreateAsync(applicationUser, password).Result;
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }

            //    result = _userManager.AddClaimsAsync(applicationUser, new Claim[]{
            //            new Claim(JwtClaimTypes.Name, userName),
            //            new Claim(JwtClaimTypes.GivenName, firstName),
            //            new Claim(JwtClaimTypes.FamilyName, lastName),
            //            new Claim(JwtClaimTypes.Email, email),
            //            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            //            new Claim(JwtClaimTypes.PhoneNumber, phoneNumber)


            //        }).Result;

            //    //add claims version of roles
            //    foreach (var role in roles.Distinct())
            //    {
            //        await _userManager.AddClaimAsync(applicationUser, new Claim($"Is{role}", "true"));
            //    }

            //    ApplicationUser user = await _userManager.FindByNameAsync(applicationUser.UserName);

            //    try
            //    {
            //        result = await _userManager.AddToRolesAsync(user, roles.Distinct());
            //    }

            //    catch
            //    {
            //        await _userManager.DeleteAsync(user);
            //        throw;
            //    }

            //    if (!result.Succeeded)
            //    {
            //        await _userManager.DeleteAsync(user);
            //    }
            //}
            //return applicationUser;
            await Task.CompletedTask;
            return null;
        }

        private Card[] FillCards()
        {
            Card[] cards =
            {
                new Card
                {
                    Id = 1, Name = "Goblin", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH, Passive = null,
                    Hp = 5, Atk = 2, Def = 0, Imm = 0, Spd = 3.6, Value = 20, BaseExp = 20, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/goblin.png",
                    LevelBoosts = null
                },
                new Card
                {
                    Id = 2, Name = "Horse", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH, Passive = null,
                    Hp = 4, Atk = 2, Def = 0, Imm = 0, Spd = 2.8, Value = 20, BaseExp = 20, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/horse.png",
                    LevelBoosts = null
                },
                new Card
                {
                    Id = 3, Name = "Fire Wraith", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Id = 1, Passive = Passive.BLAZE, Param1 = 1, Param2 = 8},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3.0, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_wraith.png",
                    LevelBoosts = null
                },
                new Card
                {
                    Id = 4, Name = "Grunt", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NONE, Passive = null,
                    Hp = 6, Atk = 1, Def = 1, Imm = 30, Spd = 4.1, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/grunt.png",
                    LevelBoosts = null
                },
                new Card
                {
                    Id = 5, Name = "Dark Eye Fighter", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.FIRE,
                    Passive = new CardPassive { Id = 2, Passive = Passive.HP_SHATTER, Param1 = 10},
                    Hp = 7, Atk = 1, Def = 0, Imm = 0, Spd = 1.3, Value = 50, BaseExp = 45, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dark_eye_fighter.png",
                    LevelBoosts = null
                },
                new Card
                {
                    Id = 6, Name = "Mosquito", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WIND, Passive = null,
                    Hp = 3, Atk = 2, Def = 0, Imm = 25, Spd = 1.5, Value = 20, BaseExp = 20, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/mosquito.png",
                    LevelBoosts = null
                },
                new Card
                {
                    Id = 7, Name = "Poison Mosquito", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Id = 3, Passive = Passive.POISON, Param1 = 1, Param2 = 2, Param3 = 4},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 2.1, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/poison_mosquito.png",
                    LevelBoosts = null
                },
            };
            return cards;
        }

        private Map[] FillMaps()
        {
            Map[] maps =
            {
                new Map
                {
                    Name = "Mountain", MinLevel = 1, Difficult = MapDifficult.EASY, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 10 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(7), this.GetPlayerCard(7)
                            }
                        }
                    }
                }
            };
            return maps;
        }

        private PlayerCard GetPlayerCard(int id, List<LevelBoost> levelBoosts = null, List<Rune> runes = null)
        {
            return new PlayerCard
            {
                //Card = this._context.Cards.Where(x => x.Id == id).FirstOrDefault(),
                CardId = id,
                ActiveLvlBoosts = levelBoosts,
                Runes = runes
            };
        }
    }
}

