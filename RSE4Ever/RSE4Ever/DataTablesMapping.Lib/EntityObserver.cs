using RSE4Ever.DataTablesMapping.Lib.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSE4Ever.DataTablesMapping.Lib
{
    public class EntityObserver : IEntityObserver
    {
        public List<EntityObservableModel> EOMs { get; set; }

        private static volatile EntityObserver instance;
        private static object syncRoot = new object();

        public static EntityObserver Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new EntityObserver();
                    }
                    return instance;
                }
            }
        }

        private EntityObserver()
        {
            EOMs = new List<EntityObservableModel>();
        }

        public void Update(string property, IBaseEntity entity)
        {
            EntityObservableModel EOM = EOMs.Where(p => p.BaseEntity.GUID == entity.GUID).First();
            if (EOM.ChangedField.Where(p => p == property).Count() == 0)
            {
                EOM.ChangedField.Add(property);
            }
        }
        public void AddEntity(IBaseEntity entity)
        {
            EntityObservableModel EOM = new EntityObservableModel();
            EOM.BaseEntity = entity;
            EOM.ChangedField = new List<string>();
            EOMs.Add(EOM);
        }


        public void RemoveEntityObservableModel(EntityObservableModel nbe)
        {
            if (EOMs.Where(p => p.BaseEntity == nbe.BaseEntity) != null)
            {
                EOMs.Remove(nbe);
            }
        }

    }
}