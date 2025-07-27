using Microsoft.AspNetCore.Mvc;
using EVABookShop.Models;
using EVABookShop.Services.Books;

namespace EVABookShop.Controllers
{
    [Route("books")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("")]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        [HttpGet("create")]
        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(BookViewModel model)
        {
            var result = await _bookService.CreateBook(model, ModelState);
            if (result)
                return RedirectToAction("GetAllBooks");
            return View(model);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditBook(int id)
        {
            var model = await _bookService.GetBookById(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(int id, BookViewModel model)
        {
            if (id != model.Id)
            {
                TempData["ErrorMessage"] = "Invalid book ID.";
                return RedirectToAction("GetAllBooks");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors and try again.";
                return View(model);
            }

            var result = await _bookService.UpdateBook(id, model, ModelState);

            if (result)
            {
                TempData["SuccessMessage"] = "Book updated successfully.";
                return RedirectToAction("GetAllBooks");
            }

            TempData["ErrorMessage"] = "Failed to update book. Please check the errors and try again.";
            return View(model);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> DetailsBook(int id)
        {
            var model = await _bookService.GetBookById(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            return RedirectToAction("GetAllBooks");
        }
    }
}
