namespace pp_blazor.ScopedService;

public interface IScopedProcessingService
{
    Task DoWorkAsync(CancellationToken stoppingToken);
}