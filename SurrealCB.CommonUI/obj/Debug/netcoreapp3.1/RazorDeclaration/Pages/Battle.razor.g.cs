#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bb967fb8f43048ade8fe4e761ca657794c76561a"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

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
        }
        #pragma warning restore 1998
#nullable restore
#line 55 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Battle.razor"
       
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
        this.Actions = battleStatus.Actions;
        this.battleStatus = battleStatus.Status;
        if (this.Actions.Any())
        {
            StateHasChanged();
            await Task.Delay(1000);
        }
        if (this.nextPosition > 3)
        {
            var card = this.cards.FirstOrDefault(x => x.Position == this.nextPosition);
            var who = -1;
            if (card.PlayerCard.Card.AtkType != AtkType.ALL && card.PlayerCard.Card.AtkType != AtkType.HEAL)
            {
                do
                {
                    var playerCardcount = this.cards.Where(x => x.Position < 4).Count();
                    var random = new Random();
                    who = random.Next(1, playerCardcount + 1) - 1;
                }
                while (this.cards.FirstOrDefault(x => x.Position == who).Hp == 0 && this.battleStatus == BattleEnd.CONTINUE);
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
