using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        public ServiceManager(UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            _authentication = new Lazy<IAuthenticationService>(new AuthenticationService(userManager, mapper, configuration));
        }

        public IAuthenticationService AuthenticationService => _authentication.Value;
    }
}
