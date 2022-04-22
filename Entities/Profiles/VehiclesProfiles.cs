﻿using AutoMapper;
using Back_End.Models;
using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.ResourcesRequestMaterialsMedicinesVehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using Entities.DataTransferObjects.Vehicles___Dto.Update;

namespace Entities.Profiles
{
    public class VehiclesProfiles : Profile
    {
        public VehiclesProfiles()
        {
            CreateMap<Vehicles, VehiclesDto>()

               .ForPath(dest => dest.Type, opts => opts.MapFrom(src => src.TypeVehicles.Type));
          

            CreateMap<Vehicles, ResourcesVehiclesDto>()

                               .ForPath(dest => dest.Name, opts => opts.MapFrom(src => src.Brands.BrandName + " " + src.Model.ModelName))

                               .ForPath(dest => dest.Type, opts => opts.MapFrom(src => src.TypeVehicles.Type));


            CreateMap<Vehicles, Resources_Dto>()

                .ForMember(dest => dest.Description,
                            opt => opt.MapFrom(src => src.VehicleDescription))
                
                .ForMember(dest => dest.Donation,
                            opt => opt.MapFrom(src => src.VehicleDonation))

                  .ForMember(dest => dest.Availability,
                            opt => opt.MapFrom(src => src.VehicleAvailability))

                 .ForMember(dest => dest.Quantity,
                                            opt => opt.MapFrom(src => src.VehicleQuantity))

                    .ForMember(dest => dest.Picture,
                            opt => opt.MapFrom(src => src.VehiclePicture))

                    .ForPath(dest => dest.Vehicles.VehicleUtility,
                            opt => opt.MapFrom(src => src.VehicleUtility))


                            .ForPath(dest => dest.Vehicles.FK_EmployeeID,
                            opt => opt.MapFrom(src => src.FK_EmployeeID))

                .ForPath(dest => dest.Vehicles.Type, opts => opts.MapFrom(src => src.TypeVehicles.Type))

                 .ForPath(dest => dest.Vehicles.VehiclePatent, opts => opts.MapFrom(src => src.VehiclePatent))

                 .ForPath(dest => dest.Vehicles.VehicleYear, opts => opts.MapFrom(src => src.VehicleYear))

                 .ForPath(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.Estates.Locations.LocationCityName))
               
                 .ForPath(dest => dest.Vehicles.Fk_TypeVehicleID, opts => opts.MapFrom(src => src.Fk_TypeVehicleID))


               .ForPath(dest => dest.Vehicles.FK_BrandID, opts => opts.MapFrom(src => src.FK_BrandID))

               .ForPath(dest => dest.Vehicles.FK_ModelID, opts => opts.MapFrom(src => src.FK_ModelID))

               .ForPath(dest => dest.Vehicles.ModelName, opts => opts.MapFrom(src => src.Model.ModelName))
               .ForPath(dest => dest.Vehicles.BrandName, opts => opts.MapFrom(src => src.Brands.BrandName))

                 .ForPath(dest => dest.Vehicles.EmployeeName, opts => opts.MapFrom(src => $"{src.Employees.Users.Persons.FirstName} {src.Employees.Users.Persons.LastName}"));



            CreateMap<VehiclesForCreationDto, Vehicles>();

            CreateMap<Resources_ForCreationDto, Vehicles>();

            CreateMap<VehiclesForUpdateDto, Vehicles>();
            CreateMap<Vehicles, VehiclesForUpdateDto>();

            CreateMap<Vehicles, Resources_ForCreationDto>()
                  .ForPath(dest => dest.Description, opts => opts.MapFrom(src => src.VehicleDescription))
                  .ForPath(dest => dest.Donation, opts => opts.MapFrom(src => src.VehicleDonation))
                  .ForPath(dest => dest.Vehicles.VehicleUtility, opts => opts.MapFrom(src => src.VehicleUtility))
                  .ForPath(dest => dest.Availability, opts => opts.MapFrom(src => src.VehicleAvailability))
                  .ForPath(dest => dest.FK_EstateID, opts => opts.MapFrom(src => src.FK_EstateID))
                  
                  .ForPath(dest => dest.Vehicles.FK_EmployeeID, opts => opts.MapFrom(src => src.FK_EmployeeID))
                  .ForPath(dest => dest.Vehicles.Fk_TypeVehicleID, opts => opts.MapFrom(src => src.Fk_TypeVehicleID))

                  .ForPath(dest => dest.Vehicles.VehiclePatent, opts => opts.MapFrom(src => src.VehiclePatent))
                  .ForPath(dest => dest.Vehicles.VehicleYear, opts => opts.MapFrom(src => src.VehicleYear));
        }
    }
}
