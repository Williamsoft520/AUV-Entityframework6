using AUV.Data;
using System;

namespace AUV.Entityframework6
{
    /// <summary>
    /// 提供使用 <see cref="Guid"/> 类型作为主键的仓储。
    /// </summary>
    /// <typeparam name="TEntity">仓储操作的实体类型，该实体必须继承自 <see cref="IUniqueIdentifierEntity"/> 接口。</typeparam>
    /// <seealso cref="EntityframeworkRepository{TEntity,TKey}" />
    /// <seealso cref="IUniqueIdentifierRepository{TEntity}" />
    public class EntityframeworkUniqueIdentifierRepository<TEntity>
        : EntityframeworkRepository<TEntity, Guid>,
        IUniqueIdentifierRepository<TEntity>
        where TEntity : class, IUniqueIdentifierEntity
    {
        /// <summary>
        /// 使用 <see cref="IUnitOfWork" /> 实例初始化 <see cref="EntityframeworkUniqueIdentifierRepository{TEntity}" /> 类的新实例。
        /// </summary>
        /// <param name="context">可管理 EF 上下文的实例。</param>
        public EntityframeworkUniqueIdentifierRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
