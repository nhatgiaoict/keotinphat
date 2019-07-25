using System;
using System.Collections.Generic;
using System.Text;
using LGCoreApp.Application.ViewModels.System;
using LGCoreApp.Data.Entities;
using LGCoreApp.Infrastructure.Interfaces;
using LGCoreApp.Utilities.Dtos;

namespace LGCoreApp.Application.Interfaces
{
    public interface IAnnouncementService
    {
        PagedResult<AnnouncementViewModel> GetAllUnReadPaging(Guid userId, int pageIndex, int pageSize);

        bool MarkAsRead(Guid userId, string id);
    }
}
