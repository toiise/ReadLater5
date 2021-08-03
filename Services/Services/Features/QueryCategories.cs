using System;
using System.Collections.Generic;
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
   public class QueryCategories
    {
        private static ILogger _logger = Log.ForContext<QueryCategories>();
        public class Request : IRequest<Response>
        {

        }

        public class Response
        {
            public List<CategoryVM> listCategories{ get; set; }
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
                var listOfCategories = _iCategoryService.GetCategories();

                if (listOfCategories.Any())
                {
                    try
                    {
                        var categoriesVmList = new List<CategoryVM>();

                       

                        foreach (var item in listOfCategories)
                        {
                            var categorykVM = new CategoryVM();

                            categorykVM.Name = item.Name;
                            categorykVM.ID = item.ID;
                            categorykVM.UserID = item.UserID;
                            categoriesVmList.Add(categorykVM);

                        }
                        return await Task.FromResult(new Response { listCategories = categoriesVmList });
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e, "Query Categories failed");
                    }

                }
                return await Task.FromResult(new Response());
            }
        }

    }
}
