using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace AUV.Entityframework6
{
    /// <summary>
    /// 作为默认的 <see cref="IDbContext"/> 管理 Entityframework 的 DbContext 实例。
    /// </summary>
    /// <seealso cref="DisposableHandler" />
    /// <seealso cref="IDbContext" />
    /// <seealso cref="IDisposable" />
    public class EntityframeworkDbContext : DisposableHandler, IDbContext,IDisposable
    {
        private readonly DbContext _context;
        /// <summary>
        /// 初始化 <see cref="EntityframeworkDbContext"/> 类的新实例。
        /// </summary>
        /// <param name="context">指定 Entityframework 中的 <see cref="DbContext"/> 实例。</param>
        public EntityframeworkDbContext(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 若上下文不存在实体，则附加到 <see cref="DbContext" /> 对象上，否则不附加。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <param name="entity">要附加的实体。</param>
        public virtual void AttachIfNotExist<TEntity>(TEntity entity) where TEntity:class
        {
            if (!this.CreateSet<TEntity>().Local.Contains(entity))
            {
                CreateSet<TEntity>().Attach(entity);
            }
        }

        /// <summary>
        /// 表示使用异步的方式将当前的工作单元完结。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public virtual Task CompleteAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }


        /// <summary>
        /// 创建 <see cref="DbEntityEntry{TEntity}" /> 的实例。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <param name="entity">实体。</param>
        /// <returns></returns>
        public virtual DbEntityEntry CreateEntry<TEntity>(TEntity entity) where TEntity : class => _context.Entry(entity);

        /// <summary>
        /// 创建 <see cref="DbSet{TEntity}" /> 实例。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <returns></returns>
        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class => _context.Set<TEntity>();

        /// <summary>
        /// 异步执行指定的命令文本并返回影响行数。
        /// </summary>
        /// <param name="commandText">命令文本。</param>
        /// <param name="parameters">命令中所包含的参数列表。</param>
        /// <returns>
        /// 执行命令后影响的结果。
        /// </returns>
        public virtual Task<int> ExecuteAsync(string commandText, params object[] parameters)
        =>
            _context.Database.ExecuteSqlCommandAsync(commandText, parameters);


        /// <summary>
        /// 查询指定命令并返回指定类型的集合。
        /// </summary>
        /// <typeparam name="T">查询结果类型。</typeparam>
        /// <param name="commandText">需要执行的命令。</param>
        /// <param name="parameters">命令中所包含的参数列表。</param>
        /// <returns>
        /// 查询的结果，必须是一个对象集合。
        /// </returns>
        internal IEnumerable<T> Query<T>(string commandText, params object[] parameters)
        =>
             _context.Database.SqlQuery<T>(commandText, parameters);


        /// <summary>
        /// 异步查询指定命令并返回指定类型的集合。
        /// </summary>
        /// <typeparam name="T">查询结果类型。</typeparam>
        /// <param name="commandText">需要执行的命令。</param>
        /// <param name="parameters">命令中所包含的参数列表。</param>
        /// <returns>
        /// 查询的结果，必须是一个对象集合。
        /// </returns>
        public virtual Task<IEnumerable<T>> QueryAsync<T>(string commandText, params object[] parameters)
        =>
             Task.FromResult(Query<T>(commandText, parameters));





        /// <summary>
        /// 释放当前的 DbContext 对象。
        /// </summary>
        protected override void DisposeHandler() => _context.Dispose();
    }
}
