using Microsoft.AspNetCore.Mvc;

namespace KPO2.Controllers
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Repositories.RssItemRepository;

    [Route("api/[controller]")]
    public class RssItemController : Controller
    {
        private readonly IRssItemRepository _rssItemRepository;
        
        public RssItemController(IRssItemRepository rssItemRepository)
        {
            _rssItemRepository = rssItemRepository;
        }

        [HttpGet]
        public async Task<RssItem[]> Get()
        {
            return await _rssItemRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<RssItem> Get(long id)
        {
            return await _rssItemRepository.Get(id);
        }

        [HttpPost]
        public async Task<RssItem> Post([FromBody] RssItem user)
        {
            return await _rssItemRepository.Add(user);
        }

        [HttpPut("{id}")]
        public async Task<RssItem> Put(long id, [FromBody] RssItem user)
        {
            user.Id = id;
            return await _rssItemRepository.Edit(user);
        }

        [HttpDelete("{id}")]
        public async Task<RssItem> Delete(long id)
        {
            return await _rssItemRepository.Remove(id);
        }
    }
}