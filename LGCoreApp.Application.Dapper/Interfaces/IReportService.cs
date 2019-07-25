using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LGCoreApp.Application.Dapper.ViewModels;

namespace LGCoreApp.Application.Dapper.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<RevenueReportViewModel>> GetReportAsync(string fromDate, string toDate);
    }
}
