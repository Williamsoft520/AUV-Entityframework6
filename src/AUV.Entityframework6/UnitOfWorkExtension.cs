using System;

namespace AUV.Entityframework6
{
    using Data;
    using System.Data.Entity;

    /// <summary>
    /// 表示工作单元的扩展实例。
    /// </summary>
    public static class UnitOfWorkExtension
    {
        /// <summary>
        /// 将当前的 <see cref="IUnitOfWork"/>实例转换为 <see cref="DbContext"/> 实例。
        /// </summary>        
        /// <param name="unitOfWork">实现了 <see cref="IUnitOfWork"/> 接口的工作单元扩展实例。</param>
        /// <returns>当前使用的 <see cref="DbContext"/> 实例。</returns>
        /// <exception cref="NotSupportedException">当前 <see cref="IUnitOfWork"/> 扩展实例无法转换为 <see cref="DbContext"/> 实例。</exception>
        public static DbContext AsDbContext(this IUnitOfWork unitOfWork)
        => unitOfWork as DbContext ?? throw new NotSupportedException("当前 UnitOfWork 不是 DbContext 的实例，请检查你派生的 DbContext 子类是否继承了 IUnitOfWork 接口；或使用 AUV.Entityframework6.EntityframeworkDbContext 实例，更多帮助请查阅 https://github.com/Williamsoft520/AUV-Entityframework6 .");


        /// <summary>
        /// 若上下文不存在实体，则附加到 <see cref="DbContext" /> 对象上，否则不附加。
        /// </summary>
        /// <param name="unitOfWork">实现了 <see cref="IUnitOfWork"/> 接口的工作单元扩展实例。</param>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <param name="entity">要附加的实体。</param>
        /// <returns>若附加成功，则返回 <c>true</c>；否则为 <c>false</c>。</returns>
        public static bool AttachIfNotExist<TEntity>(this IUnitOfWork unitOfWork, TEntity entity)
            where TEntity : class
        {
            if (!unitOfWork.AsDbContext().Set<TEntity>().Local.Contains(entity))
            {
                unitOfWork.AsDbContext().Set<TEntity>().Attach(entity);
                return true;
            }
            return false;
        }        
    }
}
