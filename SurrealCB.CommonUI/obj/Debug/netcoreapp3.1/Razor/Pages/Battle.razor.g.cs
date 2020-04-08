#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "afa991c614494bcd16bab2a576f6ea201cb3ebcf"
// <auto-generated/>
#pragma warning disable 1591
namespace SurrealCB.CommonUI.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using SurrealCB.CommonUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using SurrealCB.CommonUI.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using SurrealCB.CommonUI.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using SurrealCB.CommonUI.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using SurrealCB.CommonUI.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using SurrealCB.Data.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using SurrealCB.Data.Dto;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using static Microsoft.AspNetCore.Http.StatusCodes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using MatBlazor;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/battle")]
    [Microsoft.AspNetCore.Components.RouteAttribute("/battle/{EnemyId}")]
    public partial class Battle : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "battle-container");
            __builder.AddMarkupContent(2, "\r\n");
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
     foreach (BattleCard card in this.cards)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(3, "    ");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "battle-card" + " " + (
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                              nextPosition == card.Position ? "active" : ""

#line default
#line hidden
#nullable disable
            ) + " battle-pos-" + (
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                                         card.Position

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                                                         card.Hp == 0 ? "battle-death" : ""

#line default
#line hidden
#nullable disable
            ) + " health-" + (
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                                                                                                     GetHealthStatus(card)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 11 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                   () => PlayerPerform(card.Position)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(7, "\r\n        ");
            __builder.OpenComponent<SurrealCB.CommonUI.Components.UICard>(8);
            __builder.AddAttribute(9, "Atk", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                      card.GetAtk()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(10, "Def", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                           card.GetDef()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(11, "Spd", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                card.GetSpd()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "Hp", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                                    card.Hp

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(13, "Imm", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                                                   card.GetImm()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(14, "Name", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                                                                         card.PlayerCard.Card.Name

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(15, "Element", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<SurrealCB.Data.Model.Element>(
#nullable restore
#line 13 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                          card.PlayerCard.Card.Element

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(16, "Tier", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 13 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                               card.PlayerCard.Card.Tier

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "Rarity", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<SurrealCB.Data.Model.Rarity>(
#nullable restore
#line 13 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                                                   card.PlayerCard.Card.Rarity

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(18, "ImgSrc", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 14 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                         card.PlayerCard.Card.ImgSrc

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(19, "Passives", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<SurrealCB.Data.Model.CardPassive>>(
#nullable restore
#line 14 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                 card.PlayerCard.GetPassives()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(20, "AType", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<SurrealCB.Data.Model.AtkType>(
#nullable restore
#line 14 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                                                                       card.PlayerCard.Card.AtkType

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(21, "\r\n        ");
            __builder.OpenComponent<MatBlazor.MatProgressBar>(22);
            __builder.AddAttribute(23, "Progress", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 15 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                   GetHpBar(card)

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(24, "\r\n        ");
            __builder.OpenComponent<MatBlazor.MatProgressBar>(25);
            __builder.AddAttribute(26, "Progress", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 16 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                   GetSpeedBar(card)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(27, "Class", "speed-bar");
            __builder.CloseComponent();
            __builder.AddMarkupContent(28, "\r\n");
#nullable restore
#line 17 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
         if (card.Hp > 0 && Actions.Any(x => x.Position == card.Position))
        {
            if (Actions.FirstOrDefault(x => x.Position == card.Position).Type == HealthChange.DEATH)
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(29, "                ");
            __builder.AddMarkupContent(30, "<div class=\"battle-card-dmg\">\r\n                    <img src=\"_content/SurrealCB.CommonUI/icons/skull.svg\">\r\n                </div>\r\n");
#nullable restore
#line 24 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
            }
            else
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(31, "                ");
            __builder.OpenElement(32, "div");
            __builder.AddAttribute(33, "class", "battle-card-dmg" + " dmg-" + (
#nullable restore
#line 27 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                                                 Actions.FirstOrDefault(x => x.Position == card.Position).Type

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(34, "\r\n                    ");
            __builder.AddContent(35, 
#nullable restore
#line 28 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                     Actions.FirstOrDefault(x => x.Position == card.Position).Number

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(36, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(37, "\r\n");
#nullable restore
#line 30 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
            }
        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(38, "    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n");
#nullable restore
#line 33 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(40, "\r\n    ");
            __builder.OpenComponent<MatBlazor.MatDialog>(41);
            __builder.AddAttribute(42, "IsOpen", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 35 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                         battleStatus != BattleEnd.CONTINUE

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(43, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(44, "\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatDialogTitle>(45);
                __builder2.AddAttribute(46, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddContent(47, 
#nullable restore
#line 36 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
                         GetDialogTitle()

#line default
#line hidden
#nullable disable
                    );
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(48, "\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatDialogContent>(49);
                __builder2.AddAttribute(50, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(51, "\r\n            ");
                    __builder3.AddMarkupContent(52, "<p>Rewards: </p>\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(53, "\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatDialogActions>(54);
                __builder2.AddAttribute(55, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(56, "\r\n            ");
                    __builder3.OpenComponent<MatBlazor.MatButton>(57);
                    __builder3.AddAttribute(58, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddContent(59, "Return To World");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(60, "\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(61, "\r\n    ");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(62, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 46 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
       
    [Parameter]
    public string EnemyId { get; set; }

    private int nextPosition { get; set; }

    private ICollection<BattleAction> Actions = new List<BattleAction>();

    private List<BattleCard> cards = new List<BattleCard>();

    private BattleEnd battleStatus = BattleEnd.CONTINUE;

    protected override async Task OnInitializedAsync()
    {
        await GetAllCards();
    }

    private async Task GetAllCards()
    {
        ApiResponseDto apiResponse = await Http.GetJsonAsync<ApiResponseDto>($"api/battle/start/{EnemyId}");

        if (apiResponse.StatusCode == Status200OK)
        {
            matToaster.Add(apiResponse.Message, MatToastType.Success, "Map List Retrieved");
            this.cards = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleCard[]>(apiResponse.Result.ToString()).ToList<BattleCard>();

            await this.CheckNext();
        }
        else
        {
            matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Todo List Retrieval Failed");
        }
    }

    private async Task CheckNext()
    {
        ApiResponseDto nextResponse = await Http.PostJsonAsync<ApiResponseDto>($"api/battle/next", this.cards);
        var battleStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleStatus>(nextResponse.Result.ToString());
        this.cards = battleStatus.Cards.ToList();
        this.nextPosition = battleStatus.NextPosition;
        if (this.nextPosition > 3)
        {
            var card = this.cards.FirstOrDefault(x => x.Position == this.nextPosition);
            var who = -1;
            if (card.PlayerCard.Card.AtkType != AtkType.ALL && card.PlayerCard.Card.AtkType != AtkType.HEAL)
            {
                var playerCardcount = this.cards.Where(x => x.Position < 4).Count();
                var random = new Random();
                who = random.Next(1, playerCardcount) - 1;
            }
            ApiResponseDto atkResponse = await Http.PostJsonAsync<ApiResponseDto>($"api/battle/perform?srcPos={nextPosition}&tarPos={who}", this.cards);
            var newStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleStatus>(atkResponse.Result.ToString());
            await this.CheckStatus(newStatus);
        }
        else
        {
            var nextPlayerCard = this.cards.FirstOrDefault(x => x.Position == this.nextPosition);
            if (nextPlayerCard.PlayerCard.Card.AtkType == AtkType.RANDOM || nextPlayerCard.PlayerCard.Card.AtkType == AtkType.ALL)
            {
                ApiResponseDto atkResponse = await Http.PostJsonAsync<ApiResponseDto>($"api/battle/perform?srcPos={nextPosition}&tarPos={-1}", this.cards);
                var newStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleStatus>(atkResponse.Result.ToString());
                await this.CheckStatus(newStatus);
            }
        }
    }

    private async Task PlayerPerform(int pos)
    {
        if (this.nextPosition < 4 && this.nextPosition != -1)
        {
            var card = this.cards.FirstOrDefault(x => x.Position == pos);
            var mine = this.cards.FirstOrDefault(x => x.Position == this.nextPosition);
            if (card.Hp > 0 &&
                ((mine.PlayerCard.Card.AtkType == AtkType.NORMAL && pos > 3) ||
                (mine.PlayerCard.Card.AtkType == AtkType.HEAL && pos < 4)))
            {
                ApiResponseDto atkResponse = await Http.PostJsonAsync<ApiResponseDto>($"api/battle/perform?srcPos={nextPosition}&tarPos={pos}", this.cards);
                var newStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleStatus>(atkResponse.Result.ToString());
                await this.CheckStatus(newStatus);
            }
        }
    }

    private async Task CheckStatus(BattleStatus status)
    {
        this.cards = status.Cards.ToList();
        this.nextPosition = status.NextPosition;
        if (status.Actions?.Count() > 0)
        {
            //TODO: agarrar de a 1 accion por carta
            this.Actions = status.Actions;
            StateHasChanged();
            await Task.Delay(1000);
            this.Actions.Clear();
        }
        this.battleStatus = status.Status;
        if (this.battleStatus == BattleEnd.CONTINUE)
        {
            await this.CheckNext();
        }
    }

    private string GetHealthStatus(BattleCard card)
    {
        var statusPerc = this.GetHpBar(card);
        return statusPerc switch
        {
            _ when statusPerc > 0.8 => "green",
            _ when statusPerc > 0.6 => "yellowgreen",
            _ when statusPerc > 0.4 => "yellow",
            _ when statusPerc > 0.2 => "orange",
            _ => "red",
        };
    }

    private double GetHpBar(BattleCard card)
    {
        if (card.Hp == 0) return 0;
        return (double)((double)card.Hp / (double)card.GetHp());
    }

    private double GetSpeedBar(BattleCard card)
    {
        if (card.Time == 0 || card.Hp == 0) return 0;
        return card.Time / card.GetSpd();
    }

    private string GetDialogTitle() =>
        this.battleStatus switch
        {
            BattleEnd.WIN => "You win",
            BattleEnd.LOSE => "You lose",
            BattleEnd.DRAW => "You draw",
            _ => "You win",
        };

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMatToaster matToaster { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
