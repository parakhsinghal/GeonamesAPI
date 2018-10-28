using System.ComponentModel.DataAnnotations;

namespace GeonamesAPI.Domain.ViewModels.Insert
{

    public class TimeZone
    {
        [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public string TimeZoneId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(2, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public string ISOCountryCode { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public decimal? GMT { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public decimal? DST { get; set; }

        [Required(ErrorMessageResourceName = "TimeZone_RawOffset_Required",
                  ErrorMessageResourceType = typeof(GeonamesAPI.Domain.ErrorMessages.ErrorMessages_US_en))]
        public decimal? RawOffset { get; set; }
    }
}
