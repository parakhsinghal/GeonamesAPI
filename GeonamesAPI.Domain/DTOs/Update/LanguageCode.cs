using System.ComponentModel.DataAnnotations;
using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain.ViewModels.Update
{
    public class LanguageCode : IVersionable
    {
        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        [StringLength(24, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string ISO6393 { get; set; }

        [StringLength(24, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string ISO6392 { get; set; }

        [StringLength(24, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string ISO6391 { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string Language { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
