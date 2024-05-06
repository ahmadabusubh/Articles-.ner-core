using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Articles.core
{
    public class category
    {
        [Required]
        [Display(Name = "المعرف")]

        public int Id { get; set; }


        [Required(ErrorMessage = "هذا الحقل مطللوب")]
        [Display(Name = "اسم الصنف")]
        [MaxLength(50, ErrorMessage = "اعلى قيمه الادخال هي 50حرف")]
        [MinLength(2, ErrorMessage = "ادنى قيمه الادخال هي 2حرف")]
        [DataType(DataType.Text)]


        public string ?name { get; set; }
        public virtual  List<AuthorPost>? AuthorPosts { get; set; }

    }
}
