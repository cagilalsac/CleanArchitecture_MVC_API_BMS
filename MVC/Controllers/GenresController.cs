#nullable disable
using Application.Features.Genres;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Controllers.Bases;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class GenresController : MvcControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new ReadGenreRequest());
            var list = await response.ToListAsync();
            return View(list);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _mediator.Send(new ReadGenreRequest());
            var model = await response.SingleOrDefaultAsync(r => r.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Genres/Create
        public async Task<IActionResult> Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGenreRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.IsSuccessful)
                {
                    TempData["Message"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(request);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _mediator.Send(new EditGenreRequest() { Id = id });
            if (model == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(model);
        }

        // POST: Genres/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateGenreRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.IsSuccessful)
                {
                    TempData["Message"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(request);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new ReadGenreRequest());
            var model = await response.SingleOrDefaultAsync(r => r.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Genres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _mediator.Send(new DeleteGenreRequest() { Id = id });
            TempData["Message"] = response.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
