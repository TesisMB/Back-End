﻿using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class Resources_RequestRepository : RepositoryBase<ResourcesRequest>, IResources_RequestRepository
    {
        private CruzRojaContext _cruzRojaContext = new CruzRojaContext();
        public static CruzRojaContext db = new CruzRojaContext();
        public static ResourcesRequestMaterialsMedicinesVehicles recursos = null;
        ResourcesRequest userReq = null;

        public Resources_RequestRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }


        public async Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequest(string Condition)
        {

            var user = UsersRepository.authUser;

            var collection = _cruzRojaContext.Resources_Requests as IQueryable<ResourcesRequest>;

            //Admin y C.General -> tiene acceso a todo en funcion del departamento

            if(user.Roles.RoleName == "Admin" && string.IsNullOrEmpty(Condition))
            {
                return await GetAllResourcesRequests(user.Estates.Locations.LocationDepartmentName);
            }

            //Admin y C.General -> tiene acceso a sus propias solicitudes en funcion del departamento
            else if (user.Roles.RoleName == "Admin"  && !string.IsNullOrEmpty(Condition))
            {
                collection = collection.Where(
                                              a => a.Condition == Condition
                                              && a.EmergenciesDisasters.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName)
                                              .AsNoTracking();
            }

            else if (user.Roles.RoleName == "Coordinador General" && string.IsNullOrEmpty(Condition))
            {
                return await GetAllResourcesRequests(user.Estates.Locations.LocationDepartmentName);
            }

            else if (user.Roles.RoleName == "Coordinador General" && !string.IsNullOrEmpty(Condition))
            {
                collection = collection.Where(
                                              a => a.Condition == Condition
                                              && a.EmergenciesDisasters.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName)
                                              .AsNoTracking();
            }

            //Encargado de logistica tiene acceso a las solicitudes pendientes nomas    

            else if (user.Roles.RoleName == "Encargado de Logistica" && Condition == null)
            {
                Condition = "Pendiente";

                collection = collection.Where(
                                              a => a.Condition == Condition 
                                             && a.EmergenciesDisasters.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName)
                                             .AsNoTracking();
            }

            else if (user.Roles.RoleName == "Encargado de Logistica" && !string.IsNullOrEmpty(Condition))
            {

                collection = collection.Where(
                                              a => a.Condition == Condition
                                             && a.EmergenciesDisasters.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName)
                                             .AsNoTracking();
            }

            //C.Emergencias tiene acceso a solamente el historial de solicitudes
            else
            {
                collection = collection.Where(
                                            a => a.Condition == Condition
                                            && a.EmergenciesDisasters.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName)
                                            .AsNoTracking();
            }


            return await collection
                .Include(i => i.Users)
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.TypesEmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.Locations)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Medicines)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
            
                 .ThenInclude(i => i.TypeVehicles)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Brands)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Model)


                .Include(i => i.Users)
                .Include(i => i.Users.Persons)
                .Include(i => i.Users.Roles)
                .Include(i => i.Users.Estates)
                .Include(i => i.Users.Estates.LocationAddress)
                .Include(i => i.Users.Estates.Locations)
                .ToListAsync();
        }



        public async Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequests(string LocationDepartmentName)
        {
            var collection = _cruzRojaContext.Resources_Requests as IQueryable<ResourcesRequest>;

            collection = collection.Where(a => a.EmergenciesDisasters.Locations.LocationDepartmentName == LocationDepartmentName)
                                    .AsNoTracking();

            return await collection
                .Include(i => i.Users)
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.TypesEmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.Locations)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Medicines)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)

                 .ThenInclude(i => i.TypeVehicles)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Brands)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Model)


                .Include(i => i.Users)
                .Include(i => i.Users.Persons)
                .Include(i => i.Users.Roles)
                .Include(i => i.Users.Estates)
                .Include(i => i.Users.Estates.LocationAddress)
                .Include(i => i.Users.Estates.Locations)
                .ToListAsync();
        }


        public void CreateResource_Resquest(ResourcesRequest resources_Request)
        {

            ResourcesRequest rec = null;

            resources_Request.FK_UserID = UsersRepository.authUser.UserID;
            var rol = UsersRepository.authUser.Roles.RoleName;

            //var userRequest = ResourcesRequestForCreationDto.UserRequest;

            rec = db.Resources_Requests
                .Where(
                        a => a.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                        && a.FK_UserID == resources_Request.FK_UserID
                        && a.Condition == "Pendiente")
                        .Include(a => a.Resources_RequestResources_Materials_Medicines_Vehicles)
                       .FirstOrDefault();

         
                if(rec != null)
                {
                    foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
                    {
                        resources_Request.ID = rec.ID;
                        item.FK_Resource_RequestID = rec.ID;
                    }
            }
            else
            {
                if(userReq != null)
                {
                    foreach (var item in userReq.Resources_RequestResources_Materials_Medicines_Vehicles)
                    {
                        resources_Request.ID = userReq.ID;
                        item.FK_Resource_RequestID = userReq.ID;
                    }
                }
            }

            // Usuario existe entonces puedo actualizar y crear añadir nuevos recursos a la solicitud
            foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                var re = recurso(resources_Request, item);

                //No existe el recurso lo creo
                if (re == null && rec != null)
                {
                    if (resources_Request.Description != null)
                    {
                        rec.Description = resources_Request.Description;
                    }
                    else
                    {
                        resources_Request.Description = rec.Description;
                    }

                    spaceCamelCase(resources_Request);

                    //añado el nuevo item
                    AddRecurso(item);

                    Stock(resources_Request, item);

                    DeleteResource(resources_Request);

                    Update(resources_Request);

                    SaveAsync();
                }

                //Actualizando recursos - existe la solicitud a esa Emegrnecia de un Usuario especifico
                else if (re != null && rec != null)
                {
                    if(resources_Request.Description != null)
                    {
                         rec.Description = resources_Request.Description;
                    }


                    resources_Request.FK_UserID = rec.FK_UserID;

                    resources_Request.Status = rec.Status;

                    resources_Request.FK_EmergencyDisasterID = rec.FK_EmergencyDisasterID;

                    spaceCamelCase(rec);

                    DeleteResource(rec);

                    UpdateResources(item, resources_Request);

                    DeleteResource(rec);

                    Update(rec);

                    SaveAsync();

                }
            }


         
                if (rec == null)
                {

                     spaceCamelCase(resources_Request);

                    Create(resources_Request);

                    UpdateResource_Resquest2(resources_Request);

                    DeleteResource(resources_Request);

                    SaveAsync();
                 }
            //cuando no exite ningun registro de solicitud se procede a crearla completa
        }


        private void spaceCamelCase(ResourcesRequest resources_Request)
        {
            //Falta implementarlos en el PATCH
            if (resources_Request.Reason != null)
            {
                resources_Request.Reason = WithoutSpace_CamelCase.GetWithoutSpace(resources_Request.Reason);
            }
        }



        public void AddRecurso(ResourcesRequestMaterialsMedicinesVehicles res)
        {
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            cruzRojaContext.Add(res);

            cruzRojaContext.SaveChanges();
        }

        //Actualiza los recursos en la solicitud existente y mantengo actualizo el Stock
        public void UpdateResources(ResourcesRequestMaterialsMedicinesVehicles res, ResourcesRequest resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;

                //borrar
                var rec = recurso(resources_Request, res);

                if (res.FK_MaterialID != null && rec != null)
                {
                    materials = db.Materials
                        .Where(
                         a => a.ID == res.FK_MaterialID)
                        .FirstOrDefault();

                    res.Materials = materials;
                }

                if (res.FK_MedicineID != null && rec != null)
                {
                    medicines = db.Medicines
                    .Where(
                     a => a.ID == res.FK_MedicineID)
                    .FirstOrDefault();

                    res.Medicines = medicines;
                }

                if (res.FK_VehicleID != null && rec != null)
                {
                    vehicles = db.Vehicles
                   .Where(
                    a => a.ID == res.FK_VehicleID)
                   .FirstOrDefault();

                    res.Vehicles = vehicles;
                }


                if (res.FK_MaterialID != null && rec != null && materials.MaterialQuantity > 0)
                {

                    materials.MaterialQuantity = (materials.MaterialQuantity - res.Quantity);

                    res.ID = rec.ID;

                    rec.Quantity = res.Quantity + rec.Quantity;


                    if (res.Materials.MaterialQuantity == 0)
                    {
                        materials.MaterialAvailability = false;
                    }


                    MaterialsRepository.status(res.Materials);
                }

                if (res.FK_MedicineID != null && rec != null && medicines.MedicineQuantity > 0)
                {
                    res.Medicines.MedicineQuantity = (medicines.MedicineQuantity - res.Quantity);

                    res.ID = rec.ID;

                    rec.Quantity = res.Quantity + rec.Quantity;


                    if (res.Medicines.MedicineQuantity == 0)
                    {
                        medicines.MedicineAvailability = false;
                    }

                    MedicinesRepository.status(res.Medicines);
                }


               if (res.FK_VehicleID != null)
                {
                    res.ID = rec.ID;

                    vehicles.VehicleAvailability = false;

                    VehiclesRepository.status(res.Vehicles);
                }

        }

        public async Task<ResourcesRequest> GetResourcesRequestByID(int resource)
        {

            return await FindByCondition(res => res.ID.Equals(resource))
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Medicines)
                .FirstOrDefaultAsync();


        }


        public static ICollection<ResourcesRequestMaterialsMedicinesVehicles> valorId()
        {

            ICollection<ResourcesRequestMaterialsMedicinesVehicles> recs =

              db.Resources_RequestResources_Materials_Medicines_Vehicles
                    .ToList();


            return recs;

        }




        //Reviso la exitencia de los recursos de la slicitud existente 
        public static ResourcesRequestMaterialsMedicinesVehicles recurso(ResourcesRequest resources_Request, ResourcesRequestMaterialsMedicinesVehicles _Resources_RequestResources_Materials_Medicines_Vehicles)
        {

            if (_Resources_RequestResources_Materials_Medicines_Vehicles.FK_MaterialID != null)
            {

                recursos = db.Resources_RequestResources_Materials_Medicines_Vehicles
                               .Where
                                    (
                                    a => a.FK_Resource_RequestID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_Resource_RequestID
                                    && a.FK_MaterialID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_MaterialID
                                    && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                                    && a.Resources_Request.FK_UserID == resources_Request.FK_UserID)
                                     .Include(a => a.Materials)
                                    .FirstOrDefault();
            }
            else

                if (_Resources_RequestResources_Materials_Medicines_Vehicles.FK_MedicineID != null)
            {

                recursos = db.Resources_RequestResources_Materials_Medicines_Vehicles
                               .Where
                                    (

                                    a => a.FK_Resource_RequestID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_Resource_RequestID
                                    && a.FK_MedicineID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_MedicineID
                                    && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                                    && a.Resources_Request.FK_UserID == resources_Request.FK_UserID)
                                     .Include(a => a.Medicines)
                                    .FirstOrDefault();
            }
            else
            {
                recursos = db.Resources_RequestResources_Materials_Medicines_Vehicles
                 .Where
                      (

                      a => a.FK_Resource_RequestID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_Resource_RequestID
                      && a.FK_VehicleID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_VehicleID
                      && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                      && a.Resources_Request.FK_UserID == resources_Request.FK_UserID)
                       .Include(a => a.Vehicles)
                      .FirstOrDefault();
            }



            return recursos;
        }




        public ResourcesRequest ActualizarEstado(ResourcesRequest resources_Request)
        {

            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (resources_Request.Status == false)
                {

                    if (resources.FK_VehicleID == null && resources.FK_MedicineID == null && resources.FK_MaterialID != null)
                    {
                        Materials materials = null;

                        materials = db.Materials
                                    .Where(a => a.ID == resources.FK_MaterialID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                        materials.MaterialQuantity = materials.MaterialQuantity + resources.Quantity;

                        materials.MaterialAvailability = true;

                        MaterialsRepository.status(materials);
                    }


                    if (resources.FK_VehicleID == null && resources.FK_MaterialID == null && resources.FK_MedicineID != null)
                    {

                        Medicines medicines = null;

                        medicines = db.Medicines
                                    .Where(a => a.ID == resources.FK_MedicineID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                        medicines.MedicineQuantity = medicines.MedicineQuantity + resources.Quantity;
                        medicines.MedicineAvailability = true;

                        MedicinesRepository.status(medicines);
                    }

                    if (resources.FK_MaterialID == null && resources.FK_MedicineID == null && resources.FK_VehicleID != null)
                    {

                        Vehicles vehicles = null;

                        vehicles = db.Vehicles
                                    .Where(a => a.ID == resources.FK_VehicleID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                        vehicles.VehicleAvailability = true;
                        //Entry le dice al DbContext que cambie el estado de la entidad en modificado
                        //setvalues realiza una copia del objeto pasado con los cambios
                        VehiclesRepository.status(vehicles);
                    }
                }
            }

            return resources_Request;
        }




        //Crea la solicitud y actualizo Stock
        public void UpdateResource_Resquest2(ResourcesRequest resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;
            ResourcesRequestMaterialsMedicinesVehicles rec = null;


            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {

                //Revisar Material si existe en la base de datos
                if (resources.FK_MaterialID != null)
                {
                                        materials = db.Materials
                                            .Where(a => a.ID == resources.FK_MaterialID)
                                            .AsNoTracking()
                                            .FirstOrDefault();

                           
                                        materials.MaterialQuantity = materials.MaterialQuantity - resources.Quantity;


                                        if (materials.MaterialQuantity == 0)
                                        {
                                            materials.MaterialAvailability = false;
                                        }

                                        MaterialsRepository.status(materials);
                 }



                    else if (resources.FK_MedicineID != null)
                    {
                      

                            medicines = db.Medicines
                                  .Where(a => a.ID == resources.FK_MedicineID)
                                  .AsNoTracking()
                                  .FirstOrDefault();

                            medicines.MedicineQuantity = medicines.MedicineQuantity - resources.Quantity;


                            if (medicines.MedicineQuantity == 0)
                            {
                                medicines.MedicineAvailability = false;
                            }

                            MedicinesRepository.status(medicines);

                    }

                                else if (resources.FK_VehicleID != null)
                                {
                                    vehicles = db.Vehicles
                                                .Where(a => a.ID == resources.FK_VehicleID)
                                                .AsNoTracking()
                                                .FirstOrDefault();


                                        vehicles.VehicleAvailability = false;

                                        resources.Quantity = 1;

                                        VehiclesRepository.status(vehicles);
                                }
            }
        }



        public ResourcesRequestMaterialsMedicinesVehicles Stock(ResourcesRequest resources, ResourcesRequestMaterialsMedicinesVehicles resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;
            var db = new CruzRojaContext();
            ResourcesRequestMaterialsMedicinesVehicles rec = null;


            foreach (var resource in resources.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (resource.FK_MaterialID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                    .Where(a => a.FK_MaterialID == resources_Request.FK_MaterialID
                            && a.FK_Resource_RequestID == resources.ID
                            && a.Resources_Request.FK_UserID == resources.FK_UserID
                            && a.Resources_Request.FK_EmergencyDisasterID == resources.FK_EmergencyDisasterID)
                    .FirstOrDefault();

                    if (rec == null)
                    {
                        {
                            materials = db.Materials
                                .Where(a => a.ID == resources_Request.FK_MaterialID)
                                .FirstOrDefault();

                            resources_Request.Materials = materials;

                            resources_Request.Materials.MaterialQuantity = resources_Request.Materials.MaterialQuantity - resources_Request.Quantity;

                            if (resources_Request.Materials.MaterialQuantity == 0)
                            {
                                resources_Request.Materials.MaterialAvailability = false;
                            }

                            MaterialsRepository.status(resources_Request.Materials);
                        }
                    }
                }



                if (resource.FK_MedicineID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_MedicineID == resources_Request.FK_MedicineID
                                && a.FK_Resource_RequestID == resources_Request.ID
                                && a.Resources_Request.FK_UserID == resources.FK_UserID
                                && a.Resources_Request.FK_EmergencyDisasterID == resources.FK_EmergencyDisasterID)
                        .FirstOrDefault();


                    if (rec == null)
                    {
                        medicines = db.Medicines
                          .Where(a => a.ID == resources_Request.FK_MedicineID)
                          .FirstOrDefault();

                        resources_Request.Medicines = medicines;

                        resources_Request.Medicines.MedicineQuantity = medicines.MedicineQuantity - resources_Request.Quantity;


                        if (resources_Request.Medicines.MedicineQuantity == 0)
                        {
                            resources_Request.Medicines.MedicineAvailability = false;
                        }

                        MedicinesRepository.status(resources_Request.Medicines);
                    }
                }



                if (resource.FK_VehicleID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_VehicleID == resources_Request.FK_VehicleID
                                && a.FK_Resource_RequestID == resources_Request.ID
                                && a.Resources_Request.FK_UserID == resources.FK_UserID
                                && a.Resources_Request.FK_EmergencyDisasterID == resources.FK_EmergencyDisasterID)
                        .FirstOrDefault();

                    if (rec == null)
                    {
                        vehicles = db.Vehicles
                           .Where(a => a.ID == resources_Request.FK_VehicleID)
                           .FirstOrDefault();

                        resources_Request.Vehicles = vehicles;

                        if (resources_Request.Vehicles != null)
                        {
                            resources_Request.Vehicles.VehicleAvailability = false;

                            resources_Request.Quantity = 1;

                            VehiclesRepository.status(resources_Request.Vehicles);
                        }
                    }
                }
            }

            return resources_Request;
        }


        public ResourcesRequest DeleteResource(ResourcesRequest resources_Request)
        {

            foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (item.Materials != null)
                {
                    item.Materials = null;
                }

                if (item.Medicines != null)
                {
                    item.Medicines = null;

                }

                if (item.Vehicles != null)
                {
                    item.Vehicles = null;

                }
            }

            return resources_Request;
        }

        public ResourcesRequest UpdateStockDelete(ResourcesRequest resource)
        {
           var update =  ActualizarEstado(resource);

            return update;
        }



        public void AcceptRejectRequest(ResourcesRequest resourcesRequest, int userRequestID)
        {
            userReq = _cruzRojaContext.Resources_Requests
                  .Where(
                          a => a.FK_EmergencyDisasterID == resourcesRequest.FK_EmergencyDisasterID
                          && a.FK_UserID == userRequestID
                          && a.Condition == "Pendiente")
                          .Include(a => a.Resources_RequestResources_Materials_Medicines_Vehicles)
                          .AsNoTracking()
                         .FirstOrDefault();


            if (userReq != null)
            {

                if (resourcesRequest.Status == false)
                {
                    ActualizarEstado(userReq);

                    DeleteResource(userReq);

                    userReq.Condition = "Rechazada";
                    userReq.Reason = resourcesRequest.Reason;
                }

                else
                {
                    userReq.Condition = "Aceptada";
                    userReq.Reason = resourcesRequest.Reason;
                }

                Update(userReq);

                SaveAsync();
            }
        }
    }

}
