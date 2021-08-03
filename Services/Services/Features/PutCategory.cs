using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Services.Interfaces;
using Services.ServiceModels;

namespace Services.Features
{
   public class PutCategory
    {
        private static ILogger _logger = Log.ForContext<PutBookmark>();
        public class Request : IRequest<PutCategory.Response>
        {
           public string CategoryName { get; set; }

           public int CategoryID { get; set; }
        }

        public class Response
        {
            public CategoryVM Category { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private ICategoryService _iCategoryService;

            public Handler(ICategoryService iCategoryService)
            {
                _iCategoryService = iCategoryService;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = _iCategoryService.GetCategory(request.CategoryID);

                    if (category.ID != null)
                    {
                        category.Name = request.CategoryName;
                        _iCategoryService.UpdateCategory(category);
                    }
                    else
                    {
                        _logger.Error($"The category doesn't exist");
                        return Task.FromResult(new Response());
                    }

                  

                    return Task.FromResult(new Response { Category = category });
                }
                catch (Exception e)
                {
                    _logger.Error($"Put Category failed : {e}");
                    return Task.FromResult(new Response());
                }
               
            }
        }

    }
}
