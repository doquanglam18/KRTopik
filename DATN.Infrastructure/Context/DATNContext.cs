using DATN.Domain.Entities;
using DATN.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Context
{
    public class DATNContext : DbContext
    {
        public DATNContext(DbContextOptions options) : base(options)
        {
        }


        
      public DbSet<Comment> comment { get; set; }
        public DbSet<KoreaBlog> koreaBlog { get; set; }
        public  DbSet<ListeningQuestion> listenQuestion { get; set; }

        public DbSet<ListeningAnswer> listenAnswer { get; set; }
        public DbSet<RatingBlog> ratingBlog { get; set; }
        public DbSet<ReadingQuestion> readingQuestion { get; set; }
        public DbSet<ReadingAnswer> readingAnswer { get; set; }
        public DbSet<Role> role { get; set; }
        public DbSet<TestSet> testSet { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<UserProgress> userProgress { get; set; }
        public DbSet<SystemLogging> systemLogging { get; set; }

        public DbSet<RankQuestion> rankQuestion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


          modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new KoreaBlogConfiguration());
            modelBuilder.ApplyConfiguration(new RankQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new ListeningQuestionConfiguration());
          modelBuilder.ApplyConfiguration(new ListeningAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new RatingBlogConfiguration());
            modelBuilder.ApplyConfiguration(new ReadingQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new ReadingAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TestSetConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserProgressConfiguration());
            modelBuilder.ApplyConfiguration(new SystemLoggingConfiguration());
        }

    }
}
