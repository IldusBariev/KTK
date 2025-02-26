using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace BlazorApp2.Request
{
    public class NewsAdd
    {
        [Required(ErrorMessage = "Название объязательно")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Описание объязательно")]
        public string Description { get; set; }
    }
}
