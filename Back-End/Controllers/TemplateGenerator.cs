﻿using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace PDF_Generator.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(Employees employee)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string birthdate = employee.Users.Persons.Birthdate.ToString("dd/MM/yyyy");

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
                                            <h2 class='text-center font-weight-bold'>Reporte de empleado</h2>
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
                                            <img src= 'https://blogger.googleusercontent.com/img/a/AVvXsEhUOA8HwyYFBnh7VnZxQwveTFGUwt6_dXazOIPRBmZuKrbMiPMC_cImvxRqYZepbi2npcWouLyiXG9IVarImm1NlgL8OX6PTg9qd8KmGjryq_ckdZJq3MmJudBFp6iB-drHF36baZa8UDMZJGgkusq90G79JNBdAAwvBfh0xhfKRchan_L30yQJZ70i'>
                                           </div>

                                            <div class='dataP'>
                                                <p>Nª de empleado: <span>{employee.EmployeeID}</span></p>
                                                <p>Empleado: <span>{employee.Users.Persons.FirstName} {employee.Users.Persons.LastName}</span></p>
                                                <p>Documento: <span>{employee.Users.UserDni}</span></p>
                                            </div>
                
                                              <div class='dataP'>
                                                <p>Genero: <span>{employee.Users.Persons.Gender}</span></p>
                                                <p>Fecha de nacimiento: <span>{birthdate}</span></p>
                                              </div>
                                        </div>
                                    </div>


                                       <div class='datosPersonales'>
                                                <h5> Información de contacto </h5>

                                                <div class='datos'>
                                                    <div class='data' style='width: 18%'>
                                                        <p>Correo electronico: <span>{employee.Users.Persons.Email}</span></p>
                                                        <p>Telefono de contacto: <span>{employee.Users.Persons.Phone}</span></p>
                                                    </div>

                                                    <div class='data'>
                                                        <p class='text-left' style='margin-left: 3%;'>Ciudad de residencia: <span>{employee.Users.Persons.LocationName}</span></p>
                                                    </div>

                                                    <div class='data' style='width: 18%'>
                                                        <p class='text-left'>Domicilio: <span>{employee.Users.Persons.Address}</span></p>
                                                    </div>
                                                </div>
                                        </div>


                                        <div class='datosPersonales'>
                                                     <h5> Información de trabajo </h5>
                                                    <div class='datos'>
                                                               <div class='data'>
                                                                    <p>Cargo: <span>{employee.Users.Roles.RoleName}</span></p>
                                                                </div>

                                                                <div class='data'>
                                                                     <p>Dirección de tabajo y tipo: <span>{employee.Users.Estates.LocationAddress.Address} {employee.Users.Estates.LocationAddress.NumberAddress} - {employee.Users.Estates.EstateTypes}</span></p>
                                                                </div>


                                                                <div class='data'>
                                                                    <p>Ciudad: <span>{employee.Users.Estates.Locations.LocationCityName}</span></p>
                                                                </div>
                                                     </div>
                                              </div>
                                            
                                            <div class='border span'>
                                                  <p class='span2'>Horarios: </p>
                                        ");

                        foreach (var emp in employee.Users.Estates.EstatesTimes)
                        {
                            sb.Append($@"
                                                         <p class='span span2'>{emp.Times.Schedules.ScheduleDate} {emp.Times.StartTime} - {emp.Times.EndTime}</p>
                                                </div>
                                            ");

                        }

                        sb.Append($@"
                                          </div>
                                            <div class='datosPersonales'>
                                                <h5> Información de acceso </h5>
                                                <div class='datos'>

                                                    <div class='data'>
                                                        <p>Disponibilidad ante emergencias: <span>{employee.Users.Persons.Status}</span></p>
                                                    </div>

                                                    <div class='data'>
                                                        <p>Acceso al sistema: <span>{employee.Users.UserAvailability}<span></p>
                                                    </div>

                                                    <div class='data'>
                                                        <p>Fecha de alta: <span>{employee.Users.Employees.EmployeeCreatedate}</span></p>
                                                    </div>

                                                </div>
                                            </div>
                                         </section>
                                        </body>
                                    </html>");
                        return sb.ToString();
        }
    }
}