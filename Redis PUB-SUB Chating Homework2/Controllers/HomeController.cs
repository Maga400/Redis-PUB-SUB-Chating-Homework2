using Microsoft.AspNetCore.Mvc;
using Redis_PUB_SUB_Chating_Homework2.Models;
using Redis_PUB_SUB_Chating_Homework2.Services.Abstracts;
using Redis_PUB_SUB_Chating_Homework2.Services.Concretes;
using System.Diagnostics;

namespace Redis_PUB_SUB_Chating_Homework2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRedisChannelService _redisChannelService;
        public HomeController(IRedisChannelService redisChannelService)
        {
            _redisChannelService = redisChannelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string selectedChannel = null)
        {
            var channels = await _redisChannelService.GetAllChannelsAsync();
            ViewBag.Channels = channels;

            if (!string.IsNullOrEmpty(selectedChannel))
            {
                var messages = await _redisChannelService.GetChannelMessagesAsync(selectedChannel);
                ViewBag.SelectedChannelMessages = messages;
                ViewBag.SelectedChannelName = selectedChannel;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannel(string channelName)
        {
            await _redisChannelService.CreateChannelAsync(channelName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SelectChannel(string channelName)
        {
            var messages = await _redisChannelService.GetChannelMessagesAsync(channelName);
            ViewBag.SelectedChannelMessages = messages;
            ViewBag.SelectedChannelName = channelName;

            var channels = await _redisChannelService.GetAllChannelsAsync();
            ViewBag.Channels = channels;

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string channelName, string message)
        {
            if (!string.IsNullOrWhiteSpace(channelName) && !string.IsNullOrWhiteSpace(message))
            {
                await _redisChannelService.AddMessageToChannelAsync(channelName, message);
            }

            return RedirectToAction("Index", new { selectedChannel = channelName });
        }

    }
}
