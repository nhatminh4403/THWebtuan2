using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace WebsiteBanHang.Models
{
    public class Product
    {
        [DisplayName("Mã hàng")]
        public int Id { get; set; }
        [Required,StringLength(50)]
        [DisplayName("Tên hàng")]
        public string Name { get; set; }
        [DisplayName("Giá")]
        [Range(0.01,100000.00)]
        public decimal Price { get; set;}
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        public int CategoryId { get; set; }

        [FileExtensions(Extensions = "png,jpg")]
        public string? ImageUrl { get; set; } // Đường dẫn đến hình ảnh đại diện
        public List<string>? ImageUrls { get; set; } // Danh sách các hình ảnh khác
    }
}
