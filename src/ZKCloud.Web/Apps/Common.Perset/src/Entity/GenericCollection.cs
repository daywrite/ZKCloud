using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.Common.User.src.Entity;

namespace ZKCloud.Web.apps.Common.Perset.src.Entity
{
    /// <summary>
    /// 收藏夹
    /// </summary>
    public class GenericCollection : EntityBase{
        /// <summary>
        /// Id
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 收藏ID
        /// </summary>
        public virtual long CollectionId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual long UserId { get; set; }
        /// <summary>
        /// 所关联的用户
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public virtual string ImageUrl { get; set; }
        /// <summary>
        /// 其他属性
        /// </summary>
        public virtual string Attribute { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdated { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual long DisplayOrder { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 删除状态
        /// </summary>
        public virtual DeleteState DeleteState { get; set; }
    }

    public class GenericCollectionCreator : IModelCreator
    {
        public void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenericCollection>(d =>
            {
                d.ToTable("Common_GenericCollection");
                d.HasKey(e => e.Id);
                d.Property(c => c.CollectionId);
                d.Property(c => c.Type);
                d.Property(c => c.Name).IsRequired();
                d.Property(c => c.Url);
                d.Property(c => c.ImageUrl);
                d.Property(c => c.Attribute);
                d.Property(c => c.CreateTime);
                d.Property(c => c.LastUpdated);
                d.Property(c=>c.DisplayOrder);
                d.Property(c => c.Remark);
                d.Property(c => c.DeleteState);

            });
        }
    }
}
