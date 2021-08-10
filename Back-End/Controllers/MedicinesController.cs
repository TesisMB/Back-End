﻿using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Medicines___Dto;
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
    public class MedicinesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        public MedicinesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {

            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Medicines>> GetAllMedicines()
        {
        

            try
            {
                var volunteers = await _repository.Medicines.GetAllMedicines();

                _logger.LogInfo($"Returned all Materials from database.");

                var volunteersResult = _mapper.Map<IEnumerable<MedicinesDto>>(volunteers);

                return Ok(volunteersResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllMaterials action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }
        }

        // GET api/<MedicinesControllers>/5
        [HttpGet("{medicineId}")]
        public async Task<ActionResult<Medicines>> GetMedicineWithDetails(int medicineId)
        {
            try
            {
                var employee = await _repository.Medicines.GetMedicinelWithDetails(medicineId);

                if (employee == null)
                {
                    _logger.LogError($"Medicine with id: {medicineId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned medicine with details for id: {medicineId}");

                    var employeeResult = _mapper.Map<MedicinesDto>(employee);
                    return Ok(employeeResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Medicines>> CreateMedicine([FromBody] MedicineForCreationDto medicine)
        {
            try
            {

              /*  if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }*/

                if (medicine == null)
                {
                    _logger.LogError("Medicine object sent from client is null.");
                    return BadRequest("Medicine object is null");
                }

                var medicineEntity = _mapper.Map<Medicines>(medicine);

                _repository.Medicines.Create(medicineEntity);

                _repository.Medicines.SaveAsync();

                var createdVehicle = _mapper.Map<MedicinesDto>(medicineEntity);

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateMedicine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPatch("{medicineId}")]
        public async Task<ActionResult> UpdateMedicine(int medicineId, JsonPatchDocument<MedicineForUpdateDto> patchDocument)
        {
            try
            {
                var medicineEntity = await _repository.Medicines.GetMedicineById(medicineId);

                if (medicineEntity == null)
                {
                    _logger.LogError($"Medicine with id: {medicineId}, hasn't been found in db.");
                    return NotFound();
                }

                var medicineToPatch = _mapper.Map<MedicineForUpdateDto>(medicineEntity);

                patchDocument.ApplyTo(medicineToPatch, ModelState);


                if (!TryValidateModel(medicineToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                var medicineResult = _mapper.Map(medicineToPatch, medicineEntity);

                _repository.Medicines.Update(medicineResult);

                _repository.Medicines.SaveAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateMedicine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/<MedicinesControllers>/5
        [HttpDelete("{medicineId}")]
            public async Task<ActionResult> DeleteMedicine(int medicineId)
            {
                try
                {
                    var medicine = await _repository.Medicines.GetMedicineById(medicineId);

                    if (medicine == null)
                    {
                        _logger.LogError($"MedicineId with id: {medicineId}, hasn't ben found in db.");
                        return NotFound();
                    }

                    _repository.Medicines.Delete(medicine);

                     _repository.Medicines.SaveAsync();

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
