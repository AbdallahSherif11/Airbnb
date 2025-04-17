using Airbnb.Core.DTOs.MessageDtos;
using Airbnb.Core.Services.Contract.MessageService.Contract;
using Airbnb.Service.Services.SignalRServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

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
    [Authorize]
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

        [HttpGet("conversations")]
        public async Task<IActionResult> GetUserConversations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var conversations = await _messageService.GetUserConversationsAsync(userId);
            return Ok(conversations);
        }

        [HttpGet("with/{otherUserId}")]
        [Authorize]
        public async Task<IActionResult> GetConversation(string otherUserId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = await _messageService.GetConversationAsync(userId, otherUserId);

            var response = messages.Select(m => new
            {
                m.MessageId,
                m.SenderId,
                m.ReceiverId,
                m.MessageContent,
                m.IsDeleted,
                m.TimeStamp,
                Sender = new
                {
                    m.Sender.Id,
                    m.Sender.FirstName,
                    m.Sender.LastName,
                    m.Sender.ProfilePictureUrl
                },
                Receiver = new
                {
                    m.Receiver.Id,
                    m.Receiver.FirstName,
                    m.Receiver.LastName,
                    m.Receiver.ProfilePictureUrl
                }
            });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _messageService.SendMessageAsync(userId, dto.ReceiverId, dto.MessageContent);

            await _hubContext.Clients.User(dto.ReceiverId)
                .SendAsync("ReceiveMessage", userId, dto.MessageContent, DateTime.UtcNow);

            return Ok();
        }

        [HttpPost("start/{houseId}")]
        public async Task<IActionResult> StartConversation(int houseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _messageService.StartConversationWithHostAsync(userId, houseId);

            if (!result.Success)
                return BadRequest(result.Message);

            await _hubContext.Clients.User(result.HostId)
                .SendAsync("ReceiveMessage", userId, "Hello! I'm interested in your property", DateTime.UtcNow);

            return Ok(result);
        }
    }


}
