using AutoMapper;
using DATN.Application.Dtos.CommentDtos;
using DATN.Application.Dtos.KoreaBlogDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.ListeningDtos.ForAddTestSet;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.ReadingDtos.ForAddTestSet;
using DATN.Application.Dtos.RoleDtos;
using DATN.Application.Dtos.SystemLoggingDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Dtos.TestSetDtos.ForAdmin;
using DATN.Application.Dtos.UserDtos;
using DATN.Application.Dtos.UserProgressDtos;
using DATN.Domain.Entities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Map User
            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<User, UserTokenDTO>()
                .ForMember(ut => ut.RoleName, u => u.MapFrom(u => u.Role.RoleName)).ReverseMap();
            CreateMap<User, UserDetailDto>()
                .ForMember(ut => ut.RoleId, u => u.MapFrom(u => u.Role.Id))
                .ForMember(ut => ut.RoleName, u => u.MapFrom(u => u.Role.RoleName))
                .ReverseMap();
            CreateMap<User, UserOwnerDto>().ReverseMap();

            CreateMap<User, UserDetailForUserDto>()
                .ForMember(ut => ut.RoleName, u => u.MapFrom(u => u.Role.RoleName))
                .ForMember(ut => ut.UserProgresses, u => u.MapFrom(u => u.UserProgresses))
                .ForMember(ut => ut.CommentCount, u => u.MapFrom(u => u.Comments.Count))
                .ForMember(ut => ut.IsActive, u => u.MapFrom(u => u.IsActive))
                .ReverseMap();

            //Map ReadingQuestion 
            CreateMap<ReadingQuestion, ReadingQuestionDto>()
                .ForMember(rq => rq.TestSetName, r => r.MapFrom(rq => rq.TestSet.TestName))
                .ForMember(rq => rq.RankQuestionName, r => r.MapFrom(rq => rq.RankQuestion.RankQuestionName))
                .ForMember(rq => rq.ReadingAnswers, r => r.MapFrom(rq => rq.ReadingAnswers));
                
            CreateMap<ReadingAnswer, ReadingAnswerDTO>().ReverseMap();

            CreateMap<ReadingQuestionDto, ReadingQuestion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ReadingAnswers, opt => opt.Ignore()) 
                .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic)); 
            CreateMap<ReadingQuestionCreateDto, ReadingQuestion>()
                .ForMember(dest => dest.ReadingAnswers, opt => opt.MapFrom(src => src.ReadingAnswers));

            CreateMap<ReadingAnswerCreateDto, ReadingAnswer>();
            CreateMap<ReadingAnswerForTestDto, ReadingAnswer>().ReverseMap();

            CreateMap<ReadingQuestionForTestDto, ReadingQuestion>()
                .ForMember(rq => rq.ReadingAnswers, r => r.MapFrom(rq => rq.ReadingAnswers))
                .ReverseMap();

            CreateMap<ReadingAwDto, ReadingAnswer>()
                .ReverseMap();

            CreateMap<ReadingQsDto, ReadingQuestion>()
                .ForMember(dest => dest.ReadingAnswers, opt => opt.MapFrom(src => src.ReadingAnswers))
                .ReverseMap();


            //Map ListeningQuestion
            CreateMap<ListeningQuestion, ListeningQuestionDto>()
                .ForMember(lq => lq.TestSetName, l => l.MapFrom(lq => lq.TestSet.TestName))
                .ForMember(lq => lq.RankQuestionName, l => l.MapFrom(lq => lq.RankQuestion.RankQuestionName))
                .ForMember(lq => lq.ListeningAnswers, l => l.MapFrom(lq => lq.ListeningAnswers));

            CreateMap<ListeningAnswer, ListeningAnswerDto>().ReverseMap();

            CreateMap<ListeningQuestionDto, ListeningQuestion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.ListeningAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic));
            CreateMap<ListeningQuestionCreateDto, ListeningQuestion>()
              .ForMember(dest => dest.ListeningAnswers, opt => opt.MapFrom(src => src.ListeningAnswers));

            CreateMap<ListeningAnswerCreateDto, ListeningAnswer>();

            CreateMap<ListeningQuestionForTestDto, ListeningQuestion>()
                .ForMember(lq => lq.ListeningAnswers, l => l.MapFrom(lq => lq.ListeningAnswers))
                .ReverseMap();

            CreateMap<ListeningAnswerForTestDto, ListeningAnswer>().ReverseMap();

            CreateMap<ListeningAwDto, ListeningAnswer>()
                .ReverseMap();

            CreateMap<ListeningQsDto, ListeningQuestion>()
                .ForMember(dest => dest.ListeningAnswers, opt => opt.MapFrom(src => src.ListeningAnswers))
                .ReverseMap();




            CreateMap<RankQuestion, RankQuestionDto>()
                .ReverseMap();



            //Map TetsSet
            CreateMap<TestSet, TestSetForUserDto>()
                .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.ListeningQuestions.Count + src.ReadingQuestions.Count))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.RankQuestionName, opt => opt.MapFrom(src => src.RankQuestion.RankQuestionName))
                .ReverseMap();

            CreateMap<TestSet, TestSetDetailsDto>()
                .ForMember(dest => dest.CountListeningQuestion, opt => opt.MapFrom(src => src.ListeningQuestions.Count))
                .ForMember(dest => dest.CountReadingQuestion, opt => opt.MapFrom(src => src.ReadingQuestions.Count))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.CountUserDo, opt => opt.MapFrom(src => src.UserProgress.Count))
                .ForMember(dest => dest.RankQuestionName, opt => opt.MapFrom(src => src.RankQuestion.RankQuestionName))
                .ForMember(dest => dest.AvgRating, opt => opt.MapFrom(src => src.Comments.Count > 0 ? (int)src.Comments.Average(c => c.Rating) : 0))
                .ReverseMap();

            CreateMap<TestSet, DoTestSetDto>()
                .ForMember(dest => dest.RankQuestionName, opt => opt.MapFrom(src => src.RankQuestion.RankQuestionName))
                .ForMember(dest => dest.AvgRating, opt => opt.MapFrom(src => src.Comments.Count > 0 ? (int)src.Comments.Average(c => c.Rating) : 0))
                .ForMember(dest => dest.listeningQuestions, opt => opt.MapFrom(src => src.ListeningQuestions))
                .ForMember(dest => dest.readingQuestions, opt => opt.MapFrom(src => src.ReadingQuestions))
                .ReverseMap();

            CreateMap<TestSet, ListTestSetForAdmin>()
                .ForMember(dest => dest.CountQuestions, opt => opt.MapFrom(src => src.ListeningQuestions.Count + src.ReadingQuestions.Count))
                .ForMember(dest => dest.CountUserDoTest, opt => opt.MapFrom(src => src.UserProgress.Count))
                .ForMember(dest => dest.RankQuestionName, opt => opt.MapFrom(src => src.RankQuestion.RankQuestionName))
                .ForMember(dest => dest.CountComment, opt => opt.MapFrom(src => src.Comments.Count))
                .ReverseMap();

            CreateMap<TestSet, TestSetDetailsForAdmin>()
                .ForMember(dest => dest.ListeningQuestions, opt => opt.MapFrom(src => src.ListeningQuestions))
                .ForMember(dest => dest.ReadingQuestions, opt => opt.MapFrom(src => src.ReadingQuestions))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.CountUserDoTest, opt => opt.MapFrom(src => src.UserProgress.Count))
                .ForMember(dest => dest.RankQuestionName, opt => opt.MapFrom(src => src.RankQuestion.RankQuestionName))
                .ReverseMap();




            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<SystemLogging, SystemLoggingDto>()
                .ForMember(ut => ut.UserName, u => u.MapFrom(u => u.User.FullName))
                .ReverseMap();


            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.User.AvatarImageUrl))
                .ReverseMap();


            //Map KoreaBlog
            CreateMap<RatingBlog, RatingBlogDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.User.AvatarImageUrl))
                .ReverseMap();

            CreateMap<KoreaBlog, KoreaBlogForList>()
               .ForMember(dest => dest.AvgRating, opt => opt.MapFrom(src =>
                   src.RatingBlogs != null && src.RatingBlogs.Any()
                       ? src.RatingBlogs.Average(r => r.Rating)
                       : 0))
               .ReverseMap();


            CreateMap<KoreaBlogCreateDto, KoreaBlog>()
                .ReverseMap();

            CreateMap<KoreaBlog, KoreaBlogDetailsDto>()
                .ForMember(dest => dest.RatingBlogs, opt => opt.MapFrom(src => src.RatingBlogs))
                .ReverseMap();

            //Map User Progress
            CreateMap<CreateUserProgressDto, UserProgress>().ReverseMap();

            CreateMap<UserProgress, UserProgressDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.TestSetName, opt => opt.MapFrom(src => src.TestSet.TestName))
                .ReverseMap();
        }
    }
}
