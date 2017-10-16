using BForms.Models;
using BForms.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSE4Ever.Examples
{
    public enum DemoEnum : int
    {
        Option1 = 1,
        Option2 = 2,
        Option3 = 3,
        Option4 = 4
    }

    public class ExempleBformModel
    {
        [Display(Name = "My demo bforms dropdown", Prompt = "Select an option")]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> DemoDropdown { get; set; }
    }
}