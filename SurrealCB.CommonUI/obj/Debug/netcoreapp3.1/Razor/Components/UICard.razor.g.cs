#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "65b1967ca1da447e61dbb12aaa602f1265a5e5ff"
// <auto-generated/>
#pragma warning disable 1591
namespace SurrealCB.CommonUI.Components
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
    public partial class UICard : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "uicard-container" + " uicard-element-" + (
#nullable restore
#line 1 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                             Element

#line default
#line hidden
#nullable disable
            ) + " uicard-rarity-" + (
#nullable restore
#line 1 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                                    Rarity

#line default
#line hidden
#nullable disable
            ) + " mat-elevation-z4");
            __builder.AddMarkupContent(2, "\r\n    ");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "uicard-title");
            __builder.AddMarkupContent(5, "\r\n        ");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "uicard-level-name");
            __builder.AddMarkupContent(8, "\r\n            ");
            __builder.OpenElement(9, "span");
            __builder.AddAttribute(10, "class", "uicard-level" + " level-" + (
#nullable restore
#line 4 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                             Level

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(11, 
#nullable restore
#line 4 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                     Level

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(12, "\r\n            ");
            __builder.OpenElement(13, "span");
            __builder.AddAttribute(14, "class", "uicard-name");
            __builder.AddContent(15, 
#nullable restore
#line 5 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                       Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(17, "\r\n        ");
            __builder.OpenElement(18, "div");
            __builder.AddContent(19, " ");
            __builder.AddContent(20, 
#nullable restore
#line 7 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
               Tier

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(21, "  ");
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\r\n    ");
            __builder.OpenComponent<MatBlazor.MatDivider>(24);
            __builder.CloseComponent();
            __builder.AddMarkupContent(25, "\r\n    ");
            __builder.OpenElement(26, "div");
            __builder.AddAttribute(27, "class", "uicard-image");
            __builder.AddMarkupContent(28, "\r\n        ");
            __builder.OpenElement(29, "img");
            __builder.AddAttribute(30, "src", 
#nullable restore
#line 11 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                   ImgSrc

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n        ");
            __builder.OpenElement(32, "div");
            __builder.AddAttribute(33, "class", "uicard-rune-container");
            __builder.AddMarkupContent(34, "\r\n");
#nullable restore
#line 13 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
             foreach (var rune in Runes)
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(35, "                ");
            __builder.AddMarkupContent(36, "<div class=\"uicard-rune\"> <img src=\"rune\"> </div>\r\n");
#nullable restore
#line 16 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(37, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n    ");
            __builder.OpenComponent<MatBlazor.MatDivider>(40);
            __builder.CloseComponent();
            __builder.AddMarkupContent(41, "\r\n    ");
            __builder.OpenElement(42, "div");
            __builder.AddAttribute(43, "class", "uicard-footer");
            __builder.AddMarkupContent(44, "\r\n        ");
            __builder.OpenElement(45, "div");
            __builder.AddAttribute(46, "class", "uicard-footer-up");
            __builder.AddMarkupContent(47, "\r\n            ");
            __builder.OpenElement(48, "div");
            __builder.AddMarkupContent(49, " <img class=\"card-icon icon-health\" src=\"_content/SurrealCB.CommonUI/icons/health.svg\"> ");
            __builder.AddContent(50, 
#nullable restore
#line 22 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                                                                            Hp

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(51, " ");
            __builder.CloseElement();
            __builder.AddMarkupContent(52, "\r\n            ");
            __builder.OpenElement(53, "div");
            __builder.AddContent(54, " ");
            __builder.OpenElement(55, "img");
            __builder.AddAttribute(56, "class", "card-icon icon-atk");
            __builder.AddAttribute(57, "src", 
#nullable restore
#line 23 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                        GetAtkIcon()

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddContent(58, " ");
            __builder.AddContent(59, 
#nullable restore
#line 23 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                                          Atk

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(60, " ");
            __builder.CloseElement();
            __builder.AddMarkupContent(61, "\r\n            ");
            __builder.OpenElement(62, "div");
            __builder.AddMarkupContent(63, " <img class=\"card-icon icon-shield\" src=\"_content/SurrealCB.CommonUI/icons/shield.svg\"> ");
            __builder.AddContent(64, 
#nullable restore
#line 24 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                                                                            Def

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(65, " ");
            __builder.CloseElement();
            __builder.AddMarkupContent(66, "\r\n            ");
            __builder.OpenElement(67, "div");
            __builder.AddMarkupContent(68, " <img class=\"card-icon icon-wall\" src=\"_content/SurrealCB.CommonUI/icons/wall.svg\"> ");
            __builder.AddContent(69, 
#nullable restore
#line 25 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                                                                         Imm

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(70, "% ");
            __builder.CloseElement();
            __builder.AddMarkupContent(71, "\r\n            ");
            __builder.OpenElement(72, "div");
            __builder.AddMarkupContent(73, " <img class=\"card-icon icon-speed\" src=\"_content/SurrealCB.CommonUI/icons/speed.svg\"> ");
            __builder.AddContent(74, 
#nullable restore
#line 26 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                                                                          Spd

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(75, " ");
            __builder.CloseElement();
            __builder.AddMarkupContent(76, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(77, "\r\n        ");
            __builder.OpenElement(78, "div");
            __builder.AddAttribute(79, "class", "uicard-footer-down");
            __builder.AddMarkupContent(80, "\r\n");
#nullable restore
#line 29 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
             if (Passives != null)
            {
                foreach (CardPassive passive in Passives)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                     if (passive != null)
                    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(81, "                        ");
            __builder.OpenComponent<SurrealCB.CommonUI.Components.UICardPassive>(82);
            __builder.AddAttribute(83, "CPassive", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<SurrealCB.Data.Model.CardPassive>(
#nullable restore
#line 35 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                                                  passive

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(84, "\r\n");
#nullable restore
#line 36 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
                     
                }
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(85, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(86, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(87, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 44 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICard.razor"
 
    [Parameter]
    public string Name { get; set; }
    [Parameter]
    public int Tier { get; set; }
    [Parameter]
    public int Hp { get; set; }
    [Parameter]
    public int Atk { get; set; }
    [Parameter]
    public int Def { get; set; }
    [Parameter]
    public int Imm { get; set; }
    [Parameter]
    public double Spd { get; set; }
    [Parameter]
    public string ImgSrc { get; set; }
    [Parameter]
    public Rarity Rarity { get; set; }
    [Parameter]
    public AtkType AType { get; set; }
    [Parameter]
    public Element Element { get; set; }
    [Parameter]
    public List<Rune> Runes { get; set; } = new List<Rune>();
    [Parameter]
    public List<CardPassive> Passives { get; set; }
    [Parameter]
    public int Level { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

    }

    private string GetAtkIcon() =>
        this.AType switch
        {
            AtkType.RANDOM => "_content/SurrealCB.CommonUI/icons/atk_random.svg",
            AtkType.ALL => "_content/SurrealCB.CommonUI/icons/atk_all.svg",
            AtkType.HEAL => "_content/SurrealCB.CommonUI/icons/atk_heal.svg",
            _ => "_content/SurrealCB.CommonUI/icons/sword.svg",
        };

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
