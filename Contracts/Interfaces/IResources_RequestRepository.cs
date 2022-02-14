﻿using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IResources_RequestRepository : IRepositoryBase<ResourcesRequest>
    {
        Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequest();

        Task<ResourcesRequest> GetResourcesRequestByID(int resource);

        void CreateResource_Resquest(ResourcesRequest resources_Request, int userRequest);
    }
}
