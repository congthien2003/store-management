using AutoMapper;
using Azure;
using Microsoft.AspNet.SignalR.Hosting;
using StoreManagement.Application.DTOs.ApiClient.QR;
using StoreManagement.Application.Interfaces.IApiClientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoreManagement.Infrastructure.ApiClient
{
    public class QRService : IQRServices
    {
        public Task<QRResponse> GetQR(string BankID, string AccountNo, string AccountName, double Amount)
        {
            throw new NotImplementedException();
        }
    }
}
