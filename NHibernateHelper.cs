using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace FluentNHibernateTutorial
{

    public class NHibernateHelper
    {

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory(); return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
			
             .Database(MsSqlConfiguration.MsSql2008.ConnectionString("Data Source = DESKTOP-B5KCRVC; Initial Catalog = FluentNHibernateTutorialDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False").ShowSql())
                 .Mappings(m => m.FluentMappings
                 .AddFromAssemblyOf<Program>())
                 .ExposeConfiguration(cfg => new SchemaExport(cfg)
                 .Create(true, true))
                 .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
