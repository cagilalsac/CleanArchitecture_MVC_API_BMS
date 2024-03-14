#nullable disable
using Application.Common.Responses.Bases;
using Application.Features.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Generated from Custom Template.
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new ReadAuthorApiRequest());
            var list = await response.ToListAsync();
            return Ok(list);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new ReadAuthorApiRequest());
            var item = await response.SingleOrDefaultAsync(r => r.Id == id);
            return Ok(item);
        }

		// POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(CreateAuthorRequest request)
        {
            if (ModelState.IsValid)
            {
                Response response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Put(UpdateAuthorRequest request)
        {
            if (ModelState.IsValid)
            {
                Response response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Response response = await _mediator.Send(new DeleteAuthorRequest(id));
            return Ok(response);
        }
	}
}
