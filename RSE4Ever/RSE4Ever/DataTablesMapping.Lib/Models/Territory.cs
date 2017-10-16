

using DataTablesMapping.Lib.Attributes;
using RSE4Ever.DataTablesMapping.Lib.Attributes;
using System.Collections.Generic;

namespace RSE4Ever.DataTablesMapping.Lib.Models
{
    [Table("Territories")]
    public class Territory : BaseEntity
    {

        private string territoryDescription;
        private Region region;



        
        [SourceNames("TerritoryDescription")]
        public string TerritoryDescription
        {
            get { return territoryDescription; }
            set
            {
                if (territoryDescription != value)
                {
                    Notify("TerritoryDescription");
                    territoryDescription = value;
                }

            }
        }
        
        [SourceNames("Region_id")]
        public Region Region
        {
            get
            {

                if (region == null)
                {
                    return new Region();
                }
                else
                {
                    Region ret = base.GetOneToOne<Region>("Region", region.Id);
                    return ret;
                }
            }
            set
            {
                if (!Region.Equals(value))
                {
                    Notify("Region");
                    region = value;
                }
            }
        }
    }
}