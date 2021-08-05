using Data;
using Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.ServiceModels;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;


        public CategoryService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }
      

        public Category CreateCategory(CategoryVM category)
        {

            var entitycategory = new Category();

            if (category != null)
            {
                entitycategory.UserID = category.UserID;
                entitycategory.Name = category.Name;

                _ReadLaterDataContext.Add(entitycategory);
                _ReadLaterDataContext.SaveChanges();
            }
            return entitycategory;
        }

        public void UpdateCategory(CategoryVM categoryVM)
        {
            var ent = _ReadLaterDataContext.Categories.Where(c => c.ID == categoryVM.ID).FirstOrDefault();

            if (ent != null)
            {
                ent.Name = categoryVM.Name;
                ent.ID = categoryVM.ID ?? ent.ID;
                ent.UserID = categoryVM.UserID;

                _ReadLaterDataContext.Update(ent);
                _ReadLaterDataContext.SaveChanges();
            }

          
        }

        public List<CategoryVM> GetCategories()
        {
            var categories = _ReadLaterDataContext.Categories.ToList();

            var listOfCategoriesVM = new List<CategoryVM>();

            if (categories.Any())
            {
                foreach (var item in categories)
                {
                    var categorySM = new CategoryVM();
                    categorySM.ID = item.ID;
                    categorySM.Name = item.Name;
                    categorySM.UserID = item.UserID;

                    listOfCategoriesVM.Add(categorySM);
                }
            }

            return listOfCategoriesVM;
        }

        public CategoryVM GetCategory(int Id)
        {
            var category = _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefault();

            if (category != null)
            {
                var categorySM = new CategoryVM
                {
                    UserID = category.UserID,
                    Name = category.Name,
                    ID = category.ID
                };

                return categorySM;
            }
            else
            {
                return new CategoryVM();
            }
            
        }

        public CategoryVM GetCategory(string Name)
        {
            var category = _ReadLaterDataContext.Categories.Where(c => c.Name == Name).FirstOrDefault();

            var categorySM = new CategoryVM();

            if (category != null)
            {
                categorySM.UserID = category?.UserID;
                categorySM.Name = category?.Name;
                categorySM.ID = category?.ID;

            }

            return categorySM;
        }

        public void DeleteCategory(CategoryVM category)
        {
            var categoryCore = _ReadLaterDataContext.Categories.Where(c => c.ID == category.ID).FirstOrDefault();

            if (categoryCore != null)
            {
                _ReadLaterDataContext.Categories.Remove(categoryCore);
                _ReadLaterDataContext.SaveChanges();
            }

            
        }

        public List<CategoryVM> GetCategoriesByUser(string userId)
        {
           var categories = _ReadLaterDataContext.Categories?.Where(c => c.UserID == userId)?.ToList();

           var listOfCategoriesVM = new List<CategoryVM>();

            if (categories.Any())
            {
                foreach (var item in categories)
                {
                    var categorykSM = new CategoryVM();
                    categorykSM.ID = item.ID;
                    categorykSM.Name = item.Name;
                    categorykSM.UserID = item.UserID;
                    listOfCategoriesVM.Add(categorykSM);
                }
            }

            return listOfCategoriesVM;
        }
    }
}
