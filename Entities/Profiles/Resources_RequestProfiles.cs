﻿using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class Resources_RequestProfiles: Profile
    {
        public Resources_RequestProfiles()
        {
            CreateMap<Resources_Request, Resources_RequestDto>();
            CreateMap<Resources_RequestForCreationDto, Resources_Request>();

        }
    }
}
