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
    public class PlayerCardMappingOverride : IAutoMappingOverride<PlayerCard>
    {
        public void Override(AutoMapping<PlayerCard> mapping)
        {
            mapping.References(x => x.Card).Cascade.SaveUpdate();
        }
    }
}
