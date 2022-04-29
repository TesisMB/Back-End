﻿using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergenciesDisastersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositorWrapper _repository;
        public readonly CruzRojaContext db = new CruzRojaContext();
        public ResourcesRequestMaterialsMedicinesVehicles resources = null;
        public EmergenciesDisastersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<EmergenciesDisasters>> GetAllEmegenciesDisasters()
        {
            try
            {
                var emergenciesDisasters = await _repository.EmergenciesDisasters.GetAllEmergenciesDisasters();

                _logger.LogInfo($"Returned all emergenciesDisasters from database.");

                var emergenciesDisastersResult = _mapper.Map<IEnumerable<EmergenciesDisastersDto>>(emergenciesDisasters);

                var query = from st in emergenciesDisastersResult
                            select st;


                foreach (var item1 in query)
                {

                    foreach (var item3 in item1.Resources_Requests)
                    {

                        foreach (var item2 in item3.Resources_RequestResources_Materials_Medicines_Vehicles)
                        {


                            if (item2.Materials != null)
                            {

                                resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                            .Where(a => a.FK_MaterialID == item2.Materials.ID
                                                    && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                               .AsNoTracking()
                                            .FirstOrDefault();

                                item2.Materials.Quantity = resources.Quantity;

                            }

                            if (item2.Medicines != null)
                            {
                                resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                     .Where(a => a.FK_MedicineID == item2.Medicines.ID
                                             && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                        .AsNoTracking()
                                     .FirstOrDefault();

                                item2.Medicines.Quantity = resources.Quantity;

                            }

                        }
                    }
                }



                return Ok(emergenciesDisastersResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmegenciesDisasters action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }


        [HttpGet("WithoutFilter")]
        public async Task<ActionResult<EmergenciesDisasters>> GetAllEmergenciesDisastersWithoutFilter()
        {
            try
            {
                //cambia el filtrado por emergenicas cargadas por filial
                var emergenciesDisasters = await _repository.EmergenciesDisasters.GetAllEmergenciesDisastersWithourFilter();

                _logger.LogInfo($"Returned all emergenciesDisasters from database.");

                var emergenciesDisastersResult = _mapper.Map<IEnumerable<EmergenciesDisastersDto>>(emergenciesDisasters);


                var query = from st in emergenciesDisastersResult
                            select st;


                foreach (var item1 in query)
                {

                    foreach (var item3 in item1.Resources_Requests)
                    {

                        foreach (var item2 in item3.Resources_RequestResources_Materials_Medicines_Vehicles)
                        {


                            if (item2.Materials != null)
                            {

                                resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                            .Where(a => a.FK_MaterialID == item2.Materials.ID
                                                    && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                               .AsNoTracking()
                                            .FirstOrDefault();

                                item2.Materials.Quantity = resources.Quantity;

                            }

                            if (item2.Medicines != null)
                            {
                                resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                     .Where(a => a.FK_MedicineID == item2.Medicines.ID
                                             && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                        .AsNoTracking()
                                     .FirstOrDefault();

                                item2.Medicines.Quantity = resources.Quantity;

                            }

                        }
                    }
                }




                return Ok(emergenciesDisastersResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmegenciesDisasters action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }




        [HttpGet("WithoutFilter/{emegencyDisasterID}")]
        public async Task<ActionResult<EmergenciesDisasters>> GetEmegencyDisasterIDWithDetails(int emegencyDisasterID)
        {
            try
            {
                var emegencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterWithDetails(emegencyDisasterID);

                if (emegencyDisaster == null)
                {
                    _logger.LogError($"EmergenciesDisasters with id: {emegencyDisasterID}, hasn't been found in db.");

                    return NotFound();
                }

                _logger.LogInfo($"Returned emegencyDisaster with details for id: {emegencyDisasterID}");

                var emergencyDisasterResult = _mapper.Map<EmergenciesDisastersDto>(emegencyDisaster);


                var query = from st in emergencyDisasterResult.Resources_Requests
                            select st;


                foreach (var item3 in query)
                {

                    foreach (var item2 in item3.Resources_RequestResources_Materials_Medicines_Vehicles)
                    {


                        if (item2.Materials != null)
                        {

                            resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                        .Where(a => a.FK_MaterialID == item2.Materials.ID
                                                && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                           .AsNoTracking()
                                        .FirstOrDefault();

                            item2.Materials.Quantity = resources.Quantity;

                        }

                        if (item2.Medicines != null)
                        {
                            resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                 .Where(a => a.FK_MedicineID == item2.Medicines.ID
                                         && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                    .AsNoTracking()
                                 .FirstOrDefault();

                            item2.Medicines.Quantity = resources.Quantity;

                        }

                    }
                }


                if (emergencyDisasterResult.ChatRooms != null)
                {
                    foreach (var item2 in emergencyDisasterResult.ChatRooms.UsersChatRooms)
                    {
                        Persons usersChatRooms = new Persons();
                        Roles roles = new Roles();
                        Users users = new Users();

                        usersChatRooms = db.Persons
                                            .Where(a => a.ID == item2.UserID)
                                            .AsNoTracking()
                                            .FirstOrDefault();
                        users = db.Users
                                       .Where(a => a.UserID == item2.UserID)
                                       .AsNoTracking()
                                       .FirstOrDefault();

                        roles = db.Roles
                                       .Where(a => a.RoleID == users.FK_RoleID)
                                       .AsNoTracking()
                                       .FirstOrDefault();


                        item2.Name = usersChatRooms.FirstName + " " + usersChatRooms.LastName;
                        item2.UserDni = users.UserDni;
                        item2.RoleName = roles.RoleName;

                    }
                }


                return Ok(emergencyDisasterResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmegencyDisasterIDWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<EmergenciesDisasters> CreateEmergencyDisaster([FromBody] EmergenciesDisastersForCreationDto emergenciesDisasters)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (emergenciesDisasters == null)
                {
                    _logger.LogError("EmergencyDisaster object sent from client is null.");
                    return BadRequest("EmergencyDisaster object is null");
                }


                var emergencyDisaster = _mapper.Map<EmergenciesDisasters>(emergenciesDisasters);

                _repository.EmergenciesDisasters.CreateEmergencyDisaster(emergencyDisaster);

                _repository.EmergenciesDisasters.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEmergenciesDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{emegencyDisasterID}")]
        public async Task<ActionResult> UpdatePartialEmegencyDisaster(int emegencyDisasterID, JsonPatchDocument<EmergenciesDisastersForUpdateDto> _emergencyDisaster)
        {
            try
            {
                var emergencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterById(emegencyDisasterID);

                if (emergencyDisaster == null)
                {
                    return NotFound();
                }


                var emergencyDisasterToPatch = _mapper.Map<EmergenciesDisastersForUpdateDto>(emergencyDisaster);

                _emergencyDisaster.ApplyTo(emergencyDisasterToPatch, ModelState);

                if (emergencyDisasterToPatch.EmergencyDisasterEndDate != null)
                {
                    emergencyDisasterToPatch.EmergencyDisasterEndDate = DateTime.Now;
                }

                if (!TryValidateModel(emergencyDisasterToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }


                var emergencyDisasterResult = _mapper.Map(emergencyDisasterToPatch, emergencyDisaster);

                _repository.EmergenciesDisasters.UpdateEmergencyDisaster(emergencyDisasterResult);

                _repository.EmergenciesDisasters.SaveAsync();

                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePartialEmegencyDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{emegencyDisasterID}")]
        public async Task<ActionResult> DeletEmegencyDisaster(int emegencyDisasterID)
        {
            try
            {

                var emegencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterWithDetails(emegencyDisasterID);

                if (emegencyDisaster == null)
                {
                    _logger.LogError($"EmergencyDisaster with id: {emegencyDisasterID}, hasn't ben found in db.");

                    return NotFound();
                }


                _repository.EmergenciesDisasters.DeleteEmergencyDisaster(emegencyDisaster);

                _repository.EmergenciesDisasters.SaveAsync();

                return NoContent();

            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletemegencyDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


    }
}
