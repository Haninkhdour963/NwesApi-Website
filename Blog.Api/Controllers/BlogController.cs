using Blog.Data;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Controllers
{
    public class BlogController : ControllerBase
    {
        private readonly BlogContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _serviceApiUrl;

        public BlogController(BlogContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _serviceApiUrl = configuration["ServiceApiUrl"]; ;
        }
        public async Task<string> GetDataAsync()
        {
            var response = await _httpClient.GetAsync($"{_serviceApiUrl}/endpoint");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            //response Api from this: https://newsapi.org/account
            var response = await _httpClient.GetFromJsonAsync<CNNResponse>("aa12ec824ce745c08e9994f6e2291f23");
            foreach (var article in response.Articles)
            {
                var blogPost = new BlogPost
                {
                    Title = article.Title,
                    Article = article.Content,
                    ImageUrl = article.ImageUrl,
                    DateTime = DateTime.Now
                };
                _context.BlogPosts.Add(blogPost);
            }
            await _context.SaveChangesAsync();
            return Ok(await _context.BlogPosts.ToListAsync());
        }
    }
    public class Article
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string ImageUrl { get; set; }
        }
        public class CNNResponse
        {
            public List<Article> Articles { get; set; }
        }
    }

