﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.core
{
    public class Author
    {
        [Required]
        [Display(Name ="المعرف")]
        public int Id { get; set; }
        [Required]
        [Display(Name = " المعرف المستخدم")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "الاسم الكامل")]

        public string? FullName { get; set; }
        [Display(Name = "الصوره")]


        public string? ProfileImageUrl { get; set; }
        [Display(Name = "السيره الذاتيه")]


        public string? Bio { get; set; }
        [Display(Name = "فيسبوك")]

        public string? Facebook { get; set; }
        [Display(Name = "الاستجرام")]

        public string? Instagram { get; set; }
        [Display(Name = "تويتر")]

        public string ?Twitter { get; set; }
        //navigtion

        public virtual List<AuthorPost> AuthorPosts { get; set; }


    }
}
