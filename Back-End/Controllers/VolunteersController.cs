﻿using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;
using Contracts.Interfaces;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.Volunteers__Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        public VolunteersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [Route("api/Voluntarios")]
        [HttpGet]
        public async Task<ActionResult<Volunteers>> GetAllVolunteers()
        {
            try
            {
                var volunteers = await _repository.Volunteers.GetAllVolunteers();

                _logger.LogInfo($"Returned all Volunteers from database.");

                var volunteersResult = _mapper.Map<IEnumerable<Resources_Dto>>(volunteers);

                foreach (var item in volunteersResult)
                {
                    if(item.Picture == null)
                    {
                        item.Picture = "https://i.imgur.com/8AACVdK.png";
                    }
                    else if (item.Picture != null)
                    {
                         item.ImageSrc = String.Format("{0}://{1}{2}/StaticFiles/Images/{3}",
                                          Request.Scheme, Request.Host, Request.PathBase, item.Picture);
                    }
                }

                
                return Ok(volunteersResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllVolunteers action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }
        }

        [Route("api/app/Volunteers")]
        [HttpGet]
        public async Task<ActionResult<Volunteers>> GetAllVolunteersApp()
        {
            try
            {
                var volunteers1 = await _repository.Volunteers.GetAllVolunteersApp();

                _logger.LogInfo($"Returned all Volunteers from database.");

                var volunteersResult = _mapper.Map<IEnumerable<VolunteersAppDto>>(volunteers1);

                return Ok(volunteersResult);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVolunteersApp action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

        [Route("api/app/Volunteers/{volunteerId}")]
        [HttpGet]
        public async Task<ActionResult<Volunteers>> GetAllVolunteerApp(int volunteerId)
        {
            try
            {
                var volunteer = await _repository.Volunteers.GetVolunteerAppWithDetails(volunteerId);

                if (volunteer == null)

                {
                    _logger.LogError($"Volunteer with id: {volunteerId}, hasn't been found in db.");
                    return NotFound();


                }
                else

                {
                    _logger.LogInfo($"Returned volunteer with id: {volunteerId}");
                    var volunteerResult = _mapper.Map<VolunteersAppDto>(volunteer);
                    return Ok(volunteerResult);

                }

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [Route("api/Voluntarios/{volunteerId}")]
        [HttpGet]
        public async Task<ActionResult<Volunteers>> GetVolunteer(int volunteerId)
        {
            try
            {
                var volunteer = await _repository.Volunteers.GetVolunteerWithDetails(volunteerId);

                if (volunteer == null)

                {
                    _logger.LogError($"Volunteer with id: {volunteerId}, hasn't been found in db.");
                    return NotFound();


                }
                else

                {
                    _logger.LogInfo($"Returned volunteer with id: {volunteerId}");
                    var volunteerResult = _mapper.Map<Resources_Dto>(volunteer);
                    return Ok(volunteerResult);

                }

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [Route("api/Voluntarios")]
        [HttpPost]
        public async Task<ActionResult<Volunteers>> CreateVolunteer(VolunteersForCreationDto volunteer)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (volunteer == null)

                {
                    _logger.LogError("Volunteer object sent from client is null.");
                    return BadRequest("Volunteer object is null");

                }


                var volunteerEntity = _mapper.Map<Volunteers>(volunteer);


               /* volunteerEntity.LocationVolunteers = new LocationVolunteers()
                {
                    ID = volunteerEntity.ID,
                    LocationVolunteerLatitude = null,
                    LocationVolunteerLongitude = null,
                    Volunteers = volunteerEntity
                };*/

                volunteerEntity.VolunteerAvatar = await UploadController.SaveImage(volunteer.ImageFile);

                // Al crear un Usuario se encripta dicha contraseña para mayor seguridad.
                _repository.Volunteers.CreateVolunteer(volunteerEntity);
                volunteerEntity.Users.UserPassword = Encrypt.GetSHA256(volunteerEntity.Users.UserPassword);

                _repository.Volunteers.SaveAsync();

                //var createdVolunteer = _mapper.Map<VolunteersDto>(volunteerEntity);

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //[Authorize(Roles = "Coordinador General, Admin")] 
        [Route("api/Voluntarios/{volunteerId}")]
        [HttpPatch]
        public async Task<ActionResult> UpdatePartialUser(int volunteerId, JsonPatchDocument<VolunteersForUpdatoDto> patchDocument)
        {

            var userFromRepo = await _repository.Volunteers.GetVolunteersById(volunteerId);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<VolunteersForUpdatoDto>(userFromRepo);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            Users authUser = new Users();
            if (!string.IsNullOrEmpty(userToPatch.Users.UserNewPassword))
            {
                // AGREGARLOS EN EL REPOSITORIO
                var userPass = userToPatch.Users.UserPassword;
                userToPatch.Users.UserPassword = Encrypt.GetSHA256(userPass);

                using (var db = new CruzRojaContext())
                    authUser = db.Users.Where(u => u.UserID == userFromRepo.Users.UserID
                          && u.UserPassword == userToPatch.Users.UserPassword)
                        .AsNoTracking()
                        .FirstOrDefault();


                if (authUser == null)
                {
                    return BadRequest(ErrorHelper.Response(400, "La contraseña es erronea."));
                }

                else
                {
                    userToPatch.Users.UserNewPassword = userToPatch.Users.UserNewPassword.Trim();

                    var userNewPass = userToPatch.Users.UserNewPassword;
                    userToPatch.Users.UserNewPassword = Encrypt.GetSHA256(userNewPass);

                    userToPatch.Users.UserPassword = userToPatch.Users.UserNewPassword;
                }
            }


            _mapper.Map(userToPatch, userFromRepo);

            _repository.Volunteers.Update(userFromRepo);

            //_repository.Save();

            return NoContent();
        }


        [Route("api/Voluntarios/{volunteerId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteVolunteer(int volunteerId)
        {
            try
            {
                var volunteer = await _repository.Users.GetUserVolunteerById(volunteerId);


                if (volunteer == null)
                {
                    _logger.LogError($"Volunteer with id: {volunteerId}, hasn't ben found in db.");
                    return NotFound();
                }

                _repository.Users.Delete(volunteer);

                _repository.Volunteers.SaveAsync();

                // Se retorna con exito la eliminacion del Usuario especificado
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVolunteer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
