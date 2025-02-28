using APIshka.DbContexts;
using APIshka.Entities;
using APIshka.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Xml.Linq;

namespace APIshka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class News : Controller
    {

        private readonly AppDbContext _dbContext;

        public News(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // Для добавления новостей. Фотографию в массив байтов
        [Authorize]
        [HttpPost("add_news")]
        public async Task<IActionResult> AddNewsAsync([FromForm]CreateNewsRequest request)
        {
            string? imagesName = null;

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Название не может быть нулловым");
            }

            if (request.Image != null)
            {
                if (request.Image.Length > 0)
                {
                    var fileExtension = Path.GetExtension(request.Image.FileName);
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    imagesName = fileName;

                    string filePath = Path.Combine("static/files/images", fileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Image.CopyToAsync(fileStream);
                    }

                }
            }

            await _dbContext.News.AddAsync(new NewsEntities(request.Title, request.Description, imagesName));
            await _dbContext.SaveChangesAsync();


            return Created();
        }

        // возвращает данные о новости и изображение
        
        [HttpGet("all_news")]
        public async Task<IActionResult> GetAllNewsAsync()
        {
            var columns = await _dbContext.News
                .Select(n => new
                {
                    n.NewsId,
                    n.Title,
                    n.Description,
                    Image = n.ImageName != null ? $"http://192.168.0.7:5155/static/files/images/{n.ImageName}" : null
                })
                .ToListAsync();


            return Ok(columns);
        }


    }
}
