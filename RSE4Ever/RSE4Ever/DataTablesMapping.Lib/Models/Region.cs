using DataTablesMapping.Lib.Attributes;
using RSE4Ever.DataTablesMapping.Lib.Attributes;

namespace RSE4Ever.DataTablesMapping.Lib.Models
{

    [Table("Region")]
    public class Region : BaseEntity
    {
        private string regionDescription;

        [SourceNames("RegionDescription")]
        public string RegionDescription
        {
            get { return regionDescription; }
            set
            {
                if (regionDescription != value)
                {
                    Notify("RegionDescription");
                    regionDescription = value;
                }
            }
        }
    }
}