using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Services.Features;
using Services.ServiceModels;

namespace ReadLater5.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookmarkWebApiController : ControllerBase
    {

        private readonly IMediator _Mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public BookmarkWebApiController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _Mediator = mediator;
            _userManager = userManager;
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

        [HttpPost]
        [Route("api/postbookmark/")]
        public async Task<PostBookmark.Response> PostBookmarks([FromBody] BookmarkVM bookm)
        {


            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(currentUserName);

            var req = new PostBookmark.Request
            {
                CategoryName = bookm.Category.Name,
                ShortDescription = bookm.ShortDescription,
                Url = bookm.URL,
                UserID = user.Id
            };

            var result = await _Mediator.Send(req);

            return result;

        }


        [HttpDelete]
        [Route("api/delete/{id}")]
        public async Task<DeleteBookmark.Response> DeleteBookmark(int id)
        {
            
            var req = new DeleteBookmark.Request
            {
               ID = id
            };

            var result = await _Mediator.Send(req);

            return result;

        }

        [HttpPut]
        [Route("api/put/{id}")]
        public async Task<PutBookmark.Response> PutBookmark(int id, BookmarkVM bookm )
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(currentUserName);

            var req = new PutBookmark.Request
            {
                BookmarkId = id,
                CategoryName = bookm.Category?.Name,
                ShortDescription = bookm.ShortDescription,
                Url = bookm.URL,
                UserID = user.Id
            };

            var result = await _Mediator.Send(req);

            return result;

        }
    }
}
