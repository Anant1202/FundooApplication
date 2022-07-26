using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL:IUserRL 
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public static string SecretKey = "abgiivzxdrtcvbnm";
        public UserRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
           
        }
        public UserEntity Register(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.EmailID = userRegistrationModel.EmailID;
                userEntity.Password = EncryptPasswordBase64( userRegistrationModel.Password);
                fundooContext.User.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if(result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GenerateSecurityToken(string email,long id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public static string EncryptPasswordBase64(string Password)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Password);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string DecryptPasswordBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public string Login(LoginModel loginModel)
        {
            try
            {
                var data = fundooContext.User.SingleOrDefault(x => x.EmailID == loginModel.EmailID );
                string result = DecryptPasswordBase64(data.Password);
                if (data != null && result==loginModel.Password)
                {
                    var token = GenerateSecurityToken(data.EmailID, data.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgetPassword(string EmailID)
        {
            var emailcheck= fundooContext.User.SingleOrDefault(x=>x.EmailID== EmailID);
            if(emailcheck!=null)
            {
                var token=GenerateSecurityToken(emailcheck.EmailID, emailcheck.UserId);
                new MSMQ().sendData2Queue(token);
                return token;
            }
            else
            {
                return null;
            }
        }
        public string ResetPassword(ResetModel resetModel)
        {
            try
            {
                if(resetModel.Password.Equals(resetModel.ConfirmPassword))
                {
                    UserEntity user = fundooContext.User.SingleOrDefault((x => x.EmailID == resetModel.EmailId));
                    user.Password= resetModel.ConfirmPassword;
                    fundooContext.SaveChanges();
                    return "Reset Success";
                }
                else
                {
                    return "Reset Failed";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
