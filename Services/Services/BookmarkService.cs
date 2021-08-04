using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Services.Interfaces;
using Services.ServiceModels;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        private ICategoryService _iCategoryService;

        public BookmarkService(ReadLaterDataContext readLaterDataContext, ICategoryService iCategoryService)
        {
            _ReadLaterDataContext = readLaterDataContext;
            _iCategoryService = iCategoryService;
        }
        public Bookmark CreateBookmark(BookmarkVM bookmark)
        {
            var entityBookmark = new Bookmark();
            var bookmarkId = _iCategoryService.GetCategory(bookmark.Category.Name);

            if (bookmarkId != null)
            {
                entityBookmark.CategoryId = bookmarkId.ID;
            }
            entityBookmark.ShortDescription = bookmark.ShortDescription;
            entityBookmark.URL = bookmark.URL;
            entityBookmark.UserID = bookmark.UserID;
            entityBookmark.CreateDate = DateTime.UtcNow;
        
            _ReadLaterDataContext.Add(entityBookmark);
            _ReadLaterDataContext.SaveChanges();

            return entityBookmark;
        }

        public void DeleteBookmark(BookmarkVM bookmark)
        {
            var removedBookmark = _ReadLaterDataContext.Bookmark.Where(c => c.ID == bookmark.ID).FirstOrDefault();

            if (removedBookmark != null)
            {
                _ReadLaterDataContext.Bookmark.Remove(removedBookmark);
                _ReadLaterDataContext.SaveChanges();
            }

            
        }

        public  Task<List<BookmarkVM>>  GetBookmarksByUser(string userId)
        {
            var bookmarks =  _ReadLaterDataContext.Bookmark.Where(c => c.UserID == userId).ToList();

            var listOfBookmarksVM = new List<BookmarkVM>();
            

            foreach (var item in bookmarks)
            {
                var bookmarkSM = new BookmarkVM();
                bookmarkSM.ID = item.ID;
                bookmarkSM.ShortDescription = item.ShortDescription;
                bookmarkSM.URL = item.URL;
                bookmarkSM.CreateDate = item.CreateDate;
                bookmarkSM.Category = _ReadLaterDataContext.Categories.Where(c => c.ID == item.CategoryId)
                    .FirstOrDefault();
                bookmarkSM.UserID = item.UserID;
                listOfBookmarksVM.Add(bookmarkSM);
            }

            return Task.FromResult(listOfBookmarksVM) ;
        }

        public BookmarkVM GetBookmarkById(int Id)
        {
            var bookmark = _ReadLaterDataContext.Bookmark.Where(c => c.ID == Id).FirstOrDefault();
            var bookmarkSM = new BookmarkVM
            {
                ID = bookmark.ID,
                ShortDescription = bookmark.ShortDescription,
                URL = bookmark.URL,
                CreateDate = bookmark.CreateDate,
                UserID = bookmark.UserID,
                Category = _ReadLaterDataContext.Categories.Where(c => c.ID == bookmark.CategoryId)
                .FirstOrDefault()
        };
            return bookmarkSM;
        }

        public List<BookmarkVM> GetBookmarks()
        {
            var bookmarks = _ReadLaterDataContext.Bookmark.ToList();

            var listOfBookmarksVM = new List<BookmarkVM>();
            

            foreach (var item in bookmarks)
            {
                var bookmarkSM = new BookmarkVM();
                bookmarkSM.ID = item.ID;
                bookmarkSM.ShortDescription = item.ShortDescription;
                bookmarkSM.URL = item.URL;
                bookmarkSM.CreateDate = item.CreateDate;
                bookmarkSM.UserID = item.UserID;
                bookmarkSM.Category = _ReadLaterDataContext.Categories.Where(c => c.ID == item.CategoryId)
                    .FirstOrDefault();
                listOfBookmarksVM.Add(bookmarkSM);
            }

            return listOfBookmarksVM;
        }

        public void UpdateBookmark(BookmarkVM bookmark)
        {
            
            var ent = _ReadLaterDataContext.Bookmark.Where(c => c.ID == bookmark.ID).FirstOrDefault();

            ent.UserID = bookmark.UserID;
            ent.CategoryId = bookmark.Category.ID;
            ent.ID = bookmark.ID;
            ent.CreateDate = bookmark.CreateDate;
            ent.ShortDescription = bookmark.ShortDescription;
            ent.URL = bookmark.URL;
            
             
                _ReadLaterDataContext.Update(ent);
                _ReadLaterDataContext.SaveChanges();

        }

        
    }
}
