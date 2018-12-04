using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestIIS.Models
{
    public class ItemsAddViewModel
    {
        public int Id { set; get; }

        [Required]
        public string Title { set; get; }

        [Required]
        public string Author { set; get; }

        [AllowHtml]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { set; get; }
    }
}