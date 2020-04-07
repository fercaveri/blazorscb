#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b077abc9d0274117eef057c5ad31057bd48b7763"
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/maps")]
    public partial class Maps : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 29 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
       
    List<Map> maps = new List<Map>();
    protected override async Task OnInitializedAsync()
    {
        await GetAllCards();
    }

    private async Task GetAllCards()
    {
        ApiResponseDto apiResponse = await Http.GetJsonAsync<ApiResponseDto>("api/map/GetAll");

        if (apiResponse.StatusCode == Status200OK)
        {
            matToaster.Add(apiResponse.Message, MatToastType.Success, "Map List Retrieved");
            this.maps = Newtonsoft.Json.JsonConvert.DeserializeObject<Map[]>(apiResponse.Result.ToString()).ToList<Map>();
        }
        else
        {
            matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Todo List Retrieval Failed");
        }
    }

    private void StartBattle(Map map)
    {
        navigationManager.NavigateTo($"battle/{map.Enemies.FirstOrDefault().Id}");
    }

    private string GetTier(int minLevel)
    {
        return Math.Ceiling((double)minLevel / 5.0).ToString();
    }

    private string GetThumbnail(string img)
    {
        return img.Replace(".jpg", "_thumb.jpg");
    }

    private string GetRewardTooltip(Map map)
    {
        var reward = "Rewards: </br>";
        if (map.CompletionReward.Gold > 0)
        {
            reward += $"Gold: {map.CompletionReward.Gold} </br>";
        }
        if (map.CompletionReward.Exp > 0)
        {
            reward += $"Exp: { map.CompletionReward.Exp} </br>";
        }
        if (map.CompletionReward.Card != null)
        {
            reward += $"Card: {map.CompletionReward.Card.Name} </br>";
        }
        if (map.CompletionReward.Items != null && map.CompletionReward.Items.Any())
        {
            reward += $"Items: {string.Join(", ", map.CompletionReward.Items)}";
        }
        return reward;
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMatToaster matToaster { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
