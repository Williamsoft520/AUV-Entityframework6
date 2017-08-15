namespace AUV.Entityframework6
{
    using AUV.Data;
    using System.Data.Entity;

    /// <summary>
    /// 提供对数据库上下文的基本功能。
    /// </summary>
    /// <seealso cref="ISqlCommand" />
    /// <seealso cref="IUnitOfWork" />
    public interface IDbContext :ISqlCommand, IUnitOfWork
    {
        /// <summary>
        /// 创建 <see cref="DbSet{TEntity}"/> 实例。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <returns></returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        /// <summary>
        /// 创建 <see cref="System.Data.Entity.Infrastructure.DbEntityEntry{TEntity}"/> 的实例。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <param name="entity">实体。</param>
        /// <returns></returns>
        System.Data.Entity.Infrastructure.DbEntityEntry CreateEntry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 若上下文不存在实体，则附加到 <see cref="DbContext"/> 对象上，否则不附加。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <param name="entity">要附加的实体。</param>
        void AttachIfNotExist<TEntity>(TEntity entity) where TEntity : class;
    }
}
