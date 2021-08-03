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
    
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesWebApiController : ControllerBase
    {
        private readonly IMediator _Mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoriesWebApiController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _Mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/getcategories")]
        public async Task<QueryCategories.Response> GetCategories()
        {

            var req = new QueryCategories.Request();

            var res = await _Mediator.Send(req);

            return res;

        }

        [HttpGet]
        [Route("api/getcategoriesbyid/{id}")]
        public async Task<QueryCategoriesById.Response> GetCategoriesById(string id)
        {

            var req = new QueryCategoriesById.Request
            {
                Id = id
            };

            var res = await _Mediator.Send(req);

            return res;

        }

        [HttpPost]
        [Route("api/postcategory/")]
        public async Task<PostCategory.Response> PostCategory([FromBody] CategoryVM categoryVM)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(currentUserName);

            var req = new PostCategory.Request
            {
                CategoryName = categoryVM.Name,
                UserID = user.Id
            };

            var result = await _Mediator.Send(req);

            return result;

        }

        [HttpDelete]
        [Route("api/deletecategory/{id}")]
        public async Task<DeleteCategory.Response> DeleteCategory(int id)
        {

            var req = new DeleteCategory.Request
            {
                ID = id
            };

            var result = await _Mediator.Send(req);

            return result;

        }

        [HttpPut]
        [Route("api/putcategory/{id}")]
        public async Task<PutCategory.Response> PutCategory(int id, CategoryVM category)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(currentUserName);

            var req = new PutCategory.Request
            {
                CategoryID = id,
                CategoryName = category.Name
            };

            var result = await _Mediator.Send(req);

            return result;

        }
    }
}
