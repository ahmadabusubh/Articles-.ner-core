using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Articles.core
{
  public class AuthorPost
    {
        [Required]
        [Display(Name = "المعرف")]
        public int Id { get; set; }


        [Required]
        [Display(Name ="المعرف المستخدم")]

        public string UserId { get; set; }
        [Required]
        [Display(Name ="اسم المسخدم")]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="الاسم كامل")]
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




        [Required (ErrorMessage ="هاذا الحقل مطلوب")]
        [Display(Name = "الصوره")]
        [DataType(DataType.Upload)]

        public string PostImageUrl { get; set; }

        [Required(ErrorMessage = "هاذا الحقل مطلوب")]
        [Display(Name = "تاريخ الاضافه")]
        public DateTime  AddedDate { get; set; }


        //navigation
        public int AuthorId { get; set; }
        public int AuthodId { get; set; }
        public Author Author { get; set; }


        public int CategoryId { get; set; }
        public category Category { get; set; }

    }
}
