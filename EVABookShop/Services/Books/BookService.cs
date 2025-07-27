using EVABookShop.Models;
using EVABookShop.UnitOfWork;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVABookShop.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<BookViewModel> GetAllBooks()
        {
            var books = _unitOfWork.Repository<Book>().GetAll().Result;
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
            var book = await Task.Run(() => _unitOfWork.Repository<Book>().GetById(id));
            if (book == null) return null;

            return new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Price = book.Price,
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.CatName ?? ""
            };
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
            return true;
        }

        public async Task<bool> UpdateBook(int id, BookViewModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            var repo = _unitOfWork.Repository<Book>();
            var book = await Task.Run(() => repo.GetById(id));
            if (book == null)
                return false;

            book.Title = model.Title;
            book.Description = model.Description;
            book.Author = model.Author;
            book.Price = model.Price;
            book.CategoryId = model.CategoryId;

            await repo.Update(book);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var repo = _unitOfWork.Repository<Book>();
            var book = repo.GetById(id);
            if (book == null) return false;

            repo.Delete(book.Id);
            await _unitOfWork.SaveChanges();
            return true;
        }
    }
}
