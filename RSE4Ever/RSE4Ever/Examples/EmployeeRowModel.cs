using BForms.Models;
using BForms.Mvc;
using RSE4Ever.DataTablesMapping.Lib.Models;
using System.Collections.Generic;

public class EmployeeRowModel : BsItemModel 
{

    public int Id { get; set; }

    [BsGridColumn(Width = 2, IsEditable = true)]
    public string Name { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }


    public override object GetUniqueID()
    {
        return Id;
    }
  

    public Dictionary<string, object> RowData()
    {
        return new Dictionary<string, object>
        {
            { "data-objid", Id }
        };
    }
}