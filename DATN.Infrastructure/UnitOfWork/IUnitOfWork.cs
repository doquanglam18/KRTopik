using DATN.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
/*        IAudioFileRepository AudioFileRepository { get; }*/
/*        ICategoryRepository CategoryRepository { get; }
*/        ICommentRepository CommentRepository { get; }
        IKoreaBlogRepository KoreaBlogRepository { get; }
        IListeningAnswerRepository ListenAnswerRepository { get; }
      /*  IListeningImageFileRepository ListenImageFileRepository { get; }*/
        IListeningQuestionRepository ListenQuestionRepository { get; }
        IRatingBlogRepository RatingBlogRepository { get; }
        IReadingAnswerRepository ReadingAnswerRepository { get; }
/*        IReadingImageFileRepository ReadingImageFileRepository { get; }*/
        IReadingQuestionRepository ReadingQuestionRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISystemLoggingRepository SystemLoggingRepository { get; }
        ITestSetRepository TestSetRepository { get; }
        IUserProgressRepository UserProgressRepository { get; }
        IUserRepository UserRepository { get; } 

        IRankQuestionRepository RankQuestionRepository { get; }
    }
}
