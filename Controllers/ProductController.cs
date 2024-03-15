using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebsiteBanHang.Models;
using WebsiteBanHang.Repositories;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }
        public IActionResult Add()
        {
            var categories = _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile imageUrl, List<IFormFile> imageUrls)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện
                    product.ImageUrl = await SaveImage(imageUrl);
                }
                /*  
                  
                  if (imageUrls != null)
                  {
                      product.ImageUrls = new List<string>();
                      foreach (var file in imageUrls)
                      {
                          // Lưu các hình ảnh khác
                          product.ImageUrls.Add(await SaveImage(file));
                      }
                  }*/
                _productRepository.Add(product);
                return RedirectToAction("Index"); // Chuyển hướng tới trang danh  sách sản phẩm 
            }
            return View(product);
        }
      
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName); // Thay

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        }
        public IActionResult Display(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Show the product update form 
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);
            var categories = _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Process the product update 
        [HttpPost]
        public async Task<IActionResult> Update(Product product , IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện
                    product.ImageUrl = await SaveImage(imageUrl);
                }
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // Show the product delete confirmation 
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _productRepository.Delete(product.Id);
            return RedirectToAction("Index");
        }
    }
}
