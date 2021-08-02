using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Services.Features;

namespace ReadLater5.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookmarkCategoriesWebApiController : ControllerBase
    {

        private readonly IMediator _Mediator;

        public BookmarkCategoriesWebApiController(IMediator mediator)
        {
            _Mediator = mediator;
        }
        [HttpGet]
        [Route("api/getbookmarks")]
        public async Task<QueryBookmarks.Response> GetBookmarks()
        {

                var req = new QueryBookmarks.Request();

                var res = await _Mediator.Send(req);

                return res;

        }

        [HttpGet]
        [Route("api/getbookmarksbyid/{id}")]
        public async Task<QueryBookmarksById.Response> GetBookmarksById(string id)
        {

            var req = new QueryBookmarksById.Request
            {
                 Id = id
            };

            var res = await _Mediator.Send(req);

            return res;

        }
    }
}
