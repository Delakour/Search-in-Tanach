using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TanachApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TanachController : ControllerBase
    {
        [HttpGet]
        [Route("GetPasukName")]
        public List<string> GetPasukName(string name)
        {
            return BLL.BibleBll.SearchName(name);
        }

        [HttpGet]
        [Route("GetWord")]
        public List<Enteties.Location> GetWord(string word)
        {
            return BLL.BibleBll.SearchWord(word);
        }
    }
}
