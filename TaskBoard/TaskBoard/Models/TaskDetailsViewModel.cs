namespace TaskBoard.Models
{
    public class TaskDetailsViewModel : TaskViewModel
    {
        public string CreatedOn { get; set; } = string.Empty;

        public string Board { get; set; } = string.Empty;
    }
}
