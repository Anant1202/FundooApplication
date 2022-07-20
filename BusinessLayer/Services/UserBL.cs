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
        public string Login(LoginModel loginModel)
        {
            try
            {
                return userRL.Login(loginModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string ForgetPassword(string EmailID)
        {
            try
            {
                return userRL.ForgetPassword(EmailID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string ResetPassword(ResetModel resetModel)
        {
            try
            {
                return userRL.ResetPassword(resetModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

