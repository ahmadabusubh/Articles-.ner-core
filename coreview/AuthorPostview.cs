using Articles.core;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Articles.coreview
{
    public class AuthorPostview
    {

        
        [Display(Name = "المعرف")]
        public int Id { get; set; }


        
        [Display(Name = "المعرف المستخدم")]

        public string UserId { get; set; }

        
        [Display(Name = "اسم المسخدم")]
        public string UserName { get; set; }

        
        [Display(Name = "الاسم كامل")]
        public string FullName { get; set; }



        [Required(ErrorMessage = "هاذا الحقل مطلوب")]
        [Display(Name = "الصنف")]
        [DataType(DataType.Text)]
        public string PostCategory { get; set; }


        [Required(ErrorMessage = "هاذا الحقل مطلوب")]
        [Display(Name = "العنوان")]


        public string PostTitle { get; set; }

        [Required(ErrorMessage = "هاذا الحقل مطلوب")]
        [Display(Name = "الوصف")]
        [DataType(DataType.MultilineText)]

        public string PostDescription { get; set; }




        [Required(ErrorMessage = "هاذا الحقل مطلوب")]
        [Display(Name = "الصوره")]
        [DataType(DataType.Upload)]

        public IFormFile PostImageUrl { get; set; }

        [Display(Name = "تاريخ الاضافه")]
        public DateTime AddedDate { get; set; }


        //navigation
        public int AuthorId { get; set; }
        public Author Author { get; set; }


        public int CategoryId { get; set; }
        public category Category { get; set; }
    }
}
