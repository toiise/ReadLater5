using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Entity;
using Services.Interfaces;
using Services.ServiceModels;

namespace Services.Features
{
    public class QueryBookmarksById
    {
        private static ILogger _logger = Log.ForContext<QueryBookmarks>();
        public class Request : IRequest<Response>
        {
            public string Id { get; set; }
        }

        public class Response
        {
            public List<BookmarkVM> listBookmarksByUser { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private IBookmarkService _IBookmarkService;


            public Handler(IBookmarkService IBookmarkService)
            {
                _IBookmarkService = IBookmarkService;

            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var listOfBookmarks = _IBookmarkService.GetBookmarksByUser(request.Id);

                if (listOfBookmarks.Result.Any())
                {
                    try
                    {
                        var bookmarksVmList = new List<BookmarkVM>();

                      

                        foreach (var item in listOfBookmarks.Result)
                        {
                            var bookmarkVM = new BookmarkVM();
                            bookmarkVM.ID = item.ID;
                            bookmarkVM.Category = new Category
                            {
                                UserID = item.Category.UserID,
                                ID = item.Category.ID,
                                Name = item.Category.Name
                            };
                            bookmarkVM.CreateDate = item.CreateDate;
                            bookmarkVM.UserID = item.UserID;
                            bookmarkVM.ShortDescription = item.ShortDescription;
                            bookmarkVM.URL = item.URL;
                            bookmarksVmList.Add(bookmarkVM);
                        }
                        return await Task.FromResult(new Response { listBookmarksByUser = bookmarksVmList });
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e, "Query Bookmarks By Id failed");
                    }

                }
                else
                {
                    _logger.Error( "No Bookmarks for the user were found");
                }
                return await Task.FromResult(new Response());
            }
        }
    }
}
