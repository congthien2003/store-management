using StoreManagement.Application.DTOs.Response.KPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IServices
{
    public interface IKPIService
    {
        Task<List<KPIResponse>> GetKPIByYear(int year);   
        Task<KPIResponse> UpdateDataKpi(double totalPrice, double totalProductQuantity, int month, int year);
        Task<KPIResponse> GetDataKpiByMonth(int month, int year);
    }
}
