using System.ComponentModel.DataAnnotations;

namespace GlossaryWebUI.Models
{
    public class GlossaryModel
    {
        public int TermId { get; set; }
        [Required(ErrorMessage="Please Enter term")]
        [Display(Name = "Enter Term")]
        public string Term { get; set; }

        [Display(Name = "Enter Definition")]
        [Required(ErrorMessage = "Please Enter Term Definition")]
        public string Definition { get; set; }
    }
}