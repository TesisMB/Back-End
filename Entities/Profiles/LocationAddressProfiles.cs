﻿using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Entities.DataTransferObjects.Models.Vehicles___Dto;

namespace Back_End.Profiles
{
    public class LocationAddressProfiles :Profile
    {
        public LocationAddressProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<LocationAddress, LocationAddressDto>();

            CreateMap<LocationAddress, LocationAddressVehiclesDto>();
        }
    }
}
