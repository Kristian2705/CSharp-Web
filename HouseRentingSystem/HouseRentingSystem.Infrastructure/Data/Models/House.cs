using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Comment("House entity")]
    public class House
    {
        [Key]
        [Comment("House identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(HouseTitleMaxLength)]
        [Comment("House title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(HouseAddressMaxLength)]
        [Comment("House address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(HouseDescriptionMaxLength)]
        [Comment("House description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("House image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("House monthly price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerMonth { get; set; }

        [Required]
        [Comment("Category idenitfier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [Comment("Category navigational property")]
        public Category Category { get; set; } = null!;

        [Required]
        [Comment("Agent identifier")]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        [Comment("Agent navigational property")]
        public Agent Agent { get; set; } = null!;

        [Comment("Renter identifier")]
        public string? RenterId { get; set; }
    }
}
//· Id – a unique integer, Primary Key

//· Title – a string with min length 10 and max length 50 (required)

//· Address – a string with min length 30 and max length 150 (required)

//· Description – a string with min length 50 and max length 500 (required)

//· ImageUrl – a string (required)

//· PricePerMonth – a decimal with min value 0 and max value 2000 (required)

//· CategoryId – an integer (required)

//· Category – a Category object

//· AgentId – an integer (required)

//· Agent – an Agent object

//· RenterId – a string
