#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1603a5cecab2995189e795238478e2aef1bba5b8"
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/card_list")]
    public partial class CardList : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "card-list-container");
            __builder.AddMarkupContent(2, "\r\n");
#nullable restore
#line 6 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
     foreach (Card card in this.cards)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(3, "        ");
            __builder.OpenComponent<SurrealCB.CommonUI.Components.UICard>(4);
            __builder.AddAttribute(5, "Atk", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                      card.Atk

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "Def", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                      card.Def

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(7, "Spd", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                                      card.Spd

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(8, "Hp", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                                                     card.Hp

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(9, "Imm", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                                                                    card.Imm

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(10, "Name", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                                                                                     card.Name

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(11, "Element", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<SurrealCB.Data.Model.Element>(
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                                                                                                          card.Element

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "Tier", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                                                                                                                               card.Tier

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(13, "Rarity", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<SurrealCB.Data.Model.Rarity>(
#nullable restore
#line 9 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                         card.Rarity

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(14, "ImgSrc", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 9 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                               card.ImgSrc

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(15, "Passives", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<SurrealCB.Data.Model.CardPassive>>(
#nullable restore
#line 9 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
                                                                        new List<CardPassive>{card.Passive}

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(16, "\r\n");
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"

    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 14 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Pages\CardList.razor"
       
    List<Card> cards = new List<Card>();
    protected override async Task OnInitializedAsync()
    {
        await GetAllCards();
    }

    private async Task GetAllCards()
    {
        ApiResponseDto apiResponse = await Http.GetJsonAsync<ApiResponseDto>("api/card/GetAll");

        if (apiResponse.StatusCode == Status200OK)
        {
            matToaster.Add(apiResponse.Message, MatToastType.Success, "Card List Retrieved");
            this.cards = Newtonsoft.Json.JsonConvert.DeserializeObject<Card[]>(apiResponse.Result.ToString()).ToList<Card>();
        }
        else
        {
            matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Todo List Retrieval Failed");
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMatToaster matToaster { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
