/*
 * This Class has been written by Kumar Vivek Mitra on 16-5-2014
 * for handling of initial SignalR connection and providing a single point of contact for the
 * client application.
 * */


using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;
using System.Threading;

namespace SignalRSelfHost
{
    // Initial connection management for the SignaR Server.......... By Kumar Vivek Mitra on 16-5-2014
    class Chat
    {
        static void Main(string[] args)
        {
           
            string url = "http://10.1.81.11:8080";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

    // Custom class extending Implementation class........... By Kumar Vivek Mitra on 16-5-2014
    public class MyHub : ClassImpl
    {
        public override void EntryPoint(string method_name = "", string name_From = "", string message = "", string name_To = "", string group_Name = "")
        {
            base.EntryPoint(method_name,name_From,message,name_To,group_Name);
        }
    }
}