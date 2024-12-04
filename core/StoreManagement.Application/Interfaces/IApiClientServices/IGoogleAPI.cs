using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.IApiClientServices
{
    public interface IGoogleAPI
    {
        Task<dynamic> Gemini(string promt);
    }
}
