using Microsoft.AspNetCore.Http;


namespace Api.Models

{
    public class Picture
    {
        public string Alt { get; set; }
        public IFormFile Path { get; set; }
    }
}
