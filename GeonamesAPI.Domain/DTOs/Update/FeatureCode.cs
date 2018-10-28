using System.ComponentModel.DataAnnotations;
using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain.ViewModels.Update
{
    public class FeatureCode : IVersionable
    {
        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(16, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public string FeatureCodeId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public string FeatureCodeName { get; set; }

         [StringLength(512, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
