using Microsoft.AspNetCore.Http;

namespace BlazorApp2.Response
{
    public class GetAllNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
