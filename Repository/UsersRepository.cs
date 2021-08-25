﻿using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Login___Dto;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        private IMapper _mapper;
        public static Users authUser;

        public UsersRepository(CruzRojaContext cruzRojaContext, IMapper mapper)
            : base(cruzRojaContext)
        {
            _mapper = mapper;
        }

        public async Task<Users> GetUserEmployeeById(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                    .Include(a => a.Employees)
                    .Include(a => a.Persons)
                    .FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserVolunteerById(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                              .Include(a => a.Volunteers)
                              .Include(a => a.Persons)
                              .Include(a => a.Volunteers.VolunteersSkills)
                              .ThenInclude(a => a.Skills)
                              .FirstOrDefaultAsync();
        }

        public void DeletUser(Users user)
        {
            Delete(user);
        }


        //Funcion que sirve para poder recuperar la contraseña olvidada
        public void ForgotPassword(string email)
        {
            Users authUser = null;

            using (var db = new CruzRojaContext())
                authUser = db.Users
                               .Include(u => u.Persons)
                               .Where(u => u.Persons.Email == email).FirstOrDefault();

            //var account = _cruzRojaContext.Users.FirstOrDefault(i => i.Persons.Email == model.Email);

            if (authUser == null) return;

            authUser.ResetToken = randomTokenString();
            authUser.ResetTokenExpires = DateTime.Now.AddDays(1);

            Update(authUser);

            SaveAsync();

            sendPasswordResetEmail(authUser);
        }

        //Funcion para crear un token de manera random
        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }


        //Funcion que permite enviar un email al usuario que se olvido la contraseña.
        private void sendPasswordResetEmail(Users account)
        {
            string message;
            {
               var resetUrl = $"/account/reset-password?token={account.ResetToken}";
               message = $@"
                                <p>Sentimos que hayas tenido problemas para iniciar sesión en SYNAGIR. Podemos ayudar a recuperar tu cuenta.</p>
                                <p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                                 <a style=""color: white; text-align: center; display: block; background:red; text-decoration: none;  border-radius: 0.4rem; margin: 4rem auto; width: 15%; padding: 10px;
                                    "" href=""{resetUrl}"">Reset your Password</a>";

            }

            EmailRepository.Send
                (
                to: account.Persons.Email,
                subject: "Sign-up Verification API - Reset Password",
               html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }

        //Funcion para validar los campos: Token, contraseña nueva 
        public void  ResetPassword(string token, string password)
        {
           Users account = null;

           using (var db = new CruzRojaContext())
                account = db.Users
                               .Where(u => u.ResetToken == token
                               && u.ResetTokenExpires > DateTime.Now).FirstOrDefault();

            account.UserPassword = Encrypt.GetSHA256(password);
            account.PasswordReset = DateTime.Now;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            Update(account);

            SaveAsync();
        }

        public async Task<Users> ValidateUser(UserLoginDto user)
        {
            Users ret = new Users();

            string Pass = user.UserPassword;
            string ePass = Encrypt.GetSHA256(Pass);

            //se conecta a la base de datos para verificar las datos del usuario en cuestion
            await using (var db = new CruzRojaContext())
                authUser = db.Users.Include(u => u.Persons)
                               .Include(u => u.Roles)
                               .Include(u => u.Estates)
                               .ThenInclude(u => u.LocationAddress)
                               .Include(u => u.Estates.EstatesTimes)
                               .ThenInclude(u => u.Times)
                               .ThenInclude(u => u.Schedules)
                               .Include(u => u.Estates.Locations)
                               .Include(u => u.Volunteers)
                                .Where(u => u.UserDni == user.UserDni
                                   && u.UserPassword == ePass).FirstOrDefault();

            return authUser; //retornamos el valor de este objeto       
        }


        public UserEmployeeAuthDto ValidateUserE(Users user)
        {
            UserEmployeeAuthDto ret = new UserEmployeeAuthDto();

                ret = _mapper.Map<UserEmployeeAuthDto>(authUser); //si los datos son correctos se crea el objeto del usuario autentificado

            return ret; //retornamos el valor de este objeto          
        }

        public UserVolunteerAuthDto ValidateUserV(Users user)
        {
            UserVolunteerAuthDto ret = new UserVolunteerAuthDto();

                ret =  _mapper.Map<UserVolunteerAuthDto>(user); //si los datos son correctos se crea el objeto del usuario autentificado

            return ret; //retornamos el valor de este objeto          
        }
    }
 }
