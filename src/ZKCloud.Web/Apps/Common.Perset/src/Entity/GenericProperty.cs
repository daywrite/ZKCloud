using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.Common.Perset.src.Enum;

namespace ZKCloud.Web.apps.Common.Perset.src.Entity
{
    /// <summary>
    /// 扩展属性
    /// </summary>
    public class GenericProperty : EntityBase{

        /// <summary>
        /// Id
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 属性标识
        /// </summary>
        public virtual string Identifier { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public virtual InputControlType InputControlType { get; set; }
        /// <summary>
        /// 文本提示框
        /// </summary>
        public virtual string PlaceHolder { get; set; }
        /// <summary>
        /// 帮助提示
        /// </summary>
        public virtual string HelpText { get; set; }
        /// <summary>
        /// 正则验证
        /// </summary>
        public virtual RegularType RegularType { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public virtual bool IsRequired { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public virtual string DefaultValue { get; set; }
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
    public class GenericPropertyCreator : IModelCreator
    {
        public void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenericProperty>(d =>
            {
                d.ToTable("Common_GenericProperty");
                d.HasKey(e => e.Id);
            });
        }
    }
}
