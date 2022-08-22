using AutoMapper;
using EModel.Contracts.Repository;
using EModel.Core.Handlers;
using EModel.Entities.DTOs;
using EModel.Entities.Entities;
using EModel.Entities.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EModel.Core.V1
{
    public class UserCore
    {
        private readonly IUserRepository _context;
        private readonly ErrorHandler<User> _errorHandler;
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;
        private readonly StripeCore _stripe;


        public UserCore(IUserRepository context, ILogger<User> logger, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _errorHandler = new ErrorHandler<User>(logger);
            _stripe = new();
        }

        public async Task<ResponseService<List<User>>> GetAll()
        {
            try
            {
                var response = await _context.GetAllAsync();
                return new ResponseService<List<User>>(false, response == null ? "Record was not found" : "User list", HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "CreateUser", new List<User>());
            }
        }

        public async Task<ResponseService<User>> CreateUser(UserCreateDto userCreate)
        {
            try
            {
                User newUser = _mapper.Map<User>(userCreate);
                newUser.Created = DateTime.Now;
                newUser.Status = "Created";
                newUser.Token = _stripe.CreateCustomer($"{userCreate.FirstName} {userCreate.LastName}", userCreate.Email);
                var response = await _context.AddAsync(newUser);
                return new ResponseService<User>(false, response == null ? "Record was not found" : "User was created", HttpStatusCode.OK, response.Item1);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "CreateUser", new User());
            }
        }

        public async Task<ResponseService<User>> UpdatePassword(int userId, UserPasswordDto request)
        {
            try
            {
                User user = await _context.GetByIdAsync(userId);
                user.Password = request.Password;
                var response = await _context.UpdateAsync(user);
                return new ResponseService<User>(false, "Password was updated", HttpStatusCode.OK, response.Item1);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "UpdatePassword", new User());
            }
        }

        internal async Task<string> GetCustomerTokenById(int userId)
        {
            var result = await _context.GetByIdAsync(userId);
            return result.Token;
        }

        public async Task<Tuple<int, bool>> AuthUser(string username, string password)
        {
            var users = await _context.GetByFilterAsync(u => u.Username.Equals(username));
            if (users.Count == 0) { return new(-1, false); }
            string passwordAttempt = EncryptCore.Encrypt_SHA256(username, password);
            if (passwordAttempt == users.FirstOrDefault().Password)
            {
                users.FirstOrDefault().LastLogin = DateTime.Now;
                await _context.UpdateAsync(users.FirstOrDefault());
                return new(users.FirstOrDefault().Id, true);
            }
            else { return new(-1, false); }
        }

        public async Task<bool> SetPassword(string username, string password)
        {
            var users = await _context.GetByFilterAsync(u => u.Username.Equals(username));
            if (users.Count == 0) { return false; }
            users.FirstOrDefault().Password = EncryptCore.Encrypt_SHA256(username, password);
            users.FirstOrDefault().Status = "Active";
            await _context.UpdateAsync(users.FirstOrDefault());
            return true;
        }

        public async Task<bool> ChangePassword(string username, string password, string newPassword)
        {
            var users = await _context.GetByFilterAsync(u => u.Username.Equals(username));
            if (users.Count == 0) { return false; }
            string passwordAttempt = EncryptCore.Encrypt_SHA256(username, password);
            if (passwordAttempt != users.FirstOrDefault().Password) { return false; }
            users.FirstOrDefault().Password = EncryptCore.Encrypt_SHA256(username, newPassword);
            await _context.UpdateAsync(users.FirstOrDefault());
            return true;
        }
    }
}
