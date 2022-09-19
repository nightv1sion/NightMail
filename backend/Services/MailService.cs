using AutoMapper;
using Contracts;
using Entities.ExceptionModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repository;
using Services.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MailService : IMailService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public MailService(IRepositoryManager repository, UserManager<User> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MailDTO>> GetMailsAsync(string userEmail, bool trackChanges)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                throw new UserDoesNotExist(userEmail);
            
            var mails = await _repository.Mail.GetAllMailsForUserAsync(user, trackChanges);
            var mailsDto = _mapper.Map<IEnumerable<MailDTO>>(mails);

            return mailsDto;
        }
    }
}
