using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Entity;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using Serilog;
using Services.Interfaces;
using Services.ServiceModels;


namespace Services.Features
{
   public class QueryBookmarks
    {
        private static ILogger _logger = Log.ForContext<QueryBookmarks>();
        public class Request :  IRequest<Response>
        {

        }

        public class Response
        {
            public List<BookmarkVM> listBookmarks { get; set; }
        }

        public  class Handler : IRequestHandler<Request, Response>
        {
            private IBookmarkService _IBookmarkService;
            

            public Handler(IBookmarkService IBookmarkService)
            {
                _IBookmarkService = IBookmarkService;
                
            }
            public async  Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                
                    var listOfBookmarks = _IBookmarkService.GetBookmarks();

                    if (listOfBookmarks.Any())
                    {
                        try
                        {
                            var bookmarksVmList = new List<BookmarkVM>();

                            

                            foreach (var item in listOfBookmarks)
                            {
                                var bookmarkVM = new BookmarkVM();
                                bookmarkVM.ID = item.ID;
                                bookmarkVM.Category = new Category
                                {
                                    UserID = item.Category?.UserID,
                                    ID = item.Category?.ID ?? 0,
                                    Name = item.Category?.Name
                                };
                                bookmarkVM.CreateDate = item.CreateDate;
                                bookmarkVM.UserID = item.UserID;
                                bookmarkVM.ShortDescription = item.ShortDescription;
                                bookmarkVM.URL = item.URL;
                                bookmarksVmList.Add(bookmarkVM);
                            }
                            return await Task.FromResult(new Response { listBookmarks = bookmarksVmList });
                        }
                        catch (Exception e)
                        {
                            _logger.Error(e, "Query Bookmarks failed");
                        }
                   
                    }
                    return await Task.FromResult(new Response());

            }
        }
    }
}
