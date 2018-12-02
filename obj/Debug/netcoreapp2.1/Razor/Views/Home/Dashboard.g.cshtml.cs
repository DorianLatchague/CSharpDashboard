#pragma checksum "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "970daffd9ec234e3003b7a9ce71fd11d5610cbf6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Dashboard), @"mvc.1.0.view", @"/Views/Home/Dashboard.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Dashboard.cshtml", typeof(AspNetCore.Views_Home_Dashboard))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/_ViewImports.cshtml"
using CSharpDashboard;

#line default
#line hidden
#line 2 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/_ViewImports.cshtml"
using CSharpDashboard.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"970daffd9ec234e3003b7a9ce71fd11d5610cbf6", @"/Views/Home/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"522485bf8ea1983d2a500e858c102046d03827ae", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
  
    ViewData["Title"] = "Contact";

#line default
#line hidden
            BeginContext(40, 1, true);
            WriteLiteral("\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(58, 396, true);
                WriteLiteral(@"
    <script src=""https://cdn.jsdelivr.net/npm/bootstrap-confirmation2/dist/bootstrap-confirmation.min.js""></script>
    <script>
        <script>
            $(document).ready(function(){
                $('[data-toggle=confirmation]').confirmation({rootSelector: '[data-toggle=confirmation]',
                // other options
                });
            });
        </script>
    </script>
");
                EndContext();
            }
            );
            BeginContext(456, 563, true);
            WriteLiteral(@"<div class=""container"">
    <nav class=""navbar navbar-expand-lg navbar-light bg-light"">
        <a class=""navbar-brand"">Test App</a>
        <ul class=""navbar-nav mr-auto"">
            <li class=""nav-item active"">
                <a style=""font-weight: bold;"" class=""nav-link"" href=""/dashboard"">Dashboard</a>   
            </li>
            <li class=""nav-item active"">
                <a class=""nav-link"" href=""/selfedit"">Profile</a>
            </li>
        </ul>
        <a href=""/logout"" class=""nav-item active nav-link my-2 my-lg-0"">Log Out</a>
    </nav>
");
            EndContext();
#line 30 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
     if(@ViewBag.Level == 9)
    {

#line default
#line hidden
            BeginContext(1054, 168, true);
            WriteLiteral("        <h1 style=\"display: inline-block;\">Manage Users</h1>\n        <a style=\"float: right; margin-top: 10px;\" href=\"/new\" class=\"btn btn-outline-primary\">Add New</a>\n");
            EndContext();
#line 34 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
    }
    else
    {

#line default
#line hidden
            BeginContext(1243, 27, true);
            WriteLiteral("        <h1>All Users</h1>\n");
            EndContext();
#line 38 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
    }

#line default
#line hidden
            BeginContext(1276, 220, true);
            WriteLiteral("    <table class=\"table\">\n        <thead>\n            <tr>\n                <th>ID</th>\n                <th>Name</th>\n                <th>Email</th>\n                <th>Created At</th>\n                <th>User Level</th>\n");
            EndContext();
#line 47 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                 if(@ViewBag.Level == 9)
                {

#line default
#line hidden
            BeginContext(1555, 37, true);
            WriteLiteral("                    <th>Actions</th>\n");
            EndContext();
#line 50 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                }

#line default
#line hidden
            BeginContext(1610, 51, true);
            WriteLiteral("            </tr>\n        </thead>\n        <tbody>\n");
            EndContext();
#line 54 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
             foreach(User user in @ViewBag.Users)
            {

#line default
#line hidden
            BeginContext(1725, 45, true);
            WriteLiteral("                <tr>\n                    <td>");
            EndContext();
            BeginContext(1771, 11, false);
#line 57 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                   Write(user.UserId);

#line default
#line hidden
            EndContext();
            BeginContext(1782, 32, true);
            WriteLiteral("</td>\n                    <td><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1814, "\"", 1839, 2);
            WriteAttributeValue("", 1821, "/wall/", 1821, 6, true);
#line 58 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
WriteAttributeValue("", 1827, user.UserId, 1827, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1840, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1842, 14, false);
#line 58 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                                                Write(user.FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(1856, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(1858, 13, false);
#line 58 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                                                                Write(user.LastName);

#line default
#line hidden
            EndContext();
            BeginContext(1871, 34, true);
            WriteLiteral("</a></td>\n                    <td>");
            EndContext();
            BeginContext(1906, 10, false);
#line 59 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                   Write(user.Email);

#line default
#line hidden
            EndContext();
            BeginContext(1916, 30, true);
            WriteLiteral("</td>\n                    <td>");
            EndContext();
            BeginContext(1947, 39, false);
#line 60 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                   Write(user.CreatedAt.ToString("MMMM dd yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1986, 6, true);
            WriteLiteral("</td>\n");
            EndContext();
#line 61 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                     if(@user.Level == 9)
                    {

#line default
#line hidden
            BeginContext(2056, 39, true);
            WriteLiteral("                        <td>Admin</td>\n");
            EndContext();
#line 64 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                    }
                    else
                    {

#line default
#line hidden
            BeginContext(2164, 45, true);
            WriteLiteral("                        <td>Normal User</td>\n");
            EndContext();
#line 68 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                    }

#line default
#line hidden
            BeginContext(2231, 20, true);
            WriteLiteral("                    ");
            EndContext();
#line 69 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                     if(@ViewBag.Level == 9)
                    {

#line default
#line hidden
            BeginContext(2298, 62, true);
            WriteLiteral("                        <td><a class=\"btn btn-outline-success\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2360, "\"", 2385, 2);
            WriteAttributeValue("", 2367, "/edit/", 2367, 6, true);
#line 71 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
WriteAttributeValue("", 2373, user.UserId, 2373, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2386, 350, true);
            WriteLiteral(@">Edit</a>
                        <a class=""btn btn-outline-danger"" data-toggle=""confirmation"" data-btn-ok-label=""Continue"" data-btn-ok-class=""btn-outline-success"" data-btn-cancel-label=""Cancel"" data-btn-cancel-class=""btn-outline-danger"" data-title=""Are you sure?"" data-content=""Pressing 'Continue' will delete {{user.first_name}} from the database.""");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2736, "\"", 2762, 2);
            WriteAttributeValue("", 2743, "delete/", 2743, 7, true);
#line 72 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
WriteAttributeValue("", 2750, user.UserId, 2750, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2763, 17, true);
            WriteLiteral(">Delete</a></td>\n");
            EndContext();
#line 73 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
                    }

#line default
#line hidden
            BeginContext(2802, 22, true);
            WriteLiteral("                </tr>\n");
            EndContext();
#line 75 "/Users/dorianlatchague/Documents/c#_stack/c#_entity/CSharpDashboard/Views/Home/Dashboard.cshtml"
            }

#line default
#line hidden
            BeginContext(2838, 36, true);
            WriteLiteral("        </tbody>\n    </table>\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
