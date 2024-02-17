using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;
using TaskBoard.Data.Models;

namespace TaskBoard.Models
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.Board.NameMaxLength, 
            MinimumLength = DataConstants.Board.NameMinLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
