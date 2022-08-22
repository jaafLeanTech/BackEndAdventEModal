using AutoMapper;
using EModel.Entities.DTOs;
using EModel.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModel.Core.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserCreateDto>();

            ////Booking
            //CreateMap<BookingCreateDto, Booking>();
            //CreateMap<Booking, BookingCreateDto>();

            //Booking detail
            CreateMap<BookingDetailDto, BookingDetail>();
            CreateMap<BookingDetail, BookingDetailDto>();
        }
    }
}
