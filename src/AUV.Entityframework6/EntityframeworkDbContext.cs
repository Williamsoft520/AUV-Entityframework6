using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace AUV.Entityframework6
{
    /// <summary>
    /// 提供 <see cref="Data.IUnitOfWork"/> 默认实现并管理 Entityframework 的 <see cref="DbContext"/> 实例。
    /// 一般使用注入的方式进行管理，查看 https://github.com/Williamsoft520/AUV/wiki 获得进一步文档。
    /// </summary>
    /// <seealso cref="DisposableHandler" />
    /// <seealso cref="IDisposable" />
    public class EntityframeworkDbContext : DisposableHandler, Data.IUnitOfWork,IDisposable
    {
        private readonly ThreadLocal<DbContext> _context;
        /// <summary>
        /// 初始化 <see cref="EntityframeworkDbContext"/> 类的新实例。
        /// </summary>
        /// <param name="context">指定 Entityframework 中的 <see cref="DbContext"/> 实例。</param>
        public EntityframeworkDbContext(DbContext context)
        {
            _context = new ThreadLocal<DbContext>(() => context);
        }


        /// <summary>
        /// 表示使用异步的方式将当前的工作单元完结。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public virtual Task CompleteAsync() => _context.Value.SaveChangesAsync();
        
        /// <summary>
        /// 释放当前的 DbContext 对象。
        /// </summary>
        protected override void DisposeHandler() => _context.Dispose();
    }
}
