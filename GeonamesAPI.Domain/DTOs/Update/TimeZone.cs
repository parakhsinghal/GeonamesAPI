using System.ComponentModel.DataAnnotations;
using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain.ViewModels.Update
{

    public class TimeZone : IVersionable
    {
        [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string TimeZoneId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        [StringLength(2, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string ISOCountryCode { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public decimal? GMT { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public decimal? DST { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public decimal? RawOffset { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                 ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
