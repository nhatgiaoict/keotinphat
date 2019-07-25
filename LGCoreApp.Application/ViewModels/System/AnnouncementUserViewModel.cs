using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using LGCoreApp.Data.Entities;

namespace LGCoreApp.Application.ViewModels.System
{
    public class AnnouncementUserViewModel
    {
        public int Id { set; get; }

        [StringLength(128)]
        [Required]
        public string AnnouncementId { get; set; }

        public Guid UserId { get; set; }

        public bool? HasRead { get; set; }

    }
}
