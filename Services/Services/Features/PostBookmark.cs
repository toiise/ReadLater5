using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entity;
using MediatR;
using Serilog;
using Services.Interfaces;
using Services.ServiceModels;

namespace Services.Features
{
   public class PostBookmark
    {
        private static ILogger _logger = Log.ForContext<PostBookmark>();
        public class Request : IRequest<PostBookmark.Response>
        {
            public string Url { get; set; }
            public string ShortDescription { get; set; }
            public string UserID { get; set; }
            public string CategoryName { get; set; }
        }

        public class Response
        {
            public Bookmark Bookmark { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private IBookmarkService _IBookmarkService;
            private ICategoryService _iCategoryService;


            public Handler(IBookmarkService iBookmarkService, ICategoryService iCategoryService)
            {
                _IBookmarkService = iBookmarkService;
                _iCategoryService = iCategoryService;
            }
            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var categ = _iCategoryService.GetCategory(request.CategoryName);

                    var addCategory = new Category();

                    if (categ.Name == null)
                    {
                        try
                        {
                            var cattCreated = new CategoryVM
                            {
                                Name = request.CategoryName,
                                UserID = request.UserID,
                            };
                            addCategory = _iCategoryService.CreateCategory(cattCreated);


                        }
                        catch (Exception e)
                        {
                            _logger.Error($"Post Category failed : {e}");
                            addCategory = new Category();
                        }
                       

                    }
                    else
                    {
                        addCategory.UserID = categ.UserID;
                        addCategory.Name = categ.Name;
                    }


                    var bookmarkFromPost = new BookmarkVM
                    {
                        URL = request.Url,
                        CreateDate = DateTime.UtcNow,
                        ShortDescription = request.ShortDescription,
                        UserID = request.UserID,
                        Category = addCategory

                    };


                    var result = _IBookmarkService.CreateBookmark(bookmarkFromPost);


                    return Task.FromResult(new Response { Bookmark = result });
                }
                catch (Exception e)
                {
                    _logger.Error($"Post Bookmark failed : {e}");
                    return Task.FromResult(new Response ());
                }
               

            }
        }
    }
}
