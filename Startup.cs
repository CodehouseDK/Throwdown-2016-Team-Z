using System;
using System.Net.WebSockets;
using System.Threading;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamZ.Services;

namespace TeamZ
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<INameService, NameService>();
        }
        
        
      
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {

            app.UseWebSockets();
            app.Use(async (http, next) =>
            {
                if (http.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await http.WebSockets.AcceptWebSocketAsync();
                    if (webSocket != null && webSocket.State == WebSocketState.Open)
                    {
                        WebSocketClient.AddSocket(webSocket);
                        byte[] buffer = new byte[1024];
                        WebSocketReceiveResult received = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        
                        while (received.MessageType != WebSocketMessageType.Close)
                        {
                            var str = System.Text.Encoding.UTF8.GetString(buffer);
                            var trimmed = str.Trim();
                            Console.WriteLine(trimmed);
                            WebSocketClient.Broadcast("Hi there");
                            received = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                            buffer = new byte[received.Count];
                        }
                    }
                }
                else
                {        
                    await next();
                }
            });

            loggerFactory.AddConsole();

            app.UseIISPlatformHandler();

            app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();

            app.UseStaticFiles();

        }
    }
}