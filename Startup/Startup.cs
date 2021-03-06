using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Connect4_Web_Project.Startup.Startup))]

namespace Connect4_Web_Project.Startup
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(6);
        }
    }
}
