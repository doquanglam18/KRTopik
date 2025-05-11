using DATN.Infrastructure.Context;
using DATN.Infrastructure.Repository.Implements;
using DATN.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DATNContext _context;

        public UnitOfWork(DATNContext context)
        {
            _context = context;

/*            AudioFileRepository = new AudioFileRepository(_context);*/
/*            CategoryRepository = new CategoryRepository(_context);
*/            CommentRepository = new CommentRepository(_context);
            KoreaBlogRepository = new KoreaBlogRepository(_context);
            ListenAnswerRepository = new ListeningAnswerRepository(_context);
            /*ListenImageFileRepository = new ListeningImageFileRepository(_context);*/
            ListenQuestionRepository = new ListeningQuestionRepository(_context);
            RatingBlogRepository = new RatingBlogRepository(_context);
            ReadingAnswerRepository = new ReadingAnswerRepository(_context);
          /*  ReadingImageFileRepository = new ReadingImageFileRepository(_context);*/
            ReadingQuestionRepository = new ReadingQuestionRepository(_context);
            RoleRepository = new RoleRepository(_context);
            SystemLoggingRepository = new SystemLoggingRepository(_context);
            TestSetRepository = new TestSetRepository(_context);
            UserProgressRepository = new UserProgressRepository(_context);
            UserRepository = new UserRepository(_context);
            RankQuestionRepository = new RankQuestionRepository(_context);
        }


/*        public IAudioFileRepository AudioFileRepository { get; }*/

/*        public ICategoryRepository CategoryRepository { get; }
*/
        public ICommentRepository CommentRepository { get; }

        public IKoreaBlogRepository KoreaBlogRepository { get; }

        public IListeningAnswerRepository ListenAnswerRepository { get; }

     /*   public IListeningImageFileRepository ListenImageFileRepository { get; }*/

        public IListeningQuestionRepository ListenQuestionRepository { get; }

        public IRatingBlogRepository RatingBlogRepository { get; }

        public IReadingAnswerRepository ReadingAnswerRepository { get; }

/*        public IReadingImageFileRepository ReadingImageFileRepository { get; }*/

        public IReadingQuestionRepository ReadingQuestionRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public ISystemLoggingRepository SystemLoggingRepository { get; }

        public ITestSetRepository TestSetRepository { get; }

        public IUserProgressRepository UserProgressRepository { get; }

        public IUserRepository UserRepository { get; }

        public IRankQuestionRepository RankQuestionRepository { get; } 

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
