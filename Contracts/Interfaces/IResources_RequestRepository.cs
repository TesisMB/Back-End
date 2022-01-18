﻿using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IResources_RequestRepository : IRepositoryBase<Resources_Request>
    {
        Task<IEnumerable<Resources_Request>> GetAllResourcesRequest();

        Task<Resources_Request> GetResourcesRequestByID(int resource);

        void CreateResource_Resquest(Resources_Request resources_Request);

        void UpdateResource_Resquest(Resources_Request resources_Request);
        void UpdateResource_Resquest2(Resources_Request resources_Request);

        Resources_Request Stock(Resources_Request resources_Request);
    }
}
