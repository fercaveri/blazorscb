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
        private Rune[] runes;

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
            this.runes = this.FillRunes();

            if (!_context.Cards.Any())
            {
                _context.Cards.AddRange(
                   this.cards
                );
            }

            if (!_context.Runes.Any())
            {
                _context.Runes.AddRange(
                   this.runes
                );
            }

            _context.SaveChanges();

            //var user = await _context.Users.FirstOrDefaultAsync();
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
            //    _context.Update(user);
            //}

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
                    Hp = 5, Atk = 2, Def = 0, Imm = 0, Spd = 3.6, Value = 30, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/goblin.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 50, imm: 10), DoBoost(3, 135, spd: 0.2), DoBoost(4, 600, def: 1), DoBoost(4, 450, hp: 2), DoBoost(5, 1500, atk: 1, name: "Great Goblin"),
                        DoBoost(6, 9000, spd: 0.6, name: "Elite Goblin")
                    }
                },
                new Card
                {
                    Name = "Horse", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH, Passive = null,
                    Hp = 4, Atk = 2, Def = 0, Imm = 0, Spd = 2.8, Value = 23, BaseExp = 25, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/horse.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 15, spd: 0.1), DoBoost(3, 35, hp: 1), DoBoost(4, 60, hp: 1),
                        DoBoost(4, 100, spd: 0.4), DoBoost(5, 450, p: Passive.DODGE, p1: 30, name: "Dodge Horse"), DoBoost(5, 300, spd: 0.3, name: "Speedy Horse")
                    }
                },
                new Card
                {
                    Name = "Rockhino", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.TOUGH, Param1 = 40, Param2 = 1},
                    Hp = 2, Atk = 1, Def = 1, Imm = 0, Spd = 3.3, Value = 25, BaseExp = 27, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/Rockhino.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, imm: 15), DoBoost(3, 45, imm: 10), DoBoost(4, 110, spd: 0.4),
                        DoBoost(5, 240, hp: 1, name: "Great Rockhino"), DoBoost(5, 400, p: Passive.TOUGH, p1: 60, p2: 1, name: "RockITnho")
                    }
                },
                new Card
                {
                    Name = "Little Beast", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = null,
                    Hp = 3, Atk = 2, Def = 1, Imm = 35, Spd = 3.7, Value = 25, BaseExp = 25, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/little_beast.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 85, hp: 1), DoBoost(4, 250, imm: 10), DoBoost(5, 900, def: 1, name: "Middle Beast")
                    }
                },
                new Card
                {
                    Name = "Troll", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = null,
                    Hp = 7, Atk = 1, Def = 0, Imm = 0, Spd = 4.1, Value = 22, BaseExp = 22, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/troll.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.2), DoBoost(3, 60, hp: 1), DoBoost(4, 150, imm: 20), DoBoost(5, 700, atk: 1, name: "Rage Troll"), 
                        DoBoost(6, 5500, hp: 2, spd: 0.5, name: "Mega Troll")
                    }
                },
                new Card
                {
                    Name = "Troll Eater", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.BLEED, Param1 = 1, Param2 = 4},
                    Hp = 5, Atk = 1, Def = 0, Imm = 15, Spd = 4.1, Value = 27, BaseExp = 30, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/troll_eater.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 45, spd: 0.2), DoBoost(3, 115, hp: 1), DoBoost(4, 290, imm: 15), 
                        DoBoost(5, 1100, p: Passive.BLEED, p1: 2, p2: 3, name: "Cannibal Troll")
                    }
                },
                new Card
                {
                    Name = "Mosquito", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WIND, Passive = null,
                    Hp = 3, Atk = 2, Def = 0, Imm = 25, Spd = 1.7, Value = 21, BaseExp = 22, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/mosquito.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, imm: 15), DoBoost(3, 55, imm: 20), DoBoost(4, 130, spd: 0.1),
                        DoBoost(5, 450, hp: 1, name: "Great Mosquito"), DoBoost(6, 4000, hp: 1, def: 1, spd: 0.1, name: "Imperial Mosquito")
                    }
                },
                new Card
                {
                    Name = "Dawn Duck", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.HEAL, Element = Element.WIND,
                    Passive = null,
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 3, Value = 22, BaseExp = 22, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dawn_duck.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.2), DoBoost(3, 75, spd: 0.2), DoBoost(4, 170, spd: 0.2),
                        DoBoost(5, 500, hp: 1, spd: 0.2, name: "Dawn Ducker")
                    }
                },
                new Card
                {
                    Name = "Ghost Skull", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.GHOST},
                    Hp = 2, Atk = 1, Def = 0, Imm = 0, Spd = 2.5, Value = 25, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ghost_skull.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 95, imm: 15), DoBoost(4, 250, imm: 10),
                        DoBoost(5, 950, hp: 1, name: "Ghost Calavery")
                    }
                },
                new Card
                {
                    Name = "Imp", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = null,
                    Hp = 6, Atk = 3, Def = 0, Imm = 0, Spd = 5.5, Value = 30, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/imp.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 40, spd: 0.2), DoBoost(3, 110, imm: 20), DoBoost(4, 290, atk: 1), DoBoost(5, 1150, hp: 1, name: "Feline Imp")
                    }
                },
                new Card
                {
                    Name = "Succubus", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 15},
                    Hp = 5, Atk = 1, Def = 0, Imm = 25, Spd = 3.2, Value = 24, BaseExp = 24, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/succubus.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.2), DoBoost(3, 60, imm: 15), DoBoost(4, 170, hp: 1), DoBoost(5, 650, hp: 1, name: "Great Succubus")
                    }
                },
                new Card
                {
                    Name = "Fear Mage", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.BLOWMARK, Param1 = 1},
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 3.4, Value = 27, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fear_mage.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.2), DoBoost(3, 80, imm: 20), DoBoost(4, 225, hp: 1), DoBoost(5, 900, imm: 20, name: "Blow Mage"),
                        DoBoost(5, 7000, p: Passive.BLOWMARK, p1: 2, name: "Blowmind Mage")
                    }
                },
                new Card
                {
                    Name = "One Eye Mage", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BOUNCE, Param1 = 1},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 3.6, Value = 23, BaseExp = 23, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/one_eye_mage.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, spd: 0.2), DoBoost(3, 70, hp: 1), DoBoost(4, 220, spd: 2.8),
                        DoBoost(5, 600, p: Passive.BOUNCE, p1: 2, name: "Almost-One Eye Mage"), DoBoost(5, 5000, spd: 0.3, hp: 1, p1: 2, name: "Two Eye Mage")
                    }
                },
                new Card
                {
                    Name = "Moon Elf", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 50},
                    Hp = 2, Atk = 1, Def = 0, Imm = 0, Spd = 2.1, Value = 26, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/moon_elf.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.1), DoBoost(3, 90, p: Passive.DODGE, p1: 58), DoBoost(4, 270, hp: 1),
                        DoBoost(5, 1000, p: Passive.DODGE, p1: 65, name: "Light Moon Elf")
                    }
                },
                new Card
                {
                    Name = "Nomad Mage", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BLIND, Param1 = 25, Param2 = 5},
                    Hp = 5, Atk = 1, Def = 0, Imm = 0, Spd = 3.1, Value = 29, BaseExp = 27, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/nomad_mage.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 100, imm: 20), DoBoost(4, 285, hp: 1),
                        DoBoost(5, 1200, p: Passive.BLIND, p1: 35, name: "Blind Mage")
                    }
                },
                new Card
                {
                    Name = "Snow Rat", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.FREEZE, Param1 = 12, Param2 = 5},
                    Hp = 4, Atk = 1, Def = 0, Imm = 20, Spd = 2.6, Value = 18, BaseExp = 20, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/snow_rat.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 20, spd: 0.1), DoBoost(3, 55, imm: 10), DoBoost(4, 150, hp: 1), DoBoost(5, 450, p: Passive.FREEZE, p1: 18, p2: 5, name: "Freezing Rat")
                    }
                },
                new Card
                {
                    Name = "Native", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = null,
                    Hp = 3, Atk = 1, Def = 1, Imm = 0, Spd = 3.3, Value = 30, BaseExp = 28, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/native.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 115, imm: 20), DoBoost(4, 300, hp: 1), DoBoost(5, 850, spd: 0.4, name: "Fast Native"),
                        DoBoost(5, 1150, spd: -0.8, atk: 1, name: "Elite Native")
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
                        DoBoost(5, 6000, hp: 1, spd: 0.3, p1: 3, name: "Dangersnow Leopardus")
                    }
                },
                new Card
                {
                    Name = "Ogre", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.IGNORE_DEF},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3.3, Value = 25, BaseExp = 25, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ogre.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 30, spd: 0.2), DoBoost(3, 100, imm: 15), DoBoost(4, 280, hp: 1), DoBoost(5, 850, spd: 0.6, name: "Enhanced Ogre")
                    }
                },
                new Card
                {
                    Name = "Fire Snake", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BLAZE, Param1 = 1, Param2 = 2.5},
                    Hp = 3, Atk = 0, Def = 1, Imm = 30, Spd = 2.3, Value = 23, BaseExp = 22, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_snake.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 25, spd: 0.1), DoBoost(3, 95, imm: 15), DoBoost(4, 275, hp: 1), DoBoost(5, 750, spd: 0.5, name: "Fast-Fire Snake"),
                        DoBoost(5, 1050, p: Passive.BLAZE, p1: 1, p2: 5, name: "Blazing Snake")
                    }
                },
                new Card
                {
                    Name = "Fire Imp", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.ABLAZE, Param1 = 1},
                    Hp = 3, Atk = 1, Def = 0, Imm = 20, Spd = 3, Value = 29, BaseExp = 27, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_imp.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 110, imm: 10), DoBoost(4, 300, hp: 1), DoBoost(5, 1000, spd: 0.5, name: "Annoying Fire Imp"), 
                        DoBoost(5, 9000, p: Passive.ABLAZE, p1: 2, name: "Intouchable Fire Imp")
                    }
                },
                new Card
                {
                    Name = "Shaman", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.OBLIVION, Param1 = 8},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3.1, Value = 28, BaseExp = 23, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/shaman.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 35, spd: 0.2), DoBoost(3, 100, imm: 20), DoBoost(4, 260, hp: 1), DoBoost(5, 850, p: Passive.OBLIVION, p1: 13, name: "Oblivion Shaman")
                    }
                },
                new Card
                {
                    Name = "Slime", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = null,
                    Hp = 10, Atk = 1, Def = 0, Imm = 0, Spd = 4.4, Value = 31, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/slime.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 45, spd: 0.2), DoBoost(3, 125, imm: 10), DoBoost(4, 340, spd: 0.3), DoBoost(5, 1400, hp: 2, name: "Slimen"), DoBoost(6, 10000, hp: 3, name: "Sliking")
                    }
                },
                new Card
                {
                    Name = "Serpentine", Tier = 1, Rarity = Rarity.COMMON, AtkType = AtkType.RANDOM, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.IMMUNE},
                    Hp = 4, Atk = 1, Def = 1, Imm = 0, Spd = 3.7, Value = 33, BaseExp = 29, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/serpentine.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 50, spd: 0.2), DoBoost(3, 140, spd: 0.2), DoBoost(4, 400, hp: 1), DoBoost(5, 1700, atk: 1, spd: -1, name: "Busterserpent")
                    }
                },

                //RARE TIER 1

                new Card
                {
                    Name = "Fire Wraith", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BLAZE, Param1 = 1, Param2 = 4},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 3.0, Value = 32, BaseExp = 30, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/fire_wraith.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 45, spd: 0.1), DoBoost(3, 125, hp: 1), DoBoost(4, 445, def: 1), DoBoost(4, 325, hp: 1),
                        DoBoost(5, 1700, p: Passive.BLAZE, p1: 2, p2: 3.5, name: "Blazing Wraith"),
                        DoBoost(5, 2200, def: 1, name: "Armored Fire Wraith")
                    }
                },
                new Card
                {
                    Name = "Burning Bird", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.BURN, Param1 = 40, Param2 = 1, Param3 = 2},
                    Hp = 4, Atk = 1, Def = 0, Imm = 0, Spd = 2.9, Value = 37, BaseExp = 33, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/burning_bird.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 55, spd: 0.2), DoBoost(3, 145, hp: 1), DoBoost(4, 520, hp: 1),
                        DoBoost(5, 2600, p: Passive.BURN, p1: 60, p2: 1, p3: 1, name: "Blazing Bird"),
                        DoBoost(5, 15000, def: 1, name: "Tough Burnbird")
                    }
                },
                new Card
                {
                    Name = "Triton", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.HEAL, Element = Element.WATER,
                    Passive = null,
                    Hp = 6, Atk = 2, Def = 0, Imm = 0, Spd = 3.5, Value = 40, BaseExp = 36, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/triton.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 75, spd: 0.2), DoBoost(3, 210, hp: 1), DoBoost(4, 580, def: 1), DoBoost(5, 2800, atk: 1, name: "TRIton"),
                        DoBoost(5, 16000, spd: 0.8, name: "MegaTRIton")
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
                        DoBoost(5, 2550, def: 1, name: "Armored Ice Skeleton")
                    }
                },
                new Card
                {
                    Name = "Grunt", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NONE, Passive = null,
                    Hp = 6, Atk = 1, Def = 1, Imm = 30, Spd = 3.8, Value = 30, BaseExp = 26, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/grunt.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 55, spd: 0.2), DoBoost(3, 145, imm: 10), DoBoost(4, 470, hp: 1), DoBoost(5, 1950, p: Passive.BACKTRACK, p1: 1, name: "Time-Shift Grunt"),
                        DoBoost(5, 1550, def: 1, name: "Armored Grunt")
                    }
                },
                new Card
                {
                    Name = "Ragemonkey", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.BERSEKER, Param1 = 2, Param2 = 1.1},
                    Hp = 6, Atk = 1, Def = 0, Imm = 40, Spd = 3.3, Value = 40, BaseExp = 34, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ragemonkey.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 70, spd: 0.1), DoBoost(3, 200, hp: 1), DoBoost(4, 580, spd: 0.1), DoBoost(5, 2300, atk: 1, name: "Brute Ragemonkey"),
                        DoBoost(6, 17000, hp: 3, name: "Giant Ragemonkey")
                    }
                },
                new Card
                {
                    Name = "Poison Mosquito", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.POISON, Param1 = 1, Param2 = 2, Param3 = 4},
                    Hp = 4, Atk = 1, Def = 0, Imm = 25, Spd = 2.1, Value = 33, BaseExp = 29, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/poison_mosquito.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 60, spd: 0.1), DoBoost(3, 190, imm: 25), DoBoost(4, 550, spd: 0.2), DoBoost(5, 2400, hp: 1, name: "Great Poison Mosquito"),
                        DoBoost(6, 14000, hp: 1, def: 1, spd: 0.1, name: "Imperial Poison Mosquito"),
                    }
                },
                new Card
                {
                    Name = "Crow", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 25},
                    Hp = 5, Atk = 1, Def = 0, Imm = 50, Spd = 2.6, Value = 36, BaseExp = 31, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/crow.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 65, spd: 0.2), DoBoost(3, 200, spd: 0.2), DoBoost(4, 700, atk: 1), DoBoost(4, 1000, p: Passive.DODGE, p1: 35, name: "Fast Crow"),
                        DoBoost(5, 3000, p: Passive.DODGE, p1: 50, name: "Lighting Crow"), DoBoost(5, 2200, hp: 1, def: 1, name: "Elite Crow")
                    }
                },
                new Card
                {
                    Name = "Reptillion", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.POISON, Param1 = 2, Param2 = 3, Param3 = 6 },
                    Hp = 6, Atk = 1, Def = 0, Imm = 0, Spd = 5.3, Value = 40, BaseExp = 35, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/reptillion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 70, spd: 0.3), DoBoost(3, 230, hp: 1), DoBoost(4, 750, spd: 0.4), DoBoost(5, 2200, atk: 1, name: "Fiere Reptillion"),
                        DoBoost(5, 1900, p: Passive.POISON, p1: 2, p2: 2.5, p3: 5, name: "Poisonus Reptillion")
                    }
                },
                new Card
                {
                    Name = "Swamp Reptillion", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.NATURE,
                    Passive = new CardPassive { Passive = Passive.KNOCKOUT, Param1 = 2},
                    Hp = 4, Atk = 1, Def = 1, Imm = 25, Spd = 3.2, Value = 44, BaseExp = 37, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/swamp_reptillion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 80, spd: 0.2), DoBoost(3, 260, hp: 1), DoBoost(4, 820, imm: 10), DoBoost(5, 3000, hp: 1, name: "Swamp Reptile"),
                        DoBoost(6, 18000, p: Passive.KNOCKOUT, p1: 3, name: "Killer Reptillion")
                    }
                },
                new Card
                {
                    Name = "Lesser Vampire", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.LIFESTEAL, Param1 = 50},
                    Hp = 5, Atk = 2, Def = 0, Imm = 25, Spd = 3.4, Value = 35, BaseExp = 32, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/lesser_vampire.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 65, spd: 0.2), DoBoost(3, 180, imm: 10), DoBoost(4, 625, hp: 1), DoBoost(5, 2200, atk: 1, name: "Middle Vampire"),
                        DoBoost(6, 12500, p: Passive.LIFESTEAL, p1: 70, name: "Hungry Middle Vampire")
                    }
                },
                new Card
                {
                    Name = "West Dark Justicer", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.BOUNCE, Param1 = 2},
                    Hp = 5, Atk = 2, Def = 0, Imm = 0, Spd = 5.2, Value = 42, BaseExp = 36, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/west_dark_justicer.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 80, spd: 0.3), DoBoost(3, 270, spd: 0.3), DoBoost(4, 850, hp: 1), DoBoost(5, 2900, atk: 1, name: "Dark Justicerman"),
                        DoBoost(5, 3200, p: Passive.BOUNCE, p1: 3, name: "Side-West Justicerdark")
                    }
                },
                new Card
                {
                    Name = "Guard Minion", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.REFLECT, Param1 = 35},
                    Hp = 4, Atk = 1, Def = 1, Imm = 0, Spd = 2.9, Value = 43, BaseExp = 37, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/guard_minion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 85, spd: 0.2), DoBoost(3, 290, hp: 1), DoBoost(4, 1000, spd: 0.3), DoBoost(5, 3500, atk: 1, hp: 1, spd: 0.2, name: "Great Guard Minion"),
                        DoBoost(6, 21000, p: Passive.REFLECT, p1: 50, name: "Great Reflect Minion")
                    }
                },
                new Card
                {
                    Name = "Lightguard", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BOUNCE, Param1 = 2},
                    Hp = 4, Atk = 2, Def = 2, Imm = 0, Spd = 4.7, Value = 47, BaseExp = 38, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/lightguard.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 95, spd: 0.3), DoBoost(3, 320, spd: 0.25), DoBoost(4, 1160, hp: 1), DoBoost(5, 4000, atk: 1, name: "Hurting-Lightguard"),
                        DoBoost(5, 4600, hp: 2, name: "LightGreatGuard")
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
                        DoBoost(6, 16500, hp: 1, atk: 1, name: "Durable Golem")
                    }
                },
                new Card
                {
                    Name = "Spidor", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.DODGE, Param1 = 45},
                    Hp = 3, Atk = 1, Def = 0, Imm = 0, Spd = 1.7, Value = 49, BaseExp = 41, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/spidor.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 105, spd: 0.1), DoBoost(3, 360, imm: 15), DoBoost(4, 1300, hp: 1), DoBoost(5, 4500, p: Passive.DODGE, p1: 65, name: "Almost-Invisible Spidor"),
                        DoBoost(5, 4800, atk: 1, spd: 0.2, name: "Hurting Spidor")
                    }
                },

                // TIER 1 SPECIAL

                new Card
                {
                    Name = "Blazing Golem", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.NORMAL, Element = Element.FIRE,
                    Passive = new CardPassive { Passive = Passive.ABLAZE, Param1 = 1},
                    Hp = 4, Atk = 3, Def = 2, Imm = 0, Spd = 6.1, Value = 62, BaseExp = 46, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/blazing_golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 110, spd: 0.5), DoBoost(3, 350, hp: 1), DoBoost(4, 1100, spd: 0.6), DoBoost(5, 6500, def: 1, name: "Hard Blazing Golem"),
                        DoBoost(6, 16500, hp: 1, atk: 1, name: "Durable Blazing Golem")
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
                        DoBoost(5, 8000, hp: 2, atk: 2, name: "Great Fire Golem")
                    }
                },
                new Card
                {
                    Name = "Dark Eye Fighter", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.DARK,
                    Passive = new CardPassive { Passive = Passive.SHATTER, Param1 = 10},
                    Hp = 6, Atk = 1, Def = 0, Imm = 0, Spd = 1.8, Value = 65, BaseExp = 48, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/dark_eye_fighter.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 120, spd: 0.1), DoBoost(3, 380, spd: 0.1), DoBoost(4, 1200, spd: 0.15), DoBoost(4, 1600, hp: 1),
                        DoBoost(5, 6400, p: Passive.SHATTER, p1: 15, name: "Dark Eye Breaker"), DoBoost(5, 7700, hp: 2, name: "Great Dark Eye Fighter")
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
                        DoBoost(5, 7200, atk: 1, name: "Moonlight Vampire"), DoBoost(6, 33000, p: Passive.REGURGITATE, p1: 3, name: "Darkmoon Vampire")
                    }
                },
                new Card
                {
                    Name = "RockIT", Tier = 1, Rarity = Rarity.RARE, AtkType = AtkType.RANDOM, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.TOUGH, Param1 = 50, Param2 = 1},
                    Hp = 10, Atk = 4, Def = 1, Imm = 0, Spd = 9, Value = 80, BaseExp = 55, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/blazing_golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 180, spd: 0.5), DoBoost(3, 580, hp: 2), DoBoost(4, 1900, atk: 1), DoBoost(5, 11000, atk: 1, hp: 2, name: "RockTHEM"),
                        DoBoost(6, 40000, p: Passive.TOUGH, p1: 60, p2: 2, name: "RockROCK")
                    }
                },
                new Card
                {
                    Name = "Rune Golem", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.EARTH,
                    Passive = new CardPassive { Passive = Passive.RUNEBREAKER},
                    Hp = 7, Atk = 1, Def = 1, Imm = 50, Spd = 2.8, Value = 65, BaseExp = 47, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/rune_golem.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 115, spd: 0.2), DoBoost(3, 370, hp: 1), DoBoost(4, 1200, spd: 0.3), DoBoost(5, 5500, atk: 1, name: "Runebreaker Golem"),
                        DoBoost(5, 6500, def: 1, name: "Durune Golem")
                    }
                },
                new Card
                {
                    Name = "Skellrex", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.LIFESTEAL, Param1 = 100},
                    Hp = 10, Atk = 1, Def = -1, Imm = 0, Spd = 1.4, Value = 75, BaseExp = 53, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/skellrex.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 135, spd: 0.1), DoBoost(3, 460, spd: 0.1), DoBoost(4, 1600, atk: 1), DoBoost(5, 9500, hp: 3, name: "High Skellrex"),
                        DoBoost(6, 33000, imm: 50, name: "Near-Immune Skellrex")
                    }
                },
                new Card
                {
                    Name = "Lirin", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.RANDOM, Element = Element.NONE,
                    Passive = new CardPassive { Passive = Passive.DOOM, Param1 = 8},
                    Hp = 9, Atk = 0, Def = 0, Imm = 100, Spd = 7, Value = 70, BaseExp = 52, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/lirin.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 120, spd: 0.3), DoBoost(3, 390, spd: 0.3), DoBoost(4, 1300, spd: 0.4), DoBoost(5, 6000, def: 1, name: "KLirin"),
                        DoBoost(5, 8000, p: Passive.DOOM, p1: 2, name: "Insta-Killirin")
                    }
                },
                new Card
                {
                    Name = "Ice Witch", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.WATER,
                    Passive = new CardPassive { Passive = Passive.WINTER, Param1 = 10},
                    Hp = 7, Atk = 2, Def = 0, Imm = 0, Spd = 2.9, Value = 71, BaseExp = 52, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/ice_witch.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 125, spd: 0.2), DoBoost(3, 420, hp: 1), DoBoost(4, 1450, imm: 20),
                        DoBoost(5, 8500, p: Passive.WINTER, p2: 14, name: "Cold Witch"), DoBoost(6, 31000, hp: 1, def: 1, spd: 0.3, name: "Freezing Witch")
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
                        DoBoost(5, 10500, spd: 0.4, name: "Little Fast Hidra"), DoBoost(5, 11000, hp: -4, atk: 1, def: 1, name: "Berseker Hidra")
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
                        DoBoost(5, 7000, p: Passive.DODGE, p1: 25, name: "Evading Griffo"), DoBoost(6, 26000, atk: 1, name: "Brave Griffo")
                    }
                },
                new Card
                {
                    Name = "Thunderbird", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.WIND,
                    Passive = new CardPassive { Passive = Passive.ELECTRIFY, Param1 = 30, Param2 = 4},
                    Hp = 5, Atk = 2, Def = 0, Imm = 40, Spd = 2.7, Value = 73, BaseExp = 51, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/thunderbird.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 120, hp: 1), DoBoost(3, 410, spd: 0.3), DoBoost(4, 1500, atk: 1),
                        DoBoost(5, 7500, p: Passive.ELECTRIFY, p1: 40, p2: 5, name: "Electrybird"), DoBoost(5, 9500, hp: 2, def: 1, spd: 0.2, name: "Thunderbirdage")
                    }
                },
                new Card
                {
                    Name = "The Executor", Tier = 1, Rarity = Rarity.SPECIAL, AtkType = AtkType.NORMAL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.BLEED, Param1 = 1, Param2 = 8},
                    Hp = 8, Atk = 4, Def = 1, Imm = 0, Spd = 5.5, Value = 85, BaseExp = 58, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/the_executor.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 200, spd: 0.3), DoBoost(3, 680, hp: 1), DoBoost(4, 2400, atk: 1),
                        DoBoost(5, 13000, p: Passive.BLEED, p1: 2, p2: 6, name: "Bleeding Executor"), DoBoost(5, 14500, atk: 2, name: "Great-Axe Executor")
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
                        DoBoost(5, 8800, hp: 1, spd: 0.2, name: "Hideout Captain"), DoBoost(6, 33000, atk: 1, hp: 1, name: "Hideout Chieftain")
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
                        DoBoost(5, 9000, p: Passive.DODGE, p2: 20, name: "Shifting Vines"), DoBoost(6, 33500, atk: 1, name: "Hitting Vines")
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
                        DoBoost(5, 12000, atk: 1, hp: 1, name: "Huntingplant"), DoBoost(5, 13000, p: Passive.LIFESTEAL, p1: 100, name: "Hungryplant")
                    }
                },

                // TIER 1 CARD LEGENDARY

                new Card
                {
                    Name = "ELKchampion", Tier = 1, Rarity = Rarity.LEGENDARY, AtkType = AtkType.ALL, Element = Element.LIGHT,
                    Passive = new CardPassive { Passive = Passive.IGNORE_DEF },
                    Hp = 8, Atk = 1, Def = 1, Imm = 0, Spd = 5, Value = 250, BaseExp = 120, RuneSlots = 1,
                    ImgSrc = "_content/SurrealCB.CommonUI/images/cards/elkchampion.png",
                    LevelBoosts = new List<LevelBoost>{
                        DoBoost(2, 250, spd: 0.2), DoBoost(3, 850, hp: 1), DoBoost(4, 2650, imm: 15),
                        DoBoost(5, 22500, spd: 0.8, name: "ELiteKchampion"), DoBoost(5, 26500, p: Passive.DODGE, p1: 15, name: "DodgeELKchampion")
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
                        DoEnemy("The Dungeon Core", 3, new List<PlayerCard>{ this.GetPlayerCard("Mountain Dungeon Watchman")},
                        DoReward(200, 50), X: 53, Y: 73, false, 1),
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
                CardId = card.Id,
                ActiveLvlBoosts = levelBoosts,
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
                statBoosts.Add(new StatBoost { Type = BoostType.HP, Amount = hp });
            }
            if (hpPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.HPPERC, Amount = hpPerc });
            }
            if (atk != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.ATK, Amount = atk });
            }
            if (atkPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.ATKPERC, Amount = atkPerc });
            }
            if (def != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.DEF, Amount = def });
            }
            if (defPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.DEFPERC, Amount = defPerc });
            }
            if (spd != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.SPD, Amount = spd });
            }
            if (spdPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.SPDPERC, Amount = spdPerc });
            }
            if (imm != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.IMM, Amount = imm });
            }
            if (immPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.IMMPERC, Amount = immPerc });
            }
            if (passivePerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.PASSIVEPERC, Amount = passivePerc });
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
                    Passives = passives
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
                statBoosts.Add(new StatBoost { Type = BoostType.HP, Amount = hp });
            }
            if (hpPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.HPPERC, Amount = hpPerc });
            }
            if (atk != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.ATK, Amount = atk });
            }
            if (atkPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.ATKPERC, Amount = atkPerc });
            }
            if (def != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.DEF, Amount = def });
            }
            if (defPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.DEFPERC, Amount = defPerc });
            }
            if (spd != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.SPD, Amount = spd });
            }
            if (spdPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.SPDPERC, Amount = spdPerc });
            }
            if (imm != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.IMM, Amount = imm });
            }
            if (immPerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.IMMPERC, Amount = immPerc });
            }
            if (passivePerc != 0)
            {
                statBoosts.Add(new StatBoost { Type = BoostType.PASSIVEPERC, Amount = passivePerc });
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
                Passives = passives
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

