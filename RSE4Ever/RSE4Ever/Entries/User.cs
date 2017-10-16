using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSETechnofuturTIC.MappingTools.Tools;

namespace RSETechnofuturTIC.Entries
{
    public class User : Entry
    {

        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        [MType("User")]
        public User()
        {
          
        }
    }
}