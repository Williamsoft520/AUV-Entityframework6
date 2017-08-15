namespace AUV.Entityframework6
{
    using AUV.Data;
    using System.Linq;

    /// <summary>
    /// 提供基于 EntityFramework 框架的领域驱动设计仓储功能。
    /// </summary>
    /// <typeparam name="TEntity">派生自 <see cref="IEntity{TKey}" /> 的实例。</typeparam>
    /// <typeparam name="TKey">表示实体对象的主键标识。</typeparam>
    /// <seealso cref="IRepository{TEntity, TKey}" />
    public interface IEntityframeworkRepository<TEntity,TKey>
        :IRepository<TEntity,TKey> where TEntity:class,IEntity<TKey>
    {
    }
}
