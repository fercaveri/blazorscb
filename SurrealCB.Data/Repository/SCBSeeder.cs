using IdentityModel;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using Microsoft.Extensions.Logging;
using SurrealCB.Data.Enum;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;
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

    public class SCBSeeder : ISeeder
    {
        private readonly IRepository repository;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger _logger;

        private Card[] cards;
        private Map[] maps;
        private Item[] items;
        private Rune[] runes;

        public SCBSeeder(
            IRepository context,
            ILogger<SCBSeeder> logger
            //UserManager<ApplicationUser> userManager,
            //RoleManager<IdentityRole<Guid>> roleManager)
            )
        {
            repository = context;
            //_userManager = userManager;
            //_roleManager = roleManager;
            _logger = logger;
        }

        public virtual async Task SeedAsync()
        {
            //Seed users and roles
            await SeedASPIdentityCoreAsync();

            //Seed clients and Api
            await SeedIdentityServerAsync();

            await SeedGameData();
        }

        private async Task SeedASPIdentityCoreAsync()
        {
            if (!await repository.Query<ApplicationUser>().AnyAsync())
            {
                //Generating inbuilt accounts
                const string adminRoleName = "Administrator";
                const string userRoleName = "User";

                await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
                await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

                await CreateUserAsync("admin", "ferk!veri1S", "Admin", "Blazor", "Administrator", "admin@blazoreboilerplate.com", "+1 (123) 456-7890", new string[] { adminRoleName });
                await CreateUserAsync("user", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                await CreateUserAsync("level1", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                await CreateUserAsync("level2", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                await CreateUserAsync("level3", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                await CreateUserAsync("level4", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                await CreateUserAsync("level5", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                await CreateUserAsync("level6", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                await CreateUserAsync("level7", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                await CreateUserAsync("level8", "ferk!veri1S", "User", "Blazor", "User Blazor", "user@blazoreboilerplate.com", "+1 (123) 456-7890`", new string[] { userRoleName });
                _logger.LogInformation("Inbuilt account generation completed");
            }
            else
            {
                //const string adminRoleName = "Administrator";

                //IdentityRole<Guid> adminRole = await _roleManager.FindByNameAsync(adminRoleName);
                //var AllClaims = ApplicationPermissions.GetAllPermissionValues().Distinct();
                //var RoleClaims = (await _roleManager.GetClaimsAsync(adminRole)).Select(c => c.Value).ToList();
                //var NewClaims = AllClaims.Except(RoleClaims);
                //foreach (string claim in NewClaims)
                //{
                //    await _roleManager.AddClaimAsync(adminRole, new Claim(ClaimConstants.Permission, claim));
                //}
                //var DeprecatedClaims = RoleClaims.Except(AllClaims);
                //var roles = await _roleManager.Roles.ToListAsync();
                //foreach (string claim in DeprecatedClaims)
                //{
                //    foreach (var role in roles)
                //    {
                //        await _roleManager.RemoveClaimAsync(role, new Claim(ClaimConstants.Permission, claim));
                //    }
                //}
            }
            await Task.CompletedTask;
        }

        private async Task SeedGameData()
        {
            //ApplicationUser user = await _userManager.FindByNameAsync("user");

            //if (!repository.UserProfiles.Any())
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
            //    repository.UserProfiles.Add(userProfile);
            //}

            this.items = this.FillItems();
            this.cards = this.FillCards();
            this.runes = this.FillRunes();

            if (!await repository.Query<Card>().AnyAsync())
            {
                this.cards = (await repository.SaveCollectionAsync(this.cards)).ToArray();
            }

            //if (!repository.Runes.Any())
            //{
            //    repository.Runes.AddRange(
            //       this.runes
            //    );
            //}

            //var user = await repository.Users.FirstOrDefaultAsync();
            //if (user.Runes?.Any() == false)
            //{
            //    user.Runes = new List<PlayerRune>();
            //    this.runes.ToList().ForEach(x =>
            //    {
            //        user.Runes.Add(new PlayerRune
            //        {
            //            RuneId = x.Id,
            //            Rarity = Rarity.COMMON
            //        });
            //    });
            //    repository.Update(user);
            //}

            this.maps = this.FillMaps();

            if (!await repository.Query<Map>().AnyAsync())
            {
                await repository.SaveCollectionAsync(this.maps);
            }

            if (!await repository.Query<PlayerCard>().AnyAsync(x => x.ApplicationUser != null))
            {
                var user = await repository.Query<ApplicationUser>().FirstOrDefaultAsync();
                var cardsNoBoss = this.cards.Where(x => x.Rarity != Rarity.BOSS);
                var pcards = new List<PlayerCard>();
                var from = cardsNoBoss.Min(x => x.Id);
                var to = cardsNoBoss.Max(x => x.Id) + 1;
                for (var i = from; i < to; i++)
                {
                    pcards.Add(new PlayerCard
                    {
                        Card = cardsNoBoss.FirstOrDefault(x => x.Id == i),
                        ApplicationUser = user
                    });
                }

                var lvl1user = await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level1");
                for (var i = from; i < to; i++)
                {
                    var card = cardsNoBoss.FirstOrDefault(x => x.Id == i);
                    pcards.Add(new PlayerCard
                    {
                        Card = card,
                        ActiveLvlBoosts = card.LevelBoosts.Where(x => x.Level <= 1).Select(x => new ActiveLevelBoost { LevelBoost = x }).ToList(),
                        ApplicationUser = lvl1user
                    });
                }

                var lvl2user = await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level2");
                for (var i = from; i < to; i++)
                {
                    var card = cardsNoBoss.FirstOrDefault(x => x.Id == i);
                    pcards.Add(new PlayerCard
                    {
                        Card = card,
                        ActiveLvlBoosts = card.LevelBoosts.Where(x => x.Level <= 2).Select(x => new ActiveLevelBoost { LevelBoost = x }).ToList(),
                        ApplicationUser = lvl2user
                    });
                }

                //var lvl3Id = (await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level3")).Id;
                for (var i = from; i < to; i++)
                {
                    var card = cardsNoBoss.FirstOrDefault(x => x.Id == i);
                    pcards.Add(new PlayerCard
                    {
                        Card = card,
                        ActiveLvlBoosts = card.LevelBoosts.Where(x => x.Level <= 3).Select(x => new ActiveLevelBoost { LevelBoost = x }).ToList(),
                        ApplicationUser = await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level3")
                    });
                }

                //var lvl4Id = (await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level4")).Id;
                for (var i = from; i < to; i++)
                {
                    var card = cardsNoBoss.FirstOrDefault(x => x.Id == i);
                    pcards.Add(new PlayerCard
                    {
                        Card = card,
                        ActiveLvlBoosts = card.LevelBoosts.Where(x => x.Level <= 4).Select(x => new ActiveLevelBoost { LevelBoost = x }).ToList(),
                        ApplicationUser = await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level4")
                    });
                }

                //var lvl5Id = (await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level5")).Id;
                for (var i = from; i < to; i++)
                {
                    var card = cardsNoBoss.FirstOrDefault(x => x.Id == i);
                    pcards.Add(new PlayerCard
                    {
                        Card = card,
                        ActiveLvlBoosts = card.LevelBoosts.Where(x => x.Level <= 5).Select(x => new ActiveLevelBoost { LevelBoost = x }).ToList(),
                        ApplicationUser = await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level5")
                    });
                }

                //var lvl6Id = (await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level6")).Id;
                for (var i = from; i < to; i++)
                {
                    var card = cardsNoBoss.FirstOrDefault(x => x.Id == i);
                    pcards.Add(new PlayerCard
                    {
                        Card = card,
                        ActiveLvlBoosts = card.LevelBoosts.Where(x => x.Level <= 6).Select(x => new ActiveLevelBoost { LevelBoost = x }).ToList(),
                        ApplicationUser = await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level6")
                    });
                }

                //var lvl7Id = (await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level7")).Id;
                for (var i = from; i < to; i++)
                {
                    var card = cardsNoBoss.FirstOrDefault(x => x.Id == i);
                    pcards.Add(new PlayerCard
                    {
                        Card = card,
                        ActiveLvlBoosts = card.LevelBoosts.Where(x => x.Level <= 7).Select(x => new ActiveLevelBoost { LevelBoost = x }).ToList(),
                        ApplicationUser = await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level7")
                    });
                }

                //var lvl8Id = (await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level8")).Id;
                for (var i = from; i < to; i++)
                {
                    var card = cardsNoBoss.FirstOrDefault(x => x.Id == i);
                    pcards.Add(new PlayerCard
                    {
                        Card = card,
                        ActiveLvlBoosts = card.LevelBoosts.Where(x => x.Level <= 8).Select(x => new ActiveLevelBoost { LevelBoost = x}).ToList(),
                        ApplicationUser = await repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == "level8")
                    });
                }

                await repository.SaveCollectionAsync(pcards);

            }
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
            var applicationUser = await this.repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == userName);

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
                    EmailConfirmed = true,
                };

                var result = await this.repository.SaveAsync(applicationUser);
                //var result = _userManager.CreateAsync(applicationUser, password).Result;
                if (result.Id == 0)
                {
                    throw new Exception("ERROR");
                }

                //result = _userManager.AddClaimsAsync(applicationUser, new Claim[]{
                //        new Claim(JwtClaimTypes.Name, userName),
                //        new Claim(JwtClaimTypes.GivenName, firstName),
                //        new Claim(JwtClaimTypes.FamilyName, lastName),
                //        new Claim(JwtClaimTypes.Email, email),
                //        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                //        new Claim(JwtClaimTypes.PhoneNumber, phoneNumber)


                //    }).Result;

                //add claims version of roles
                foreach (var role in roles.Distinct())
                {
                    //await _userManager.AddClaimAsync(applicationUser, new Claim($"Is{role}", "true"));
                }

                //ApplicationUser user = await _userManager.FindByNameAsync(applicationUser.UserName);

                //try
                //{
                //    result = await _userManager.AddToRolesAsync(user, roles.Distinct());
                //}

                //catch
                //{
                //    await _userManager.DeleteAsync(user);
                //    throw;
                //}

                //if (!result.Succeeded)
                //{
                //    await _userManager.DeleteAsync(user);
                //}
            }
            //return applicationUser;
            return null;
        }

        private Item[] FillItems()
        {
            Item[] items = {
                new Item
                {
                    Name = "Horse Hair", Rarity = Rarity.RARE, Tier = 1, Value = 100, ItemType = ItemType.MATERIAL
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
                    Hp = 5, Atk = 2, Def = 0, Imm = 0, Spd = 3.8, Value = 30, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/goblin.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 50, imm: 10), DoBoost(3, 135, spd: 0.2), DoBoost(4, 600, def: 1), DoBoost(5, 1500, atk: 1, name: "Great Goblin"),
                        DoBoost(6, 9000, spd: 0.6, name: "Elite Goblin"), DoBoost(7, 0, atk: 1, hp: 1, spd: 0.3, name: "Mega Goblin"),
                        DoBoost(8, 0, atk: 1, imm: 25, def: 1, name: "Infamous Chief Goblin"),
                    }
                },
                new Card
                {
                    Name = "Horse", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH, Passive = null,
                    Hp = 4, Atk = 2, Def = 0, Imm = 15, Spd = 3.3, Value = 23, BaseExp = 21, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/horse.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 15, spd: 0.1), DoBoost(3, 35, hp: 1), DoBoost(4, 160, spd: 0.4), DoBoost(5, 650, p: Passive.DODGE, p1: 20, name: "Dodge Horse"),
                        DoBoost(6, 0, atk: 1, spd: 0.4, name: "Rage Horse"), DoBoost(7, 0, def: 1, p: Passive.DODGE, p1: 30, name: "Tough Rage Horse"),
                        DoBoost(8, 0, spd: 0.8, name: "Elder Rage Horse"),
                    }
                },
                new Card
                {
                    Name = "Rockhino", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.TOUGH, Param1 = 30, Param2 = 1},
                    Hp = 3, Atk = 1, Def = 1, Imm = 0, Spd = 3.3, Value = 23, BaseExp = 21, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/rockhino.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, imm: 15), DoBoost(3, 45, imm: 10), DoBoost(4, 110, spd: 0.4), DoBoost(5, 400, imm: 10, p: Passive.TOUGH, p1: 65, p2: 1, name: "RockITnho"),
                        DoBoost(6, 0, hp: 1, def: 1, name: "RRRockinho"), DoBoost(7, 0, hp: 1, atk: 1, name: "Great Rockinho"),
                        DoBoost(8, 0, hp: 3, name: "More Greater Rockinho"),
                    }
                },
                new Card
                {
                    Name = "Pismire", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.DOUBLE_ATTACK, Param1 = 50},
                    Hp = 3, Atk = 1, Def = 0, Imm = 30, Spd = 1.7, Value = 23, BaseExp = 20, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/pismire.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 16, imm: 10), DoBoost(3, 40, spd: 0.15), DoBoost(4, 105, spd: 0.15),
                        DoBoost(5, 420, p: Passive.DOUBLE_ATTACK, p1: 65, name: "Fastismire"),
                        DoBoost(6, 0, spd: 0.3, hp: 1, name: "Speedismire"), DoBoost(7, 0, hp: 1, atk: 1, name: "Great Pismire"),
                        DoBoost(8, 0, spd: 0.5, imm: 20, p: Passive.DOUBLE_ATTACK, p1: 80, name: "Piscopismire"),
                    }
                },
                new Card
                {
                    Name = "Dummy", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.ENDURABLE, Param1 = 50},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3.2, Value = 24, BaseExp = 23, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dummy.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 18, imm: 30), DoBoost(3, 42, spd: 0.5), DoBoost(4, 108, hp: 1, imm: 10),
                        DoBoost(5, 390, hp: 1, def: 1, name: "Giant Dummy"), DoBoost(6, 0, spd: 1, hp: 1, name: "Hurtspeed Dummy"), DoBoost(7, 0, hp: 1, atk: 1, name: "A Great Dummy"),
                        DoBoost(8, 0, def: 1, imm: 20, p: Passive.ENDURABLE, p1: 75, name: "Can Hurt My Dummy?"),
                    }
                },
                new Card
                {
                    Name = "Little Beast", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = null,
                    Hp = 3, Atk = 2, Def = 1, Imm = 20, Spd = 4.6, Value = 31, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/little_beast.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 85, hp: 1), DoBoost(4, 250, imm: 10), DoBoost(5, 900, def: 1, name: "Middle Beast"),
                        DoBoost(6, 0, atk: 1, hp: 1, name: "High Beast"), DoBoost(7, 0, hp: 1, def: 1, name: "Potent Beast"),
                        DoBoost(8, 0, spd: 1.3, atk: 1, imm: 5, name: "Fast & Power Beast"),
                    }
                },
                new Card
                {
                    Name = "Troll", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = null,
                    Hp = 7, Atk = 1, Def = 0, Imm = 20, Spd = 4.1, Value = 22, BaseExp = 22, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/troll.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.2), DoBoost(3, 60, hp: 1), DoBoost(4, 150, imm: 20), DoBoost(5, 500, atk: 1, name: "Rage Troll"),
                        DoBoost(6, 5500, hp: 2, spd: 0.5, name: "Mega Troll"), DoBoost(7, 0, hp: 1, spd: 0.3, atk: 1, name: "Giga Troll"),
                        DoBoost(8, 0, spd: 0.6, hp: 3, name: "Elder Troll"),
                    }
                },
                new Card
                {
                    Name = "Troll Eater", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.BLEED, Param1 = 1, Param2 = 4},
                    Hp = 5, Atk = 1, Def = 0, Imm = 15, Spd = 3.7, Value = 32, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/troll_eater.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 45, spd: 0.2), DoBoost(3, 115, hp: 1), DoBoost(4, 290, imm: 15),
                        DoBoost(5, 1100, p: Passive.BLEED, p1: 2, p2: 3, name: "Cannibal Troll"), DoBoost(6, 0, atk: 1, spd: 0.4, name: "Eat You Troll"),
                        DoBoost(7, 0, hp: 2, spd: 0.2, imm: 10, name: "Will Eat You Troll"), DoBoost(8, 0, hp: 2, p: Passive.BLEED, p1: 3, p2: 4, name: "Jekyll & Hyde Troll"),
                    }
                },
                new Card
                {
                    Name = "Quango", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.FLEE, Param1 = 50},
                    Hp = 5, Atk = 2, Def = 0, Imm = 0, Spd = 2, Value = 27, BaseExp = 24, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/quango.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.15), DoBoost(3, 95, spd: 0.15), DoBoost(4, 250, spd: 0.1),
                        DoBoost(5, 650, p: Passive.FLEE, p1: 35, name: "Quangooo"), DoBoost(6, 0, atk: 1, spd: 0.3, name: "Quangou"),
                        DoBoost(7, 0, hp: 3, name: "Giga Quangou"), DoBoost(8, 0, p: Passive.FLEE, p1: 5, name: "Not Today Quangou"),
                    }
                },
                new Card
                {
                    Name = "Mosquito", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WIND, Passive = null,
                    Hp = 3, Atk = 2, Def = 0, Imm = 25, Spd = 1.8, Value = 21, BaseExp = 20, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/mosquito.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, imm: 15), DoBoost(3, 55, imm: 20), DoBoost(4, 130, spd: 0.1),
                        DoBoost(5, 450, hp: 1, name: "Great Mosquito"), DoBoost(6, 4000, hp: 1, def: 1, spd: 0.1, name: "Imperial Mosquito"),
                        DoBoost(7, 0, hp: 3, name: "Tank Mosquito"), DoBoost(8, 0, spd: 0.6, name: "Still More Fastquito"),
                    }
                },
                new Card
                {
                    Name = "Dawn Duck", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.HEAL, Element = Element.WIND,
                    Passive = null,
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 2.4, Value = 22, BaseExp = 21, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dawn_duck.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.2), DoBoost(3, 75, spd: 0.2), DoBoost(4, 170, spd: 0.2),
                        DoBoost(5, 500, hp: 1, spd: 0.2, name: "Dawn Ducker"), DoBoost(6, 0, hp: 2, atk: 1, spd: 0.3, name: "The Ducker"),
                        DoBoost(7, 0, spd: 0.7, name: "SpeeDucker"), DoBoost(8, 0, p: Passive.DISPELL, name: "Dispellducker"),
                    }
                },
                new Card
                {
                    Name = "Ghost Skull", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.GHOST},
                    Hp = 2, Atk = 1, Def = 0, Imm = 0, Spd = 2.5, Value = 25, BaseExp = 24, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ghost_skull.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 95, imm: 15), DoBoost(4, 250, imm: 10),
                        DoBoost(5, 850, hp: 1, name: "Ghost Calavery"), DoBoost(6, 0, hp: 1, atk: 1, spd: 0.3, name: "Skuller"),
                        DoBoost(7, 0, spd: 0.7, name: "Skuller+"), DoBoost(8, 0, hp: 1, atk: 1, spd: 0.3, name: "The Incredible Skuller"),
                    }
                },
                new Card
                {
                    Name = "Imp", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = null,
                    Hp = 6, Atk = 3, Def = 0, Imm = 0, Spd = 5.5, Value = 30, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/imp.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 40, spd: 0.3), DoBoost(3, 110, imm: 20), DoBoost(4, 290, atk: 1), DoBoost(5, 1150, hp: 1, name: "Feline Imp"),
                        DoBoost(6, 0, hp: 1, imm: 20, spd: 0.4, name: "Felimp"),
                        DoBoost(7, 0, spd: 1.4, name: "Rapimp"), DoBoost(8, 0, atk: 2, name: "Rapimpale"),
                    }
                },
                new Card
                {
                    Name = "Succubus", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 15},
                    Hp = 5, Atk = 1, Def = 0, Imm = 15, Spd = 3, Value = 24, BaseExp = 24, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/succubus.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.2), DoBoost(3, 60, imm: 15), DoBoost(4, 170, hp: 1), DoBoost(5, 650, hp: 1, spd: 0.3, name: "Great Succubus"),
                        DoBoost(6, 0, p: Passive.DODGE, p1: 30, name: "Dodger Succubusss"),
                        DoBoost(7, 0, atk: 1, spd: 0.5, name: "Sucuboss"), DoBoost(8, 0, hp: 2, atk: 1, spd: 0.3, imm: 15, name: "SucubuX"),
                    }
                },
                new Card
                {
                    Name = "Fear Mage", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.BLOWMARK, Param1 = 1},
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 2.5, Value = 29, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fear_mage.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.2), DoBoost(3, 80, imm: 20), DoBoost(4, 225, hp: 1), DoBoost(5, 825, imm: 20, name: "Blow Mage"),
                        DoBoost(6, 7000, p: Passive.BLOWMARK, p1: 2, name: "Blowmind Mage"), DoBoost(7, 0, hp: 2, spd: 0.4, name: "Blowhead Mage"),
                        DoBoost(8, 0, hp: 1, atk: 1, spd: 0.5, name: "Blowbody Mage"),
                    }
                },
                new Card
                {
                    Name = "Twin Bone", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.DOUBLE_ATTACK, Param1 = 100},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 2.4, Value = 24, BaseExp = 22, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/twin_bone.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 22, spd: 0.2), DoBoost(3, 60, imm: 15), DoBoost(4, 160, hp: 1), DoBoost(5, 550, spd: 0.5, name: "HurTwin Bone"),
                        DoBoost(6, 0, atk: 1, name: "Twin Skeleton"), DoBoost(7, 0, hp: 1, spd: 0.3, name: "Twin Xeleton"),
                        DoBoost(8, 0, hp: 2, def: 1, spd: 0.2, name: "Twin SKXeleton"),
                    }
                },
                new Card
                {
                    Name = "One Eye Mage", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BOUNCE, Param1 = 1},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 3.6, Value = 23, BaseExp = 23, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/one_eye_mage.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, spd: 0.2), DoBoost(3, 70, hp: 1), DoBoost(4, 220, spd: 0.4),
                        DoBoost(5, 600, p: Passive.BOUNCE, p1: 2, name: "Almost-One Eye Mage"), DoBoost(6, 5000, spd: 0.3, hp: 1, p1: 2, name: "Two Eye Mage"),
                        DoBoost(7, 0, hp: 2, spd: 0.4, name: "Elder Two Eye Mage"), DoBoost(8, 0, hp: 1, atk: 1, spd: 0.3, imm: 10, name: "Malkito"),
                    }
                },
                new Card
                {
                    Name = "Moon Elf", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 60},
                    Hp = 2, Atk = 1, Def = 0, Imm = 0, Spd = 2.1, Value = 26, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/moon_elf.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.1), DoBoost(3, 90, p: Passive.DODGE, p1: 67), DoBoost(4, 270, hp: 1),
                        DoBoost(5, 920, p: Passive.DODGE, p1: 75, name: "Light Moon Elf"), DoBoost(6, 0, spd: 0.4, hp: 1, name: "Moonyelf"),
                        DoBoost(7, 0, atk: 1, def: 1, name: "Moonlight Elf"), DoBoost(8, 0, hp: 2, spd: 0.2, imm: 10, name: "Radiant Moon Elf"),
                    }
                },
                new Card
                {
                    Name = "Nomad Mage", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BLIND, Param1 = 25, Param2 = 3},
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 3.1, Value = 31, BaseExp = 27, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/nomad_mage.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 100, imm: 20), DoBoost(4, 285, hp: 1),
                        DoBoost(5, 1100, p: Passive.BLIND, p1: 35, p2: 3, name: "Blind Mage"), DoBoost(6, 0, hp: 1, imm: 20, name: "Blindex Mage"),
                        DoBoost(7, 0, atk: 1, def: 1, name: "Lighex Mage"), DoBoost(8, 0, hp: 2, p: Passive.BLIND, p1: 45, p2: 3, imm: 10, name: "Blindor Great Mage"),
                    }
                },
                new Card
                {
                    Name = "Woodman", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = null,
                    Hp = 4, Atk = 3, Def = 0, Imm = 30, Spd = 4.4, Value = 32, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/woodman.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 40, spd: 0.2), DoBoost(3, 110, imm: 10), DoBoost(4, 300, imm: 10),
                        DoBoost(5, 1200, hp: 1, name: "Woodsuperman"), DoBoost(6, 0, hp: 1, def: 1, name: "Woodyman"),
                        DoBoost(7, 0, atk: 2, name: "Woodhurtman"), DoBoost(8, 0, hp: 1, def: 1, spd: 1.1, name: "Woodgreatman"),
                    }
                },
                new Card
                {
                    Name = "Snow Rat", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.FREEZE, Param1 = 12, Param2 = 5},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 2.3, Value = 18, BaseExp = 16, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/snow_rat.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 10, spd: 0.1), DoBoost(3, 28, imm: 10), DoBoost(4, 75, hp: 1), DoBoost(5, 250, p: Passive.FREEZE, p1: 18, p2: 5, name: "Freezing Rat"),
                        DoBoost(6, 0, hp: 1, def: 1, name: "Cold Rat"), DoBoost(7, 0, hp: 2, name: "Giant Cold Rat"), DoBoost(8, 0, spd: 1.3, name: "Freezeall Giant Rat"),
                    }
                },
                new Card
                {
                    Name = "Native", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = null,
                    Hp = 3, Atk = 1, Def = 1, Imm = 0, Spd = 3.3, Value = 30, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/native.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 115, imm: 20), DoBoost(4, 300, hp: 1),
                        DoBoost(5, 1150, spd: -0.8, atk: 1, name: "Elite Native"), DoBoost(6, 0, hp: 2, spd: 0.4, name: "Super Native"),
                        DoBoost(7, 0, atk: 2, name: "NativeX"), DoBoost(8, 0, hp: 1, def: 1, spd: 0.5, name: "Insane Native"),
                    }
                },
                new Card
                {
                    Name = "Snowy Leopard", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.DEVIANT, Param1 = 2},
                    Hp = 4, Atk = 0, Def = 0, Imm = 0, Spd = 1.8, Value = 27, BaseExp = 24, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/snowy_leopard.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.1), DoBoost(3, 85, spd: 0.1), DoBoost(4, 250, spd: 0.1), DoBoost(5, 700, p: Passive.DEVIANT, p1: 3, name: "Snowy Leopardus"),
                        DoBoost(6, 6000, hp: 1, spd: 0.3, p1: 3, name: "Dangersnow Leopardus"), DoBoost(7, 0, atk: 1, hp: 1, spd: 0.2, name: "Great Snow Leopard"),
                        DoBoost(8, 0, hp: 2, def: 1, p: Passive.DEVIANT, p1: 4, name: "The Snow Leopard"),
                    }
                },
                new Card
                {
                    Name = "Crabber", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = null,
                    Hp = 7, Atk = 1, Def = 1, Imm = 0, Spd = 3.8, Value = 29, BaseExp = 25, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/crabber.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.3), DoBoost(3, 110, imm: 15), DoBoost(4, 285, hp: 1), DoBoost(5, 950, hp: 1, name: "Crabberous"),
                        DoBoost(6, 0, hp: 3, name: "Crawbber"), DoBoost(7, 0, atk: 1, spd: 0.4, name: "Crawbberous"),
                        DoBoost(8, 0, hp: 2, def: 1, imm: 20, name: "Crametalbber"),
                    }
                },
                new Card
                {
                    Name = "Ogre", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.IGNORE_DEF},
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 3.3, Value = 25, BaseExp = 25, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ogre.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.2), DoBoost(3, 100, imm: 15), DoBoost(4, 280, hp: 1), DoBoost(5, 850, spd: 0.6, name: "Enhanced Ogre"),
                        DoBoost(6, 0, hp: 1, atk: 1, spd: 0.3, name: "Ogre Killer"), DoBoost(7, 0, hp: 2, def: 1, name: "High Ogre Killer"),
                        DoBoost(8, 0, hp: 1, atk: 1, spd: 0.2, name: "Massacre Ogre"),
                    }
                },
                new Card
                {
                    Name = "Fire Snake", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BLAZE, Param1 = 1, Param2 = 2.5},
                    Hp = 3, Atk = 0, Def = 1, Imm = 30, Spd = 2.3, Value = 23, BaseExp = 22, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_snake.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.1), DoBoost(3, 95, imm: 15), DoBoost(4, 275, hp: 1),
                        DoBoost(5, 1050, p: Passive.BLAZE, p1: 1, p2: 4, name: "Blazing Snake"), DoBoost(6, 0, hp: 2, imm: 10, name: "BlaHigh Snake"),
                        DoBoost(7, 0, hp: 1, atk: 2, name: "Burning Snake"), DoBoost(8, 0, spd: 0.4, p: Passive.BLAZE, p1: 2, p2: 4, name: "Blaze You Snake"),
                    }
                },
                new Card
                {
                    Name = "Fire Imp", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.ABLAZE, Param1 = 1},
                    Hp = 4, Atk = 1, Def = 0, Imm = 20, Spd = 2.5, Value = 29, BaseExp = 27, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_imp.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 110, imm: 10), DoBoost(4, 300, hp: 1), DoBoost(5, 1000, spd: 0.5, name: "Annoying Fire Imp"),
                        DoBoost(6, 9000, p: Passive.ABLAZE, p1: 2, name: "Untouchable Fire Imp"),
                        DoBoost(7, 0, hp: 3, imm: 10, name: "Higher Demonic Imp"), DoBoost(8, 0, p: Passive.ABLAZE, p1: 3, def: 1, name: "ABlazing ALL Imp"),
                    }
                },
                new Card
                {
                    Name = "Scorched Goblin", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.SCORCHED, Param1 = 1, Param2 = 1},
                    Hp = 4, Atk = 1, Def = 0, Imm = 30, Spd = 2.4, Value = 32, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/scorched_goblin.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 45, spd: 0.2), DoBoost(3, 130, imm: 10), DoBoost(4, 370, hp: 1), DoBoost(5, 1250, p: Passive.SCORCHED, p1: 1, p2: 1.6, name: "Scorchering Goblin"),
                        DoBoost(6, 0, hp: 1, imm: 5, spd: 0.3, name: "Scorchthem Goblin"), DoBoost(7, 0, hp: 1, atk: 1, spd: 0.2, def: 1, name: "Scorchall Goblin"),
                        DoBoost(8, 0, p: Passive.SCORCHED, p1: 2, p2: 1.9, hp: 1, name: "Elite Scorch Goblin"),
                    }
                },
                new Card
                {
                    Name = "Shaman", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.OBLIVION, Param1 = 9},
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 3.1, Value = 28, BaseExp = 23, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/shaman.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 100, imm: 20), DoBoost(4, 260, hp: 1), DoBoost(5, 850, p: Passive.OBLIVION, p1: 13, name: "Oblivion Shaman"),
                        DoBoost(6, 0, spd: 0.6, hp: 1, name: "Fastix Shaman"), DoBoost(7, 0, p: Passive.OBLIVION, p1: 17, atk: 1, name: "Killer Shaman"),
                        DoBoost(8, 0, p: Passive.TRANSFUSE, p1: 100, hp: 1, name: "Souleater Shaman"),
                    }
                },
                new Card
                {
                    Name = "Slime", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = null,
                    Hp = 10, Atk = 1, Def = 0, Imm = 0, Spd = 4.1, Value = 31, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/slime.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 45, spd: 0.2), DoBoost(3, 125, imm: 10), DoBoost(4, 340, spd: 0.3), DoBoost(5, 1400, hp: 2, name: "Slimen"), DoBoost(6, 10000, hp: 3, name: "Sliking"),
                        DoBoost(7, 0, atk: 2, def: 1, imm: 10, name: "Slimonster"), DoBoost(8, 0, hp: 7, name: "City Invade Slime"),
                    }
                },
                new Card
                {
                    Name = "Serpentine", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.IMMUNE},
                    Hp = 4, Atk = 1, Def = 1, Imm = 0, Spd = 3.7, Value = 33, BaseExp = 29, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/serpentine.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 50, spd: 0.2), DoBoost(3, 140, spd: 0.2), DoBoost(4, 400, hp: 1), DoBoost(5, 1700, atk: 1, spd: -1, name: "Busterserpent"),
                        DoBoost(6, 0, atk: 1, hp: 1, spd: 0.2, name: "SerpenT"), DoBoost(7, 0, hp: 2, spd: 0.5, name: "Warrior Serpentine"),
                        DoBoost(8, 0, def: 1, hp: 1, spd: 0.3, name: "The Final Serpentine"),
                    }
                },
                new Card
                {
                    Name = "Plantanous", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.FLAMMABLE},
                    Hp = 5, Atk = 1, Def = 0, Imm = 25, Spd = 3.3, Value = 33, BaseExp = 29, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/plantanous.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 50, spd: 0.2), DoBoost(3, 140, spd: 0.2), DoBoost(4, 400, hp: 1), DoBoost(5, 1700, atk: 1, spd: -1, name: "Swamptanous"),
                        DoBoost(6, 0, hp: 3, name: "PlantNSwamp"), DoBoost(7, 0, hp: 1, imm: 35, name: "Can You Burn Plant?"),
                        DoBoost(8, 0, atk: 1, spd: 1.2, name: "Attackplantous"),
                    }
                },

                //RARE TIER 1

                new Card
                {
                    Name = "Fire Wraith", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BLAZE, Param1 = 1, Param2 = 2.2},
                    Hp = 5, Atk = 1, Def = 0, Imm = 30, Spd = 2.6, Value = 32, BaseExp = 30, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_wraith.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 45, spd: 0.1), DoBoost(3, 125, hp: 1), DoBoost(4, 445, def: 1),
                        DoBoost(5, 1700, p: Passive.BLAZE, p1: 2, p2: 2.5, name: "Blazing Wraith"), DoBoost(6, 0, hp: 2, spd: 0.3, name: "Burning Wraith"),
                        DoBoost(7, 0, atk: 1, hp: 1, imm: 30, name: "Tough Burn Wraith"), DoBoost(8, 0, p: Passive.BLAZE, p1: 3, p2: 2.8, name: "Hellfire Wraith"),
                    }
                },
                new Card
                {
                    Name = "Burning Bird", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BURN, Param1 = 40, Param2 = 1, Param3 = 2},
                    Hp = 4, Atk = 1, Def = 0, Imm = 35, Spd = 1.9, Value = 37, BaseExp = 33, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/burning_bird.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 55, spd: 0.2), DoBoost(3, 145, hp: 1), DoBoost(4, 520, hp: 1),
                        DoBoost(5, 2600, p: Passive.BURN, p1: 60, p2: 1, p3: 1, name: "Blazing Bird"), DoBoost(6, 15000, def: 1, name: "Tough Burnbird"),
                        DoBoost(7, 0, atk: 1, hp: 1, imm: 10, spd: 0.2, name: "Burnerbird"), DoBoost(8, 0, p: Passive.MELT, p1: 1, hp: 1, spd: 0.2, name: "Meltingbird"),
                    }
                },
                new Card
                {
                    Name = "Demon", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.MELT, Param1 = 1},
                    Hp = 6, Atk = 1, Def = 0, Imm = 15, Spd = 2.8, Value = 45, BaseExp = 38, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/demon.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 85, spd: 0.15), DoBoost(3, 255, hp: 1), DoBoost(4, 750, atk: 1),
                        DoBoost(5, 3700, spd: 0.5, imm: 15, name: "Melting Demon"), DoBoost(6, 0, hp: 2, atk: 1, name: "Melt You Demon"),
                        DoBoost(7, 0, hp: 2, def: 1, spd: 0.4, name: "Hell Demon"), DoBoost(8, 0, p: Passive.MELT, p1: 2, hp: 1, name: "Hellfire Demon"),
                    }
                },
                new Card
                {
                    Name = "Triton", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.HEAL, Element = Element.WATER,
                    Passive = null,
                    Hp = 6, Atk = 2, Def = 0, Imm = 0, Spd = 3.2, Value = 40, BaseExp = 36, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/triton.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 75, spd: 0.2), DoBoost(3, 210, hp: 1), DoBoost(4, 580, def: 1), DoBoost(5, 2800, atk: 1, name: "TRIton"),
                        DoBoost(6, 16000, spd: 0.8, hp: 1, name: "HealTRIton"),
                        DoBoost(7, 0, hp: 1, def: 1, atk: 1, name: "MegaTRIton"), DoBoost(8, 0, imm: 35, hp: 2, name: "Poseidon"),
                    }
                },
                new Card
                {
                    Name = "Ice Skeleton", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.RANDOM, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.PIERCING, Param1 = 1},
                    Hp = 4, Atk = 2, Def = 1, Imm = 0, Spd = 4, Value = 45, BaseExp = 40, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ice_skeleton.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 80, spd: 0.2), DoBoost(3, 240, imm: 25), DoBoost(4, 700, hp: 1), DoBoost(5, 3250, atk: 1, name: "Brute Ice Skeleton"),
                        DoBoost(6, 0, hp: 2, spd: 0.2, def: 1, name: "Armored Ice Skeleton"),
                        DoBoost(7, 0, p: Passive.PIERCING, p1: 2, spd: 0.4, name: "Crystal Skeleton"), DoBoost(8, 0, hp: 2, atk: 1, imm: 15, name: "Cryogen Skeleton"),
                    }
                },
                new Card
                {
                    Name = "Licezard", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.FREEZE, Param1 = 20, Param2 = 2.5},
                    Hp = 5, Atk = 2, Def = 0, Imm = 30, Spd = 2.3, Value = 38, BaseExp = 34, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/licezard.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 60, spd: 0.15), DoBoost(3, 175, imm: 10), DoBoost(4, 520, hp: 1),
                        DoBoost(5, 2650, atk: 1, name: "Sharping Licezard"), DoBoost(6, 0, p: Passive.FREEZE, hp: 1, p1: 27, p2: 2.8, name: "Colding Licezard"),
                        DoBoost(7, 0, hp: 1, def: 1, spd: 0.4, name: "Crystal Licezard"), DoBoost(8, 0, hp: 2, atk: 1, imm: 10, spd: 0.3, name: "Kingcezard"),
                    }
                },
                new Card
                {
                    Name = "Grunt", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NONE, Passive = null,
                    Hp = 6, Atk = 1, Def = 1, Imm = 30, Spd = 3.8, Value = 30, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/grunt.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 55, spd: 0.2), DoBoost(3, 145, imm: 10), DoBoost(4, 470, hp: 1), DoBoost(5, 1950, p: Passive.BACKTRACK, p1: 1, name: "Time-Shift Grunt"),
                        DoBoost(6, 0, hp: 1, atk: 1, spd: 0.4, name: "Forcegrunt"),
                        DoBoost(7, 0, spd: 0.7, def: 1, name: "Gruntfos"), DoBoost(8, 0, hp: 2, atk: 1, p: Passive.BACKTRACK, p1: 1.4, name: "Eldergrunt"),
                    }
                },
                new Card
                {
                    Name = "Ragemonkey", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.BERSEKER, Param1 = 2, Param2 = 1.1},
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 3.7, Value = 40, BaseExp = 34, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ragemonkey.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 70, spd: 0.1), DoBoost(3, 200, hp: 1), DoBoost(4, 580, spd: 0.1), DoBoost(5, 2300, atk: 1, name: "Brute Ragemonkey"),
                        DoBoost(6, 17000, hp: 2, name: "Giant Ragemonkey"), DoBoost(7, 0, spd: 0.5, name: "Gorillamonkey"),
                        DoBoost(8, 0, hp: 3, p: Passive.BERSEKER, p1: 2, p2: 1.4, name: "Gorillamonster"),
                    }
                },
                new Card
                {
                    Name = "Shadelf", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.TIMESHIFT, Param1 = 22},
                    Hp = 6, Atk = 1, Def = 0, Imm = 25, Spd = 1.9, Value = 46, BaseExp = 37, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/shadelf.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 80, imm: 10), DoBoost(3, 230, hp: 1), DoBoost(4, 650, spd: 0.1), DoBoost(5, 3000, p: Passive.TIMESHIFT, p1: 30, name: "Shadingelf"),
                        DoBoost(6, 0, hp: 1, atk: 1, name: "Elf In Shadows"), DoBoost(7, 0, spd: 0.2, hp: 1, imm: 15, name: "Timeshadelf"),
                        DoBoost(8, 0, hp: 1, p: Passive.TIMESHIFT, p1: 37, name: "Your Time Is Minelf"),
                    }
                },
                new Card
                {
                    Name = "Poison Mosquito", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.POISON, Param1 = 1, Param2 = 2, Param3 = 4},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 1.7, Value = 33, BaseExp = 29, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/poison_mosquito.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 60, spd: 0.1), DoBoost(3, 190, imm: 25), DoBoost(4, 550, spd: 0.2), DoBoost(5, 2400, hp: 1, name: "Great Poison Mosquito"),
                        DoBoost(6, 14000, hp: 1, def: 1, spd: 0.1, name: "Imperial Poison Mosquito"), DoBoost(7, 0, spd: 0.3, hp: 1, name: "Faster Poison Mosquito"),
                        DoBoost(8, 0, hp: 1, p: Passive.POISON, p1: 2, p2: 1.6, p3: 3.2, name: "Venom Mosquito"),
                    }
                },
                new Card
                {
                    Name = "Crow", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 30},
                    Hp = 5, Atk = 1, Def = 0, Imm = 50, Spd = 2.5, Value = 36, BaseExp = 31, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/crow.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 65, spd: 0.2), DoBoost(3, 200, spd: 0.2), DoBoost(4, 700, atk: 1), DoBoost(4, 1000, p: Passive.DODGE, p1: 40, name: "Fast Crow"),
                        DoBoost(5, 3000, p: Passive.DODGE, p1: 50, name: "Lighting Crow"), DoBoost(6, 0, hp: 1, spd: 0.6, name: "Elite Crow"),
                        DoBoost(7, 0, hp: 2, def: 1, spd: 0.2, name: "Elder Crow"), DoBoost(8, 0, hp: 4, name: "Giga Crow"),
                    }
                },
                new Card
                {
                    Name = "Fairy", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.CONFUSSION, Param1 = 0.7},
                    Hp = 4, Atk = 1, Def = 0, Imm = 40, Spd = 2.9, Value = 40, BaseExp = 34, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fairy.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 75, spd: 0.2), DoBoost(3, 220, spd: 0.3), DoBoost(4, 640, hp: 1), DoBoost(5, 2700, def: 1, name: "The Fairy"),
                        DoBoost(6, 0, hp: 1, spd: 0.3, name: "Speedy Fairy"), DoBoost(7, 0, hp: 1, atk: 1, def: 1, name: "Elder Fairy"),
                        DoBoost(8, 0, hp: 2, p: Passive.CONFUSSION, p1: 0.9, name: "Skyfairy"),
                    }
                },
                new Card
                {
                    Name = "Reptillion", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.POISON, Param1 = 2, Param2 = 3, Param3 = 6 },
                    Hp = 6, Atk = 1, Def = 0, Imm = 0, Spd = 4.2, Value = 40, BaseExp = 35, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/reptillion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 70, spd: 0.3), DoBoost(3, 230, hp: 1), DoBoost(4, 750, spd: 0.3),
                        DoBoost(5, 1900, p: Passive.POISON, p1: 2, p2: 2.5, p3: 5, name: "Poisonus Reptillion"), DoBoost(6, 0, hp: 1, spd: 0.5, name: "Reptillione"),
                        DoBoost(7, 0, hp: 1, atk: 1, imm: 20, name: "Al Reptillione"), DoBoost(8, 0, hp: 2, spd: 0.7, name: "Al Poisiniolle"),
                    }
                },
                new Card
                {
                    Name = "Swamp Reptillion", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.KNOCKOUT, Param1 = 2},
                    Hp = 4, Atk = 1, Def = 1, Imm = 25, Spd = 3.2, Value = 44, BaseExp = 37, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/swamp_reptillion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 80, spd: 0.2), DoBoost(3, 250, hp: 1), DoBoost(4, 800, imm: 10), DoBoost(5, 3000, hp: 1, name: "Swamp Reptile"),
                        DoBoost(6, 18000, p: Passive.KNOCKOUT, p1: 3, name: "Killer Reptillion"),
                        DoBoost(7, 0, hp: 2, def: 1, imm: 10, name: "Killtough Reptillion"), DoBoost(8, 0, hp: 3, spd: 0.4, name: "Kill Bill Reptillion"),
                    }
                },
                new Card
                {
                    Name = "Demon Frog", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.RANDOM, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.REGURGITATE, Param1 = 1},
                    Hp = 4, Atk = 2, Def = 0, Imm = 25, Spd = 2.2, Value = 41, BaseExp = 35, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/demon_frog.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 70, spd: 0.1), DoBoost(3, 220, imm: 15), DoBoost(4, 690, hp: 1), DoBoost(5, 2800, spd: 0.4, name: "Killer Frog"),
                        DoBoost(6, 0, p: Passive.REGURGITATE, p1: 2, atk: 1, name: "Demonic Frog"),
                        DoBoost(7, 0, hp: 3, name: "Giant Demon Toad"), DoBoost(8, 0, hp: 2, spd: 0.3, def: 1, name: "Gigantic & Demonic Toad"),
                    }
                },
                new Card
                {
                    Name = "Lesser Vampire", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.LIFESTEAL, Param1 = 50},
                    Hp = 5, Atk = 2, Def = 0, Imm = 25, Spd = 3.1, Value = 35, BaseExp = 32, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/lesser_vampire.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 65, spd: 0.2), DoBoost(3, 180, imm: 10), DoBoost(4, 625, hp: 1), DoBoost(5, 2200, atk: 1, name: "Middle Vampire"),
                        DoBoost(6, 12500, p: Passive.LIFESTEAL, p1: 75, name: "Hungry Middle Vampire"),
                        DoBoost(7, 0, hp: 2, spd: 0.2, atk: 1, name: "Not A Lesser Vampire"), DoBoost(8, 0, hp: 4, def: 1, spd: 0.3, name: "Giant Vampire"),
                    }
                },
                new Card
                {
                    Name = "West Dark Justicer", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.BOUNCE, Param1 = 2},
                    Hp = 5, Atk = 2, Def = 0, Imm = 0, Spd = 4.4, Value = 42, BaseExp = 36, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/west_dark_justicer.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 80, spd: 0.3), DoBoost(3, 270, spd: 0.3), DoBoost(4, 850, hp: 1),
                        DoBoost(5, 3200, p: Passive.BOUNCE, p1: 3, name: "Dark Justicerman"), DoBoost(6, 0, hp: 1, spd: 0.7, name: "Void Justicer"),
                        DoBoost(7, 0, hp: 2, spd: 0.4, atk: 1, name: "Dark Void Justicer"), DoBoost(8, 0, imm: 40, hp: 1, spd: 0.3, name: "Voidless Justicer"),
                    }
                },
                new Card
                {
                    Name = "Abomination", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.BLEED, Param1 = 1, Param2 = 1.5},
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 1.6, Value = 44, BaseExp = 38, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/abomination.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 85, spd: 0.1), DoBoost(3, 280, hp: 1), DoBoost(4, 880, imm: 25), DoBoost(5, 3100, atk: 1, name: "Woundernation"),
                        DoBoost(6, 0, hp: 1, spd: 0.1, imm: 10, name: "Abominable"), DoBoost(7, 0, hp: 1, spd: 0.1, atk: 1, name: "Abominabless"),
                        DoBoost(8, 0, hp: 3, spd: 0.1, name: "The Abominator"),
                    }
                },
                new Card
                {
                    Name = "Guard Minion", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.REFLECT, Param1 = 35},
                    Hp = 4, Atk = 1, Def = 1, Imm = 0, Spd = 2.9, Value = 43, BaseExp = 37, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/guard_minion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 85, spd: 0.2), DoBoost(3, 290, hp: 1), DoBoost(4, 900, spd: 0.3), DoBoost(5, 3200, atk: 1, hp: 1, spd: 0.2, name: "Great Guard Minion"),
                        DoBoost(6, 21000, p: Passive.REFLECT, p1: 50, name: "Great Reflect Minion"), DoBoost(7, 0, hp: 2, spd: 0.3, atk: 1, name: "ReflectAll Minion"),
                        DoBoost(8, 0, imm: 40, spd: 0.4, hp: 1, name: "ReflectNDisable Minion"),
                    }
                },
                new Card
                {
                    Name = "Lightguard", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BOUNCE, Param1 = 1},
                    Hp = 3, Atk = 2, Def = 2, Imm = 15, Spd = 4.7, Value = 47, BaseExp = 38, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/lightguard.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 95, spd: 0.3), DoBoost(3, 320, spd: 0.25), DoBoost(4, 1060, hp: 1),
                        DoBoost(5, 4600, hp: 2, name: "LightGreatGuard"), DoBoost(6, 0, spd: 0.6, hp: 1, name: "Lighting Guard"),
                        DoBoost(7, 0, p: Passive.BOUNCE, p1: 2, spd: 0.3, name: "Bounce Guard"), DoBoost(8, 0, hp: 2, def: 1, spd: 0.6, name: "Bounclight Guard"),
                    }
                },
                new Card
                {
                    Name = "Armored Defender", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.DOUBLE_ATTACK, Param1 = 100},
                    Hp = 5, Atk = 2, Def = 1, Imm = 0, Spd = 5, Value = 45, BaseExp = 37, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/armored_defender.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 90, spd: 0.3), DoBoost(3, 300, hp: 1), DoBoost(4, 970, spd: 0.3), DoBoost(5, 3400, spd: 0.8, name: "Armoredcore Defender"),
                        DoBoost(6, 0, spd: 0.7, hp: 1, name: "Twin Defender"), DoBoost(7, 0, def: 1, imm: 25, name: "Twinguard Defender"),
                        DoBoost(8, 0, atk: 1, name: "Twindualguard Defender"),
                    }
                },
                new Card
                {
                    Name = "Golem", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = null,
                    Hp = 3, Atk = 3, Def = 2, Imm = 0, Spd = 6.1, Value = 40, BaseExp = 35, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 70, spd: 0.5), DoBoost(3, 245, hp: 1), DoBoost(4, 740, spd: 0.6), DoBoost(5, 2600, def: 1, name: "Hard Golem"),
                        DoBoost(6, 16500, hp: 1, atk: 1, name: "Durable Golem"), DoBoost(7, 0, hp: 2, spd: 0.5, name: "Great Golem"),
                        DoBoost(8, 0, hp: 1, atk: 1, spd: 1.5, imm: 25, name: "Gorgeous Golem"),
                    }
                },
                new Card
                {
                    Name = "Spidor", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 45},
                    Hp = 3, Atk = 1, Def = 0, Imm = 0, Spd = 1.8, Value = 49, BaseExp = 41, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/spidor.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 105, spd: 0.1), DoBoost(3, 360, imm: 15), DoBoost(4, 1300, hp: 1), DoBoost(5, 3800, p: Passive.DODGE, p1: 65, name: "Almost-Invisible Spidor"),
                        DoBoost(6, 0, hp: 1, atk: 1, spd: 0.20, name: "SpidX"), DoBoost(7, 0, hp: 1, spd: 0.20, name: "SpidXor"),
                        DoBoost(8, 0, hp: 1, imm: 20, spd: 0.20, name: "SpiderX"),
                    }
                },
                new Card
                {
                    Name = "Sand Golem", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.ALL, Element = Element.EARTH,
                    Passive = null,
                    Hp = 6, Atk = 1, Def = 2, Imm = 0, Spd = 5.5, Value = 44, BaseExp = 38, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/sand_golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 85, spd: 0.4), DoBoost(3, 285, hp: 1), DoBoost(4, 890, spd: 0.5), DoBoost(5, 2600, hp: 3, name: "Giant Sand Golem"),
                        DoBoost(6, 0, hp: 2, atk: 1, name: "Sander Golem"), DoBoost(7, 0, spd: 1.1, imm: 25, name: "Gigas Sand Golem"),
                        DoBoost(8, 0, hp: 5, name: "Entirely Giant Sand Golem"),
                    }
                },

                // TIER 1 SPECIAL

                new Card
                {
                    Name = "Blazing Golem", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.ABLAZE, Param1 = 1},
                    Hp = 4, Atk = 2, Def = 2, Imm = 0, Spd = 5.2, Value = 62, BaseExp = 46, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/blazing_golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 110, spd: 0.5), DoBoost(3, 350, hp: 1), DoBoost(4, 1100, spd: 0.6), DoBoost(5, 6500, def: 1, name: "Hard Blazing Golem"),
                        DoBoost(6, 16500, hp: 1, atk: 1, name: "Durable Blazing Golem"), DoBoost(7, 0, hp: 1, spd: 1.4, name: "Calcinating Golem"),
                        DoBoost(8, 0, hp: 2, atk: 1, spd: 0.5, name: "Calcinator Golem"),
                    }
                },
                new Card
                {
                    Name = "Fire Golem", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BURNOUT, Param1 = 1, Param2 = 3},
                    Hp = 6, Atk = 1, Def = 0, Imm = 20, Spd = 3.1, Value = 77, BaseExp = 54, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 145, spd: 0.3), DoBoost(3, 480, hp: 1), DoBoost(4, 1650, imm: 15), DoBoost(5, 9000, p: Passive.BURNOUT, p1: 2, p2: 2.5, name: "Burnout Golem"),
                        DoBoost(6, 0, hp: 2, spd: 0.4, imm: 10, name: "Firing Golem"),
                        DoBoost(7, 0, p: Passive.BURNOUT, p1: 3, p2: 2, name: "Fireburn Golem"), DoBoost(8, 0, hp: 3, atk: 1, spd: 0.3, name: "Fireburner Golem"),
                    }
                },
                new Card
                {
                    Name = "Dark Eye Fighter", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.SHATTER, Param1 = 10},
                    Hp = 6, Atk = 1, Def = 0, Imm = 0, Spd = 2, Value = 65, BaseExp = 48, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dark_eye_fighter.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 120, spd: 0.1), DoBoost(3, 380, spd: 0.5), DoBoost(4, 1200, spd: 0.1, hp: 1),
                        DoBoost(5, 6400, p: Passive.SHATTER, p1: 15, name: "Dark Eye Breaker"), DoBoost(6, 0, hp: 1, spd: 0.2, imm: 25, name: "Void Eye Fighter"),
                        DoBoost(7, 0, p: Passive.SHATTER, p1: 20, name: "Void Eye Breaker"), DoBoost(8, 0, hp: 1, spd: 0.3, def: 1, name: "Insane Void Eye Breaker"),
                    }
                },
                new Card
                {
                    Name = "Moon Vampire", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.REGURGITATE, Param1 = 2},
                    Hp = 7, Atk = 2, Def = 0, Imm = 20, Spd = 2.7, Value = 70, BaseExp = 50, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/moon_vampire.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 130, spd: 0.2), DoBoost(3, 420, imm: 10), DoBoost(4, 1550, hp: 2),
                        DoBoost(5, 7200, atk: 1, name: "Moonlight Vampire"), DoBoost(6, 33000, p: Passive.REGURGITATE, p1: 3, name: "Darkmoon Vampire"),
                        DoBoost(7, 0, hp: 3, imm: 10, name: "Gigantic Darkmoon Vampire"), DoBoost(8, 0, hp: 5, def: 1, name: "Colossal Darkmoon Vampire"),
                    }
                },
                new Card
                {
                    Name = "RockIT", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.RANDOM, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.TOUGH, Param1 = 50, Param2 = 1},
                    Hp = 10, Atk = 3, Def = 1, Imm = 0, Spd = 8, Value = 80, BaseExp = 55, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/blazing_golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 180, spd: 0.5), DoBoost(3, 580, hp: 2), DoBoost(4, 1900, atk: 1), DoBoost(5, 11000, atk: 1, hp: 2, name: "RockTHEM"),
                        DoBoost(6, 40000, p: Passive.TOUGH, p1: 60, p2: 2, name: "RockROCK"),
                        DoBoost(7, 0, hp: 4, imm: 25, name: "RockOnMe"), DoBoost(8, 0, hp: 2, atk: 2, spd: 1, name: "RockHigh"),
                    }
                },
                new Card
                {
                    Name = "Rune Golem", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.RUNEBREAKER},
                    Hp = 7, Atk = 1, Def = 1, Imm = 50, Spd = 2.8, Value = 65, BaseExp = 47, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/rune_golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 115, spd: 0.2), DoBoost(3, 370, hp: 1), DoBoost(4, 1200, spd: 0.3),
                        DoBoost(5, 6500, def: 1, name: "Durune Golem"), DoBoost(6, 0, hp: 2, atk: 1, name: "Rune Rune Golem"),
                        DoBoost(7, 0, hp: 4, name: "Runeruning Golem"), DoBoost(8, 0, hp: 3, imm: 50, name: "Hit Me Rune Golem"),
                    }
                },
                new Card
                {
                    Name = "Skellrex", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.LIFESTEAL, Param1 = 100},
                    Hp = 10, Atk = 1, Def = -1, Imm = 0, Spd = 1.3, Value = 75, BaseExp = 53, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/skellrex.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 135, spd: 0.1), DoBoost(3, 460, spd: 0.1), DoBoost(4, 1600, atk: 1), DoBoost(5, 9500, hp: 3, name: "High Skellrex"),
                        DoBoost(6, 33000, imm: 50, name: "Near-Immune Skellrex"), DoBoost(7, 0, hp: 2, spd: 0.1, atk: 1, name: "SkellREX"),
                        DoBoost(8, 0, hp: 3, imm: 17, spd: 0.1, name: "T-SkellREX"),
                    }
                },
                new Card
                {
                    Name = "Lirin", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.DOOM, Param1 = 4},
                    Hp = 9, Atk = 0, Def = 0, Imm = 100, Spd = 6, Value = 70, BaseExp = 52, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/lirin.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 120, spd: 0.3), DoBoost(3, 390, spd: 0.3), DoBoost(4, 1300, spd: 0.4),
                        DoBoost(5, 8000, p: Passive.DOOM, p1: 1.5, name: "Insta-Killirin"), DoBoost(6, 0, hp: 3, def: 1, name: "Lirion"),
                        DoBoost(7, 0, hp: 2, spd: 1, name: "Lirionner"), DoBoost(8, 0, hp: 7, name: "GLirionner"),
                    }
                },
                new Card
                {
                    Name = "Ice Witch", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.WINTER, Param1 = 15},
                    Hp = 7, Atk = 2, Def = 0, Imm = 0, Spd = 2.7, Value = 71, BaseExp = 52, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ice_witch.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 125, spd: 0.2), DoBoost(3, 420, hp: 1), DoBoost(4, 1450, imm: 20),
                        DoBoost(5, 8500, p: Passive.WINTER, p1: 20, name: "Cold Witch"), DoBoost(6, 31000, hp: 1, def: 1, spd: 0.3, name: "Freezing Witch"),
                        DoBoost(7, 0, hp: 2, spd: 0.8, name: "Dangerous Freezing Witch"), DoBoost(8, 0, hp: 2, def: 1, p: Passive.WINTER, p1: 25, name: "Stuck-Colding Witch"),
                    }
                },
                new Card
                {
                    Name = "Little Hidra", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.WATER,
                    Passive = null,
                    Hp = 9, Atk = 1, Def = 0, Imm = 0, Spd = 1.5, Value = 78, BaseExp = 55, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/little_hidra.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 140, spd: 0.1), DoBoost(3, 530, hp: 1), DoBoost(4, 1850, imm: 25),
                        DoBoost(5, 11000, hp: -3, atk: 1, def: 1, name: "Berseker Hidra"), DoBoost(6, 0, hp: 2, def: 1, spd: 0.2, name: "Giga Hidra"),
                        DoBoost(7, 0, hp: 4, imm: 20, name: "Gigantous Hidra"), DoBoost(8, 0, hp: 3, def: 1, name: "Mega Hidra"),
                    }
                },
                new Card
                {
                    Name = "Griffo", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.SURPRISEATTACK},
                    Hp = 6, Atk = 2, Def = 0, Imm = 30, Spd = 2.3, Value = 67, BaseExp = 48, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/griffo.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 110, spd: 0.2), DoBoost(3, 360, hp: 1), DoBoost(4, 1250, spd: 0.2),
                        DoBoost(5, 7000, p: Passive.DODGE, p1: 25, name: "Evading Griffo"), DoBoost(6, 26000, atk: 1, name: "Brave Griffo"),
                        DoBoost(7, 0, hp: 1, def: 1, name: "Elder Griffo"), DoBoost(8, 0, atk: 1, spd: 0.3, imm: 10, name: "Griffindor"),
                    }
                },
                new Card
                {
                    Name = "Thunderbird", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.ELECTRIFY, Param1 = 30, Param2 = 4},
                    Hp = 6, Atk = 2, Def = 0, Imm = 40, Spd = 2.55, Value = 73, BaseExp = 51, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/thunderbird.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 120, hp: 1), DoBoost(3, 410, spd: 0.3), DoBoost(4, 1500, atk: 1),
                        DoBoost(5, 7500, p: Passive.ELECTRIFY, p1: 40, p2: 5, name: "Electrybird"), DoBoost(6, 0, hp: 2, def: 1, spd: 0.2, name: "Thunderbirdage"),
                        DoBoost(7, 0, hp: 3, spd: 0.2, name: "Tormentbird"), DoBoost(8, 0, atk: 2, hp: 1, name: "Zapdos"),
                    }
                },
                new Card
                {
                    Name = "The Executor", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BLEED, Param1 = 1, Param2 = 8},
                    Hp = 7, Atk = 4, Def = 1, Imm = 0, Spd = 6.3, Value = 85, BaseExp = 58, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/the_executor.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 200, spd: 0.3), DoBoost(3, 680, hp: 1), DoBoost(4, 2400, atk: 1),
                        DoBoost(5, 13000, p: Passive.BLEED, p1: 2, p2: 6, name: "Bleeding Executor"), DoBoost(6, 0, atk: 2, name: "Great-Axe Executor"),
                        DoBoost(7, 0, spd: 1.2, name: "FastNGreat Executor"), DoBoost(8, 0, atk: 1, hp: 2, name: "Decapitate Executor"),
                    }
                },
                new Card
                {
                    Name = "Hideout Thief", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.THIEF, Param1 = 0.8},
                    Hp = 4, Atk = 2, Def = 0, Imm = 35, Spd = 2.1, Value = 73, BaseExp = 51, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/hideout_thief.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 130, spd: 0.1), DoBoost(3, 435, hp: 1), DoBoost(4, 1500, imm: 10),
                        DoBoost(5, 8800, hp: 1, spd: 0.2, name: "Hideout Captain"), DoBoost(6, 33000, atk: 1, hp: 1, name: "Hideout Chieftain"),
                        DoBoost(7, 0, atk: 1, def: 1, name: "Tricktest Thief"), DoBoost(8, 0, p: Passive.THIEF, p1: 1.2, hp: 1, name: "In Shadows Thief"),
                    }
                },
                new Card
                {
                    Name = "Cursed Vines", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.ALL, Element = Element.NATURE,
                    Passive = null,
                    Hp = 8, Atk = 1, Def = 0, Imm = 25, Spd = 3.4, Value = 74, BaseExp = 51, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/cursed_vines.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 130, spd: 0.2), DoBoost(3, 440, hp: 1), DoBoost(4, 1525, imm: 15),
                        DoBoost(5, 9000, p: Passive.DODGE, p2: 20, name: "Shifting Vines"), DoBoost(6, 33500, atk: 1, name: "Hitting Vines"),
                        DoBoost(7, 0, hp: 3, imm: 10, name: "Great Cursed Vines"), DoBoost(8, 0, atk: 1, name: "Violent Vines"),
                    }
                },
                new Card
                {
                    Name = "Eaterplant", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.LIFESTEAL, Param1 = 75},
                    Hp = 7, Atk = 4, Def = 0, Imm = 30, Spd = 3.7, Value = 79, BaseExp = 53, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/eaterplant.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 170, hp: 1), DoBoost(3, 610, imm: 10), DoBoost(4, 2100, spd: 0.3),
                        DoBoost(5, 12000, atk: 1, hp: 1, name: "Huntingplant"), DoBoost(6, 0, p: Passive.LIFESTEAL, p1: 100, spd: 0.3, name: "Hungryplant"),
                        DoBoost(7, 0, hp: 4, name: "Gigahuntplant"), DoBoost(8, 0, atk: 1, spd: 0.4, name: "Sachin"),
                    }
                },

                // TIER 1 CARD LEGENDARY

                new Card
                {
                    Name = "Phoenix", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.RANDOM, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.HELLFIRE, Param1 = 1, Param2 = 3.2 },
                    Hp = 9, Atk = 0, Def = 0, Imm = 100, Spd = 2.8, Value = 270, BaseExp = 124, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/phoenix.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(1, 0, p: Passive.BURN, p1: 100, p2: 1, p3: 1.4), DoBoost(2, 265, spd: 0.3), DoBoost(3, 880, hp: 2), DoBoost(4, 2900, spd: 0.3),
                        DoBoost(5, 31000, p: Passive.BURN, p1: 100, p2: 2, p3: 1.7, name: "Hell Phoenix"), DoBoost(6, 0, hp: 3, name: "Megaphoenix"),
                        DoBoost(7, 0, hp: 1, spd: 0.6, def: 1, name: "Gigaphoenix"), DoBoost(8, 0, p: Passive.HELLFIRE, p1: 1, p2: 2, hp: 2, name: "Icarus"),
                    }
                },
                new Card
                {
                    Name = "ELKchampion", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.ALL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.IGNORE_DEF },
                    Hp = 8, Atk = 1, Def = 1, Imm = 0, Spd = 3.7, Value = 250, BaseExp = 120, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/elkchampion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 250, spd: 0.3), DoBoost(3, 850, hp: 1, imm: 10), DoBoost(4, 2650, imm: 20),
                        DoBoost(5, 22500, spd: 0.9, name: "ELiteKchampion"), DoBoost(6, 0, p: Passive.DODGE, p1: 20, imm: 10, name: "DodgeELKchampion"),
                        DoBoost(7, 0, atk: 1, spd: -0.4, name: "ViolentELKchampion"), DoBoost(8, 0, p: Passive.DODGE, p1: 35, hp: 2, def: 1, spd: 0.3, name: "Ornixor"),
                    }
                },
                new Card
                {
                    Name = "Glypho", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.BACKTRACK, Param1 = 1.2 },
                    Hp = 7, Atk = 2, Def = 1, Imm = 35, Spd = 2.7, Value = 230, BaseExp = 115, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/glypho.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(1, 0, p: Passive.TOUGH, p1: 50, p2: 2), DoBoost(2, 240, spd: 0.25), DoBoost(3, 820, hp: 1), DoBoost(4, 2470, imm: 15),
                        DoBoost(5, 20000, def: 1, name: "Duracell Glypho"), DoBoost(6, 0, atk: 1, spd: 0.5, name: "Destroyer Glypho"),
                        DoBoost(7, 0, hp: 5, name: "Stone Glypho"), DoBoost(8, 0, p: Passive.BACKTRACK, p1: 1.7, hp: 1, def: 1, spd: 0.2, name: "Eribor"),
                    }
                },
                new Card
                {
                    Name = "Snowbear", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.RANDOM, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.IGNORE_DEF },
                    Hp = 8, Atk = 3, Def = 0, Imm = 25, Spd = 2.6, Value = 285, BaseExp = 126, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/snowbear.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(1, 0, p: Passive.BERSEKER, p1: 1, p2: 0.5), DoBoost(2, 280, spd: 0.15), DoBoost(3, 915, hp: 1), DoBoost(4, 3200, imm: 15),
                        DoBoost(5, 27000, p: Passive.BERSEKER, p1: 2, p2: 0.7, name: "Bersekersnowbear"), DoBoost(6, 0, hp: 4, name: "Giant Snowbear"),
                        DoBoost(7, 0, spd: 0.15, imm: 10, atk: 1, name: "Radiant Snowbear"), DoBoost(8, 0, hp: 2, p: Passive.BERSEKER, p1: 3, p2: 0.9, def: 1, name: "Gigantus"),
                    }
                },
                new Card
                {
                    Name = "Rock Beast", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.STUN, Param1 = 2.4 },
                    Hp = 16, Atk = 4, Def = 2, Imm = 0, Spd = 7.2, Value = 260, BaseExp = 122, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/rock_beast.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 245, spd: 0.4), DoBoost(3, 860, atk: 1), DoBoost(4, 2800, hp: 2),
                        DoBoost(5, 24500, hp: 3, atk: 1, def: 1, name: "Rock Monster"), DoBoost(6, 0, p: Passive.STUN, p1: 3.2, spd: 1.2, name: "Stunner Rock Monster"),
                        DoBoost(7, 0, atk: 1, spd: 1.2, name: "Violent Rock Monster"), DoBoost(8, 0, p: Passive.STUN, p1: 4, hp: 3, spd: 0.4, name: "Keltter"),
                    }
                },
                new Card
                {
                    Name = "Cebion", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 30 },
                    Hp = 8, Atk = 2, Def = 0, Imm = 40, Spd = 2.2, Value = 290, BaseExp = 130, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/cebion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(1, 0, p: Passive.ELECTRIFY, p1: 40, p2: 4), DoBoost(1, 0, p: Passive.BLAZE, p1: 2, p2: 3),
                        DoBoost(2, 300, spd: 0.2), DoBoost(3, 980, hp: 2), DoBoost(4, 3350, imm: 15),
                        DoBoost(5, 35000, p: Passive.ELECTRIFY, p1: 60, p2: 4, name: "Electric Cebion"), DoBoost(6, 0, hp: 1, p: Passive.BLAZE, p1: 3, p2: 4, name: "Blazing Cebion"),
                        DoBoost(7, 0, hp: 2, p: Passive.DODGE, p1: 45, name: "Feline Cebion"), DoBoost(8, 0, hp: 3, spd: 0.5, def: 1, atk: 1, name: "Elementus")
                    }
                },
                new Card
                {
                    Name = "Faceless", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.ALL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BLIND, Param1 = 50, Param2 = 0.9 },
                    Hp = 10, Atk = 1, Def = 0, Imm = 50, Spd = 3.2, Value = 255, BaseExp = 121, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/faceless.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 240, spd: 0.2), DoBoost(3, 840, hp: 1), DoBoost(4, 2700, spd: 0.3),
                        DoBoost(5, 23000, p: Passive.BLIND, p1: 60, p2: 1.1, name: "Blindeless"), DoBoost(6, 0, atk: 1, hp: 1, name: "Facehurtless"),
                        DoBoost(7, 0, hp: 2, atk: 1, name: "Gigoblindless"), DoBoost(8, 0, p: Passive.BLIND, p1: 75, p2: 1.3, spd: 0.2, hp: 1, name: "Blindorxer")
                    }
                },
                new Card
                {
                    Name = "Night Zombie", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.LIFESTEAL, Param1 = 67},
                    Hp = 9, Atk = 3, Def = 0, Imm = 25, Spd = 2.3, Value = 295, BaseExp = 132, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/night_zombie.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(1, 0, p: Passive.IGNORE_DEF), DoBoost(2, 315, spd: 0.15), DoBoost(3, 1020, hp: 1), DoBoost(4, 3500, spd: 0.15),
                        DoBoost(5, 37000, p: Passive.LIFESTEAL, p1: 75, atk: 1, name: "Feast Zombie"), DoBoost(6, 0, def: 2, name: "Armored Zombie"),
                        DoBoost(7, 0, hp: 3, spd: 0.3, name: "Hungryless Zombie"), DoBoost(8, 0, spd: 0.4, hp: 2, def: 1, name: "Tyrant")
                    }
                },

                //COMMON TIER 2
                new Card
                {
                    Name = "Dark Minions", Tier = 2, Rarity = Rarity.COMMON, AtkType = AtkType.LOWEST_HP, Element = Element.DARK, Passive = null,
                    Hp = 5, Atk = 2, Def = 0, Imm = 100, Spd = 1.3, Value = 70, BaseExp = 45, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dark_minions.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 120, spd: 0.1), DoBoost(3, 420, hp: 1), DoBoost(4, 1400, def: 1),
                        DoBoost(5, 6000, atk: 1, spd: 0.1, name: "Darkest Minions"), DoBoost(6, 26000, hp: 2),
                        DoBoost(7, 0, spd: 0.3), DoBoost(8, 0, hp: 1, def: 1, spd: 0.1, name: "Demonic Minions"),
                    }
                },
                new Card
                {
                    Name = "Jade Mage", Tier = 2, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.DEVIATE, Param1 = 2},
                    Hp = 7, Atk = 2, Def = 0, Imm = 25, Spd = 2.4, Value = 73, BaseExp = 47, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/jade_mage.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 125, spd: 0.2), DoBoost(3, 440, imm: 15), DoBoost(4, 1450, hp: 2),
                        DoBoost(5, 6100, p: Passive.DEVIATE, p1: 3, spd: 0.2, name: "Lighjade Mage"), DoBoost(6, 27000, def: 1),
                        DoBoost(7, 0, atk: 1, spd: 0.3), DoBoost(8, 0, hp: 2, imm: 20, spd: 0.4, name: "Shinning Jade Mage"),
                    }
                },
                new Card
                {
                    Name = "Ooze", Tier = 2, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.INUNDATE, Param1 = 0.3},
                    Hp = 8, Atk = 1, Def = 0, Imm = 50, Spd = 2.3, Value = 67, BaseExp = 44, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ooze.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 115, hp: 1), DoBoost(3, 405, imm: 10), DoBoost(4, 1340, spd: 0.25),
                        DoBoost(5, 5800, p: Passive.INUNDATE, p1: 0.45, name: "Gooze"), DoBoost(6, 24500, hp: 1, def: 1),
                        DoBoost(7, 0, atk: 1, imm: 10), DoBoost(8, 0, hp: 2, spd: 0.5, name: "Greatooze"),
                    }
                },
                new Card
                {
                    Name = "BettleX", Tier = 2, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.TOUGH, Param1 = 50, Param2 = 1},
                    Hp = 5, Atk = 2, Def = 2, Imm = 0, Spd = 3.1, Value = 75, BaseExp = 49, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/bettlex.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 130, hp: 1), DoBoost(3, 460, imm: 15), DoBoost(4, 1525, spd: 0.3),
                        DoBoost(5, 5800, hp: 1, atk: 1, name: "MegabettleX"), DoBoost(6, 29000, spd: 0.6),
                        DoBoost(7, 0, imm: 25, atk: 1), DoBoost(8, 0, hp: 2, p: Passive.TOUGH, p1: 75, p2: 1, name: "BoosterbettleX"),
                    }
                },
                new Card
                {
                    Name = "Raptor", Tier = 2, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.FRENZY, Param1 = 0.1},
                    Hp = 6, Atk = 2, Def = 0, Imm = 0, Spd = 1.6, Value = 77, BaseExp = 50, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/raptor.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 135, hp: 1), DoBoost(3, 480, imm: 20), DoBoost(4, 1600, spd: 0.15),
                        DoBoost(5, 5800, hp: 1, atk: 1, name: "Raptorex"), DoBoost(6, 30500, def: 1),
                        DoBoost(7, 0, imm: 20, spd: 0.15, hp: 1), DoBoost(8, 0, spd: 0.3, hp: 1, p: Passive.FRENZY, p1: 0.15, name: "Fastorex"),
                    }
                },
                new Card
                {
                    Name = "Redrobot", Tier = 2, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.ABLAZE, Param1 = 2},
                    Hp = 8, Atk = 2, Def = 0, Imm = 0, Spd = 3.3, Value = 72, BaseExp = 47, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/redrobot.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 125, spd: 0.2), DoBoost(3, 440, hp: 1), DoBoost(4, 1430, imm: 25),
                        DoBoost(5, 5800, def: 1, name: "Crimsonbot"), DoBoost(6, 27500, hp: 2, spd: 0.3),
                        DoBoost(7, 0, imm: 30, atk: 1), DoBoost(8, 0, hp: 4, name: "Gigasonbot"),
                    }
                },
                new Card
                {
                    Name = "Thunderfly", Tier = 2, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.STUN, Param1 = 0.4 },
                    Hp = 5, Atk = 1, Def = 0, Imm = 20, Spd = 1.5, Value = 65, BaseExp = 43, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/thunderfly.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 105, imm: 15), DoBoost(3, 360, hp: 1), DoBoost(4, 1230, atk: 1),
                        DoBoost(5, 5200, p: Passive.ELECTRIFY, p1: 50, p2: 1.2, name: "Electryfly"), DoBoost(6, 23500, hp: 1, spd: 0.2),
                        DoBoost(7, 0, def: 1, imm: 15), DoBoost(8, 0, hp: 2, atk: 1, name: "Boltfly"),
                    }
                },
                new Card
                {
                    Name = "Bambi", Tier = 2, Rarity = Rarity.COMMON, AtkType = AtkType.HEAL, Element = Element.NATURE,
                    Passive = null,
                    Hp = 7, Atk = 1, Def = 0, Imm = 30, Spd = 1.2, Value = 70, BaseExp = 46, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/bambi.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 115, hp: 1), DoBoost(3, 400, spd: 0.1), DoBoost(4, 1350, imm: 15),
                        DoBoost(5, 5450, atk: 1, spd: -0.2, name: "Bamboo"), DoBoost(6, 26000, hp: 1, def: 1),
                        DoBoost(7, 0, hp: 1, imm: 10, spd: 0.2), DoBoost(8, 0, atk: 1, name: "Bamboning"),
                    }
                },

                // BOSSES
                new Card
                {
                    Name = "Mountain Dungeon Watchman", Tier = 1, Rarity = Rarity.BOSS, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.BLEED, Param1 = 2, Param2 = 5},
                    Hp = 25, Atk = 4, Def = 0, Imm = 0, Spd = 3.5, Value = 0, BaseExp = 0, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/mountain_dungeon_watchman.png",
                    LevelBoosts = new List<LevelBoost>{}
                },
                new Card
                {
                    Name = "Swamp Cockatrice", Tier = 1, Rarity = Rarity.BOSS, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = null,
                    Hp = 18, Atk = 3, Def = 1, Imm = 25, Spd = 2.7, Value = 0, BaseExp = 0, RuneSlots = 0,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/swamp_cockatrice.png",
                    LevelBoosts = new List<LevelBoost>{}
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
                    Name = "Mountain", MinLevel = 1, Difficult = MapDifficult.EASY, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 100}, SrcImg = "mountain.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        DoEnemy("Rocks Encounter", 1, this.DoCards(new[] { ("Little Beast", 1), ("Goblin", 1) }),
                        DoReward(8, 4), X: 5, Y: 25, true, 2),
                        DoEnemy("Lake Encounter", 1, this.DoCards(new[] { ("Dawn Duck", 1), ("Mosquito", 1) }),
                        DoReward(12, 7), X: 65, Y: 40, true, 3),
                        DoEnemy("Forest Encounter", 1, this.DoCards(new[] { ("Horse", 1), ("Horse", 2), ("Goblin", 1) }),
                        DoReward(15, 10), X: 40, Y: 20, true, 3),
                        DoEnemy("Snowfield", 1, this.DoCards(new[] { ("Native", 1), ("Snow Rat", 2) }),
                        DoReward(17, 10), X: 45, Y: 55, true, 4),
                        DoEnemy("Volcan", 1, this.DoCards(new[] { ("Ogre", 1), ("Ogre", 2), ("Fire Snake", 1) }),
                        DoReward(14, 9), X: 28, Y: 80, true, 3),
                        DoEnemy("Great Volcan", 2, this.DoCards(new[] { ("Ogre", 2), ("Ogre", 3), ("Fire Wraith", 2) }),
                        DoReward(25, 15), X: 40, Y: 83, true, 3),
                        DoEnemy("The Dungeon", 2, this.DoCards(new[] { ("Fear Mage", 2), ("Fear Mage", 4), ("Succubus", 3) }),
                        DoReward(30, 20), X: 49, Y: 65, true, 3),
                        DoEnemy("The Dungeon Core", 3, this.DoCards(new[] { ("Mountain Dungeon Watchman", 1) }),
                        DoReward(200, 50), X: 53, Y: 71, false, 1),

                        DoEnemy("Don't Enter This Forest", 1, this.DoCards(new[] { ("Horse", 8), ("Goblin", 7) }),
                        DoReward(1500, 300), X: 45, Y: 13, false, 2),
                    }
                },
                new Map
                {
                    Name = "Swamp", MinLevel = 1, Difficult = MapDifficult.NORMAL, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 200 }, SrcImg = "swamp.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        DoEnemy("Entering Swamp", 1, this.DoCards(new[] { ("Reptillion", 1), ("Crabber", 1), ("Mosquito", 1)}),
                        DoReward(40, 20), X: 5, Y: 15, true, 4),
                        DoEnemy("Mosquito Feast", 2, this.DoCards(new[] { ("Mosquito", 2), ("Mosquito", 3), ("Poison Mosquito", 1)}),
                        DoReward(60, 25), X: 5, Y: 75, true, 4),
                        DoEnemy("Reptile Feast", 2, this.DoCards(new[] { ("Reptillion", 2), ("Reptillion", 3), ("Swamp Reptillion", 2), ("Swamp Reptillion", 3) }),
                        DoReward(72, 30), X: 20, Y: 32, true, 3),
                        DoEnemy("Quango Trouble", 2, this.DoCards(new[] { ("Quango", 2), ("Quango", 3), ("Quango", 4), ("Quango", 5) }),
                        DoReward(90, 35), X: 34, Y: 36, true, 4),
                        DoEnemy("More Reptiles", 3, this.DoCards(new[] { ("Reptillion", 3), ("Reptillion", 5), ("Swamp Reptillion", 3), ("Swamp Reptillion", 5), ("Serpentine", 5) }),
                        DoReward(120, 35), X: 47, Y: 27, true, 3),
                        DoEnemy("They Have Hunry", 3, this.DoCards(new[] { ("Eaterplant", 1) }),
                        DoReward(150, 42), X: 56, Y: 30, true, 2),
                        DoEnemy("Plant attack", 3, this.DoCards(new[] { ("Eaterplant", 1), ("Cursed Vines", 1), ("Plantanous", 5) }),
                        DoReward(200, 50), X: 63, Y: 12, false, 3),
                        DoEnemy("Swamplant", 4, this.DoCards(new[] { ("Eaterplant", 1), ("Eaterplant", 3), ("Cursed Vines", 1), ("Cursed Vines", 3), ("Little Hidra", 1) }),
                        DoReward(200, 50), X: 63, Y: 12, true, 3),
                        DoEnemy("Cockatrice Spot", 4, this.DoCards(new[] { ("Shaman", 1), ("Swamp Cockatrice", 1), ("Shaman", 8), ("Shaman", 1) }),
                        DoReward(600, 90), X: 86, Y: 20, false, 4),
                    }
                },
                new Map
                {
                    Name = "Skies", MinLevel = 2, Difficult = MapDifficult.HARD, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 200 }, SrcImg = "skies.jpg",
                    Enemies = new List<EnemyNpc>
                    {
                        DoEnemy("Rainbow", 2, this.DoCards(new[] { ("Poison Mosquito", 3), ("Mosquito", 5), ("Dawn Duck", 5)}),
                        DoReward(80, 30), X: 5, Y: 15, true, 3),
                        new EnemyNpc
                        {
                            Name = "Rainbow", Level = 2, X = 10, Y = 0, Reward = new Reward { Gold = 16 },
                            Cards = new List<PlayerCard>{
                                this.GetPlayerCard("Poison Mosquito", 4), this.GetPlayerCard("Mosquito", 4)
                            }
                        }
                    }
                },
                new Map
                {
                    Name = "Snow Mountain", MinLevel = 6, Difficult = MapDifficult.INSANE, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 100}, SrcImg = "snow_mountain.jpg",
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
                    Name = "Swampcamp", MinLevel = 8, Difficult = MapDifficult.HARD, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 100}, SrcImg = "swampcamp.jpg",
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
                    Name = "Megacloud", MinLevel = 10, Difficult = MapDifficult.HELL, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 100}, SrcImg = "megacloud.jpg",
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
                    Name = "Fire Mountain", MinLevel = 11, Difficult = MapDifficult.INSANE, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 100}, SrcImg = "fire_mountain.jpg",
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
                    Name = "Beautiful Mountain", MinLevel = 11, Difficult = MapDifficult.EASY, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 100}, SrcImg = "beautiful_mountain.jpg",
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
                    Name = "Darkpit", MinLevel = 13, Difficult = MapDifficult.NORMAL, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 1000}, SrcImg = "darkpit.jpg",
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
                    Name = "Green Swamp", MinLevel = 15, Difficult = MapDifficult.EASY, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 100}, SrcImg = "green_swamp.jpg",
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
                    Name = "Innora", MinLevel = 15, Difficult = MapDifficult.INSANE, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 100}, SrcImg = "innora.jpg",
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
                    Name = "Bluepoint", MinLevel = 17, Difficult = MapDifficult.NORMAL, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 1000}, SrcImg = "bluepoint.jpg",
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
                    Name = "Demon Church", MinLevel = 18, Difficult = MapDifficult.INSANE, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 1000}, SrcImg = "demon_church.jpg",
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
                    Name = "Goffinger", MinLevel = 20, Difficult = MapDifficult.HELL, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 1000}, SrcImg = "elysium.jpg",
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
                    Name = "Dark Floating Castle", MinLevel = 27, Difficult = MapDifficult.INSANE, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 1000}, SrcImg = "dark_floating_castle.jpg",
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
                    Name = "Elysium", MinLevel = 30, Difficult = MapDifficult.HELL, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 1000}, SrcImg = "elysium.jpg",
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
                    Name = "Above Everything", MinLevel = 33, Difficult = MapDifficult.HARD, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 1000}, SrcImg = "above_everything.jpg",
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
                    Name = "Nortrex", MinLevel = 40, Difficult = MapDifficult.HELL, GameType = GameType.NORMAL, RequiredEnemies = null,
                    CompletionReward = new Reward { Gold = 1000}, SrcImg = "nortrex.jpg",
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

        private Rune[] FillRunes()
        {
            Rune[] runes =
            {
                DoRune("Speedy Rune", 50, DoRuneBoost(spd: 0.2), 1, 0, "abstract-029"),
                DoRune("Healty Rune", 70, DoRuneBoost(hp: 1), 1, 0, "abstract-085"),
                DoRune("Munity Rune", 125, DoRuneBoost(imm: 10), 1, 0, "abstract-113"),
                DoRune("Strenght Rune", 150, DoRuneBoost(atk: 1), 1, 0, "abstract-030"),
                DoRune("Endure Rune", 225, DoRuneBoost(def: 1), 1, 0, "abstract-031", Rarity.RARE),
            };
            return runes;
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
                ActiveLvlBoosts = levelBoosts.Select(x => new ActiveLevelBoost { LevelBoost = x }).ToList(),
                Runes = runes
            };
        }

        private LevelBoost DoBoost(int level, int cost, int hp = 0, int atk = 0, int def = 0, double spd = 0,
            int imm = 0, Passive p = Passive.NONE, double p1 = 0, double p2 = 0, double p3 = 0, string name = "", int hpPerc = 0,
            int atkPerc = 0, int defPerc = 0, int spdPerc = 0, int immPerc = 0, int passivePerc = 0)
        {
            var statBoosts = new List<StatBoost>();
            if (hp != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.HP, Amount = hp });
            }
            if (hpPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.HPPERC, Amount = hpPerc });
            }
            if (atk != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.ATK, Amount = atk });
            }
            if (atkPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.ATKPERC, Amount = atkPerc });
            }
            if (def != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.DEF, Amount = def });
            }
            if (defPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.DEFPERC, Amount = defPerc });
            }
            if (spd != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.SPD, Amount = spd });
            }
            if (spdPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.SPDPERC, Amount = spdPerc });
            }
            if (imm != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.IMM, Amount = imm });
            }
            if (immPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.IMMPERC, Amount = immPerc });
            }
            if (passivePerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.PASSIVEPERC, Amount = passivePerc });
            }
            var passives = new List<CardPassive>();
            //TODO: SOLO UN PASIVO
            if (p != Passive.NONE)
            {
                passives.Add(new CardPassive
                {
                    Passive = p,
                    Param1 = p1,
                    Param2 = p2,
                    Param3 = p3
                });
            }
            return new LevelBoost
            {
                Level = level,
                Cost = cost,
                Boost = new CardBoost
                {
                    StatBoosts = statBoosts,
                    Passives = passives.Select(x => new PassiveBoost { CardPassive = x}).ToList()
                },
                ImprovedName = name
            };
        }

        private CardBoost DoRuneBoost(int hp = 0, int atk = 0, int def = 0, double spd = 0,
            int imm = 0, Passive p = Passive.NONE, double p1 = 0, double p2 = 0, double p3 = 0, string name = "", int hpPerc = 0,
            int atkPerc = 0, int defPerc = 0, int spdPerc = 0, int immPerc = 0, int passivePerc = 0)
        {
            var statBoosts = new List<StatBoost>();
            if (hp != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.HP, Amount = hp });
            }
            if (hpPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.HPPERC, Amount = hpPerc });
            }
            if (atk != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.ATK, Amount = atk });
            }
            if (atkPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.ATKPERC, Amount = atkPerc });
            }
            if (def != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.DEF, Amount = def });
            }
            if (defPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.DEFPERC, Amount = defPerc });
            }
            if (spd != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.SPD, Amount = spd });
            }
            if (spdPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.SPDPERC, Amount = spdPerc });
            }
            if (imm != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.IMM, Amount = imm });
            }
            if (immPerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.IMMPERC, Amount = immPerc });
            }
            if (passivePerc != 0)
            {
                statBoosts.Add(new StatBoost { BoostType = BoostType.PASSIVEPERC, Amount = passivePerc });
            }
            var passives = new List<CardPassive>();
            //TODO: SOLO UN PASIVO
            if (p != Passive.NONE)
            {
                passives.Add(new CardPassive
                {
                    Passive = p,
                    Param1 = p1,
                    Param2 = p2,
                    Param3 = p3
                });
            }
            return new CardBoost
            {
                StatBoosts = statBoosts,
                Passives = passives.Select(x => new PassiveBoost { CardPassive = x }).ToList()
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

        private List<PlayerCard> DoCards((string, int)[] cards)
        {
            var list = new List<PlayerCard>();
            foreach (var tuple in cards)
            {
                if (tuple.Item2 == 0) ;
                list.Add(this.GetPlayerCard(tuple.Item1, tuple.Item2 != 0 ? (int)tuple.Item2 : 1));
            }
            return list;
        }

        private Rune DoRune(string name, int value, CardBoost boost, int minTier, int maxTier, string imgSrc, Rarity rarity = Rarity.COMMON, Element element = Element.NONE)
        {
            return new Rune
            {
                Name = name,
                Boost = boost,
                MinTier = minTier,
                MaxTier = maxTier,
                Rarity = rarity,
                Element = element,
                Value = value,
                ImgSrc = imgSrc
            };
        }

        private Reward DoReward(int gold = 0, int exp = 0, Card card = null, List<Item> items = null)
        {
            return new Reward
            {
                Gold = gold,
                Exp = exp,
                //Card = card,
                //Items = items
            };
        }

        private Item GetItem(string name)
        {
            return this.items.Where(x => x.Name == name).FirstOrDefault();
        }
    }
}

