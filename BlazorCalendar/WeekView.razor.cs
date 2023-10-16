using Microsoft.AspNetCore.Components;
using TheLair.BlazorCalendar.Base;
using TheLair.BlazorCalendar.Models;

namespace TheLair.BlazorCalendar;

partial class WeekView : CalendarBase
{
    [CascadingParameter(Name = "SelectedView")]
    public DisplayedView DisplayedView { get; set; } = DisplayedView.Weekly;

    private DateTime _firstdate;
    [CascadingParameter(Name = "FirstDate")]
    public DateTime FirstDate
    {
        get
        {
            if (_firstdate == DateTime.MinValue) _firstdate = DateTime.Today;
            return _firstdate.Date;
        }
        set
        {
            _firstdate = value;
        }
    }

    [CascadingParameter(Name = "TasksList")]
    public Tasks[]? TasksList { get; set; }

    [Parameter]
    public PriorityLabel PriorityDisplay { get; set; } = PriorityLabel.Code;

    [Parameter]
    public bool HighlightToday { get; set; } = false;

    [Parameter]
    public EventCallback<int> OutsideCurrentMonthClick { get; set; }

    [Parameter]
    public EventCallback<ClickEmptyDayParameter> DayClick { get; set; }

    [Parameter]
    public EventCallback<ClickTaskParameter> TaskClick { get; set; }

    [Parameter]
    public EventCallback<DragDropParameter> DragStart { get; set; }

    [Parameter]
    public EventCallback<DragDropParameter> DropTask { get; set; }

    [Parameter]
    public TimeOnly ScheduleStart { get; set; } = TimeOnly.MinValue;

    [Parameter]
    public TimeOnly ScheduleEnd { get; set; } = TimeOnly.MaxValue;

    [Parameter]
    public TimeSpan Units { get; set; } = TimeSpan.FromHours(1);

    private Tasks? TaskDragged;
    private async Task HandleDragStart(int taskID)
    {
        TaskDragged = new Tasks()
        {
            ID = taskID
        };

        DragDropParameter dragDropParameter = new()
        {
            taskID = TaskDragged.ID
        };

        await DragStart.InvokeAsync(dragDropParameter);
    }

    private async Task HandleDayOnDrop(DateTime day)
    {
        if (!Draggable) return;
        if (TaskDragged == null) return;

        DragDropParameter dragDropParameter = new()
        {
            Day = day,
            taskID = TaskDragged.ID
        };

        await DropTask.InvokeAsync(dragDropParameter);

        TaskDragged = null;
    }

    private async Task HandleClickEvent(Tasks ev)
    {
        await TaskClick.InvokeAsync(new ClickTaskParameter
        {
            IDList = new List<int>
            {
                ev.ID
            },
            Tasks = new [] { ev },
            Day = ev.DateStart
        });
    }

    private string GetBackground(DateTime day)
    {
        int d = (int)day.DayOfWeek;

        if (d == 6)
        {
            return $"background:{SaturdayColor}";
        }
        else if (d == 0)
        {
            return $"background:{SundayColor}";
        }

        return $"background:{WeekDaysColor}";
    }
}
