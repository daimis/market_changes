using FluentNHibernate.Mapping;

namespace MarketChanges.DataEntities.Mappings
{
    public abstract class EntityMapBase<TEntity> : ClassMap<TEntity>
        where TEntity : class, IEntity<int>
    {
        public EntityMapBase()
        {
            Id(x => x.Id);
        }
    }
}
