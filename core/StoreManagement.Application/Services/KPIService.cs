using AutoMapper;
using StoreManagement.Application.DTOs.Response.KPI;
using StoreManagement.Application.Interfaces.IServices;
using StoreManagement.Domain.IRepositories;
using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace StoreManagement.Application.Services
{
    public class KPIService : IKPIService
    {
        private readonly IKPIRepository<KPI> _repository;
        private readonly IMapper _mapper;

        public KPIService(IKPIRepository<KPI> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<KPIResponse> GetDataKpiByMonth(int month, int year)
        {
            var kpi = await _repository.GetByLastestMonth(month, year);
            if (kpi == null) {
                
                    throw new Exception("KPI này chưa có");
            }
            return _mapper.Map<KPIResponse>(kpi);
        }

        public async Task<List<KPIResponse>> GetKPIByYear(int year)
        {
            var kpi = await _repository.GetByLastestYear(year);
            if (kpi == null)
            {

                throw new Exception("KPI này chưa có");
            }
            
            return _mapper.Map<List<KPIResponse>>(kpi);
        }

        public async Task<KPIResponse> UpdateDataKpi(double totalPrice, double totalProductQuantity, int month, int year)
        {
            var kpi =  await _repository.GetByLastestMonth(month, year);
            if (kpi != null) {
                kpi.TotalRevenue += totalPrice;
                kpi.TotalSales += totalProductQuantity;
                kpi.UpdatedAt = DateTime.Now;
                await _repository.Update(kpi);
                return _mapper.Map<KPIResponse>(kpi);
            }
            else
            {
                KPI newKPI = new KPI();
                newKPI.TotalRevenue += totalPrice;
                newKPI.TotalSales += totalProductQuantity;
                newKPI.Month = month;
                newKPI.Year = year;
                await _repository.Add(newKPI);
                return _mapper.Map<KPIResponse>(newKPI);
            }
        }
    }
}
