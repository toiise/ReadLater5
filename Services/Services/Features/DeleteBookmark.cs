using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Services.Interfaces;

namespace Services.Features
{
   public class DeleteBookmark
    {
        private static ILogger _logger = Log.ForContext<DeleteBookmark>();
        public class Request : IRequest<DeleteBookmark.Response>
        {
            public int ID { get; set; }
       
        }

        public class Response
        {
            public string deleted { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private IBookmarkService _IBookmarkService;

            public Handler(IBookmarkService iBookmarkService)
            {
                _IBookmarkService = iBookmarkService;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                try
                {
                    var bookmark =  _IBookmarkService.GetBookmarkById(request.ID);

                    if (bookmark.ShortDescription != null)
                    {
                        _IBookmarkService.DeleteBookmark(bookmark);
                    }
                    else
                    {
                        return await Task.FromResult(new Response { deleted = "Bookmark not found" });
                    }

                 
                    

                    return await Task.FromResult(new Response {deleted = "Bookmark was deleted" });
                }
                catch (Exception e)
                {
                    _logger.Error($"Delete Bookmark failed : {e}");
                    return await Task.FromResult(new Response());
                }
            }
        }
    }
}
