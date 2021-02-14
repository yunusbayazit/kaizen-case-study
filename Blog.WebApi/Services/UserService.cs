using AutoMapper;
using Blog.WebApi.Entities;
using Blog.WebApi.Exceptions;
using Blog.WebApi.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.WebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        //IEnumerable<User> GetAll();
        User GetById(int id);
        bool Create(User user, string password);
        bool Update(User user, string password = null);
        bool Delete(int id);
    }

    public class UserService : IUserService
    {
        private ISqlClient _sqlClient;
        public UserService(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            SqlParameter[] param = {
                new SqlParameter("@Username", username)
            };

            var user = _sqlClient.ExecuteProcedureReturnData<User>("sp_UsersSelect", param).FirstOrDefault();
            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        //public IEnumerable<User> GetAll()
        //{
        //    return _context.Users;
        //}

        public User GetById(int id)
        {
            SqlParameter[] param = {
                new SqlParameter("@Id", id)
            };

            return _sqlClient.ExecuteProcedureReturnData<User>("sp_UsersSelectFindById", param).FirstOrDefault();
        }

        public bool Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            var findUser = _sqlClient.ExecuteProcedureReturnData<User>("sp_UsersSelect", new SqlParameter[] { new SqlParameter("@Username", user.Username) }).FirstOrDefault();

            if (findUser != null)
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var result = _sqlClient.ExecuteProcedureReturnString("sp_UsersInsert", new SqlParameter[] {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@PasswordSalt", user.PasswordSalt)
            });
            if (!string.IsNullOrEmpty(result))
                return true;

            return false;
        }

        public bool Update(User userParam, string password = null)
        {
            var findUser = _sqlClient.ExecuteProcedureReturnData<User>("sp_UsersSelect", new SqlParameter[] { new SqlParameter("@Username", userParam.Username) }).FirstOrDefault();

            if (findUser == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != findUser.Username)
            {
                // throw error if the new username is already taken
                if (findUser != null)
                    throw new AppException("Username " + userParam.Username + " is already taken");

                findUser.Username = userParam.Username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                findUser.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                findUser.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                findUser.PasswordHash = passwordHash;
                findUser.PasswordSalt = passwordSalt;
            }

            var result = _sqlClient.ExecuteProcedureReturnString("sp_UsersUpdate", new SqlParameter[] {
                new SqlParameter("@Id", findUser.Id),
                new SqlParameter("@Username", findUser.Username),
                new SqlParameter("@FirstName", findUser.FirstName),
                new SqlParameter("@LastName", findUser.LastName),
                new SqlParameter("@PasswordHash", findUser.PasswordHash),
                new SqlParameter("@PasswordSalt", findUser.PasswordSalt)
            });

            if (!string.IsNullOrEmpty(result))
                return true;

            return false;
        }

        public bool Delete(int id)
        {
            var result = _sqlClient.ExecuteProcedureReturnString("sp_UsersDelete", new SqlParameter[] {
                new SqlParameter("@Id", id),
            });

            if (!string.IsNullOrEmpty(result))
                return true;

            return false;
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
