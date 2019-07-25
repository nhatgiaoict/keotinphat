using System;
using System.Collections.Generic;
using System.Text;
using LGCoreApp.Application.ViewModels.Common;

namespace LGCoreApp.Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
        List<SlideViewModel> GetSlides(string groupAlias);
        SystemConfigViewModel GetSystemConfig(string code);
    }
}
