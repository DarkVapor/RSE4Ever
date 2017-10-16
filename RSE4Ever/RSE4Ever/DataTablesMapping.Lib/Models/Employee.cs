using DataTablesMapping.Lib.Attributes;
using RSE4Ever.DataTablesMapping.Lib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Linq.Expressions;

namespace RSE4Ever.DataTablesMapping.Lib.Models
{
    [Table("Employee")]
    public class Employee : BaseEntity
    {

        private string lastName;
        private string firstName;
        private List<Territory> territories;
        public static string Name { get; set; }


        [SourceNames("LastName")]
        public string LastName {
            get {return lastName;}
            set
            {
                if(lastName != value)
                {
                    Notify("LastName");
                    lastName = value;
                }
            
            }
        }

        [SourceNames("FirstName")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    Notify("FirstName");
                    firstName = value;
                }
            }
        }
        
        public List<Territory> Territories
        {
            get {
                Territories =  base.GetManyToMany<Territory>("Territories");
                return territories;
            }
            set
            {
                territories = value;
            }
           
        }

        [SourceNames("Title")]
        public string Title { get; set; }

        [SourceNames("BirthDate")]
        public DateTime BirthDate { get; set; }

        [SourceNames("ReportsTo")]
        public int ReportsTo { get; set; }

       
    }
}