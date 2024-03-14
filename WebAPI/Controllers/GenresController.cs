#nullable disable
using Application.Common.Responses.Bases;
using Application.Features.Genres;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Generated from Custom Template.
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IQueryable<ReadGenreResponse> response = await _mediator.Send(new ReadGenreRequest());
            List<ReadGenreResponse> list = await response.ToListAsync();
            return Ok(list);
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            IQueryable<ReadGenreResponse> response = await _mediator.Send(new ReadGenreRequest());
            ReadGenreResponse item = await response.SingleOrDefaultAsync(r => r.Id == id);
            return Ok(item);
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(CreateGenreRequest request)
        {
            if (ModelState.IsValid)
            {
                Response response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Put(UpdateGenreRequest request)
        {
            if (ModelState.IsValid)
            {
                Response response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Response response = await _mediator.Send(new DeleteGenreRequest(id));
            return Ok(response);
        }
    }
}
