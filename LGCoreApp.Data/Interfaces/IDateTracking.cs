﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LGCoreApp.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime DateCreated { set; get; }

        DateTime DateModified { set; get; }
    }
}
