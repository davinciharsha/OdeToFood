﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
   //IMPLEMENTATION OF MAX WORDS TO RESTRICT REVIEWER NAME ONLY
   //TO ONE WORD

        //inheriting from IValidatableObject inteface to implement custom
        //validation

    public class RestaurantReview
    {

        public int Id { get; set; }
        [Range(1,10)]
        [Required]
        public int Rating { get; set; }

        [Required]
        [StringLength(1024)]
        public string Body { get; set; }

        [Display(Name ="User Name")]
        [DisplayFormat(NullDisplayText ="anonymous")]
        //[MaxWords(1)]
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; }

    }
}