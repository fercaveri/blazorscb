using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SurrealCB.Data.Repository;

namespace SurrealCB.Data.Repository
{
    public interface IRepositoryConfiguration
    {
        void Configure(string connectrionString, bool showSQL, System.Data.IsolationLevel isolationLevel, string sqliteFileName = null);
        void UpdateSchema();
        ISessionFactory SessionFactory { get; }
    }

    public abstract class NHibernateConfiguration : IRepositoryConfiguration
    {
        protected ISessionFactory sessionFactory;
        protected FluentConfiguration fluentConfiguration;
        private object lockObject = new object();

        public ISessionFactory SessionFactory
        {
            get
            {
                lock (lockObject)
                {
                    if (this.sessionFactory == null)
                    {
                        this.sessionFactory = this.fluentConfiguration.BuildSessionFactory();
                    }

                    return this.sessionFactory;
                }
            }
        }

        public abstract void Configure(string connectrionString, bool showSQL, System.Data.IsolationLevel isolationLevel, string sqliteFileName = null);

        public virtual void UpdateSchema()
        {
            var schemaUpdate = new SchemaUpdate(this.fluentConfiguration.BuildConfiguration());
            schemaUpdate.Execute(true, true);
            if (schemaUpdate.Exceptions.Count > 0)
            {
                throw new AggregateException("Exceptions occurred during export", schemaUpdate.Exceptions);
            }
        }
    }

    public class SCBNHibernateConfiguration : NHibernateConfiguration
    {
        public override void Configure(string connectrionString, bool showSQL, System.Data.IsolationLevel isolationLevel, string sqliteFileName = null)
        {
            IPersistenceConfigurer configuration = null;

            if (sqliteFileName != null)
            {
                var connectionString = @"Data Source=|DataDirectory|\" + sqliteFileName;
                var sqliteConfig = SQLiteConfiguration.Standard.IsolationLevel(isolationLevel).ConnectionString(connectionString);

                if (showSQL)
                    sqliteConfig = sqliteConfig.ShowSql();

                configuration = sqliteConfig;
            }
            else
            {
                var msSqlConfiguration = MsSqlConfiguration.MsSql2012.ConnectionString(connectrionString).IsolationLevel(isolationLevel);

                if (showSQL)
                    msSqlConfiguration = msSqlConfiguration.ShowSql();

                configuration = msSqlConfiguration;
            }

            this.fluentConfiguration = Fluently.Configure()
                .Database(configuration)
                .Mappings(m =>
                {
                    m.AutoMappings
                    .Add(AutoMap.AssemblyOf<SurrealCB.Data.Model.IEntity>(new SCBAutomappingConfiguration())
                    .IgnoreBase<SurrealCB.Data.Model.IEntity>()
                    .Conventions.Add
                    (
                        Table.Is(o => o.EntityType.Name),
                        PrimaryKey.Name.Is(o => "Id"),
                        ForeignKey.EndsWith("Id"),
                        DefaultLazy.Never(),
                        DefaultCascade.All()
                    )
                    .Conventions.AddFromAssemblyOf<ReferenceConvention>()
                    .Conventions.AddFromAssemblyOf<ManyToManyConvention>()
                    .Conventions.AddFromAssemblyOf<HasManyConvention>()
                    .UseOverridesFromAssemblyOf<ActiveLevelBoostMappingOverride>()
                    .UseOverridesFromAssemblyOf<ApplicationUserMappingOverride>()
                    .UseOverridesFromAssemblyOf<MapMappingOverride>()
                    .UseOverridesFromAssemblyOf<PassiveBoostMappingOverride>()
                    .UseOverridesFromAssemblyOf<PlayerCardMappingOverride>()
                    );
                });
        }
    }
}
