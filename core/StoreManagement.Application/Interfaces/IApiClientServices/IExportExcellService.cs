using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IApiClientServices
{
    public interface IExportExcellService
    {
        Task<byte[]> ExportFoodSalesToExcel(int idStore, DateTime startDate, DateTime endDate);
    }
}
