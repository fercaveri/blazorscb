using System;
using System.Collections.Generic;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using SurrealCB.Data.Model;

namespace SurrealCB.Data.Repository
{
    public class MapMappingOverride : IAutoMappingOverride<Map>
    {
        public void Override(AutoMapping<Map> mapping)
        {
            mapping.HasMany(x => x.Enemies).Cascade.All();
            mapping.HasMany(x => x.RequiredEnemies).Cascade.All();
        }
    }
}
