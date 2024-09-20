using Blog.Data.Models;

namespace Blog.Blazor.Services
{
    public class BlogService
    {
        private readonly HttpClient _httpClient;

        public BlogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BlogPost>> GetBlogPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BlogPost>>("https://localhost:5001/Blog");
        }
    }
}
