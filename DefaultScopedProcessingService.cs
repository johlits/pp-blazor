using System.Text;
using System;
using System.Net.NetworkInformation;

namespace pp_blazor.ScopedService;

public sealed class DefaultScopedProcessingService : IScopedProcessingService
{
    private int _executionCount;
    private readonly ChatService _chatService;
    private readonly ILogger<DefaultScopedProcessingService> _logger;

    public DefaultScopedProcessingService(
        ChatService chatService,
        ILogger<DefaultScopedProcessingService> logger) =>
        (_chatService, _logger) = (chatService, logger);

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        var pp = new pp.Program();
        var chats = new List<Tuple<string, string, int>>();
        var chatCount = 0;
        
        pp.Init(chats);
        _chatService.Chat = chats;

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_chatService.Init)
            {
                pp.SetUserAlias(_chatService.UserName);
                pp.SetEventTitle(_chatService.EventTitle);
                Task t = pp.Start();
                var firstFetch = false;
                while (!t.IsCompleted)
                {
                    if (!_chatService.Init)
                    {
                        chats.Clear();
                        break;
                    }
                    while (_chatService.messages.Count > 0)
                    {
                        pp.GetUI().SendMessage(_chatService.messages.Dequeue());
                    }
                    if (!firstFetch && pp.FirstFetch())
                    {
                        firstFetch = true;
                        _chatService.FirstFetch = true;
                    }
                    if (chatCount != pp.ChatCount())
                    {
                        chatCount = pp.ChatCount();
                        _chatService.Chat = chats;
                    }
                    
                    await Task.Delay(100, stoppingToken);
                }
            }
            
            await Task.Delay(100);
        }
    }
}