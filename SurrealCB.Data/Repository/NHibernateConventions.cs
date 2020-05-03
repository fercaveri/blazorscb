using System;
using System.Collections.Generic;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;

namespace SurrealCB.Data.Repository
{
    public class ReferenceConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.ForeignKey(string.Format("{0}{1}{2}{3}",
                 "FK_",
                 instance.EntityType.Name,
                 "_",
                 instance.Name));
        }
    }

    public class ManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(string.Format("{0}{1}{2}{3}",
                   "FK_", instance.TableName,
                   "_",
                  instance.EntityType.Name));

            instance.Relationship.ForeignKey(string.Format("{0}{1}{2}{3}",
                   "FK_", instance.TableName,
                   "_",
                  instance.ChildType.Name));
        }
    }

    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(string.Format("{0}{1}{2}{3}",
                   "FK_", instance.ChildType.Name,
                   "_",
                  instance.EntityType.Name));
        }
    }
}
