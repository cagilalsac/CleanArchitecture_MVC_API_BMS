#nullable disable
using Application.Features.Authors;
using Application.Features.Books;
using Application.Features.Genres;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Controllers.Bases;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class BooksController : MvcControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new ReadBookMvcRequest());
            var list = await response.ToListAsync();
            return View(list);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _mediator.Send(new ReadBookMvcRequest());
            var model = await response.SingleOrDefaultAsync(r => r.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            var authorResponse = await _mediator.Send(new ReadAuthorMvcRequest());
            ViewData["AuthorId"] = new SelectList(authorResponse.ToList(), "Id", "FullName");
            var genreResponse = await _mediator.Send(new ReadGenreRequest());
            ViewBag.Genres = new MultiSelectList(genreResponse.ToList(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = response.Id });
                ModelState.AddModelError("", response.Message);
            }
			// TODO: Add get related items service logic here to set ViewData if necessary
			var authorResponse = await _mediator.Send(new ReadAuthorMvcRequest());
			ViewData["AuthorId"] = new SelectList(authorResponse.ToList(), "Id", "FullName");
			var genreResponse = await _mediator.Send(new ReadGenreRequest());
			ViewBag.Genres = new MultiSelectList(genreResponse.ToList(), "Id", "Name");
			return View(request);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _mediator.Send(new EditBookRequest(id));
            if (model == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            var authorResponse = await _mediator.Send(new ReadAuthorMvcRequest());
            ViewData["AuthorId"] = new SelectList(authorResponse.ToList(), "Id", "FullName");
            var genreResponse = await _mediator.Send(new ReadGenreRequest());
            ViewBag.Genres = new MultiSelectList(genreResponse.ToList(), "Id", "Name");
            return View(model);
        }

        // POST: Books/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateBookRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return RedirectToAction(nameof(Index));
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["AuthorId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(request);
        }

        // GET: Books/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = await _mediator.Send(new ReadBookRequest());
        //    var model = await response.SingleOrDefaultAsync(r => r.Id == id);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(model);
        //}

        // POST: Books/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _mediator.Send(new DeleteBookRequest(id));
            return RedirectToAction(nameof(Index));
        }
	}
}
