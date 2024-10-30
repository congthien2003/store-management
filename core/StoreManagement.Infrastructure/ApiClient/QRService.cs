using AutoMapper;
using StoreManagement.Application.DTOs.ApiClient.QR;
using StoreManagement.Application.Interfaces.IApiClientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.ApiClient
{
    public class QRService : IQRServices
    {
        private readonly IMapper mapper;
        public QRService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IMapper GetMapper()
        {
            return mapper;
        }

        public async Task<QRResponse> GetQR(string BankID, string BankName, string BankType, double Amount)
        {
            string result = BankID + BankName + BankType + "Amount: " + Amount;
            return new QRResponse { code = result };
        }
    }
}
