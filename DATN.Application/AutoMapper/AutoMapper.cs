using AutoMapper;
using DATN.Application.Dtos.CommentDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.RoleDtos;
using DATN.Application.Dtos.SystemLoggingDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Dtos.UserDtos;
using DATN.Domain.Entities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //Map ReadingQuestion 
            CreateMap<ReadingQuestion, ReadingQuestionDto>()
                .ForMember(rq => rq.TestSetName, r => r.MapFrom(rq => rq.TestSet.TestName))
                .ForMember(rq => rq.RankQuestionName, r => r.MapFrom(rq => rq.RankQuestion.RankQuestionName))
                .ForMember(rq => rq.ReadingAnswers, r => r.MapFrom(rq => rq.ReadingAnswers));
                
            CreateMap<ReadingAnswer, ReadingAnswerDTO>().ReverseMap();

            CreateMap<ReadingQuestionDto, ReadingQuestion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Nếu bạn không muốn ghi đè Id
                .ForMember(dest => dest.ReadingAnswers, opt => opt.Ignore()) // Tuỳ bạn xử lý riêng
                .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic)); // Ánh xạ IsPublic
            CreateMap<ReadingQuestionCreateDto, ReadingQuestion>()
                .ForMember(dest => dest.ReadingAnswers, opt => opt.MapFrom(src => src.ReadingAnswers));

            CreateMap<ReadingAnswerCreateDto, ReadingAnswer>();
            CreateMap<ReadingAnswerForTestDto, ReadingAnswer>().ReverseMap();

            CreateMap<ReadingQuestionForTestDto, ReadingQuestion>()
                .ForMember(rq => rq.ReadingAnswers, r => r.MapFrom(rq => rq.ReadingAnswers))
                .ReverseMap();


            //Map ListeningQuestion
            CreateMap<ListeningQuestion, ListeningQuestionDto>()
                .ForMember(lq => lq.TestSetName, l => l.MapFrom(lq => lq.TestSet.TestName))
                .ForMember(lq => lq.RankQuestionName, l => l.MapFrom(lq => lq.RankQuestion.RankQuestionName))
                .ForMember(lq => lq.ListeningAnswers, l => l.MapFrom(lq => lq.ListeningAnswers));

            CreateMap<ListeningAnswer, ListeningAnswerDto>().ReverseMap();

            CreateMap<ListeningQuestionDto, ListeningQuestion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Nếu bạn không muốn ghi đè Id
                .ForMember(dest => dest.ListeningAnswers, opt => opt.Ignore()) // Tuỳ bạn xử lý riêng
                .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic)); // Ánh xạ IsPublic
            CreateMap<ListeningQuestionCreateDto, ListeningQuestion>()
              .ForMember(dest => dest.ListeningAnswers, opt => opt.MapFrom(src => src.ListeningAnswers));

            CreateMap<ListeningAnswerCreateDto, ListeningAnswer>();

            CreateMap<ListeningQuestionForTestDto, ListeningQuestion>()
                .ForMember(lq => lq.ListeningAnswers, l => l.MapFrom(lq => lq.ListeningAnswers))
                .ReverseMap();

            CreateMap<ListeningAnswerForTestDto, ListeningAnswer>().ReverseMap();





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




            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<SystemLogging, SystemLoggingDto>()
                .ForMember(ut => ut.UserName, u => u.MapFrom(u => u.User.FullName))
                .ReverseMap();


            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.User.AvatarImageUrl))
                .ReverseMap();


        }
    }
}
