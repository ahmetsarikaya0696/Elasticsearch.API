using Elasticsearch.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentsController(DocumentRepository documentRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> TermLevelQuery(string customerFirstName)
        {
            return Ok(await documentRepository.TermLevelQueryAsync(customerFirstName));
        }

        [HttpPost]
        public async Task<IActionResult> TermsLevelQuery(List<string> customerFirstNameList)
        {
            return Ok(await documentRepository.TermsLevelQueryAsync(customerFirstNameList));
        }

        [HttpGet]
        public async Task<IActionResult> PrefixQuery(string customerFullName)
        {
            return Ok(await documentRepository.PrefixQueryAsync(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
        {
            return Ok(await documentRepository.RangeQueryAsync(fromPrice, toPrice));
        }

        [HttpGet]
        public async Task<IActionResult> MatchAllQuery()
        {
            return Ok(await documentRepository.MatchAllQueryAsync());
        }

        [HttpGet]
        public async Task<IActionResult> MatchAllQueryPagination(int page, int size)
        {
            return Ok(await documentRepository.MatchAllQueryPaginationAsync(page, size));
        }

        [HttpGet]
        public async Task<IActionResult> WildcardQuery(string customerFullName)
        {
            return Ok(await documentRepository.WildcardQueryAsync(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> FuzzyQuery(string customerFirstName)
        {
            return Ok(await documentRepository.FuzzyQueryAsync(customerFirstName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchQueryFullText(string categoryName)
        {
            return Ok(await documentRepository.MatchQueryFullTextAsync(categoryName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchBooleanPrefixQueryFullText(string customerFullName)
        {
            return Ok(await documentRepository.MatchBooleanPrefixQueryFullTextAsync(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchPhraseQueryFullText(string customerFullName)
        {
            return Ok(await documentRepository.MatchPhraseQueryFullTextAsync(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> CompoundQueryExampleOne(string cityName, double taxfulTotalPrice, string categoryName, string manufacturerName)
        {
            return Ok(await documentRepository.CompoundQueryExampleOneAsync(cityName, taxfulTotalPrice, categoryName, manufacturerName));
        }

        [HttpGet]
        public async Task<IActionResult> CompoundQueryExampleTwo(string customerFullName)
        {
            return Ok(await documentRepository.CompoundQueryExampleTwoAsync(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> MultiMatchQueryExample(string name)
        {
            return Ok(await documentRepository.MultiMatchQueryExampleAsync(name));
        }
    }
}
