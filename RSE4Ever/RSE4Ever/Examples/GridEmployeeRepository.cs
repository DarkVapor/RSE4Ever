using BForms.Grid;
using RSE4Ever.DataTablesMapping.Lib;
using RSE4Ever.DataTablesMapping.Lib.Models;
using RSE4Ever.QueryBuilder.Lib;
using RSE4Ever.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace RSE4Ever.Examples
{
    public class GridEmployeeRepository : BsBaseGridRepository<Employee, EmployeeRowModel>
    {

        public override IEnumerable<EmployeeRowModel> MapQuery(IQueryable<Employee> query)
        {
            List<EmployeeRowModel> list = new List<EmployeeRowModel>();
            foreach( Employee e in query)
            {
                EmployeeRowModel erm = new EmployeeRowModel();

                erm.Id = e.Id;
                erm.Name = e.LastName + " " + e.FirstName;
                erm.FirstName = e.FirstName;
                erm.LastName = e.LastName;
                list.Add(erm);
            }
            return list;
        }


        public override IOrderedQueryable<Employee> OrderQuery(IQueryable<Employee> query, BsGridBaseRepositorySettings gridSettings = null)
        {
            return  query.OrderBy(x => x.BirthDate) ;
        }

        public override IQueryable<Employee> Query()
        {
            EmployeeRepository er = new EmployeeRepository();
            var query = er.GetList().AsQueryable();
            return query;
        }
    }
}