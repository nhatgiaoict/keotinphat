using System;
using System.Collections.Generic;
using System.Text;
using LGCoreApp.Application.ViewModels.Common;
using LGCoreApp.Utilities.Dtos;

namespace LGCoreApp.Application.Interfaces
{
   public  interface IContactService
    {
        void Add(ContactViewModel contactVm);

        void Update(ContactViewModel contactVm);

        void Delete(string id);

        List<ContactViewModel> GetAll();

        PagedResult<ContactViewModel> GetAllPaging(string keyword, int page, int pageSize);

        ContactViewModel GetById(string id);

        void SaveChanges();
    }
}
