using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LGCoreApp.Application.ViewModels.Common;
using LGCoreApp.Application.ViewModels.Product;
using LGCoreApp.Data.Enums;
using LGCoreApp.Utilities.Extensions;

namespace LGCoreApp.Models
{
    public class CheckoutViewModel : BillViewModel
    {
        public List<ShoppingCartViewModel> Carts { get; set; }
        public List<EnumModel> PaymentMethods
        {
            get
            {
                return ((PaymentMethod[])Enum.GetValues(typeof(PaymentMethod)))
                    .Select(c => new EnumModel
                    {
                        Value = (int)c,
                        Name = c.GetDescription()
                    }).ToList();
            }
        }
    }
}
