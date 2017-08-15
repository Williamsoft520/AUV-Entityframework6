using AUV.Data;
using System;

namespace AUV.Entityframework6
{
    /// <summary>
    /// 提供使用 <see cref="Int32"/> 类型作为主键的仓储。
    /// </summary>
    /// <typeparam name="TEntity">仓储操作的实体类型，该实体必须继承自 <see cref="IIdentityEntity"/> 接口。</typeparam>
    /// <seealso cref="EntityframeworkRepository{TEntity, TKey}" />
    /// <seealso cref="IIdentityRepository{TEntity}" />
    public class EntityframeworkIdentityRepository<TEntity>
        : EntityframeworkRepository<TEntity, int>,
        IIdentityRepository<TEntity>
        where TEntity :class,IIdentityEntity
    {
        /// <summary>
        /// 使用 <see cref="IDbContext" /> 实例初始化 <see cref="EntityframeworkIdentityRepository{TEntity}" /> 类的新实例。
        /// </summary>
        /// <param name="context">可管理 EF 上下文的实例。</param>
        public EntityframeworkIdentityRepository(IDbContext context) : base(context)
        {
        }
    }
}
