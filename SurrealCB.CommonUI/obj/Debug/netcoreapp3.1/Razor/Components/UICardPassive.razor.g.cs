#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2992564a1ceb2da9d2f71c4008e935e794509bf1"
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
                __builder2.AddContent(9, " ");
                __builder2.OpenElement(10, "img");
                __builder2.AddAttribute(11, "class", "card-icon" + " icon-" + (
#nullable restore
#line 3 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                          PassiveHelper.GetPassiveTuple(p, p1, p2, p3).Item2

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(12, "src", 
#nullable restore
#line 4 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                          $"_content/SurrealCB.CommonUI/icons/{PassiveHelper.GetPassiveTuple(p, p1, p2, p3).Item2}.svg"

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(13, "\r\n");
#nullable restore
#line 5 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
         if (p1 != 0)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(14, "div");
                __builder2.AddAttribute(15, "class", "uicard-passive-param");
                __builder2.AddContent(16, " ");
                __builder2.AddContent(17, 
#nullable restore
#line 6 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                              p1 + RenderPercentage()

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(18, "  ");
                __builder2.CloseElement();
#nullable restore
#line 6 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                                                              }

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
         if (p2 != 0)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(19, "div");
                __builder2.AddAttribute(20, "class", "uicard-passive-param");
                __builder2.AddContent(21, " |");
                __builder2.AddContent(22, 
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                              p2

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(23, " ");
                __builder2.CloseElement();
#nullable restore
#line 8 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                                       }

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
         if (p3 != 0)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(24, "div");
                __builder2.AddAttribute(25, "class", "uicard-passive-param");
                __builder2.AddContent(26, " |");
                __builder2.AddContent(27, 
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                              p3

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(28, " ");
                __builder2.CloseElement();
#nullable restore
#line 10 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
                                                       }

#line default
#line hidden
#nullable disable
                __builder2.AddContent(29, "    ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(30, "\r\n");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 15 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
 
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
        if (p == Passive.HP_SHATTER || p == Passive.HP_DEFRAGMENTER || p == Passive.DODGE) return "%";
        return "";
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
