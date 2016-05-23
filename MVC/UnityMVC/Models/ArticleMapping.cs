using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace UnityTutorials.Models
{
    public class ArticleMapping : EntityTypeConfiguration<Article>
    {
        public ArticleMapping()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Title)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();


            this.Property(x => x.CreatedDate)
                .HasColumnType("DATE")
                .IsOptional();
                
            this.ToTable("Articles");
        }
    }
}