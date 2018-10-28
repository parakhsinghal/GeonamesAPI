using System.ComponentModel.DataAnnotations;
using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain.ViewModels.Update
{
    public class AlternateName : IVersionable
    {
        [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public int AlternateNameId { get; set; }

         [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public int GeonameId { get; set; }

         [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
         [StringLength(24, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string ISO6393LanguageCode { get; set; }

         [StringLength(512, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string AlternateName1 { get; set; }

        public bool? IsPreferredName { get; set; }
        public bool? IsShortName { get; set; }
        public bool? IsColloquial { get; set; }
        public bool? IsHistoric { get; set; }

         [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
