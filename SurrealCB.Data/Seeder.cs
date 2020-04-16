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

            this.items = this.FillItems();
            this.cards = this.FillCards();

            if (!_context.Cards.Any())
            {
                _context.Cards.AddRange(
                   this.cards
                );
            }

            _context.SaveChanges();

            this.maps = this.FillMaps();

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
                var from = this.cards.Min(x => x.Id);
                var to = this.cards.Max(x => x.Id) + 1;
                for (var i = from; i < to; i++)
                {
                    pcards.Add(new PlayerCard
                    {
                        CardId = this.cards.FirstOrDefault(x => x.Id == i).Id,
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
                //COMMON TIER 1
                new Card
                {
                    Name = "Goblin", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH, Passive = null,
                    Hp = 5, Atk = 2, Def = 0, Imm = 0, Spd = 3.6, Value = 20, BaseExp = 20, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/goblin.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 15, imm: 10), DoBoost(3, 15, spd: 0.2), DoBoost(4, 80, def: 1), DoBoost(4, 50, hp: 2), DoBoost(5, 150, atk: 1)
                    }
                },
                new Card
                {
                    Name = "Horse", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH, Passive = null,
                    Hp = 4, Atk = 2, Def = 0, Imm = 0, Spd = 2.8, Value = 20, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/horse.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 5, spd: 0.1), DoBoost(3, 25, hp: 1), DoBoost(4, 60, hp: 1),
                        DoBoost(4, 100, spd: 0.4), DoBoost(5, 450, p: Passive.DODGE, p1: 30, name: "Dodge Horse"), DoBoost(5, 300, spd: 0.3, name: "Speedy Horse")
                        //new LevelBoost { Id = 9, Boost = new CardBoost { Spd = 0.4 }, Level = 4, Cost = 100, RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Horse Hair"), Amount = 2 } } },
                    }
                },
                new Card
                {
                    Name = "Little Beast", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = null,
                    Hp = 3, Atk = 2, Def = 1, Imm = 35, Spd = 3.7, Value = 25, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/little_beast.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 145, hp: 1), DoBoost(4, 310, imm: 10), DoBoost(5, 1100, def: 1, name: "Middle Beast")
                    }
                },
                new Card
                {
                    Name = "Mosquito", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WIND, Passive = null,
                    Hp = 3, Atk = 2, Def = 0, Imm = 25, Spd = 1.5, Value = 20, BaseExp = 20, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/mosquito.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, imm: 15), DoBoost(3, 45, imm: 20), DoBoost(4, 120, spd: 0.1),
                        DoBoost(5, 260, hp: 1, name: "Great Mosquito"), DoBoost(6, 1250, hp: 1, def: 1, spd: 0.1, name: "Imperial Mosquito")
                        //new LevelBoost { Id = 32, Boost = new CardBoost { Hp = 1 }, Level = 5, Cost = 500, ImprovedName = "Great Mosquito", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Leaf"), Amount = 3 } } },
                        //new LevelBoost { Id = 75, Boost = new CardBoost { Hp = 1, Def = 1, Spd = 0.1 }, Level = 6, Cost = 3000, ImprovedName = "Imperial Mosquito", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Light Wings"), Amount = 1 } } }
                    }
                },
                new Card
                {
                    Name = "Dawn Duck", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.HEAL, Element = Element.WIND,
                    Passive = null,
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3, Value = 22, BaseExp = 22, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dawn_duck.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.2), DoBoost(3, 75, spd: 0.2), DoBoost(4, 170, spd: 0.2),
                        DoBoost(5, 450, hp: 1, name: "Dawn Ducker"), DoBoost(6, 1500, hp: 1, spd: 0.2, name: "Dawn DucKing")
                    }
                },
                new Card
                {
                    Name = "Imp", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = null,
                    Hp = 6, Atk = 3, Def = 0, Imm = 0, Spd = 5.5, Value = 30, BaseExp = 28, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/imp.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.2), DoBoost(3, 90, imm: 20), DoBoost(4, 250, atk: 1), DoBoost(5, 650, hp: 1, name: "Feline Imp")
                    }
                },
                new Card
                {
                    Name = "Succubus", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 15},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 3.2, Value = 24, BaseExp = 24, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/succubus.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, spd: 0.2), DoBoost(3, 80, imm: 15), DoBoost(4, 220, hp: 1), DoBoost(5, 750, hp: 1, name: "Great Succubus")
                    }
                },
                new Card
                {
                    Name = "One Eye Mage", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BOUNCE, Param1 = 1},
                    Hp = 3, Atk = 1, Def = 0, Imm = 25, Spd = 3.2, Value = 23, BaseExp = 23, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/one_eye_mage.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, spd: 0.2), DoBoost(3, 70, hp: 1), DoBoost(4, 220, spd: 2.8),
                        DoBoost(5, 600, p: Passive.BOUNCE, p1: 2, name: "Almost-One Eye Mage")
                    }
                },
                new Card
                {
                    Name = "Moon Elf", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 50},
                    Hp = 2, Atk = 1, Def = 0, Imm = 0, Spd = 2.1, Value = 26, BaseExp = 26, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/moon_elf.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.1), DoBoost(3, 120, p: Passive.DODGE, p1: 58), DoBoost(4, 320, hp: 1),
                        DoBoost(5, 1100, p: Passive.DODGE, p1: 65, name: "Light Moon Elf")
                    }
                },
                new Card
                {
                    Name = "Snow Rat", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.FREEZE, Param1 = 12, Param2 = 5},
                    Hp = 3, Atk = 1, Def = 0, Imm = 20, Spd = 2.6, Value = 25, BaseExp = 20, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/snow_rat.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 10, spd: 0.1), DoBoost(3, 35, imm: 10), DoBoost(4, 150, hp: 1), DoBoost(5, 450, p: Passive.FREEZE, p1: 18, p2: 5, name: "Freezing Imp")
                    }
                },
                new Card
                {
                    Name = "Native", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = null,
                    Hp = 3, Atk = 1, Def = 1, Imm = 0, Spd = 3.3, Value = 30, BaseExp = 28, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/native.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 115, imm: 20), DoBoost(4, 300, hp: 1), DoBoost(5, 850, spd: 0.4, name: "Fast Native"),
                        DoBoost(5, 1150, spd: -0.8, atk: 1, name: "Elite Native")
                    }
                },
                new Card
                {
                    Name = "Ogre", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.IGNORE_DEF},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3.3, Value = 25, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ogre.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.2), DoBoost(3, 100, imm: 15), DoBoost(4, 280, hp: 1), DoBoost(5, 750, spd: 0.6, name: "Enhanced Ogre")
                    }
                },
                new Card
                {
                    Name = "Fire Snake", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BLAZE, Param1 = 1, Param2 = 2.5},
                    Hp = 3, Atk = 0, Def = 0, Imm = 30, Spd = 2.3, Value = 25, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_snake.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.1), DoBoost(3, 95, imm: 15), DoBoost(4, 275, hp: 1), DoBoost(5, 750, spd: 0.5, name: "Fast-Fire Snake"),
                        DoBoost(5, 1050, p: Passive.BLAZE, p1: 1, p2: 5, name: "Blazing Snake")
                    }
                },

                //RARE TIER 1

                new Card
                {
                    Name = "Fire Wraith", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BLAZE, Param1 = 1, Param2 = 4},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3.0, Value = 32, BaseExp = 35, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_wraith.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 40, spd: 0.1), DoBoost(3, 125, hp: 1), DoBoost(4, 445, def: 1), DoBoost(4, 325, hp: 1),
                        DoBoost(5, 1700, p: Passive.BLAZE, p1: 2, p2: 3.5, name: "Blazing Wraith"),
                        DoBoost(5, 2200, def: 1, name: "Armored Fire Wraith")
                        //new LevelBoost { Id = 14, Boost = new CardBoost { Def = 1 }, Level = 4, Cost = 250, RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Fire Emblem"), Amount = 1 } } },
                    }
                },
                new Card
                {
                    Name = "Triton", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.HEAL, Element = Element.WATER,
                    Passive = null,
                    Hp = 6, Atk = 2, Def = 0, Imm = 0, Spd = 3.5, Value = 40, BaseExp = 40, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/triton.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 70, spd: 0.2), DoBoost(3, 210, hp: 1), DoBoost(4, 500, def: 1), DoBoost(5, 1700, atk: 1, name: "TRIton"),
                    }
                },
                new Card
                {
                    Name = "Ice Skeleton", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.RANDOM, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.PIERCING, Param1 = 1},
                    Hp = 4, Atk = 2, Def = 1, Imm = 0, Spd = 4, Value = 45, BaseExp = 50, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ice_skeleton.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 80, spd: 0.2), DoBoost(3, 240, imm: 25), DoBoost(4, 700, hp: 1), DoBoost(5, 2250, atk: 1, name: "Brute Ice Skeleton"),
                        DoBoost(5, 1750, def: 1, name: "Armored Ice Skeleton")
                    }
                },
                new Card
                {
                    Name = "Grunt", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NONE, Passive = null,
                    Hp = 6, Atk = 1, Def = 1, Imm = 30, Spd = 4.1, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/grunt.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 60, spd: 0.2), DoBoost(3, 210, imm: 10), DoBoost(4, 550, hp: 1), DoBoost(5, 1950, p: Passive.BACKTRACK, p1: 1, name: "Time-Shift Grunt"),
                        DoBoost(5, 1550, def: 1, name: "Armored Grunt")
                    }
                },
                new Card
                {
                    Name = "Poison Mosquito", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.POISON, Param1 = 1, Param2 = 2, Param3 = 4},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 2.1, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/poison_mosquito.png",
                    LevelBoosts = new List<LevelBoost>{
                        //new LevelBoost { Id = 36, Boost = new CardBoost { Hp = 1 }, Level = 5, Cost = 850, ImprovedName = "Great Poison Mosquito", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Leaf"), Amount = 3 } } },
                        //new LevelBoost { Id = 37, Boost = new CardBoost { Hp = 1, Def = 1, Spd = 0.1 }, Level = 6, Cost = 4000, ImprovedName = "Imperial Poison Mosquito", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Light Wings"), Amount = 1 } } },
                        DoBoost(2, 60, spd: 0.1), DoBoost(3, 190, imm: 25), DoBoost(4, 450, spd: 0.2), DoBoost(5, 1150, hp: 1, name: "Great Poison Mosquito"),
                        DoBoost(6, 4000, hp: 1, def: 1, spd: 0.1, name: "Imperial Poison Mosquito"),
                    }
                },
                new Card
                {
                    Name = "Crow", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 25},
                    Hp = 4, Atk = 1, Def = 0, Imm = 50, Spd = 2.6, Value = 30, BaseExp = 25, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/crow.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 65, spd: 0.2), DoBoost(3, 200, spd: 0.2), DoBoost(4, 700, atk: 1), DoBoost(4, 1000, p: Passive.DODGE, p1: 35, name: "Fast Crow"),
                        DoBoost(5, 3000, p: Passive.DODGE, p1: 50, name: "Lighting Crow"), DoBoost(5, 1600, hp: 1, def: 1, name: "Elite Crow")
                    }
                },
                new Card
                {
                    Name = "Reptillion", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.POISON, Param1 = 2, Param2 = 3, Param3 = 6 },
                    Hp = 6, Atk = 1, Def = 0, Imm = 0, Spd = 5.3, Value = 25, BaseExp = 35, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/reptillion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 70, spd: 0.3), DoBoost(3, 250, hp: 1), DoBoost(4, 650, spd: 0.4), DoBoost(5, 1900, atk: 1, name: "Fiere Reptillion"),
                        DoBoost(5, 1400, p: Passive.POISON, p1: 2, p2: 2.5, p3: 5, name: "Poisonus Reptillion")
                    }
                },
                new Card
                {
                    Name = "Dark Eye Fighter", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.HP_SHATTER, Param1 = 10},
                    Hp = 6, Atk = 1, Def = 0, Imm = 0, Spd = 1.8, Value = 50, BaseExp = 45, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dark_eye_fighter.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 100, spd: 0.1), DoBoost(3, 260, spd: 0.1), DoBoost(4, 480, spd: 0.1), DoBoost(4, 650, hp: 1),
                        DoBoost(5, 2400, p: Passive.HP_SHATTER, p1: 15, name: "Dark Eye Breaker"), DoBoost(5, 2700, hp: 2, name: "Great Dark Eye Fighter")
                        //new LevelBoost { Id = 28, Boost = new CardBoost { Hp = 2 }, Level = 5, Cost = 1250, ImprovedName = "Great Dark Eye Fighter", RequiredItems = new List<RequiredItem>{ new RequiredItem { Item = this.GetItem("Dark Shield"), Amount = 1 } } }
                    }
                },
                new Card
                {
                    Name = "Lirin", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.DOOM, Param1 = 8},
                    Hp = 9, Atk = 0, Def = 0, Imm = 100, Spd = 7, Value = 60, BaseExp = 60, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/lirin.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 120, spd: 0.3), DoBoost(3, 290, spd: 0.3), DoBoost(4, 800, spd: 0.4), DoBoost(5, 6000, def: 1, name: "KLirin"),
                        DoBoost(5, 8000, p: Passive.DOOM, p1: 2, name: "Insta-Killirin")
                    }
                },
                new Card
                {
                    Name = "Little Hidra", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.WATER,
                    Passive = null,
                    Hp = 9, Atk = 1, Def = 0, Imm = 0, Spd = 1.5, Value = 80, BaseExp = 120, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/little_hidra.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 140, spd: 0.1), DoBoost(3, 350, hp: 1), DoBoost(4, 850, imm: 25),
                        DoBoost(5, 4500, spd: 0.4, name: "Little Fast Hidra"), DoBoost(5, 6500, hp: -4, atk: 1, def: 1, name: "Berseker Hidra")
                    }
                },
                new Card
                {
                    Name = "ELKchampion", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.ALL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.IGNORE_DEF },
                    Hp = 8, Atk = 1, Def = 1, Imm = 0, Spd = 5, Value = 120, BaseExp = 200, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/elkchampion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 250, spd: 0.2), DoBoost(3, 650, hp: 1), DoBoost(4, 1250, imm: 15),
                        DoBoost(5, 8500, spd: 0.8, name: "ELiteKchampion"), DoBoost(5, 6500, p: Passive.DODGE, p1: 15, name: "DodgeELKchampion")
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
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "mountain.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        DoEnemy("Rocks Encounter", 1, new List<PlayerCard>{ this.GetPlayerCard("Little Beast"), this.GetPlayerCard("Goblin") },
                        DoReward(10, 5), X: 5, Y: 25, false, 3),
                        DoEnemy("Forest Encounter", 1, new List<PlayerCard>{ this.GetPlayerCard("Goblin"), this.GetPlayerCard("Horse"), this.GetPlayerCard("Horse", 2)}, 
                        DoReward(15, 10), X: 40, Y: 20, false, 3),
                        DoEnemy("Lake Encounter", 1, new List<PlayerCard>{ this.GetPlayerCard("Dawn Duck"), this.GetPlayerCard("Mosquito"), this.GetPlayerCard("Mosquito", 2)},
                        DoReward(12, 7), X: 65, Y: 40, false, 3),
                        DoEnemy("Snowfield", 1, new List<PlayerCard>{ this.GetPlayerCard("Native"), this.GetPlayerCard("Snow Rat")},
                        DoReward(17, 10), X: 45, Y: 55, true, 4),
                        DoEnemy("Volcan", 1, new List<PlayerCard>{ this.GetPlayerCard("Ogre"), this.GetPlayerCard("Fire Snake"), this.GetPlayerCard("Ogre", 2)},
                        DoReward(14, 9), X: 28, Y: 80, false, 3),
                        DoEnemy("Great Volcan", 2, new List<PlayerCard>{ this.GetPlayerCard("Ogre"), this.GetPlayerCard("Fire Wraith", 2), this.GetPlayerCard("Ogre", 3)},
                        DoReward(25, 15), X: 40, Y: 83, false, 3),
                    }
                },
                new Map
                {
                    Name = "Swamp", MinLevel = 1, Difficult = MapDifficult.NORMAL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 200 }, SrcImg = "swamp.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Entering", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 12 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Grunt"), this.GetPlayerCard("Goblin", 3)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Skies", MinLevel = 3, Difficult = MapDifficult.HARD, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 200 }, SrcImg = "skies.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Rainbow", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 16 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Poison Mosquito", 4), this.GetPlayerCard("Mosquito", 4)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Snow Mountain", MinLevel = 6, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "snow_mountain.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 10 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Triton")
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Swampcamp", MinLevel = 8, Difficult = MapDifficult.HARD, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "swampcamp.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Reptillion", 5), this.GetPlayerCard("Reptillion")
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Megacloud", MinLevel = 10, Difficult = MapDifficult.HELL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "megacloud.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Horse", 5), this.GetPlayerCard("Grunt", 5)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Fire Mountain", MinLevel = 11, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "fire_mountain.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 200 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Horse"), this.GetPlayerCard("Grunt")
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Beautiful Mountain", MinLevel = 11, Difficult = MapDifficult.EASY, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "beautiful_mountain.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Horse"), this.GetPlayerCard("Grunt"), this.GetPlayerCard("Dark Eye Fighter"), this.GetPlayerCard("Mosquito")
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Darkpit", MinLevel = 13, Difficult = MapDifficult.NORMAL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "darkpit.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Horse"), this.GetPlayerCard("Grunt"), this.GetPlayerCard("Dark Eye Fighter", 3), this.GetPlayerCard("Mosquito", 5)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Green Swamp", MinLevel = 15, Difficult = MapDifficult.EASY, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "green_swamp.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Little Hidra"), this.GetPlayerCard("Lirin"), this.GetPlayerCard("Reptillion", 5), this.GetPlayerCard("Poison Mosquito", 6)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Innora", MinLevel = 15, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 100}, SrcImg = "innora.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Crow", 5), this.GetPlayerCard("Fire Wraith", 5), this.GetPlayerCard("Dark Eye Fighter", 5)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Bluepoint", MinLevel = 17, Difficult = MapDifficult.NORMAL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "bluepoint.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Dark Eye Fighter"), this.GetPlayerCard("Mosquito"), this.GetPlayerCard("ELKChampion"), this.GetPlayerCard("Grunt")
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Demon Church", MinLevel = 18, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "demon_church.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Poison Mosquito", 6),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Goffinger", MinLevel = 20, Difficult = MapDifficult.HELL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "elysium.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Little Beast", 5),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Dark Floating Castle", MinLevel = 27, Difficult = MapDifficult.INSANE, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "dark_floating_castle.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("ELKChampion", 5),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Elysium", MinLevel = 30, Difficult = MapDifficult.HELL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "elysium.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Reptillion"),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Above Everything", MinLevel = 33, Difficult = MapDifficult.HARD, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "above_everything.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("ELKChampion"),
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Nortrex", MinLevel = 40, Difficult = MapDifficult.HELL, Type = GameType.NORMAL, RequiredEnemies = null,
                    RequiredMaps = null, CompletionReward = new Reward { Gold = 1000}, SrcImg = "nortrex.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        new EnemyNpc
                        {
                            Name = "Forest Encounter", Level = 1, X = 10, Y = 0, Reward = new Reward { Gold = 150 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Lirin"),
                            }
                        }
                    }
                },
            };
            return maps;
        }

        private PlayerCard GetPlayerCard(string name, int level = 1, List<Rune> runes = null)
        {
            var levelBoosts = new List<LevelBoost>();
            levelBoosts = this.cards.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()).LevelBoosts != null ?
                this.cards.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()).LevelBoosts.Where(x => x.Level <= level).ToList() : new List<LevelBoost>();

            var card = this.cards.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            return new PlayerCard
            {
                Card = card,
                CardId = card.Id,
                ActiveLvlBoosts = levelBoosts,
                Runes = runes
            };
        }

        private LevelBoost DoBoost(int level, int cost, int hp = 0, int atk = 0, int def = 0, double spd = 0,
            int imm = 0, Passive p = Passive.NONE, double p1 = 0, double p2 = 0, double p3 = 0, string name = "")
        {
            return new LevelBoost
            {
                Level = level,
                Cost = cost,
                Boost = new CardBoost
                {
                    Hp = hp,
                    Atk = atk,
                    Def = def,
                    Spd = spd,
                    Imm = imm,
                    Passive = p != Passive.NONE ? new CardPassive
                    {
                        Passive = p,
                        Param1 = p1,
                        Param2 = p2,
                        Param3 = p3
                    } : null
                },
                ImprovedName = name
            };
        }

        private EnemyNpc DoEnemy(string name, int level, List<PlayerCard> cards, Reward reward, int X = 0, int Y = 0, bool randomCards = true, int cardCount = 4)
        {
            return new EnemyNpc
            {
                Name = name,
                Level = level,
                RandomCards = randomCards,
                CardCount = cardCount,
                X = X,
                Y = Y,
                Reward = reward,
                Cards = cards
            };
        }

        private Reward DoReward(int gold = 0, int exp = 0, Card card = null, List<Item> items = null)
        {
            return new Reward
            {
                Gold = gold,
                Exp = exp,
                Card = card,
                Items = items
            };
        }

        private Item GetItem(string name)
        {
            return this.items.Where(x => x.Name == name).FirstOrDefault();
        }
    }
}

