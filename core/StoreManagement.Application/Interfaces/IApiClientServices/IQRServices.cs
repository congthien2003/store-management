using StoreManagement.Application.DTOs.ApiClient.QR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IApiClientServices
{
    public interface IQRServices
    {
        Task<QRResponse> GetQR(string BankID, string AccountNo, string AccountName, double Amount);
    }
}
