//using Microsoft.AspNetCore.Mvc;
//using DigiShopping.Models;
//using System.Collections.Generic;
//using System.Linq;

//namespace DigiShopping.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ProductDetailsController : ControllerBase
//    {
//        private static readonly List<ProductDetail> Products = new();

//        [HttpGet]
//        public ActionResult<IEnumerable<ProductDetail>> GetAll()
//        {
//            return Ok(Products);
//        }

//        [HttpGet("{id}")]
//        public ActionResult<ProductDetail> GetById(string id)
//        {
//            var product = Products.FirstOrDefault(p => p.ProductId == id);
//            if (product == null)
//                return NotFound();
//            return Ok(product);
//        }

//        [HttpPost]
//        public ActionResult<ProductDetail> Create(ProductDetail product)
//        {
//            Products.Add(product);
//            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
//        }

//        [HttpPut("{id}")]
//        public IActionResult Update(string id, ProductDetail updatedProduct)
//        {
//            var product = Products.FirstOrDefault(p => p.ProductId == id);
//            if (product == null)
//                return NotFound();

//            product.ProductName = updatedProduct.ProductName;
//            product.Category = updatedProduct.Category;
//            product.UnitPrice = updatedProduct.UnitPrice;

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(string id)
//        {
//            var product = Products.FirstOrDefault(p => p.ProductId == id);
//            if (product == null)
//                return NotFound();

//            Products.Remove(product);
//            return NoContent();
//        }
//    }
//}
