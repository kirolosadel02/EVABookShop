using Models;
using EVABookShop.UnitOfWork;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using EVABookShop.Models;

namespace EVABookShop.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BookViewModel>> GetAllBooks()
        {
            var books = await _unitOfWork.Repository<Book>().GetAll(new List<string> { "Category" });
            return books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Author = b.Author,
                Price = b.Price,
                CategoryId = b.CategoryId,
                CategoryName = b.Category?.CatName ?? ""
            }).ToList();
        }

        public async Task<BookViewModel> GetBookById(int id)
        {
            var book = _unitOfWork.Repository<Book>().GetById(id, new List<string> { "Category" });
            if (book == null) return null;

            return await Task.FromResult(new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Price = book.Price,
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.CatName ?? ""
            });
        }

        public async Task<bool> CreateBook(BookViewModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            var repo = _unitOfWork.Repository<Book>();
            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                Author = model.Author,
                Price = model.Price,
                CategoryId = model.CategoryId
            };
            repo.Add(book);
            await _unitOfWork.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateBook(int id, BookViewModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            var repo = _unitOfWork.Repository<Book>();
            var book = repo.GetById(id);
            if (book == null)
                return false;

            book.Title = model.Title;
            book.Description = model.Description;
            book.Author = model.Author;
            book.Price = model.Price;
            book.CategoryId = model.CategoryId;

            await repo.Update(book);
            await _unitOfWork.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteBook(int id)
        {
            var repo = _unitOfWork.Repository<Book>();
            var book = repo.GetById(id);
            if (book == null) return false;

            repo.Delete(book.Id);
            await _unitOfWork.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}
