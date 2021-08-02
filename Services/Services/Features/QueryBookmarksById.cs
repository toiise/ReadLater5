﻿using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.ViewModels;
using System.Threading;

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

                if (listOfBookmarks.Any())
                {
                    try
                    {
                        var bookmarksVmList = new List<BookmarkVM>();

                        var bookmarkVM = new BookmarkVM();

                        foreach (var item in listOfBookmarks)
                        {
                            bookmarkVM.ID = item.ID;
                            bookmarkVM.CategoryId = item.CategoryId;
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
