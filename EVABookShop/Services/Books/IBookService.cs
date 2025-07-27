using EVABookShop.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVABookShop.Services.Books
{
    public interface IBookService
    {
        List<BookViewModel> GetAllBooks();
        Task<BookViewModel> GetBookById(int id);
        Task<bool> CreateBook(BookViewModel model, ModelStateDictionary modelState);
        Task<bool> UpdateBook(int id, BookViewModel model, ModelStateDictionary modelState);
        Task<bool> DeleteBook(int id);
    }
}
