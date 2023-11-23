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
        var chats = new List<string>();
        pp.Init(chats);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_chatService.UserName != null && _chatService.EventTitle != null)
            {
                pp.SetUserAlias(_chatService.UserName);
                pp.SetEventTitle(_chatService.EventTitle);
                Task t = pp.Start();
                while (!t.IsCompleted)
                {
                    while (_chatService.messages.Count > 0)
                    {
                        pp.GetUI().SendMessage(_chatService.messages.Dequeue());
                    }
                    _chatService.Chat = chats;
                    _chatService.PingId++;
                    await Task.Delay(1000, stoppingToken);
                }
            }
            
            await Task.Delay(1000);
        }
    }
}