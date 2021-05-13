﻿using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Users, UsersDto>()

                /* .ForMember(dest => dest.Roles.RoleName,
                                     opt => opt.MapFrom(src => src.Roles.RoleName))*/

                .ForMember(dest => dest.UserID,
                                    opt => opt.MapFrom(src => src.ID))

                .ForMember(dest => dest.UserAvailable,
                                    opt => opt.MapFrom(src => src.UserAvailability))

                 .ForPath(d => d.Roles, opt => opt.MapFrom(src => src.Roles));



            CreateMap<UsersForCreationDto, Users>()
                .ForPath(d => d.Employees.EmployeeCreatedate, opt => opt.MapFrom(src => src.EmployeeCreatedate));
            ;

            CreateMap<UsersForUpdate, Users>();
            CreateMap<Users, UsersForUpdate>();


            /*Al momento de mapear, defino que el campo denominado token, sera devuelto con los valores obtenido de la funcion
              con el ID, y el RoleName de usuario logueado*/
            CreateMap<Users, UserAuthDto>()
                 .ForMember(dest => dest.token,
                                    opt => opt.MapFrom(src => UserSecurity.GenerateAccessToken(src.ID, src.Roles.RoleName)))

                 .ForMember(dest => dest.RoleName,
                                    opt => opt.MapFrom(src => src.Roles.RoleName));
        }
    }
    
}



