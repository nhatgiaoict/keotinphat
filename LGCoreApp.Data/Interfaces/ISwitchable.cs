using System;
using System.Collections.Generic;
using System.Text;
using LGCoreApp.Data.Enums;

namespace LGCoreApp.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
