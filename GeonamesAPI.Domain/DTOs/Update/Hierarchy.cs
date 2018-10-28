using System.ComponentModel.DataAnnotations;
using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain.ViewModels.Update
{
    public class Hierarchy : IVersionable
    {
        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public long ParentId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public long ChildId { get; set; }

         [StringLength(50, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public string Type { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
