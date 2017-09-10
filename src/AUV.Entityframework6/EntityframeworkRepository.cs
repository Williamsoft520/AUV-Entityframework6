using System.Linq;

namespace AUV.Entityframework6
{
    using AUV.Data;
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// 基于 EntityFramework 框架的领域驱动设计的仓储基本实现。
    /// </summary>
    /// <typeparam name="TEntity">派生自 <see cref="IEntity{TKey}" /> 的实例。</typeparam>
    /// <typeparam name="TKey">表示实体对象的主键标识。</typeparam>
    /// <seealso cref="AUV.Entityframework6.IEntityframeworkRepository{TEntity, TKey}" />
    public abstract class EntityframeworkRepository<TEntity, TKey>
        : IEntityframeworkRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        IUnitOfWork _unitOfWork;
        /// <summary>
        /// 使用 <see cref="IUnitOfWork"/> 实例初始化 <see cref="EntityframeworkRepository{TEntity, TKey}" /> 类的新实例
        /// </summary>
        /// <param name="unitOfWork">表示一个工作单元。</param>
        protected EntityframeworkRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 获取当前可查询的对象。
        /// </summary>
        public virtual IQueryable<TEntity> Query() => Entity;

        /// <summary>
        /// 获取当前仓储的数据集对象。
        /// </summary>
        internal DbSet<TEntity> Entity => _unitOfWork.AsDbContext().Set<TEntity>();

        /// <summary>
        /// 可以将指定实体添加到当前仓储。
        /// </summary>
        /// <param name="entity">需要添加的实体。</param>
        public virtual void Add(TEntity entity)
        {
            Entity.Add(entity);
        }

        /// <summary>
        /// 异步从当前仓储中查找指定唯一 Id 的实体。
        /// </summary>
        /// <param name="id">要查找的实体唯一 Id 值。</param>
        /// <returns></returns>
        public virtual Task<TEntity> FindAsync(TKey id) => Entity.FindAsync(id);

        /// <summary>
        /// 从 DbSet 中移除指定的实体，请先实体附加到上下文，该实体必须存在于数据库中，在调用 SaveChanges 后进行物理删除；否则会抛出异常。
        /// </summary>
        /// <param name="entity">需要移除的实体。</param>
        public virtual void Remove(TEntity entity)
        {
            Entity.Remove(entity);
        }
    }
}
