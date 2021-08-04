using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Services.ServiceModels;

namespace Services.Interfaces
{
   public interface IBookmarkService
    {
        Bookmark CreateBookmark(BookmarkVM bookmark);
        List<BookmarkVM> GetBookmarks();
        BookmarkVM GetBookmarkById(int Id);
        void UpdateBookmark(BookmarkVM bookmark);
        void DeleteBookmark(BookmarkVM bookmark);
        Task<List<BookmarkVM>>  GetBookmarksByUser(string userId);
    }
}
