using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.Common.User.src.Entity;

namespace ZKCloud.Web.apps.Common.Perset.src.Entity
{
    /// <summary>
    /// 评论
    /// </summary>
    public class GenericComment : EntityBase{

        /// <summary>
		/// Id
		/// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 评论ID
        /// </summary>
        public virtual long CommentId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public virtual CommentCheck State { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual long UserId { get; set; }
        /// <summary>
        /// 所关联的用户
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public virtual bool Anony { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string Nick { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 回复
        /// </summary>
        public virtual string Reply { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public virtual decimal Score { get; set; }
        /// <summary>
        /// 赞
        /// </summary>
        public virtual long Support { get; set; }
        /// <summary>
        /// 踩
        /// </summary>
        public virtual long StepOn { get; set; }
        /// <summary>
        /// 置顶
        /// </summary>
        public virtual bool PlacedTop { get; set; }
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

    /// <summary>
    /// 创建实体
    /// </summary>
    public class GenericCommentCreator : IModelCreator
    {
        public void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenericComment>(d =>
            {
                d.ToTable("Common_GenericComment");
                d.HasKey(e => e.Id);
            });
        }
    }
}
