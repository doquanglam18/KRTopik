using AutoMapper;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.RoleDtos;
using DATN.Application.Dtos.SystemLoggingDtos;
using DATN.Application.Dtos.UserDtos;
using DATN.Domain.Entities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
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




            CreateMap<RankQuestion, RankQuestionDto>()
                .ReverseMap();

            CreateMap<ReadingQuestionCreateDto, ReadingQuestion>()
            .ForMember(dest => dest.ReadingAnswers, opt => opt.MapFrom(src => src.ReadingAnswers));

            CreateMap<ReadingAnswerCreateDto, ReadingAnswer>();


            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<SystemLogging, SystemLoggingDto>()
                .ForMember(ut => ut.UserName, u => u.MapFrom(u => u.User.FullName))
                .ReverseMap();

        }
    }
}
