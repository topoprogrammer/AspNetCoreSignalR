using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using AspCoreSignalR.Helpers;

namespace AspCoreSignalR.Hubs
{
    //[Authorize]
    public class CoffeeHub: Hub
    {
        private readonly OrderChecker _orderChecker;

        public CoffeeHub(OrderChecker orderChecker)
        {
            _orderChecker = orderChecker;
        }

        public async Task GetUpdateForOrder(int orderId)
        {
            CheckResult result;
            do
            {
                result = _orderChecker.GetUpdate(orderId);
                Thread.Sleep(1000);
                if (result.New)
                    await Clients.All.SendAsync("ReceiveOrderUpdate", 
                        result.Update);
            } while (!result.Finished);
            await Clients.All.SendAsync("Finished");
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            //await Clients.Client(connectionId).SendAsync("NewOrder",ord)
            //await Clients.AllExcept(connectionId).SendAsync
            //await Groups.AddToGroupAsync(connectionId, "AmericanoGroup")
            //await Groups.RemoveFromGroupAsync(connectionId, "AmericanoGroup");
            //await Clients.Group("AmericanoGroup").SendAsync()

        }
    }
}
