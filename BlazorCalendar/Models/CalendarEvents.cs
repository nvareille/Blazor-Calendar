namespace TheLair.BlazorCalendar.Models;

public class ClickTaskParameter
{
    public required List<int> IDList { get; set; }
    public required Tasks[] Tasks { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
	public DateTime Day { get; set; }
}

public class ClickEmptyDayParameter
{
    public DateTime Day { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}

public class DragDropParameter
{
    public DateTime Day { get; set; }
    public int taskID { get; set; }
}