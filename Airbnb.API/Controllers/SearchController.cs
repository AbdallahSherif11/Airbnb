using Airbnb.Core.Services.Contract.SmartSearchService.Contract;
using Microsoft.AspNetCore.Mvc;
using Airbnb.Core.DTOs.SmartSearchDTOs;
using Airbnb.Service.Services.SearchService;
using Newtonsoft.Json;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISmartSearchService _smartSearchService;
        private readonly IOllamaService _ollamaService;

        public SearchController(ISmartSearchService smartSearchService, IOllamaService ollamaService)
        {
            _smartSearchService = smartSearchService;
            _ollamaService = ollamaService;
        }

        [HttpPost("smart")]
        public async Task<IActionResult> SmartSearch([FromBody] SmartSearchQueryDTO query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _smartSearchService.SmartSearchAsync(query.ToFilterResult(), query.Keyword);
            return Ok(result);
        }


        [HttpPost("smart-search-nl")]
        public async Task<IActionResult> SmartSearchNaturalLanguage([FromBody] string prompt)
        {
            var filters = await _ollamaService.ExtractFiltersFromPromptAsync(prompt);
            Console.WriteLine("=== FilterResult ===");
            Console.WriteLine(JsonConvert.SerializeObject(filters, Formatting.Indented));
            var results = await _smartSearchService.SmartSearchAsync(filters);
            return Ok(results);
        }




    }

}
