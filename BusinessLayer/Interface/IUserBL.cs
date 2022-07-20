using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Register(UserRegistrationModel userRegistrationModel);
        public string Login(LoginModel loginModel);
        public string ForgetPassword(string EmailID);
        public string ResetPassword(ResetModel resetModel);
    }
}
