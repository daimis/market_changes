using MarketChanges.DataContracts;

using NHibernate;

namespace MarketChanges.Data
{
    public class Repository : GenericRepositoryBase<int>, IRepository
    {
        public Repository(ISessionFactoryProvider sessionFactoryProvider)
            : base(sessionFactoryProvider)
        {
        }

        public Repository(ISession session)
            : base(session)
        {
        }
    }  
}