using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.ServiceModels;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Category CreateCategory(CategoryVM category);
        List<CategoryVM> GetCategories();
        CategoryVM GetCategory(int Id);
        CategoryVM GetCategory(string Name);
        List<CategoryVM>  GetCategoriesByUser(string userId);
        void UpdateCategory(CategoryVM category);
        void DeleteCategory(CategoryVM category);
    }
}
