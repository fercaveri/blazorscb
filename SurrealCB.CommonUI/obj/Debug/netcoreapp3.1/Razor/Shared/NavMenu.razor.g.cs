#pragma checksum "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c553f6926bdacc4a0ee0c78786ed357126a90fc8"
// <auto-generated/>
#pragma warning disable 1591
namespace SurrealCB.CommonUI.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
#nullable restore
#line 1 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
    public partial class NavMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<MatBlazor.MatNavMenu>(0);
            __builder.AddAttribute(1, "Multi", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 4 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                       true

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "Class", "app-sidebar");
            __builder.AddAttribute(3, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(4, "\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatNavItem>(5);
                __builder2.AddAttribute(6, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 5 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                           navigationManager.ToAbsoluteUri(" ").AbsoluteUri

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(7, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(8, "\r\n            ");
                    __builder3.OpenComponent<MatBlazor.MatIcon>(9);
                    __builder3.AddAttribute(10, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddContent(11, "home");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddContent(12, " ");
                    __builder3.AddMarkupContent(13, "<span class=\"miniHover\">Home page</span>\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(14, "\r\n\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatNavItem>(15);
                __builder2.AddAttribute(16, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 9 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                           navigationManager.ToAbsoluteUri("card_list").AbsoluteUri

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(17, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(18, "\r\n            ");
                    __builder3.OpenComponent<MatBlazor.MatIcon>(19);
                    __builder3.AddAttribute(20, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddContent(21, "list_alt");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddContent(22, " ");
                    __builder3.AddMarkupContent(23, "<span class=\"miniHover\">Card List</span>\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(24, "\r\n\r\n\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatNavItem>(25);
                __builder2.AddAttribute(26, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 32 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                           navigationManager.ToAbsoluteUri("maps").AbsoluteUri

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(27, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(28, "\r\n            ");
                    __builder3.OpenComponent<MatBlazor.MatIcon>(29);
                    __builder3.AddAttribute(30, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddContent(31, "bar_chart");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddContent(32, " ");
                    __builder3.AddMarkupContent(33, "<span class=\"miniHover\">Maps</span>\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(34, "\r\n\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatNavSubMenu>(35);
                __builder2.AddAttribute(36, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(37, "\r\n            ");
                    __builder3.OpenComponent<MatBlazor.MatNavSubMenuHeader>(38);
                    __builder3.AddAttribute(39, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(40, "\r\n                ");
                        __builder4.OpenComponent<MatBlazor.MatNavItem>(41);
                        __builder4.AddAttribute(42, "AllowSelection", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 38 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                            false

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(43, "Disabled", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 38 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                                               !IsLoggedIn

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(44, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(45, "\r\n                    ");
                            __builder5.OpenComponent<MatBlazor.MatIcon>(46);
                            __builder5.AddAttribute(47, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddContent(48, "mail_outline");
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(49, "\r\n                    ");
                            __builder5.AddMarkupContent(50, "<span class=\"miniHover\"> Email</span>\r\n                ");
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(51, "\r\n            ");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(52, "\r\n            ");
                    __builder3.OpenComponent<MatBlazor.MatNavSubMenuList>(53);
                    __builder3.AddAttribute(54, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(55, "\r\n                ");
                        __builder4.OpenComponent<MatBlazor.MatNavItem>(56);
                        __builder4.AddAttribute(57, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 44 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                   navigationManager.ToAbsoluteUri("email_view").AbsoluteUri

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(58, "Disabled", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 44 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                                                                                          !IsLoggedIn

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(59, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(60, "\r\n                    ");
                            __builder5.OpenComponent<MatBlazor.MatIcon>(61);
                            __builder5.AddAttribute(62, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddContent(63, "inbox");
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddContent(64, " ");
                            __builder5.AddMarkupContent(65, "<span class=\"miniHover\">Read Email</span>\r\n                ");
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(66, "\r\n                ");
                        __builder4.OpenComponent<MatBlazor.MatNavItem>(67);
                        __builder4.AddAttribute(68, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 47 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                   navigationManager.ToAbsoluteUri("email").AbsoluteUri

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(69, "Disabled", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 47 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                                                                                     !IsLoggedIn

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.AddAttribute(70, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(71, "\r\n                    ");
                            __builder5.OpenComponent<MatBlazor.MatIcon>(72);
                            __builder5.AddAttribute(73, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddContent(74, "send");
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddContent(75, " ");
                            __builder5.AddMarkupContent(76, "<span class=\"miniHover\">Send Email</span>\r\n                ");
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(77, "\r\n            ");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(78, "\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(79, "\r\n\r\n        ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(80);
                __builder2.AddAttribute(81, "Authorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((AuthorizeContext) => (__builder3) => {
                    __builder3.AddMarkupContent(82, "\r\n                ");
                    __builder3.OpenComponent<MatBlazor.MatNavSubMenu>(83);
                    __builder3.AddAttribute(84, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(85, "\r\n                    ");
                        __builder4.OpenComponent<MatBlazor.MatNavSubMenuHeader>(86);
                        __builder4.AddAttribute(87, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(88, "\r\n                        ");
                            __builder5.OpenComponent<MatBlazor.MatNavItem>(89);
                            __builder5.AddAttribute(90, "AllowSelection", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 58 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                                    false

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(91, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddMarkupContent(92, "\r\n                            ");
                                __builder6.OpenComponent<MatBlazor.MatIcon>(93);
                                __builder6.AddAttribute(94, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
                                    __builder7.AddContent(95, "settings_applications");
                                }
                                ));
                                __builder6.CloseComponent();
                                __builder6.AddContent(96, " ");
                                __builder6.AddMarkupContent(97, "<span class=\"miniHover\">Admin</span>\r\n                        ");
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(98, "\r\n                    ");
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(99, "\r\n                    ");
                        __builder4.OpenComponent<MatBlazor.MatNavSubMenuList>(100);
                        __builder4.AddAttribute(101, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddMarkupContent(102, "\r\n                        ");
                            __builder5.OpenComponent<MatBlazor.MatNavItem>(103);
                            __builder5.AddAttribute(104, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 63 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                           navigationManager.ToAbsoluteUri("admin/users").AbsoluteUri

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(105, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddMarkupContent(106, "\r\n                            ");
                                __builder6.OpenComponent<MatBlazor.MatIcon>(107);
                                __builder6.AddAttribute(108, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
                                    __builder7.AddContent(109, "supervisor_account");
                                }
                                ));
                                __builder6.CloseComponent();
                                __builder6.AddContent(110, " ");
                                __builder6.AddMarkupContent(111, "<span class=\"miniHover\">Users</span>\r\n                        ");
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(112, "\r\n                        ");
                            __builder5.OpenComponent<MatBlazor.MatNavItem>(113);
                            __builder5.AddAttribute(114, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 66 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                           navigationManager.ToAbsoluteUri("admin/roles").AbsoluteUri

#line default
#line hidden
#nullable disable
                            ));
                            __builder5.AddAttribute(115, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder6) => {
                                __builder6.AddMarkupContent(116, "\r\n                            ");
                                __builder6.OpenComponent<MatBlazor.MatIcon>(117);
                                __builder6.AddAttribute(118, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder7) => {
                                    __builder7.AddContent(119, "supervisor_account");
                                }
                                ));
                                __builder6.CloseComponent();
                                __builder6.AddContent(120, " ");
                                __builder6.AddMarkupContent(121, "<span class=\"miniHover\">Roles</span>\r\n                        ");
                            }
                            ));
                            __builder5.CloseComponent();
                            __builder5.AddMarkupContent(122, "\r\n                    ");
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddMarkupContent(123, "\r\n                ");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(124, "\r\n            ");
                }
                ));
                __builder2.AddAttribute(125, "NotAuthorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((AuthorizeContext) => (__builder3) => {
                    __builder3.AddMarkupContent(126, "\r\n                ");
                    __builder3.OpenComponent<MatBlazor.MatNavItem>(127);
                    __builder3.AddAttribute(128, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 73 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                   navigationManager.ToAbsoluteUri("admin/users").AbsoluteUri

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(129, "Disabled", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 73 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                                                                                         true

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(130, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(131, "\r\n                    ");
                        __builder4.OpenComponent<MatBlazor.MatIcon>(132);
                        __builder4.AddAttribute(133, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder5) => {
                            __builder5.AddContent(134, "supervisor_account");
                        }
                        ));
                        __builder4.CloseComponent();
                        __builder4.AddContent(135, " ");
                        __builder4.AddMarkupContent(136, "<span class=\"miniHover\">Admin</span>\r\n                ");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(137, "\r\n            ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(138, "\r\n\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatNavItem>(139);
                __builder2.AddAttribute(140, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 79 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                           navigationManager.ToAbsoluteUri("forum").AbsoluteUri

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(141, "Disabled", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 79 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                                                                                             !IsLoggedIn

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(142, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(143, "\r\n            ");
                    __builder3.OpenComponent<MatBlazor.MatIcon>(144);
                    __builder3.AddAttribute(145, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddContent(146, "forum");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddContent(147, " ");
                    __builder3.AddMarkupContent(148, "<span class=\"miniHover\">Forum</span>\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(149, "\r\n\r\n        ");
                __builder2.OpenComponent<MatBlazor.MatNavItem>(150);
                __builder2.AddAttribute(151, "Href", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 83 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
                           navigationManager.ToAbsoluteUri("sponsors").AbsoluteUri

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(152, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(153, "\r\n            ");
                    __builder3.OpenComponent<MatBlazor.MatIcon>(154);
                    __builder3.AddAttribute(155, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddContent(156, "attach_money");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddContent(157, " ");
                    __builder3.AddMarkupContent(158, "<span class=\"miniHover\">Sponsors</span>\r\n        ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(159, "\r\n    ");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 88 "C:\Rikito\SurrealCB\SurrealCB.CommonUI\Shared\NavMenu.razor"
       
    public bool IsLoggedIn = false;

    [CascadingParameter]
    Task<AuthenticationState> authenticationStateTask { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        IsLoggedIn = false;
        var user = (await authenticationStateTask).User;

        if (user.Identity.IsAuthenticated)
        {
            IsLoggedIn = true;
        }

        //https://gist.github.com/SteveSandersonMS/175a08dcdccb384a52ba760122cd2eda Examples
        //if (user.IsInRole("Administrator"))
        //{
        // Perform some action only available to users in the 'admin' role
        //}
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
    }
}
#pragma warning restore 1591
