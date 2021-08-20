using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Babel.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Babel.Api.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController: ControllerBase
    {
        private readonly NgonbLibraryService _ngonbLibraryService;

        public BookController(NgonbLibraryService ngonbLibraryService)
        {
            _ngonbLibraryService = ngonbLibraryService;
        }

        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> FindBooks(SearchRusmarc search)
        {
            var result = await _ngonbLibraryService.SearchByAll(search);
            return Content(result);
        }
    }
}
