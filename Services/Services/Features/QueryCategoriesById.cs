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
   public class QueryCategoriesById
    {
        private static ILogger _logger = Log.ForContext<QueryCategoriesById>();
        public class Request : IRequest<Response>
        {
            public string Id { get; set; }
        }

        public class Response
        {
            public List<CategoryVM> listCategoriesByUser { get; set; }
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
                var listOfCategories = _iCategoryService.GetCategoriesByUser(request.Id);

                if (listOfCategories.Any())
                {
                    try
                    {
                        var categoriesVmList = new List<CategoryVM>();

                        

                        foreach (var item in listOfCategories)
                        {
                            var categoriesVM = new CategoryVM();
                            categoriesVM.Name = item.Name;
                            categoriesVM.ID = item.ID;
                            categoriesVM.UserID = item.UserID;

                            categoriesVmList.Add(categoriesVM);
                        }
                        return await Task.FromResult(new Response { listCategoriesByUser = categoriesVmList });
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e, "Query Categories By Id failed");
                    }

                }
                else
                {
                    _logger.Error("No Categories for the user were found");
                }
                return await Task.FromResult(new Response());
            }
        }

    }
}
