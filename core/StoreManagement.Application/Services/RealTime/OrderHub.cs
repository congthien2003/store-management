﻿
using Microsoft.AspNetCore.SignalR;
using StoreManagement.Application.Interfaces.IServices;
namespace StoreManagement.Application.RealTime
{
    public class OrderHub : Hub
    {
        private static Dictionary<string, string> ActiveUsers = new Dictionary<string, string>();
        private readonly ITableService _tableService;

        public OrderHub(ITableService tableService)
        {
            _tableService = tableService;
        }

        // Invoke từ Front end
        public async Task NotifyOwner(string orderId, string storeId)
        {
            await Clients.Group(storeId).SendAsync("ReceiveOrderNotification", orderId);
        }

        public async Task JoinStoreGroup(string storeId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, storeId);
            await Clients.Group(storeId).SendAsync("Có người truy cập", "URL");

        }

        public async Task JoinTableGroup(string tableID)
        {
            Console.WriteLine("Có người truy cập vào bàn: " + tableID);
            await Groups.AddToGroupAsync(Context.ConnectionId, tableID);
        }

        public async Task NotiTableGroup(string tableID)
        {
            Console.WriteLine("Có thông báo từ bàn: " + tableID);

            await Clients.Group(tableID).SendAsync("ReloadData", "Có thay đổi");
        }

        // Create Connection
        public async Task<bool> RequestAccess(string tableId, string storeId)
        {
            if (!ActiveUsers.ContainsKey(tableId))
            {
                var connectionId = Context.ConnectionId;
                ActiveUsers[tableId] = connectionId;
                await Clients.Caller.SendAsync("AccessGranted", tableId);
                await Clients.Group(storeId).SendAsync("Có người truy cập", tableId);
                return true;
            }
            await Clients.Caller.SendAsync("AccessDenied", tableId);
            return false;
        }

        public async Task ReleaseAccess(string tableId)
        {
            if (ActiveUsers.ContainsKey(tableId))
            {
                ActiveUsers.Remove(tableId);
                await Clients.All.SendAsync("AccessReleased", tableId);
            }
        }

        public async Task LeaveStoreGroup(string storeId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, storeId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Bạn có thể xử lý logic ở đây, như cập nhật trạng thái người dùng
            var connectionId = Context.ConnectionId;

            var exists = ActiveUsers.Values.FirstOrDefault(connectionId);

            if (exists != null)
            {
                var tableId = ActiveUsers.FirstOrDefault(x => x.Value == exists).Key;
                if (tableId != null)
                {
                    ActiveUsers.Remove(tableId);
                    await Clients.All.SendAsync("AccessReleased", tableId);
                }
            }
            Console.WriteLine($"Client {Context.ConnectionId} đã ngắt kết nối.");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task RequestCallStaff(string guidTable, string guidStore)
        {
            var table = await _tableService.GetByGuIdAsync(new Guid(guidTable));
            await Clients.Group(table.IdStore.ToString()).SendAsync("RequestCallStaff", $"{table.Name} đang yêu cầu gọi nhân viên !");
        }
    }
}
