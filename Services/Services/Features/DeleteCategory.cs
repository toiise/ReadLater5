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
    public class DeleteCategory
    {
        private static ILogger _logger = Log.ForContext<DeleteCategory>();
        public class Request : IRequest<DeleteCategory.Response>
        {
            public int ID { get; set; }

        }

        public class Response
        {
            public string deleted { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private ICategoryService _iCategoryService;

            public Handler(ICategoryService iCategoryService)
            {
                _iCategoryService = iCategoryService;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = _iCategoryService.GetCategory(request.ID);

                    if (category.Name != null)
                    {
                        _iCategoryService.DeleteCategory(category);
                    }
                    else
                    {
                        return await Task.FromResult(new Response { deleted = "Category not found" });
                    }

                    return await Task.FromResult(new Response { deleted = "Category was deleted" });
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
