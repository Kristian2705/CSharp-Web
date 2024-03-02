using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Comment("House Agent entity")]
    public class Agent
    {
        [Key]
        [Comment("Agent identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(AgentPhoneNumberMaxLength)]
        [Comment("Agent phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Comment("User identifier")]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [Comment("User navigational property")]
        public IdentityUser User { get; set; } = null!;

        [Comment("Agent's houses")]
        public IEnumerable<House> Houses { get; set; } = new List<House>();
    }
}
//· Id – a unique integer, Primary Key

//· PhoneNumber – a string with min length 7 and max length 15 (required)

//· UserId – a string (required)

//· User – an IdentityUser object