#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d62c45145e2d69d31f8c44c752009b01e9e20607"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

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
        }
        #pragma warning restore 1998
#nullable restore
#line 14 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Components\UICardPassive.razor"
 
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

    public Tuple<string, string> GetPassiveTuple() =>
        p switch
        {
            Passive.BLAZE => Tuple.Create($"Inflicts {p1} dmg every card turn for {p2} seconds.", "blaze"),
            Passive.DOOM => Tuple.Create($"Kill the card in {p1} seconds.", "doom"),
            Passive.FREEZE => Tuple.Create($"Slows the speed {p1} for {p2} seconds.", ""),
            Passive.HP_SHATTER => Tuple.Create($"Reduce {p1} of enemy total hp on every hit.", "hp_shatter"),
            Passive.HP_DEFRAGMENTER => Tuple.Create($"Reduce {p1} of enemy current hp on every hit.", "hp_defragmenter"),
            Passive.IGNORE_DEF => Tuple.Create($"Ignore entire enemy defense.", ""),
            Passive.PIERCING => Tuple.Create($"Ignore {p1} of defense on attack.", ""),
            Passive.POISON => Tuple.Create($"Inflicts {p1} dmg every {p2} seconds for {p3} seconds.", "poison"),
            Passive.STUN => Tuple.Create($"Stun the target for {p1} seconds.", ""),
            Passive.BACKTRACK => Tuple.Create($"Backtrack the target speed for {p1} seconds.", ""),
            Passive.DODGE => Tuple.Create($"{p1}% chance of evasion.", "dodge"),
            _ => Tuple.Create("", ""),
        };

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
