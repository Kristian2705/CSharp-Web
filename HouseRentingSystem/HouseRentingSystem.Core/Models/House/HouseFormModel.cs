using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Constants.DataConstants;
using static HouseRentingSystem.Core.Constants.MessageConstants;

namespace HouseRentingSystem.Core.Models.House
{
	public class HouseFormModel
	{
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(HouseTitleMaxLength,
            MinimumLength = HouseTitleMinLength,
            ErrorMessage = LengthMessage)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(HouseAddressMaxLength,
            MinimumLength = HouseAddressMinLength,
            ErrorMessage = LengthMessage)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(HouseDescriptionMaxLength,
            MinimumLength = HouseDescriptionMinLength,
            ErrorMessage = LengthMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [Range(typeof(decimal),
            HouseRentingMinPrice,
            HouseRentingMaxPrice,
            ErrorMessage = PriceRangeMessage)]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } =
            new List<HouseCategoryServiceModel>();
    }
}
