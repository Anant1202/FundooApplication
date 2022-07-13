using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserEntity Register(UserRegistrationModel userRegistrationModel)
        {
            try 
	        {	        
                return userRL.Register(userRegistrationModel);
            }
	        catch (Exception ex)
	        {
                throw ex;
	        }
        }
    }
}

