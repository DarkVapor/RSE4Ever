using BForms.Models;
using BForms.Mvc;
using RSE4Ever.DataTablesMapping.Lib.Models;
using RSE4Ever.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSE4Ever.Examples
{

    public class EmployeeViewModel
    {
        [BsGrid(HasDetails = false, Theme = BsTheme.Blue)]
        public BsGridModel<EmployeeRowModel> Grid { get; set; }
    }

}