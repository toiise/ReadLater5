using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using System.Threading;
using Services.Interfaces;
using Services.ServiceModels;

namespace Services.Features
{
   public class PutBookmark
    {
        private static ILogger _logger = Log.ForContext<PutBookmark>();
        public class Request : IRequest<PutBookmark.Response>
        {
            public int BookmarkId { get; set; }
            public string Url { get; set; }
            public string ShortDescription { get; set; }
            public string UserID { get; set; }
            public string CategoryName { get; set; }
        }

        public class Response
        {
            public BookmarkVM Bookmark { get; set; }
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
                    var bookmark = _IBookmarkService.GetBookmarkById(request.BookmarkId);

                    if (bookmark.ID != 0)
                    {

                        if (request.CategoryName != null)
                        {

                            var category = _iCategoryService.GetCategory(request.CategoryName);

                            var addCategoryName = new Category();

                            if (category.Name == null)
                            {
                                var cattCreated = new CategoryVM
                                {
                                    Name = request.CategoryName,
                                    UserID = request.UserID,
                                };
                                addCategoryName = _iCategoryService.CreateCategory(cattCreated);
                                bookmark.Category.Name = addCategoryName.Name;
                            }
                            else
                            {
                                addCategoryName.Name = category.Name;
                            }

                            
                           
                        }

                        if (request.ShortDescription != null)
                        {
                            bookmark.ShortDescription = request.ShortDescription;
                        }

                        if (bookmark.URL != null)
                        {
                            bookmark.URL = request.Url;
                        }

                        _IBookmarkService.UpdateBookmark(bookmark);
                    }
                    else
                    {
                        _logger.Error($"Bookmark not found");
                    }
                    return Task.FromResult(new Response { Bookmark = bookmark });

                }
                catch (Exception e)
                {
                    _logger.Error($"Put Bookmark failed : {e}");
                    return Task.FromResult(new Response());
                }
                
            }
        }
    }
}
