using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Comment("Category entity")]
    public class Category
    {
        [Key]
        [Comment("Category idenitfier")]
        public int Key { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Category name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Houses collection")]
        public IEnumerable<House> Houses { get; set; } = new List<House>();
    }
}
//· Id – a unique integer, Primary Key

//· Name – a string with max length 50 (required)

//· Houses – a collection of House