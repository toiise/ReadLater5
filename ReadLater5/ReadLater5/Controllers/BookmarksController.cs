using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace ReadLater5.Controllers
{
    public class BookmarksController : Controller
    {

         IBookmarkService _iBookmarkService;

         private ICategoryService _iCategoryService;

         public BookmarksController(IBookmarkService bookmarkService, ICategoryService iCategoryService)
         {
             _iBookmarkService = bookmarkService;
             _iCategoryService = iCategoryService;
         }
        // GET: Bookmarks
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Bookmark> model = _iBookmarkService.GetBookmarksByUser(userId);

            var categ = _iCategoryService.GetCategories();

            foreach (var item in model)
            {
                item.Category = new Category
                {
                    Name = categ.FirstOrDefault(x => x.ID == item.CategoryId).Name
                };
               


            }

            return View(model);
        }

        // GET: Bookmarks/Details/5
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _iBookmarkService.GetBookmarkById((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);

        }

        // GET: Bookmarks/Create
        [Authorize]
        public IActionResult Create()
        {
            var categories = _iCategoryService.GetCategories().Select(x=>x.Name);

            var itemList = new List<SelectListItem>();

            foreach (var item in categories)
            {
                var selItem = new SelectListItem
                {
                    Value = item,
                    Text = item
                };

                itemList.Add(selItem);
            }

            ViewBag.select2 = new SelectList(itemList, "Value", "Text");

            return View();
        }

        // POST: Bookmarks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create( Bookmark bookmark)
        {
            
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var bookmarkId = _iCategoryService.GetCategory(bookmark.Category.Name);

                if (bookmarkId != null)
                {
                    bookmark.CategoryId = bookmarkId.ID;
                }

                if (userId != "")
                {
                    bookmark.UserID = userId;
                }
              

                _iBookmarkService.CreateBookmark(bookmark);
                return RedirectToAction("Index");
            }

            return View(bookmark);
        }

        // GET: Bookmarks/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _iBookmarkService.GetBookmarkById((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // POST: Bookmarks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != "")
                {
                    bookmark.UserID = userId;
                }
                _iBookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }
            return View(bookmark);
        }

        // GET: Bookmarks/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _iBookmarkService.GetBookmarkById((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = _iBookmarkService.GetBookmarkById(id);
            _iBookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
    }
}
