using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;

namespace TaskBoard.Models
{
    public class TaskFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequireError)]
        [StringLength(DataConstants.Task.TitleMaxLength,
            MinimumLength = DataConstants.Task.TitleMinLength,
            ErrorMessage = ErrorMessages.StringLengthError)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessages.RequireError)]
        [StringLength(DataConstants.Task.DescriptionMaxLength,
            MinimumLength = DataConstants.Task.DescriptionMinLength,
            ErrorMessage = ErrorMessages.StringLengthError)]
        public string Description { get; set; } = string.Empty;

        public int? BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards = new List<TaskBoardModel>();
    }
}
