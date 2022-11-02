using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Repository
{
    public class MailRepositoryTests
    {
        private readonly IRepositoryManager _repository;
        private readonly Guid _userReceiverId;
        private readonly Guid _userSenderId;
        private readonly Guid _folderId;
        private readonly Guid _mailId;
        public MailRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _userReceiverId = Guid.NewGuid();
            _userSenderId = Guid.NewGuid();
            _folderId = Guid.NewGuid();
            _mailId = Guid.NewGuid();
            var context = new RepositoryContext(contextOptions);
            SeedData(context);
            _repository = new RepositoryManager(context);
        }
        private void SeedData(RepositoryContext context)
        {
            var user1 = new User() { Id = _userReceiverId, FirstName = "Danila", LastName = "Uprivanov" };
            var user2 = new User() { Id = _userSenderId, FirstName = "Danila", LastName = "Uprivanov" };
            var folder = new Folder
            {
                FolderId = _folderId,
                Name = "SomeFolder1",
                UserId = user1.Id
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Folders.Add(folder);
            var mailFromUser2ForUser1_1 = new Mail
            {
                MailId = _mailId,
                Subject = "SomeSubject1",
                Text = "SomeText1",
                ReceiverId = user1.Id,
                SenderId = user2.Id,
            };

            var mailFromUser2ForUser1_2 = new Mail
            {
                MailId = Guid.NewGuid(),
                Subject = "SomeSubject2",
                Text = "SomeText2",
                ReceiverId = user1.Id,
                SenderId = user2.Id
            };

            var mailFromUser1ForUser2_1 = new Mail
            {
                MailId = Guid.NewGuid(),
                Subject = "SomeSubject3",
                Text = "SomeText3",
                ReceiverId = user2.Id,
                SenderId = user1.Id
            };

            var mailFromUser1ForUser2_2 = new Mail
            {
                MailId = Guid.NewGuid(),
                Subject = "SomeSubject4",
                Text = "SomeText4",
                ReceiverId = user2.Id,
                SenderId = user1.Id
            };

            var mailFromUser1ForUser2_3 = new Mail
            {
                MailId = Guid.NewGuid(),
                Subject = "SomeSubject5",
                Text = "SomeText5",
                ReceiverId = user2.Id,
                SenderId = user1.Id
            };

            context.Mails.AddRange(mailFromUser2ForUser1_1, mailFromUser2ForUser1_2, mailFromUser1ForUser2_1,
                mailFromUser1ForUser2_2, mailFromUser1ForUser2_3);

            context.SaveChanges();
        }

        [Fact]
        public async Task MailRepository_GetIncomingMailsForUserAsync_ReturnsListOfMails()
        {
            // Arrange
            var user = new User() { Id = _userReceiverId };

            // Act
            var result = await _repository.Mail.GetIncomingMailsForUserAsync(user, false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Mail>>(result);
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task MailRepository_GetOutgoingMailsForUserAsync_ReturnsListOfMails()
        {
            // Arrange
            var user = new User() { Id = _userReceiverId };

            // Act
            var result = await _repository.Mail.GetOutgoingMailsForUserAsync(user, false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Mail>>(result);
            Assert.Equal(3, result.Count);
        }
        [Fact]
        public async Task MailRepository_GetMailForUserAsync_ReturnsListOfMails()
        {
            // Arrange
            var user = new User() { Id = _userReceiverId };
            var expectedSubject = "SomeSubject1";
            // Act
            var result = await _repository.Mail.GetMailForUserAsync(user,_mailId, false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Mail>(result);
            Assert.Equal(expectedSubject, result.Subject);
        }
    }
}
