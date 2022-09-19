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
        private Lazy<IMailService> _mail;
        public ServiceManager(UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IRepositoryManager repository)
        {
            _authentication = new Lazy<IAuthenticationService>(new AuthenticationService(userManager, mapper, configuration));
            _mail = new Lazy<IMailService>(new MailService(repository, userManager, mapper));
        }

        public IAuthenticationService AuthenticationService => _authentication.Value;
        public IMailService MailService => _mail.Value;
    }
}
