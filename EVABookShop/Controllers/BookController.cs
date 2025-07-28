using Microsoft.AspNetCore.Mvc;
using EVABookShop.Models;
using EVABookShop.Services.Books;
using EVABookShop.Services.Categories;

namespace EVABookShop.Controllers
{
    [Route("books")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BookController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return View(books);
        }

        [HttpGet("create")]
        public IActionResult CreateBook()
        {
            var categories = _categoryService.GetAllCategoryViewModels();
            ViewBag.Categories = categories.Where(c => c.IsActive).OrderBy(c => c.CatOrder).ThenBy(c => c.CatName).ToList();
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(BookViewModel model)
        {
            var categories = _categoryService.GetAllCategoryViewModels();
            ViewBag.Categories = categories.Where(c => c.IsActive).OrderBy(c => c.CatOrder).ThenBy(c => c.CatName).ToList();
            
            var result = await _bookService.CreateBook(model, ModelState);
            if (result)
            {
                TempData["SuccessMessage"] = "Book created successfully.";
                return RedirectToAction("GetAllBooks");
            }
            
            TempData["ErrorMessage"] = "Failed to create book. Please check the errors and try again.";
            return View(model);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditBook(int id)
        {
            var model = await _bookService.GetBookById(id);
            if (model == null) return NotFound();
            
            var categories = _categoryService.GetAllCategoryViewModels();
            ViewBag.Categories = categories.Where(c => c.IsActive).OrderBy(c => c.CatOrder).ThenBy(c => c.CatName).ToList();
            
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

            var categories = _categoryService.GetAllCategoryViewModels();
            ViewBag.Categories = categories.Where(c => c.IsActive).OrderBy(c => c.CatOrder).ThenBy(c => c.CatName).ToList();

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
            if (result)
            {
                TempData["SuccessMessage"] = "Book deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete book.";
            }
            return RedirectToAction("GetAllBooks");
        }
    }
}
