using System.Configuration;
using System.Xml;

namespace DataTablesMapping.Lib.Configuration
{
    //This class reads the defined config section (if available) and stores it locally in the static _Config variable.  
    //This config data is available by calling MedGroups.GetMedGroups().
    public class EntityMapperConfig
    {
        public static EntityMapperSection _Config = ConfigurationManager.GetSection("entityMapperConfiguration") as EntityMapperSection;

        public static EntityMapperElementCollection GetConfigList()
        {
            return _Config.ConfigList;
        }

        public static string getConfigElement(string name)
        {
            string ret = "";
            foreach (EntityMapperElement eme in GetConfigList())
            {
                if (string.Compare(name, eme.Name) == 0)
                {
                    ret = eme.QueryString;
                }
            }
            return ret;
        }

 
    }

    //Extend the ConfigurationSection class.  Your class name should match your section name and be postfixed with "Section".
    public class EntityMapperSection : ConfigurationSection
    {
        //Decorate the property with the tag for your collection.
        [ConfigurationProperty("entityMapper")]
        public EntityMapperElementCollection ConfigList
        {
            get { return (EntityMapperElementCollection)this["entityMapper"]; }
        }
    }

    //Extend the ConfigurationElementCollection class.
    //Decorate the class with the class that represents a single element in the collection.	
    [ConfigurationCollection(typeof(EntityMapperElement))]
    public class EntityMapperElementCollection : ConfigurationElementCollection
    {
        public EntityMapperElement this[int index]
        {
            get { return (EntityMapperElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new EntityMapperElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EntityMapperElement)element).Name;
        }
    }

    //Extend the ConfigurationElement class.  This class represents a single element in the collection.	
    //Create a property for each xml attribute in your element.  
    //Decorate each property with the ConfigurationProperty decorator.  See MSDN for all available options.
    public class EntityMapperElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("queryString", IsRequired = true)]
        public string QueryString
        {
            get { return (string)this["queryString"]; }
            set { this["queryString"] = value; }
        }
    }
}