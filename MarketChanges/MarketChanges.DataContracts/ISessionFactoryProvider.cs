using NHibernate;

namespace MarketChanges.DataContracts
{
    public interface ISessionFactoryProvider
    {
        ISessionFactory SessionFactory { get; }

        ISession Open();
    }
}