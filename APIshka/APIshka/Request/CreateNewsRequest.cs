using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIshka.Request
{
    public class CreateNewsRequest
    {
        //[Required(ErrorMessage ="Заголовок Обязательно")]

        public string Title { get ; set; }

        public string Description { get ; set; }

        public IFormFile? Image { get ; set; }

    }
}
