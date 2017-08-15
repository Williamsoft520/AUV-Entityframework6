using AUV.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AUV.Entityframework6
{
    /// <summary>
    /// 为 Entityframework 仓储实例提供扩展。
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 获取当前仓储的 <see cref="DbSet{TEntity}"/> 实例。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository"><see cref="IRepository{TEntity, TKey}"/> 实例。</param>
        /// <returns>仓储对应的 <see cref="DbSet{TEntity}"/> 实例。</returns>
        /// <exception cref="System.NotSupportedException">当前使用的查询对象不支持对 DbSet 的转换。</exception>
        public static DbSet<TEntity> GetDbSet<TEntity, TKey>(this IRepository<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>
        {
            var query = repository.AsEF().Query() as DbSet<TEntity>;
            if (query == null)
            {
                throw new NotSupportedException("当前使用的查询对象不支持对 DbSet 的转换。");
            }
            return query;
        }

        /// <summary>
        /// 转换成 <see cref="IEntityframeworkRepository{TEntity, TKey}" /> 实例。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <returns>
        /// 实现了 <see cref="IEntityframeworkRepository{TEntity, TKey}" /> 的实例。
        /// </returns>
        /// <exception cref="ArgumentNullException">repository 值不能为空。</exception>
        /// <exception cref="NotSupportedException">不支持对 IEFRepository 的转换。</exception>
        public static IEntityframeworkRepository<TEntity, TKey> AsEF<TEntity, TKey>(this IRepository<TEntity, TKey> repository)
                    where TEntity : class, IEntity<TKey>
        {
            repository = repository ?? throw new ArgumentNullException(nameof(repository));

            var result = repository as IEntityframeworkRepository<TEntity, TKey>;
            if (result == null)
            {
                throw new NotSupportedException("不支持对 IEFRepository 的转换。");
            }
            return result;
        }

        ///// <summary>
        ///// 使用异步方式获取以指定 Lambda 表达式为排序字段的分页数据。
        ///// </summary>
        ///// <typeparam name="TOrderKey">排序字段类型。</typeparam>
        ///// <param name="repository">仓储的扩展实例。</param>
        ///// <param name="orderBy">排序字段的 Lambda 表达式。</param>
        ///// <param name="page">当前页码。</param>
        ///// <param name="size">每页的数据量。</param>
        ///// <param name="orderByStyles">排序方式。</param>
        ///// <typeparam name="TEntity">分页的实体。</typeparam>
        ///// <typeparam name="TKey">主键类型。</typeparam>
        ///// <returns>分页集合的实例。</returns>
        //public static Task<IPagedCollection<TEntity>> FindPagedAsync<TEntity, TKey, TOrderKey>
        //(this IEntityframeworkRepository<TEntity, TKey> repository,
        //    Expression<Func<TEntity, TOrderKey>> orderBy, int page, int size, OrderByStyles orderByStyles = OrderByStyles.DESC)
        //    where TEntity : class, IEntity<TKey>
        //=>
        //    Task.Run(() => FindPaged(repository, orderBy, page, size, orderByStyles));


        ///// <summary>
        ///// 获取以指定 Lambda 表达式为排序字段的分页数据。
        ///// </summary>
        ///// <typeparam name="TOrderKey">排序字段类型。</typeparam>
        ///// <param name="repository">仓储的扩展实例。</param>
        ///// <param name="orderBy">排序字段的 Lambda 表达式。</param>
        ///// <param name="page">当前页码。</param>
        ///// <param name="size">每页的数据量。</param>
        ///// <param name="orderByStyles">排序方式。</param>
        ///// <typeparam name="TEntity">分页的实体。</typeparam>
        ///// <typeparam name="TKey">主键类型。</typeparam>
        ///// <returns>分页集合的实例。</returns>
        //public static IPagedCollection<TEntity> FindPaged<TEntity, TKey, TOrderKey>
        //(this IEntityframeworkRepository<TEntity, TKey> repository,
        //    Expression<Func<TEntity, TOrderKey>> orderBy, int page, int size, OrderByStyles orderByStyles = OrderByStyles.DESC)
        //where TEntity : class, IEntity<TKey>
        //=> (
        //        orderByStyles == OrderByStyles.ASC
        //        ? repository.GetDbSet().OrderBy(orderBy)
        //        : repository.GetDbSet().OrderByDescending(orderBy)
        //        )
        //    .Paged(page, size);

        /// <summary>
        /// 表示可使用一个带有 Lambda 表达式作为条件的的查询标准。
        /// </summary>
        /// <typeparam name="TEntity">查询的实体类型。</typeparam>
        /// <typeparam name="TKey">实体主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 扩展实例。</param>
        /// <param name="predicate">这是一个 Lambda 表达式，作为一个查询条件。</param>
        /// <returns>
        ///   <see cref="IQueryable{T}" /> 查询标准接口。
        /// </returns>
        public static IQueryable<TEntity> Query<TEntity, TKey>(this IRepository<TEntity, TKey> repository, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, IEntity<TKey>
        {
            return repository.Query().Where(predicate);
        }

        /// <summary>
        /// 将符合指定 Id 的实体从仓储中移除。在调用工作单元的 <see cref="IUnitOfWork.CompleteAsync" /> 后更新数据库。
        /// </summary>
        /// <typeparam name="TEntity">查询的实体类型。</typeparam>
        /// <typeparam name="TKey">实体主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 扩展实例。</param>
        /// <param name="id">要移除的实体主键。</param>
        /// <exception cref="NullReferenceException">要移除的实体不存在。</exception>
        public static async Task Remove<TEntity, TKey>(this IRepository<TEntity, TKey> repository, TKey id)
            where TEntity : class, IEntity<TKey>
        {
            var entity = await repository.FindAsync(id);
            if (entity == null)
            {
                throw new NullReferenceException($"当前指定的 Id({id}) 所对应的数据无效，可能已被删除。");
            }

            repository.Remove(entity);
        }

        /// <summary>
        /// 从仓储中移除指定实体集合。在调用工作单元的 <see cref="IUnitOfWork.CompleteAsync" /> 后更新数据库。
        /// </summary>
        /// <typeparam name="TEntity">查询的实体类型。</typeparam>
        /// <typeparam name="TKey">实体主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 扩展实例。</param>
        /// <param name="entities">要进行移除的实体集合。</param>
        public static void RemoveRange<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TEntity> entities)
            where TEntity : class, IEntity<TKey>
        {
            foreach (var item in entities)
            {
                repository.Remove(item);
            }
        }


        /// <summary>
        /// 从仓储中移除符合指定 Id 集合的实体。在调用工作单元的 <see cref="IUnitOfWork.CompleteAsync" /> 后更新数据库。
        /// </summary>
        /// <typeparam name="TEntity">查询的实体类型。</typeparam>
        /// <typeparam name="TKey">实体主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 扩展实例。</param>
        /// <param name="idCollection">要移除的实体主键集合。若指定的主键实体不存在则不进行移除。</param>
        public static async Task RemoveRange<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TKey> idCollection)
                    where TEntity : class, IEntity<TKey>
        {
            foreach (var item in idCollection)
            {
                try
                {
                   await repository.Remove(item);
                }
                catch (NullReferenceException)
                {
                    continue;
                }
            }
        }
        
        /// <summary>
        /// 表示使用非跟踪的实体方式进行查询。
        /// </summary>
        /// <typeparam name="TEntity">查询的实体类型。</typeparam>
        /// <typeparam name="TKey">实体主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 扩展实例。</param>
        /// <returns>
        ///   <see cref="IQueryable{T}" /> 查询标准接口。
        /// </returns>
        public static IQueryable<TEntity> QueryNoTracking<TEntity, TKey>(this IRepository<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>
            => repository.GetDbSet().AsNoTracking<TEntity>();

        /// <summary>
        /// 表示使用非跟踪的实体方式进行查询。
        /// </summary>
        /// <typeparam name="TEntity">查询的实体类型。</typeparam>
        /// <typeparam name="TKey">实体主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 扩展实例。</param>
        /// <param name="predicate">这是一个 Lambda 表达式，作为一个查询条件。</param>
        /// <returns>
        ///   <see cref="IQueryable{T}" /> 查询标准接口。
        /// </returns>
        public static IQueryable<TEntity> QueryNoTracking<TEntity, TKey>(this IRepository<TEntity, TKey> repository, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, IEntity<TKey>
            => repository.GetDbSet().Where(predicate).AsNoTracking();
    }
}
