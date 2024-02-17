using System.Runtime.CompilerServices;

namespace TaskBoard.Models
{
    public class HomeBoardModel
    {
        public string BoardName { get; set; } = string.Empty;

        public int TasksCount {  get; set; }
    }
}
