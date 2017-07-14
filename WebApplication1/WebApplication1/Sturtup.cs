using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(WebApplication1.Sturtup))]
namespace WebApplication1
{
    public partial class Sturtup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}