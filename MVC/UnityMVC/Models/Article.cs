using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace UnityTutorials.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ArticleContent { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUsername { get; set; }
        public string Tags { get; set; }
    }
}