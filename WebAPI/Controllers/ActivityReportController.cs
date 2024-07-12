using Application.Features.ActivityReport.Commands.Add;
using Application.Features.ActivityReport.Commands.Delete;
using Application.Features.ActivityReport.Commands.Update;
using Application.Features.ActivityReport.Queries.GetAllByUserId;
using Application.Features.ActivityReport.Queries.GetAllPaginatedByUserId;
using Application.Features.ActivityReport.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ActivityReportController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddActivityReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteActivityReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateActivityReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdActivityReportQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginatedAndFilteredByUserId([FromQuery] GetPaginatedActivityReportsByUserIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetFilteredByUserId([FromQuery] GetAllActivityReportsByUserIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
