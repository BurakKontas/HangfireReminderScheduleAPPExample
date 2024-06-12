using Hangfire;

namespace HangfireReminderScheduleAPPExample;

public class ReminderService(IHttpClientFactory httpClientFactory)
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task SendWebhookRequest(string url)
    {
        var client = _httpClientFactory.CreateClient();
        Console.WriteLine($"Webhook sent successfully to {url}");
        
    }

    public bool ScheduleReminder(DateTime eventTime, string webhookUrl)
    {
        var timeUntilReminder = eventTime - DateTime.Now;
        if (timeUntilReminder > TimeSpan.Zero)
        {
            BackgroundJob.Schedule(() => SendWebhookRequest(webhookUrl), timeUntilReminder);
            return true;
        }

        return false;
    }
}