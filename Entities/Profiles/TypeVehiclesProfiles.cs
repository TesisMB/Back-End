﻿using AutoMapper;
using Back_End.Models;
using Back_End.Models.TypeVehicles___Dto;
using Entities.DataTransferObjects.BrandsModels__Dto;
using Entities.DataTransferObjects.TypeVehicles___Dto;

namespace Entities.Profiles
{
    public class TypeVehiclesProfiles : Profile
    {
        public TypeVehiclesProfiles()
        {
            CreateMap<TypeVehicles, TypeVehiclesDto>();
            CreateMap<TypeVehiclesForCreationDto, TypeVehicles>();


            CreateMap<TypeVehicles, BrandsModelsForSelectDto>()
                                .ForPath(i => i.FK_TypeVehicleID, opt => opt.MapFrom(src => src.ID));

        }
    }
}
