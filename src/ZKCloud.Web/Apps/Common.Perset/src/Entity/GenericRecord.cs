using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.Apps.Common.User.src.Entity;

namespace ZKCloud.Web.apps.Common.Perset.src.Entity
{
    /// <summary>
    /// 通用记录
    /// 删除：直接从数据库中删除
    /// 注意：
    /// 这个类型应该用于记录性的数据，不应该储存有业务关联的数据
    /// </summary>
    public class GenericRecord : EntityBase{
        /// <summary>
        /// 记录Id
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 记录类型
        /// 格式请使用 "插件.类型"
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 关联的数据Id
        /// </summary>
        public virtual long? ReleatedId { get; set; }
        /// <summary>
        /// 创建记录的用户Id，系统记录时等于null
        /// </summary>
        public virtual long? CreatorId { get; set; }
        /// <summary>
        /// 创建记录的用户，根据CreatorId自动获取
        /// </summary>
        public virtual User Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 记录的保留时间，等于null时永久保留
        /// </summary>
        public virtual DateTime? KeepUntil { get; set; }
        /// <summary>
        /// 记录内容，文本或Html
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 内部数据，可以储存一些特殊数据的Json
        /// </summary>
        public virtual string InternalData { get; set; }
    }

    /// <summary>
    /// 创建实体
    /// </summary>
    public class GenericRecordCreator : IModelCreator
    {
        public void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenericRecord>(d =>
            {
                d.ToTable("Common_GenericRecord");
                d.HasKey(e => e.Id);
            });
        }
    }
}
