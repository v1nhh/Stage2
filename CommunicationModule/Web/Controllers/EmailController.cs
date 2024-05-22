using CommunicationModule.ApplicationCore.Commands;
using CTAM.Core;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationModule.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IMediator _mediator;
        // only for test purposes, TODO: remove
        private MainDbContext _context;


        public EmailController(ILogger<EmailController> logger, IMediator mediator, MainDbContext context)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
        }

        [HttpPost("send/template")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<IActionResult> SendFromTemplate(SendEmailFromTemplateCommand request)
        {
            try
            {
                await _mediator.Send(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("send/template/markup/test")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<IActionResult> SendFromMarkupTemplate()
        {
            try
            {
                var mailFromQueue = await _context.MailQueue().AsNoTracking().OrderByDescending(mq => mq.ID).FirstOrDefaultAsync();
                var result = await _mediator.Send(new CombineEmailTemplatesCommand(mailFromQueue.Body, mailFromQueue.MailMarkupTemplateID));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
