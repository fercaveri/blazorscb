using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SurrealCB.Data.Model;
using SurrealCB.Data.Shared;
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
        private Item[] items;

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
            this.items = this.FillItems();
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
            if (!await _context.Users.AnyAsync())
            {
                //Generating inbuilt accounts
                const string adminRoleName = "Administrator";
                const string userRoleName = "User";

                await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
                await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

                await CreateUserAsync("admin", "ferk!veri1S", "Admin", "Blazor", "Administrator", "admin@blazoreboilerplate.com", "+1 (123) 456-7890", new string[] { adminRoleName });
                await CreateUserAsync("user", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }
            else
            {
                const string adminRoleName = "Administrator";

                IdentityRole<Guid> adminRole = await _roleManager.FindByNameAsync(adminRoleName);
                var AllClaims = ApplicationPermissions.GetAllPermissionValues().Distinct();
                var RoleClaims = (await _roleManager.GetClaimsAsync(adminRole)).Select(c => c.Value).ToList();
                var NewClaims = AllClaims.Except(RoleClaims);
                foreach (string claim in NewClaims)
                {
                    await _roleManager.AddClaimAsync(adminRole, new Claim(ClaimConstants.Permission, claim));
                }
                var DeprecatedClaims = RoleClaims.Except(AllClaims);
                var roles = await _roleManager.Roles.ToListAsync();
                foreach (string claim in DeprecatedClaims)
                {
                    foreach (var role in roles)
                    {
                        await _roleManager.RemoveClaimAsync(role, new Claim(ClaimConstants.Permission, claim));
                    }
                }
            }
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

            if (!_context.PlayerCards.Any())
            {
                var userId = (await _context.Users.FirstOrDefaultAsync()).Id;
                var pcards = new List<PlayerCard>();
                for (var i = 1; i < 16; i++)
                {
                    pcards.Add(new PlayerCard
                    {
                        Card = this.cards.FirstOrDefault(x => x.Id == i),
                        ApplicationUserId = userId
                    });
                }
                _context.PlayerCards.AddRange(
                   pcards
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
            if ((await _roleManager.FindByNameAsync(roleName)) == null)
            {
                if (claims == null)
                    claims = new string[] { };

                string[] invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null).ToArray();
                if (invalidClaims.Any())
                    throw new Exception("The following claim types are invalid: " + string.Join(", ", invalidClaims));

                IdentityRole<Guid> applicationRole = new IdentityRole<Guid>(roleName);

                var result = await _roleManager.CreateAsync(applicationRole);

                IdentityRole<Guid> role = await _roleManager.FindByNameAsync(applicationRole.Name);

                foreach (string claim in claims.Distinct())
                {
                    result = await _roleManager.AddClaimAsync(role, new Claim(ClaimConstants.Permission, ApplicationPermissions.GetPermissionByValue(claim)));

                    if (!result.Succeeded)
                    {
                        await _roleManager.DeleteAsync(role);
                    }
                }
            }
        }

        private async Task<ApplicationUser> CreateUserAsync(string userName, string password, string firstName, string fullName, string lastName, string email, string phoneNumber, string[] roles)
        {
            var applicationUser = _userManager.FindByNameAsync(userName).Result;

            if (applicationUser == null)
            {
                applicationUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    FullName = fullName,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailConfirmed = true
                };

                var result = _userManager.CreateAsync(applicationUser, password).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = _userManager.AddClaimsAsync(applicationUser, new Claim[]{
                        new Claim(JwtClaimTypes.Name, userName),
                        new Claim(JwtClaimTypes.GivenName, firstName),
                        new Claim(JwtClaimTypes.FamilyName, lastName),
                        new Claim(JwtClaimTypes.Email, email),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.PhoneNumber, phoneNumber)


                    }).Result;

                //add claims version of roles
                foreach (var role in roles.Distinct())
                {
                    await _userManager.AddClaimAsync(applicationUser, new Claim($"Is{role}", "true"));
                }

                ApplicationUser user = await _userManager.FindByNameAsync(applicationUser.UserName);

                try
                {
                    result = await _userManager.AddToRolesAsync(user, roles.Distinct());
                }

                catch
                {
                    await _userManager.DeleteAsync(user);
                    throw;
                }

                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            //return applicationUser;
            await Task.CompletedTask;
            return null;
        }

        private Item[] FillItems()
        {
            Item[] items = {
                new Item
                {
                    Name = "Horse Hair", Rarity = Rarity.RARE, Tier = 1, Value = 100, Type = ItemType.MATERIAL
                }
            };
            return items;
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
                    LevelBoosts = new List<LevelBoost>{ 
                        new LevelBoost { Id = 1, Boost = new CardBoost { Imm = 10 }, Level = 2, Cost = 5},
                        new LevelBoost { Id = 2, Boost = new CardBoost { Spd = 0.2 }, Level = 3, Cost = 15},
                        new LevelBoost { Id = 3, Boost = new CardBoost { Def = 1 }, Level = 4, Cost = 80},
                        new LevelBoost { Id = 4, Boost = new CardBoost { Hp = 2 }, Level = 4, Cost = 50},
                        new LevelBoost { Id = 5, Boost = new CardBoost { Atk = 1 }, Level = 5, Cost = 150},
                    }
                },
                new Card
                {
                    Id = 2, Name = "Horse", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH, Passive = null,
                    Hp = 4, Atk = 2, Def = 0, Imm = 0, Spd = 2.8, Value = 20, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/horse.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 6, Boost = new CardBoost { Spd = 0.1 }, Level = 2, Cost = 5},
                        new LevelBoost { Id = 7, Boost = new CardBoost { Hp = 1 }, Level = 3, Cost = 25},
                        new LevelBoost { Id = 8, Boost = new CardBoost { Hp = 1 }, Level = 4, Cost = 60},
                        new LevelBoost { Id = 9, Boost = new CardBoost { Spd = 0.4 }, Level = 4, Cost = 100, RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Horse Hair"), Amount = 2 } } },
                        new LevelBoost { Id = 10, Boost = new CardBoost { Passive = new CardPassive { Id = 100000, Passive = Passive.DODGE, Param1 = 35} }, Level = 5, Cost = 400, ImprovedName = "Dodge Horse"},
                        new LevelBoost { Id = 11, Boost = new CardBoost { Spd = 0.3 }, Level = 5, Cost = 300, RequiredBoostId = 9, ImprovedName = "Speedy Horse"},
                    }
                },
                new Card
                {
                    Id = 3, Name = "Fire Wraith", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Id = 1, Passive = Passive.BLAZE, Param1 = 1, Param2 = 8},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3.0, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_wraith.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 12, Boost = new CardBoost { Spd = 0.1 }, Level = 2, Cost = 20},
                        new LevelBoost { Id = 13, Boost = new CardBoost { Hp = 1 }, Level = 3, Cost = 75},
                        new LevelBoost { Id = 14, Boost = new CardBoost { Def = 1 }, Level = 4, Cost = 250, RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Fire Emblem"), Amount = 1 } } },
                        new LevelBoost { Id = 15, Boost = new CardBoost { Hp = 1 }, Level = 4, Cost = 150},
                        new LevelBoost { Id = 16, Boost = new CardBoost { Passive = new CardPassive { Id = 100001, Passive = Passive.BLAZE, Param1 = 2, Param2 = 9} }, Level = 5, Cost = 600, ImprovedName = "Blazing Wraith"},
                        new LevelBoost { Id = 17, Boost = new CardBoost { Def = 1 }, Level = 5, Cost = 700, ImprovedName = "Armored Fire Wraith"},
                    }
                },
                new Card
                {
                    Id = 4, Name = "Grunt", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NONE, Passive = null,
                    Hp = 6, Atk = 1, Def = 1, Imm = 30, Spd = 4.1, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/grunt.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 18, Boost = new CardBoost { Spd = 0.2 }, Level = 2, Cost = 30},
                        new LevelBoost { Id = 19, Boost = new CardBoost { Imm = 10 }, Level = 3, Cost = 90},
                        new LevelBoost { Id = 20, Boost = new CardBoost { Hp = 1 }, Level = 4, Cost = 300, },
                        new LevelBoost { Id = 22, Boost = new CardBoost { Passive = new CardPassive { Id = 100002, Passive = Passive.BACKTRACK, Param1 = 1} }, Level = 5, Cost = 900, ImprovedName = "Time-Shift Grunt"},
                        new LevelBoost { Id = 23, Boost = new CardBoost { Def = 1 }, Level = 5, Cost = 750, ImprovedName = "Armored Grunt"}
                    }
                },
                new Card
                {
                    Id = 5, Name = "Dark Eye Fighter", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.FIRE,
                    Passive = new CardPassive { Id = 2, Passive = Passive.HP_SHATTER, Param1 = 10},
                    Hp = 6, Atk = 1, Def = 0, Imm = 0, Spd = 1.8, Value = 50, BaseExp = 45, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dark_eye_fighter.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 24, Boost = new CardBoost { Spd = 0.1 }, Level = 2, Cost = 100},
                        new LevelBoost { Id = 25, Boost = new CardBoost { Spd = 0.1 }, Level = 3, Cost = 250},
                        new LevelBoost { Id = 26, Boost = new CardBoost { Spd = 0.1 }, Level = 4, Cost = 600, },
                        new LevelBoost { Id = 21, Boost = new CardBoost { Hp = 1 }, Level = 4, Cost = 350, },
                        new LevelBoost { Id = 27, Boost = new CardBoost { Passive = new CardPassive { Id = 100003, Passive = Passive.HP_SHATTER, Param1 = 15} }, Level = 5, Cost = 1000, ImprovedName = "Dark Eye Breaker"},
                        new LevelBoost { Id = 28, Boost = new CardBoost { Hp = 2 }, Level = 5, Cost = 1250, ImprovedName = "Great Dark Eye Fighter", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Dark Shield"), Amount = 1 } } }
                    }
                },
                new Card
                {
                    Id = 6, Name = "Mosquito", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WIND, Passive = null,
                    Hp = 3, Atk = 2, Def = 0, Imm = 25, Spd = 1.5, Value = 20, BaseExp = 20, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/mosquito.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 29, Boost = new CardBoost { Imm = 15 }, Level = 2, Cost = 40},
                        new LevelBoost { Id = 30, Boost = new CardBoost { Imm = 20 }, Level = 3, Cost = 90},
                        new LevelBoost { Id = 31, Boost = new CardBoost { Spd = 0.1 }, Level = 4, Cost = 200, },
                        new LevelBoost { Id = 32, Boost = new CardBoost { Hp = 1 }, Level = 5, Cost = 500, ImprovedName = "Great Mosquito", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Leaf"), Amount = 3 } } },
                        new LevelBoost { Id = 75, Boost = new CardBoost { Hp = 1, Def = 1, Spd = 0.1 }, Level = 6, Cost = 3000, ImprovedName = "Imperial Mosquito", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Light Wings"), Amount = 1 } } }
                    }
                },
                new Card
                {
                    Id = 7, Name = "Poison Mosquito", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Id = 3, Passive = Passive.POISON, Param1 = 1, Param2 = 2, Param3 = 4},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 2.1, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/poison_mosquito.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 33, Boost = new CardBoost { Spd = 0.1 }, Level = 2, Cost = 60},
                        new LevelBoost { Id = 34, Boost = new CardBoost { Imm = 25 }, Level = 3, Cost = 190},
                        new LevelBoost { Id = 35, Boost = new CardBoost { Spd = 0.2 }, Level = 4, Cost = 400, },
                        new LevelBoost { Id = 36, Boost = new CardBoost { Hp = 1 }, Level = 5, Cost = 850, ImprovedName = "Great Poison Mosquito", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Leaf"), Amount = 3 } } },
                        new LevelBoost { Id = 37, Boost = new CardBoost { Hp = 1, Def = 1, Spd = 0.1 }, Level = 6, Cost = 4000, ImprovedName = "Imperial Poison Mosquito", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Light Wings"), Amount = 1 } } }
                    }
                },
                new Card
                {
                    Id = 8, Name = "Crow", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Id = 4, Passive = Passive.DODGE, Param1 = 25},
                    Hp = 4, Atk = 1, Def = 0, Imm = 50, Spd = 2.6, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/crow.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 38, Boost = new CardBoost { Spd = 0.2 }, Level = 2, Cost = 65},
                        new LevelBoost { Id = 39, Boost = new CardBoost { Spd = 0.3 }, Level = 3, Cost = 200},
                        new LevelBoost { Id = 40, Boost = new CardBoost { Atk = 1 }, Level = 4, Cost = 700, },
                        new LevelBoost { Id = 41, Boost = new CardBoost { Passive = new CardPassive { Id = 100004, Passive = Passive.DODGE, Param1 = 35} }, Level = 4, Cost = 1000, ImprovedName = "Fast Crow"},
                        new LevelBoost { Id = 42, Boost = new CardBoost { Passive = new CardPassive { Id = 100005, Passive = Passive.DODGE, Param1 = 50} }, Level = 5, Cost = 3000, ImprovedName = "Lighting Crow"},
                        new LevelBoost { Id = 43, Boost = new CardBoost { Hp = 1, Def = 1 }, Level = 5, Cost = 1300, ImprovedName = "Elite Crow"},
                    }
                },
                new Card
                {
                    Id = 9, Name = "Imp", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = null,
                    Hp = 6, Atk = 3, Def = 0, Imm = 0, Spd = 4.8, Value = 20, BaseExp = 20, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/imp.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 44, Boost = new CardBoost { Spd = 0.2 }, Level = 2, Cost = 40},
                        new LevelBoost { Id = 45, Boost = new CardBoost { Imm = 20 }, Level = 3, Cost = 150},
                        new LevelBoost { Id = 46, Boost = new CardBoost { Atk = 1 }, Level = 4, Cost = 350, },
                        new LevelBoost { Id = 47, Boost = new CardBoost { Hp = 1 }, Level = 5, Cost = 700, ImprovedName = "Feline Imp"},
                    }
                },
                new Card
                {
                    Id = 10, Name = "Lirin", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Id = 5, Passive = Passive.DOOM, Param1 = 8},
                    Hp = 9, Atk = 0, Def = 0, Imm = 100, Spd = 7, Value = 60, BaseExp = 60, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/lirin.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 48, Boost = new CardBoost { Spd = 0.3 }, Level = 2, Cost = 200},
                        new LevelBoost { Id = 49, Boost = new CardBoost { Spd = 0.3 }, Level = 3, Cost = 400},
                        new LevelBoost { Id = 50, Boost = new CardBoost { Spd = 0.4 }, Level = 4, Cost = 800},
                        new LevelBoost { Id = 51, Boost = new CardBoost { Def = 1 }, Level = 5, Cost = 6000, ImprovedName = "KLirin"},
                    }
                },
                new Card
                {
                    Id = 11, Name = "Little Beast", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = null,
                    Hp = 3, Atk = 2, Def = 1, Imm = 35, Spd = 3.7, Value = 25, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/little_beast.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 52, Boost = new CardBoost { Spd = 0.2 }, Level = 2, Cost = 40},
                        new LevelBoost { Id = 53, Boost = new CardBoost { Hp = 1 }, Level = 3, Cost = 230},
                        new LevelBoost { Id = 54, Boost = new CardBoost { Imm = 10 }, Level = 4, Cost = 500},
                        new LevelBoost { Id = 55, Boost = new CardBoost { Def = 1 }, Level = 5, Cost = 1700, ImprovedName = "Middle Beast"},
                    }
                },
                new Card
                {
                    Id = 12, Name = "Reptillion", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = new CardPassive { Id = 6, Passive = Passive.POISON, Param1 = 2, Param2 = 3, Param3 = 6 },
                    Hp = 6, Atk = 1, Def = 0, Imm = 0, Spd = 5.3, Value = 25, BaseExp = 35, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/reptillion.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 56, Boost = new CardBoost { Spd = 0.3 }, Level = 2, Cost = 70},
                        new LevelBoost { Id = 57, Boost = new CardBoost { Hp = 1 }, Level = 3, Cost = 270},
                        new LevelBoost { Id = 58, Boost = new CardBoost { Spd = 0.3 }, Level = 4, Cost = 550},
                        new LevelBoost { Id = 59, Boost = new CardBoost { Atk = 1 }, Level = 5, Cost = 1900, ImprovedName = "Fiere Reptillion"},
                        new LevelBoost { Id = 60, Boost = new CardBoost { Passive = new CardPassive { Id = 100006, Passive = Passive.POISON, Param1 = 2, Param2 = 2.5, Param3 = 5} }, Level = 5, Cost = 1300, ImprovedName = "Poisonus Reptillion"},
                    }
                },
                new Card
                {
                    Id = 13, Name = "ELKchampion", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.ALL, Element = Element.LIGHT,
                    Passive = new CardPassive { Id = 7, Passive = Passive.IGNORE_DEF },
                    Hp = 8, Atk = 1, Def = 1, Imm = 0, Spd = 5, Value = 120, BaseExp = 200, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/elkchampion.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 61, Boost = new CardBoost { Spd = 0.2 }, Level = 2, Cost = 250},
                        new LevelBoost { Id = 62, Boost = new CardBoost { Hp = 1 }, Level = 3, Cost = 650},
                        new LevelBoost { Id = 63, Boost = new CardBoost { Imm = 15 }, Level = 4, Cost = 1250},
                        new LevelBoost { Id = 64, Boost = new CardBoost { Spd = 0.8 }, Level = 5, Cost = 10000, ImprovedName = "ELiteKchampion"},
                        new LevelBoost { Id = 65, Boost = new CardBoost { Passive = new CardPassive { Id = 100007, Passive = Passive.DODGE, Param1 = 15} }, Level = 5, Cost = 1300, ImprovedName = "DodgeELKchampion"},
                    }
                },
                new Card
                {
                    Id = 14, Name = "Little Hidra", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.WATER,
                    Passive = null,
                    Hp = 9, Atk = 1, Def = 0, Imm = 0, Spd = 1.5, Value = 80, BaseExp = 120, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/little_hidra.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 66, Boost = new CardBoost { Spd = 0.1 }, Level = 2, Cost = 180},
                        new LevelBoost { Id = 67, Boost = new CardBoost { Hp = 1 }, Level = 3, Cost = 400},
                        new LevelBoost { Id = 68, Boost = new CardBoost { Imm = 25 }, Level = 4, Cost = 1000},
                        new LevelBoost { Id = 69, Boost = new CardBoost { Spd = 0.4 }, Level = 5, Cost = 6000, ImprovedName = "Little Fast Hidra"},
                        new LevelBoost { Id = 70, Boost = new CardBoost { Hp = -4, Atk = 1, Def = 1 }, Level = 5, Cost = 8000, ImprovedName = "Berseker Hidra"},
                    }
                },
                new Card
                {
                    Id = 15, Name = "Triton", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.HEAL, Element = Element.WATER,
                    Passive = null,
                    Hp = 6, Atk = 2, Def = 0, Imm = 0, Spd = 3.5, Value = 40, BaseExp = 40, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/triton.png",
                    LevelBoosts = new List<LevelBoost>{
                        new LevelBoost { Id = 71, Boost = new CardBoost { Spd = 0.2 }, Level = 2, Cost = 70},
                        new LevelBoost { Id = 72, Boost = new CardBoost { Hp = 1 }, Level = 3, Cost = 210},
                        new LevelBoost { Id = 73, Boost = new CardBoost { Def = 1 }, Level = 4, Cost = 550},
                        new LevelBoost { Id = 74, Boost = new CardBoost { Atk = 1 }, Level = 5, Cost = 1700, ImprovedName = "TRIton"},
                    }
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
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/mountain.jpg",
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
                },
                new Map
                {
                    Name = "Swamp", MinLevel = 1, Difficult = MapDifficult.NORMAL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 200 }, SrcImg = "_content/SurrealCB.CommonUI/images/maps/swamp.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Entering", Level = 1, ExpGain = 7, X = 10, Y = 0, Reward = new Reward { Gold = 12 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(3), this.GetPlayerCard(3)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Skies", MinLevel = 3, Difficult = MapDifficult.HARD, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 200 }, SrcImg = "_content/SurrealCB.CommonUI/images/maps/skies.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Rainbow", Level = 1, ExpGain = 7, X = 10, Y = 0, Reward = new Reward { Gold = 16 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(1, 4), this.GetPlayerCard(2, 4), this.GetPlayerCard(3, 4)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Snow Mountain", MinLevel = 6, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/snow_mountain.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 10 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(13), this.GetPlayerCard(14)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Swampcamp", MinLevel = 8, Difficult = MapDifficult.HARD, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/swampcamp.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(13), this.GetPlayerCard(14), this.GetPlayerCard(12), this.GetPlayerCard(11)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Megacloud", MinLevel = 10, Difficult = MapDifficult.HELL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/megacloud.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(13), this.GetPlayerCard(14), this.GetPlayerCard(12), this.GetPlayerCard(11)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Fire Mountain", MinLevel = 11, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/fire_mountain.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 200 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(13), this.GetPlayerCard(14)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Beautiful Mountain", MinLevel = 11, Difficult = MapDifficult.EASY, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/beautiful_mountain.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(13), this.GetPlayerCard(14), this.GetPlayerCard(12), this.GetPlayerCard(11)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Darkpit", MinLevel = 13, Difficult = MapDifficult.NORMAL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/darkpit.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(13), this.GetPlayerCard(14), this.GetPlayerCard(15)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Green Swamp", MinLevel = 15, Difficult = MapDifficult.EASY, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/green_swamp.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(13), this.GetPlayerCard(14), this.GetPlayerCard(12), this.GetPlayerCard(11)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Innora", MinLevel = 15, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/innora.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(13), this.GetPlayerCard(14), this.GetPlayerCard(12), this.GetPlayerCard(11)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Bluepoint", MinLevel = 17, Difficult = MapDifficult.NORMAL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/bluepoint.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(7), this.GetPlayerCard(6), this.GetPlayerCard(9), this.GetPlayerCard(10)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Demon Church", MinLevel = 18, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/demon_church.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(15),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Goffinger", MinLevel = 20, Difficult = MapDifficult.HELL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/elysium.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(15),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Dark Floating Castle", MinLevel = 27, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/dark_floating_castle.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(15),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Elysium", MinLevel = 30, Difficult = MapDifficult.HELL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/elysium.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(15),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Above Everything", MinLevel = 33, Difficult = MapDifficult.HARD, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/above_everything.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(15),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Nortrex", MinLevel = 40, Difficult = MapDifficult.HELL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "_content/SurrealCB.CommonUI/images/maps/nortrex.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, ExpGain = 5, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard(15),
                            }
                        }
                    }
                },
            };
            return maps;
        }

        private PlayerCard GetPlayerCard(int id, int level = 1, List<Rune> runes = null)
        {
            var levelBoosts = new List<LevelBoost>();
            levelBoosts = this.cards.FirstOrDefault(x => x.Id == id).LevelBoosts != null ?
                this.cards.FirstOrDefault(x => x.Id == id).LevelBoosts.Where(x => x.Level <= level).ToList() : new List<LevelBoost>();
            return new PlayerCard
            {
                CardId = id,
                ActiveLvlBoosts = levelBoosts,
                Runes = runes
            };
        }

        private Item GetItem(string name)
        {
            return this.items.Where(x => x.Name == name).FirstOrDefault();
        } 
    }
}

