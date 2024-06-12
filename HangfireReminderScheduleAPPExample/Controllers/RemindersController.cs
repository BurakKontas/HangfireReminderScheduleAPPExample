using Microsoft.AspNetCore.Mvc;

namespace HangfireReminderScheduleAPPExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RemindersController(ReminderService reminderService) : ControllerBase
{
    private readonly ReminderService _reminderService = reminderService;

    [HttpPost("schedule")]
    public IActionResult ScheduleReminder([FromBody] ReminderDto reminder)
    {
        var isSuccess = _reminderService.ScheduleReminder(reminder.EventTime, reminder.WebhookUrl);
        if (isSuccess)
        {
            return Ok("Reminder scheduled successfully");
        }

        return BadRequest("Reminder not scheduled.");
    }
}

public class ReminderDto
{
    public DateTime EventTime { get; set; }
    public required string WebhookUrl { get; set; }
}