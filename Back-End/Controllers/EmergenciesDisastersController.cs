﻿using AutoMapper;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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


        //********************************* FUNCIONANDO *********************************
        [HttpGet]
        public async Task<ActionResult<EmergenciesDisasters>> GetAllEmegenciesDisasters(int userId)
        {
            try
            {
                var emergenciesDisasters = await _repository.EmergenciesDisasters.GetAllEmergenciesDisasters(userId);

                _logger.LogInfo($"Returned all emergenciesDisasters from database.");

                var emergenciesDisastersResult = _mapper.Map<IEnumerable<EmergenciesDisastersSelectDto>>(emergenciesDisasters);

                var query = from st in emergenciesDisastersResult
                            select st;


                return Ok(emergenciesDisastersResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmegenciesDisasters action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpGet("WithoutFilter")]

        public async Task<ActionResult<EmergenciesDisasters>> GetAllEmergenciesDisastersWithoutFilter([FromQuery] int userId, [FromQuery] string limit)
        {
            try
            {
                var emergenciesDisasters = await _repository.EmergenciesDisasters.GetAllEmergenciesDisastersWithourFilter(userId, limit);

                _logger.LogInfo($"Returned all emergenciesDisasters from database.");

                var emergenciesDisastersResult = _mapper.Map<IEnumerable<EmergenciesDisastersAppDto>>(emergenciesDisasters);


                return Ok(emergenciesDisastersResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmegenciesDisasters action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }



        //********************************* FUNCIONANDO *********************************

        //TODO Hacer modelo dto aparte tanto para victims como resourceRequest(WebApp)
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

                if (emergencyDisasterResult.ChatRooms.UsersChatRooms != null)
                {

                    foreach (var item in emergencyDisasterResult.ChatRooms.UsersChatRooms)
                    {
                        if (item.Picture != null)
                        {
                            item.Picture = String.Format("{0}://{1}{2}/StaticFiles/Images/Resources/{3}",
                                                         Request.Scheme, Request.Host, Request.PathBase, item.Picture);

                        }
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


        //********************************* FUNCIONANDO *********************************
        [HttpPost]
        public ActionResult<EmergenciesDisasters> CreateEmergencyDisaster([FromBody] EmergenciesDisastersForCreationDto emergenciesDisasters, [FromQuery] int userId)
        {
            emergenciesDisasters.CreatedBy = userId;

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

                emergencyDisaster.ChatRooms = new ChatRooms();
                emergencyDisaster.ChatRooms.CreationDate = emergenciesDisasters.CreationDate;
                emergencyDisaster.ChatRooms.FK_TypeChatRoomID = emergenciesDisasters.FK_TypeChatRoomID;


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


        //********************************* FUNCIONANDO *********************************
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

                emergencyDisasterToPatch.EmergencyDisasterDateModified = DateTime.Now;

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


        //********************************* FUNCIONANDO *********************************

        [HttpDelete("{emegencyDisasterID}")]
        public async Task<ActionResult> DeletEmegencyDisaster(int emegencyDisasterID)
        {
            try
            {

                var emegencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterById(emegencyDisasterID);

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
