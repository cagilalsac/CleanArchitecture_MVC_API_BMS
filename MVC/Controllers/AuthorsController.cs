#nullable disable
using Application.Features.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Controllers.Bases;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class AuthorsController : MvcControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new ReadAuthorMvcRequest());
            var list = await response.ToListAsync();
            return View(list);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _mediator.Send(new ReadAuthorMvcRequest());
            var model = await response.SingleOrDefaultAsync(r => r.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Authors/Create
        public async Task<IActionResult> Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = response.Id });
                ModelState.AddModelError("", response.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(request);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _mediator.Send(new EditAuthorRequest(id));
            if (model == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(model);
        }

        // POST: Authors/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateAuthorRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = response.Id });
                ModelState.AddModelError("", response.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(request);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new ReadAuthorMvcRequest());
            var model = await response.SingleOrDefaultAsync(r => r.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Authors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _mediator.Send(new DeleteAuthorRequest(id));
            TempData["Message"] = response.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
