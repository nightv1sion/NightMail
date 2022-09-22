using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private Lazy<IAuthenticationService> _authentication;
        private Lazy<IUserService> _userService;
        public ServiceManager(UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IRepositoryManager repository)
        {
            _authentication = new Lazy<IAuthenticationService>(new AuthenticationService(userManager, mapper, configuration));
            _userService = new Lazy<IUserService>(new UserService(repository, mapper));
        }
        public IAuthenticationService AuthenticationService => _authentication.Value;
        public IUserService UserService => _userService.Value;
    }
}
