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
        private Lazy<IUserProfileImageService> _userImageService;
        public ServiceManager(UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IRepositoryManager repository)
        {
            _authentication = new Lazy<IAuthenticationService>(new AuthenticationService(userManager, mapper, configuration, repository));
            _userService = new Lazy<IUserService>(new UserService(repository, mapper));
            _userImageService = new Lazy<IUserProfileImageService>(new UserProfileImageService(repository));
        }
        public IAuthenticationService AuthenticationService => _authentication.Value;
        public IUserService UserService => _userService.Value;
        public IUserProfileImageService UserProfileImageService => _userImageService.Value;
    }
}
