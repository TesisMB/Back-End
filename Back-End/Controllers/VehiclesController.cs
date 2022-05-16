﻿using AutoMapper;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.Vehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto.Update;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/Vehiculos")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;


        public VehiclesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //********************************* FUNCIONANDO *********************************
        [HttpGet]
        public async Task<ActionResult<Vehicles>> GetAllVehicles([FromQuery] int userId)
        {
            try
            {
                var vehicles = await _repository.Vehicles.GetAllVehiclesFilters(userId);
                _logger.LogInfo($"Returned all vehicles from database.");

                var vehiclesResult = _mapper.Map<IEnumerable<Resources_Dto>>(vehicles);


                foreach (var item in vehiclesResult)
                {
                    if (item.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        item.Picture = String.Format("{0}://{1}{2}/StaticFiles/Images/Resources/{3}",
                                        Request.Scheme, Request.Host, Request.PathBase, item.Picture);
                    }

                }

                return Ok(vehiclesResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVehicles action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

      
        //********************************* FUNCIONANDO *********************************
        [HttpGet("{vehicleId}")]
        public async Task<ActionResult<Vehicles>> GetVehicle(string vehicleId)
        {
            try
            {
                var vehicle = await _repository.Vehicles.GetVehicleWithDetails(vehicleId);

                if (vehicle == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't been found in db.");
                    return NotFound();
                }

                else
                {
                    _logger.LogInfo($"Returned vehicle with id: {vehicleId}");



                    var vehicleResult = _mapper.Map<Resources_Dto>(vehicle);

                    if (vehicleResult.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        vehicleResult.Picture = String.Format("{0}://{1}{2}/StaticFiles/Images/Resources/{3}",
                                        Request.Scheme, Request.Host, Request.PathBase, vehicleResult.Picture);
                    }

                    return Ok(vehicleResult);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVehicleById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpPost]
        public async Task<ActionResult<Vehicles>> CreateVehicle([FromBody] Resources_ForCreationDto vehicle)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (vehicle == null)
                {
                    _logger.LogError("Vehicle object sent from client is null.");
                    return BadRequest("Vehicle object is null");
                }
                
                var vehicleEntity = _mapper.Map<Vehicles>(vehicle);

                if (vehicle.Picture == null)
                    vehicleEntity.VehiclePicture = "https://i.imgur.com/S9HJEwF.png";
                else
                     vehicleEntity.VehiclePicture = vehicle.Picture;
                

                _repository.Vehicles.CreateVehicle(vehicleEntity);

                _repository.Vehicles.SaveAsync();

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

      
        //********************************* FUNCIONANDO *********************************
        [HttpPatch("{vehicleId}")]
        public async Task<ActionResult> UpdateVehicle(string vehicleId, JsonPatchDocument<VehiclesForUpdateDto> patchDocument)
        {
            try
            {
                var vehicleEntity = await _repository.Vehicles.GetVehicleById(vehicleId);

                if (vehicleEntity == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't been found in db.");
                    return NotFound();
                }

                var vehicleToPatch = _mapper.Map<VehiclesForUpdateDto>(vehicleEntity);

                vehicleToPatch.DateModified = DateTime.Now;

                patchDocument.ApplyTo(vehicleToPatch, ModelState);


                if (!TryValidateModel(vehicleToPatch))
                {
                    return ValidationProblem(ModelState);
                }


                var vehicleResult = _mapper.Map(vehicleToPatch, vehicleEntity);

                _repository.Vehicles.Update(vehicleResult);

                _repository.Vehicles.SaveAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

       
        //********************************* FUNCIONANDO *********************************
        [HttpDelete("{vehicleId}")]
        public async Task<ActionResult> DeleteVehicle(string vehicleId)
        {
            try
            {
                var vehicle = await _repository.Vehicles.GetVehicleById(vehicleId);

                if (vehicle == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't ben found in db.");
                    return NotFound();
                }

                _repository.Vehicles.DeleteVehicle(vehicle);

                _repository.Vehicles.SaveAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
