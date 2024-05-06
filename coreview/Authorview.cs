using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;


namespace Articles.coreview
{
    public class Authorview
    {
        [Required]
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required]
        [Display(Name = " المعرف المستخدم")]

        public string? UserId { get; set; }
        [Required]
        [Display(Name = "اسم المستخدم")]



        public string? UserName { get; set; }
        [Required]
        [Display(Name = "الاسم الكامل")]

        public string? FullName { get; set; }
        [Display(Name = "الصوره")]


        public IFormFile? ProfileImageUrl { get; set; }
        [Display(Name = "السيره الذاتيه")]


        public string? Bio { get; set; }
        [Display(Name = "فيسبوك")]

        public string? Facebook { get; set; }
        [Display(Name = "الاستجرام")]

        public string? Instagram { get; set; }
        [Display(Name = "تويتر")]

        public string? Twitter { get; set; }


    }
}
