using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

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
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            var bookmarkId = _iCategoryService.GetCategory(bookmark.Category.Name);

            if (bookmarkId != null)
            {
                bookmark.CategoryId = bookmarkId.ID;
            }
            bookmark.CreateDate = DateTime.UtcNow;
        
            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Bookmark> GetBookmarksByUser(string userId)
        {
            return _ReadLaterDataContext.Bookmark.Where(c => c.UserID == userId).ToList();
        }

        public Bookmark GetBookmarkById(int Id)
        {
            return _ReadLaterDataContext.Bookmark.Where(c => c.ID == Id).FirstOrDefault();
        }

        public List<Bookmark> GetBookmarks()
        {
            return _ReadLaterDataContext.Bookmark.ToList();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
