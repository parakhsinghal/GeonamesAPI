using System.ComponentModel.DataAnnotations;

namespace GeonamesAPI.Domain.ViewModels.Insert
{
    public class Admin2Code
    {
        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string Admin2CodeId { get; set; }

        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string Admin2CodeName { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string ASCIIName { get; set; }
    }
}
