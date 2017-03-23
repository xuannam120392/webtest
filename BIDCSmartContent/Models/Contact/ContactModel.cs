using BIDVSmartContent.Models.Block;
using System.ComponentModel.DataAnnotations;

namespace BIDVSmartContent.Models.Contact
{
    public class ContactModel
    {
        public BlockModel BlockFooter { get; set; }
        public string SiteMain { get; set; }
        public string HotLineTitle { get; set; }
        public string HotLine { get; set; }
        public string ContactTitle { get; set; }
        public string CopyRight { get; set; }
        public string LinkFace { get; set; }
        public string LinkedIn { get; set; }
        public string LinkYoutube { get; set; }
        public string TitleMain { get; set; }
        public string DescMain { get; set; }
        public string Keyword { get; set; }
        public string Email_Server { get; set; }
        public string Pass_Email_Server { get; set; }
        public string Email_To { get; set; }
        [Display(Name = "Tên của bạn")]
        [Required(ErrorMessage = "Tên không được trống")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được trống")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email không đúng")]
        public string Email { get; set; }

        [Display(Name = "Sđt")]
        [Required(ErrorMessage = "Sđt không được trống")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Sdt không đúng")]
        public string Sdt { get; set; }

        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string Noidung { get; set; }
        public string Status { get; set; }
    }
}