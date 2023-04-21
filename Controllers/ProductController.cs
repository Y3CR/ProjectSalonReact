using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSalonReact.Models;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectSalonReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProjectSalonContext _dbcontext;
        private readonly IWebHostEnvironment _environment;


        public ProductController(ProjectSalonContext context, IWebHostEnvironment environment)
        {
            _dbcontext = context;
            _environment = environment;



        }







        [HttpGet]
        [Route("Lista")]//llamado al metodo
        public async Task<IActionResult> Lista()
        {
            List<Product> list = _dbcontext.Products.OrderByDescending(x => x.IdProduct).ThenBy(x => x.DateAdmission).ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }


        //[HttpPost]
        //[Route("Guardar")]
        //public async Task<IActionResult> Guardar([FromBody] Product request)


        //{

        //var inter = new Product
        //{
        //    NameProduct = request.NameProduct,
        //    Description = request.Description,
        //    Stocks = request.Stocks,
        //    Image= request.Image,
        //    DateAdmission = DateTime.Now,


        //};


        //await _dbcontext.Products.AddAsync(inter);
        //await _dbcontext.SaveChangesAsync();
        //return StatusCode(StatusCodes.Status200OK, "ok");

        //}

        [HttpPost]
        [Route("Guardar")]

        public async Task<ActionResult<Product>> Guardar([FromForm] ProductForm form)
        {
            var name = form.NameProduct;
            var description = form.Description;
            var image = form.Image;
            var stocks = form.Stocks;
            form.DateAdmission = DateTime.Now;
            var dateA = form.DateAdmission;
            var filePath = string.Empty;
            var guid = Guid.NewGuid();
            var imageNameGlobal=""; 
            


            if (form.Image != null)
            {

                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                var fileExtension = Path.GetExtension(image.FileName);
                var uniqueFileName = $"{guid}_{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";
                imageNameGlobal = uniqueFileName;
                filePath = Path.Combine(_environment.ContentRootPath, "ClientApp", "src", "images", uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }


                //// Save image to wwwroot directory
                //var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                //var fileExtension = Path.GetExtension(image.FileName);
                //var uniqueFileName = $"{guid}_{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";
                ////var uniqueFileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";
                //filePath = Path.Combine(_environment.WebRootPath, "images", uniqueFileName);

                ////  filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await image.CopyToAsync(stream);
                //}

                //aparte de guardar en wwwroot guarda en public src


                //var publicImagesPath = Path.Combine(_environment.ContentRootPath, "clientApp", "public", "assets", "images");
                //var publicFilePath = Path.Combine(publicImagesPath, uniqueFileName);
                //using (var stream = new FileStream(publicFilePath, FileMode.Create))
                //{
                //    await image.CopyToAsync(stream);
                //}


            }


            // Save data to SQL Server
            var productNew = new Product { 
                NameProduct = name, 
                Description = description, 
                ImagePath = filePath, 
                Stocks = stocks, 
                DateAdmission = dateA, 
                imageName = imageNameGlobal };


            _dbcontext.Products.Add(productNew);
            await _dbcontext.SaveChangesAsync();

            return Ok();

        }

        [HttpGet]
        [Route("Cards")]
        public ActionResult<IEnumerable<Product>> GetProductos()
        {
            using (var context = new ProjectSalonContext())
            {
                var productos = context.Products.ToList();
                return productos;
            }
        }


        [HttpDelete]
        [Route("Borrar/{id:int}")]
        public async Task<IActionResult> Borrar(int id)
        {
           // Product product = _dbcontext.Products.Find(id);

            Product product = await _dbcontext.Products.FindAsync(id);
          

            if (System.IO.File.Exists(product.ImagePath))
            {
                // Eliminar la imagen
                System.IO.File.Delete(product.ImagePath);
            }



            _dbcontext.Products.Remove(product);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }






    }
}
