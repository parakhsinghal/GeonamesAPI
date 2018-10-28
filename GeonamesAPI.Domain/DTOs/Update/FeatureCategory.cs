using System.ComponentModel.DataAnnotations;

namespace GeonamesAPI.Domain.ViewModels.Update
{
    public class FeatureCategory
    {
         [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
         [StringLength(1, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string FeatureCategoryId { get; set; }

         [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
         [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string FeatureCategoryName { get; set; }

         [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
