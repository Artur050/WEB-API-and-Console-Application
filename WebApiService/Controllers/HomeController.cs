using Microsoft.AspNetCore.Mvc;
using WordСount;

namespace WebApiService.Controllers
{
    [ApiController]
    [Route("api/word-count")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IDictionary<string, int>> Get([FromQuery]string path)
        {
            var wordCount = new WordCount();

            return wordCount.WordCountUniq2(path);
        }
    }
}
