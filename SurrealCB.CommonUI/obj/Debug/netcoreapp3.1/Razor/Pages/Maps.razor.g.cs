#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b077abc9d0274117eef057c5ad31057bd48b7763"
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/maps")]
    public partial class Maps : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "maps-container");
            __builder.AddMarkupContent(2, "\r\n");
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
     foreach (Map map in this.maps)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(3, "        ");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "map-item" + " map-" + (
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                   map.Difficult

#line default
#line hidden
#nullable disable
            ) + " map-tier-" + (
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                                            GetTier(map.MinLevel)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                                                                             () => StartBattle(map)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(7, "\r\n            ");
            __builder.OpenElement(8, "img");
            __builder.AddAttribute(9, "class", "map-thumb");
            __builder.AddAttribute(10, "src", 
#nullable restore
#line 11 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                          GetThumbnail(map.SrcImg)

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(11, "\r\n            ");
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "class", "map-stats");
            __builder.AddMarkupContent(14, "\r\n                ");
            __builder.OpenElement(15, "span");
            __builder.AddAttribute(16, "class", "map-name");
            __builder.AddContent(17, " ");
            __builder.AddContent(18, 
#nullable restore
#line 13 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                         map.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n                ");
            __builder.OpenElement(20, "div");
            __builder.AddAttribute(21, "class", "map-bottom-stats");
            __builder.AddMarkupContent(22, "\r\n                    ");
            __builder.OpenElement(23, "span");
            __builder.AddAttribute(24, "class", "min-level");
            __builder.AddContent(25, " ");
            __builder.AddContent(26, 
#nullable restore
#line 15 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                              map.MinLevel

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(27, "\r\n                    ");
            __builder.OpenElement(28, "span");
            __builder.AddAttribute(29, "class", "map-difficult");
            __builder.AddContent(30, " ");
            __builder.AddContent(31, 
#nullable restore
#line 16 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                                  map.Difficult.ToString()

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(32, "\r\n                    ");
            __builder.OpenElement(33, "span");
            __builder.AddAttribute(34, "class", "map-reward");
            __builder.AddMarkupContent(35, "\r\n                        ");
            __builder.OpenComponent<MatBlazor.MatTooltip>(36);
            __builder.AddAttribute(37, "Tooltip", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 18 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                              GetRewardTooltip(map)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(38, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<MatBlazor.ForwardRef>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(39, "\r\n                            ");
                __builder2.OpenElement(40, "img");
                __builder2.AddAttribute(41, "class", "card-icon");
                __builder2.AddAttribute(42, "src", "_content/SurrealCB.CommonUI/icons/reward.svg");
                __builder2.AddElementReferenceCapture(43, (__value) => {
#nullable restore
#line 19 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
                                        context.Current = __value;

#line default
#line hidden
#nullable disable
                }
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(44, "\r\n                        ");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(45, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(46, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(47, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(48, "\r\n            \r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(49, "\r\n");
#nullable restore
#line 26 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\Maps.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
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
