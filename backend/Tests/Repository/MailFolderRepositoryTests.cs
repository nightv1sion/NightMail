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
    public class MailFolderRepositoryTests
    {
        private readonly IRepositoryManager _repository;
        private readonly RepositoryContext _context;
        private readonly Guid _userId;
        private readonly Guid _folderId;
        public MailFolderRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _userId = Guid.NewGuid();
            _folderId = Guid.NewGuid();
            _context = new RepositoryContext(contextOptions);
            SeedData(_context);
            _repository = new RepositoryManager(_context);
        }
        private void SeedData(RepositoryContext context)
        {
            var user = new User() { Id = _userId, FirstName = "Danila", LastName = "Uprivanov" };
            var folder1 = new Folder() {FolderId = Guid.NewGuid(), Name = "SomeFolder"};
            var mailForFolder1 = new Mail() {MailId = Guid.NewGuid(), Text = "SomeText", Subject = "SomeSubject"};

            var folder2 = new Folder() { FolderId = Guid.NewGuid(), Name = "SomeFolder" };
            var mail1ForFolder2 = new Mail { MailId = Guid.NewGuid(), Text = "SomeText", Subject = "SomeSubject" };
            var mail2ForFolder2 = new Mail { MailId = Guid.NewGuid(), Text = "SomeText", Subject = "SomeSubject" };

            context.Mails.AddRange(mailForFolder1, mail1ForFolder2, mail2ForFolder2);
            context.Folders.AddRange(folder1, folder2);

            context.MailFolders.AddRange(new MailFolder
            {
                FolderId = folder1.FolderId,
                MailId = mailForFolder1.MailId
            },
            new MailFolder
            {
                FolderId = folder2.FolderId,
                MailId = mail1ForFolder2.MailId
            },
            new MailFolder
            {
                FolderId = folder2.FolderId,
                MailId = mail2ForFolder2.MailId
            }
            );

            context.SaveChanges();
        }
        [Fact]
        public async Task MailFolderRepository_CreateMailFolder_CreatesMailFolder()
        {
            // Arrange
            var newMail = new Mail { MailId = Guid.NewGuid(), Text = "SomeText", Subject = "SomeSubject" };
            var newFolder = new Folder { FolderId = Guid.NewGuid() , Name = "SomeFolder"};
            var newMailFolder = new MailFolder() { FolderId = newFolder.FolderId, MailId = newMail.MailId };

            // Act
            _repository.MailFolder.CreateMailFolder(newMailFolder);
            await _repository.SaveAsync();
            var result = await _context.MailFolders.ToListAsync();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<MailFolder>>(result);
            Assert.Equal(4, result.Count);
        }
    }
}
