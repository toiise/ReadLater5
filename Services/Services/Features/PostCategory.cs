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
   public class PostCategory
    {
        private static ILogger _logger = Log.ForContext<PostCategory>();
        public class Request : IRequest<PostCategory.Response>
        {
            public string UserID { get; set; }
            public string CategoryName { get; set; }
        }

        public class Response
        {
            public Category Category { get; set; }
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

                  

                    return Task.FromResult(new Response { Category = addCategory });
                }
                catch (Exception e)
                {
                    _logger.Error($"Post Category failed : {e}");
                    return Task.FromResult(new Response());
                }

                
            }
        }
    }
}
