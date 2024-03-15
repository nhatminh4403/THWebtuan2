using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteBanHang.Models
{
    public class Category
    {
        [DisplayName("Mã loại")]
        public int Id { get; set; }
        [DisplayName("Tên loại")]
        [Required,StringLength(50)]
        public string Name { get; set; }
    }
}
