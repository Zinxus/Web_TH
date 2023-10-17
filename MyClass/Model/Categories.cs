using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
	[Table("Categories")]
	public class Categories
	{
		[Key]
		public int Id { get; set; }

		[Required (ErrorMessage ="ten loai SP khong de trong")]
		[Display(Name = "Tên loại SP")]
		public string Name { get; set; }

		[Display(Name = "Tên rút gọn")]
		public string Slug { get; set; }

		[Display(Name = "Cấp cha")]
		public int? ParentId { get; set; }

		[Display(Name = "Sắp xếp")]
		public int? Order { get; set; }
		[Display(Name = "Mô tả")]
		[Required(ErrorMessage = "Mo ta khong de trong")]
		public string MetaDesc { get; set; }

		[Required]
		[Display(Name = "Từ khóa")]
		public string MetaKey { get; set; }

		[Display(Name = "Tạo bởi")]
		[Required(ErrorMessage = "Nguoi tao khong de trong")]
		public int CreateBy { get; set; }

		[Display(Name = "Ngày tạo")]
		[Required(ErrorMessage = "Ngay tao khong de trong")]
		public DateTime CreateAt { get; set; }
		[Display(Name = "Cập nhật bởi")]
		[Required(ErrorMessage = "Nguoi cap nhat khong de trong")]
		public int UpdateBy { get; set; }

		[Display(Name = "Ngày cập nhật")]
		[Required(ErrorMessage = "Ngay cap nhat khong de trong")]
		public DateTime UpdateAt { get; set; }

		[Display(Name = "Trạng thái")]
		[Required(ErrorMessage = "Trang thai khong de trong")]
		public int Status { get; set; }
	}
}