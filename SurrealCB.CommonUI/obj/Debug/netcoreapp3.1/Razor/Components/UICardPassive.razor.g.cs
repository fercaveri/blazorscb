#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "deedd3204575260b3611fcf987284bbf8f2363d3"
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
using SurrealCB.Data.Enum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using SurrealCB.Data.Dto;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using static Microsoft.AspNetCore.Http.StatusCodes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\_Imports.razor"
using MatBlazor;

#line default
#line hidden
#nullable disable
    public partial class UICardPassive : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<MatBlazor.MatTooltip>(0);
            __builder.AddAttribute(1, "Tooltip", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 1 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                      PassiveHelper.GetPassiveTuple(p, p1, p2, p3).Item1

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<MatBlazor.ForwardRef>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(3, "\r\n    ");
                __builder2.OpenElement(4, "div");
                __builder2.AddAttribute(5, "class", "uicard-passive");
                __builder2.AddElementReferenceCapture(6, (__value) => {
#nullable restore
#line 2 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
               context.Current = __value;

#line default
#line hidden
#nullable disable
                }
                );
                __builder2.AddMarkupContent(7, "\r\n        ");
                __builder2.OpenElement(8, "div");
                __builder2.AddMarkupContent(9, "\r\n            ");
                __builder2.OpenElement(10, "img");
                __builder2.AddAttribute(11, "class", "card-icon" + " icon-" + (
#nullable restore
#line 4 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                        PassiveHelper.GetPassiveTuple(p, p1, p2, p3).Item2

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(12, "src", 
#nullable restore
#line 5 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                        $"_content/SurrealCB.CommonUI/icons/{PassiveHelper.GetPassiveTuple(p, p1, p2, p3).Item2}.svg"

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(13, "\r\n        ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(14, "\r\n");
#nullable restore
#line 7 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
         if (p1 != 0)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(15, "div");
                __builder2.AddAttribute(16, "class", "uicard-passive-param");
                __builder2.AddContent(17, " ");
                __builder2.AddContent(18, 
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                              p1 + RenderPercentage()

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(19, "  ");
                __builder2.CloseElement();
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                                                              }

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
         if (p2 != 0)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(20, "div");
                __builder2.AddAttribute(21, "class", "uicard-passive-param");
                __builder2.AddContent(22, " |");
                __builder2.AddContent(23, 
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                              p2

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(24, " ");
                __builder2.CloseElement();
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                                       }

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
         if (p3 != 0)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(25, "div");
                __builder2.AddAttribute(26, "class", "uicard-passive-param");
                __builder2.AddContent(27, " |");
                __builder2.AddContent(28, 
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                              p3

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(29, " ");
                __builder2.CloseElement();
#nullable restore
#line 12 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                                       }

#line default
#line hidden
#nullable disable
                __builder2.AddContent(30, "    ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(31, "\r\n");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 17 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
 
    [Parameter]
    public CardPassive CPassive { get; set; }
    public Passive p => CPassive.Passive;
    public double p1 => CPassive.Param1;
    public double p2 => CPassive.Param2;
    public double p3 => CPassive.Param3;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

    }

    public string RenderPercentage()
    {
        var passives = new List<Passive> {
            Passive.SHATTER, Passive.DEFRAGMENTER, Passive.DODGE, Passive.FREEZE, Passive.TOUGH, Passive.BLIND, Passive.OBLIVION, Passive.BURN, Passive.LIFESTEAL,
            Passive.REFLECT, Passive.ELECTRIFY, Passive.DOUBLE_ATTACK, Passive.FLEE, Passive.WINTER, Passive.TRANSFUSE, Passive.TIMESHIFT, Passive.SPIKE_ARMOR, Passive.INTENSIFY, Passive.COLDING
        };
        if (passives.Any(x => x == p)) return "%";
        return "";
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
