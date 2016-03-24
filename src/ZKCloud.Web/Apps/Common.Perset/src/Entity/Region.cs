using Microsoft.Data.Entity;
using System.Collections.Generic;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.Apps.Common.Perset.src.Enum;

namespace ZKCloud.Web.Apps.Common.Perset.src.Entity {
	/// <summary>
	/// 地区
	/// 这个对象中的值生成后不应该修改
	/// </summary>
	public class Region : EntityBase {
		/// <summary>
		/// 地区Id
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 所属国家
		/// </summary>
		public Country Country { get; set; }
		/// <summary>
		/// 上级地区的Id，没有时等于0
		/// </summary>
		public long ParentId { get; set; }
		/// <summary>
		/// 地区名称
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// 创建实体
	/// </summary>
	public class RegionCreator : IModelCreator {
		public void CreateModel(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Region>(d =>
			{
				d.ToTable("Common_Region");
				d.HasKey(e => e.Id);
				d.Property<Country>(e => e.Country);
			});
		}
	}

}
