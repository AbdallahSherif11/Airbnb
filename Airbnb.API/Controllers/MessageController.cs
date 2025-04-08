using Airbnb.Core.DTOs.MessageDtos;
using Airbnb.Core.Services.Contract.MessageService.Contract;
using Airbnb.Service.Services.SignalRServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Airbnb.API.Controllers
{
    //[Authorize]
    //[Route("api/[controller]")]
    //[ApiController]
    //public class MessageController : ControllerBase
    //{
    //    private readonly IMessageService _messageService;

    //    public MessageController(IMessageService messageService)
    //    {
    //        _messageService = messageService;
    //    }

    //    [HttpGet("{user1}/{user2}")]
    //    public async Task<IActionResult> GetMessages(string user1, string user2)
    //    {
    //        var messages = await _messageService.GetConversationAsync(user1, user2);
    //        return Ok(messages.Select(m=>m.MessageContent));
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
    //    {
    //        await _messageService.SendMessageAsync(dto.SenderId, dto.ReceiverId, dto.MessageContent);
    //        return Ok();
    //    }
    //}
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            await _messageService.SendMessageAsync(dto.SenderId, dto.ReceiverId, dto.MessageContent);

            await _hubContext.Clients.User(dto.ReceiverId).SendAsync("ReceiveMessage", dto.SenderId, dto.MessageContent);

            return Ok();
        }
        [HttpGet("{user1}/{user2}")]
        public async Task<IActionResult> GetMessages(string user1, string user2)
        {
            var messages = await _messageService.GetConversationAsync(user1, user2);
            return Ok(messages.Select(m => m.MessageContent));
        }
    }


}
