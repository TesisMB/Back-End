﻿using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Back_End.Controllers
{
    [Route("api/Materiales")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        /*Este metodo va a permitir despues poder conectarme tanto para mapear, como para obtener 
         las funciones que se establecieron repositorios correspondientes*/
        public MaterialsController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {

            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<Materials>> GetAllVolunteers()
        {
            try
            {
                var volunteers = await _repository.Materials.GetAllMaterials();

                _logger.LogInfo($"Returned all Materials from database.");

                var materialsResult = _mapper.Map<IEnumerable<Resources_Dto>>(volunteers);

                foreach (var item in materialsResult)
                {
                    item.ImageSrc = String.Format("{0}://{1}{2}/StaticFiles/Images/{3}",
                                                  Request.Scheme, Request.Host, Request.PathBase, item.Picture);
                }

                return Ok(materialsResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllMaterials action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }
        }

        [HttpGet("{materialId}")]
        public async Task<ActionResult<Materials>> GetMaterial(int materialId)
        {
            try
            {

                var volunteer = await _repository.Materials.GetMaterialWithDetails(materialId);

                if (volunteer == null)

                {
                    _logger.LogError($"Material with id: {materialId}, hasn't been found in db.");
                    return NotFound();


                }
                else

                {
                    _logger.LogInfo($"Returned Material with id: {materialId}");
                   
                    var volunteerResult = _mapper.Map<Resources_Dto>(volunteer);
                    return Ok(volunteerResult);

                }

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetMaterialById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpPost]
        public async Task<ActionResult<Materials>> CreateMaterial([FromBody] MaterialsForCreationDto material)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (material == null)

                {
                    _logger.LogError("Material object sent from client is null.");
                    return BadRequest("Material object is null");

                }

                var materialEntity = _mapper.Map<Materials>(material);

                materialEntity.MaterialPicture = await UploadController.SaveImage(material.ImageFile);


                _repository.Materials.CreateMaterial(materialEntity);

                 _repository.Materials.SaveAsync();

                var createdVolunteer = _mapper.Map<MaterialsDto>(materialEntity);

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize(Roles = "Coordinador General, Admin")] 
        [HttpPatch("{materialId}")]
        public async Task<ActionResult> UpdatePartialUser(int materialId, JsonPatchDocument<MaterialsForUpdateDto> _materials)
        {

            try
            {

                var materialEntity = await _repository.Materials.GetMaterialById(materialId);

                if (materialEntity == null)
                {
                    _logger.LogError($"Material with id: {materialId}, hasn't been found in db.");
                    return NotFound();
                }

                var materialToPatch = _mapper.Map<MaterialsForUpdateDto>(materialEntity);

                _materials.ApplyTo(materialToPatch, ModelState);


                if (!TryValidateModel(materialToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                var employeeResult = _mapper.Map(materialToPatch, materialEntity);

                _repository.Materials.UpdateMaterial(employeeResult);
              
                 _repository.Materials.SaveAsync();

                return NoContent();

            }


            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpDelete("{materialId}")]
        public async Task<ActionResult> DeleteEmployee(int materialId)
        {

            try
            {
                var material = await _repository.Materials.GetMaterialById(materialId);

                if (material == null)
                {
                    _logger.LogError($"Material with id: {materialId}, hasn't ben found in db.");
                    return NotFound();
                }

                /*if (_repository.Vehicles.VehciclesByEmployees(employeeId).Any())
                {
                    _logger.LogError($"Cannot delete employee with id: {employeeId}. It has related {_repository.Vehicles}. Delete those accounts first");
                    return BadRequest();
                }*/

                _repository.Materials.Delete(material);

                 _repository.Materials.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
