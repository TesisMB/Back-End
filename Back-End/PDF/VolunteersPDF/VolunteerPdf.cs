﻿using Back_End.Models;
using System;
using System.Text;

namespace Back_End.VolunteersPDF
{
    public static class VolunteerPdf
    {
        public static string GetHTMLString(Volunteers employee)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string birthdate = employee.Users.Persons.Birthdate.ToString("dd/MM/yyyy");
            string status = string.Empty;
            string statusEmer = string.Empty;

            if (employee.Users.UserAvailability)
                status = "Disponible";
            else
                status = "No Dispobile";


            if (employee.Users.Persons.Status)
                statusEmer = "Disponible";
            else
                statusEmer = "No Disponible";



            var sb = new StringBuilder();
            sb.Append($@"
                        <html>
                            <head>
                                <link rel = 'stylesheet' href = 'https://use.fontawesome.com/releases/v5.15.3/css/all.css'
                                      integrity = 'sha384-SZXxX4whJ79/gErwcOYf+zWLeJdY/qpuqC4cAa9rOGUstPomtqpuNWT9wdPEn2fk' crossorigin = 'anonymous'>
                                <link href = 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css' rel = 'stylesheet'>
                                <script src = 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.bundle.min.js'></script>
                            </head>

                            <body>

                            <section>
                                    <div class='principal'>
                                        <div class='logo'>
                                            <img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'>
                                        </div>

                                        <div class='titulo'>
                                            <h2 class='text-center font-weight-bold'>Reporte de voluntario</h2>
                                        </div>

                                        <div class='fecha'>
                                            <p>Versión: <span>01</span></p>
                                            <p>Aprobado: <span>Gerencia Gral</span></p>
                                            <p>Fecha: <span>{date}</span></p>
                                        </div>
                                    </div> 


                                <div class='datosPersonales'>
                                        <h5>Datos personales</h5>

                                        <div class='datos'> 
                                          <div>
                                            <img src= 'https://almacenamientotesis.blob.core.windows.net/publicuploads/{employee.VolunteerAvatar}'>
                                           </div>

                                            <div class='dataP' style='margin-left: 9.5%;'>
                                                <p>Nª de empleado: <span>{employee.ID}</span></p>
                                                <p>Empleado: <span>{employee.Users.Persons.FirstName} {employee.Users.Persons.LastName}</span></p>
                                                <p>Documento: <span>{employee.Users.UserDni}</span></p>
                                            </div>
                
                                              <div class='dataP' style='margin-left: 15%;'>
                                                <p>Genero: <span>{employee.Users.Persons.Gender}</span></p>
                                                <p>Fecha de nacimiento: <span>{birthdate}</span></p>
                                              </div>
                                        </div>
                                    </div>


                                       <div class='datosPersonales'>
                                                <h5 style='margin-bottom: 2%;'>Información de contacto </h5>

                                                <div class='datos'> 
                                                            <div class='data'>
                                                                <p>Correo electronico: <span>{employee.Users.Persons.Email}</span></p>
                                                                <p>Telefono de contacto: <span>{employee.Users.Persons.Phone}</span></p>
                                                            </div>

                                                            <div class='data' style='margin-right: 2%;'>
                                                                <p class='text-left'>Ciudad de residencia: <span>{employee.Users.Persons.LocationName}</span></p>
                                                            </div>

                                                            <div class='data'>
                                                                <p class='text-left'>Domicilio: <span>{employee.Users.Persons.Address}</span></p>
                                                            </div>
                                                </div>
                                        </div>

");
            if (employee.Users.Volunteers.VolunteersSkills.Count != 0)
            {
                sb.Append($@"
                         <div class='datosPersonales'>
                                       <h5 style='margin-bottom: 2%;'>Preferencias</h5> ");

                foreach (var item in employee.Users.Volunteers.VolunteersSkills)
                {
                    foreach (var item2 in item.VolunteersSkillsFormationEstates)
                    {

                    sb.Append($@"
                                       <p> - <span>{item.Skills.SkillName}</span></p> 
                                ");
                    }
                }
            }

            sb.Append(@$"
                         </div> ");

            sb.Append(@$"

                                        <div class='datosPersonales'>
                                                     <h5 style='margin-bottom: 2%;'>Información de trabajo </h5>
                                        <div class='datos'> 
                                                                    <p>Cargo: <span>{employee.Users.Roles.RoleName}</span></p>

                                                                     <p class='text-left' style='margin-left: 25.5%;'>Dirección: <span>{employee.Users.Estates.LocationAddress.Address} {employee.Users.Estates.LocationAddress.NumberAddress} ({employee.Users.Estates.EstateTypes})</span></p>


                                                                    <p style='margin-left: 21%;'>Ciudad: <span>{employee.Users.Estates.Locations.LocationCityName}</span></p>
                                                     </div>
                                            
                                                  <p>Horarios: </p>
                                        ");

            foreach (var emp in employee.Users.Estates.EstatesTimes)
            {
                sb.Append($@"
                                                   <p class='span'>{emp.Times.Schedules.ScheduleDate} {emp.Times.StartTime} - {emp.Times.EndTime}</p>
                                            ");

            }

            sb.Append($@"

                                                 </div>
                                ");

            sb.Append($@"
                                          </div>
                                            <div class='datosPersonales'>
                                                <h5 style='margin-bottom: 2%;'>Información de acceso </h5>
                                        <div class='datos'> 
                         ");


            sb.Append($@"
                                                        <p class='text-left' style='margin-left: 12.5%;'>Disponibilidad ante emergencias: <span>{statusEmer}</span></p>


                           ");

            sb.Append($@"
                                                        <p style='margin-left: 9%;''>Acceso al sistema: <span>{status}<span></p>


                                                </div>
                                            </div>
                                         </section>
                                        </body>
                                    </html>");
            return sb.ToString();
        }
    }
}