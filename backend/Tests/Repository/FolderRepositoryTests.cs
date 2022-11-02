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
    public class FolderRepositoryTests
    {
        private readonly IRepositoryManager _repository;
        private readonly Guid _userId;
        private readonly Guid _folderId;
        public FolderRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _userId = Guid.NewGuid();
            _folderId = Guid.NewGuid();
            var context = new RepositoryContext(contextOptions);
            SeedData(context);
            _repository = new RepositoryManager(context);
        }
        private void SeedData(RepositoryContext context)
        {
            var user = new User() { Id = _userId, FirstName = "Danila", LastName = "Uprivanov" };
            context.Users.Add(user);
            context.Folders.AddRange(new Folder
            {
                FolderId = Guid.NewGuid(),
                Name = "SomeFolder1",
                UserId = user.Id
            },
            new Folder
            {
                FolderId = _folderId,
                Name = "SomeFolder2",
                UserId = user.Id
            },
            new Folder
            {
                FolderId = Guid.NewGuid(),
                Name = "SomeFolder3",
                UserId = user.Id
            }
            );
            context.SaveChanges();
        }
        [Fact]
        public async Task FolderRepository_GetFoldersAsync_ReturnsListOfFolders()
        {
            // Arrange 
            var user = new User { Id = _userId };

            // Act
            var result = await _repository.Folder.GetFoldersAsync(user, false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Folder>>(result);
            Assert.Equal(3, result.Count);
        }
        [Fact]
        public async Task FolderRepository_GetFolderAsync_ReturnsFolder()
        {
            // Arrange 
            var user = new User { Id = _userId };
            var expectedName = "SomeFolder2";

            // Act
            var result = await _repository.Folder.GetFolderAsync(user, _folderId,false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Folder>(result);
            Assert.Equal(expectedName, result.Name);
        }
        [Fact]
        public async Task FolderRepository_DeleteFolder_DeletesFolder()
        {
            // Arrange 
            var user = new User { Id = _userId };
            var folder = await _repository.Folder.GetFolderAsync(user, _folderId, true);
            // Act
            _repository.Folder.DeleteFolder(folder);
            await _repository.SaveAsync();
            var result = await _repository.Folder.GetFoldersAsync(user, true);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Folder>>(result);
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task FolderRepository_UpdateFolder_UpdatesFolder()
        {
            // Arrange 
            var user = new User { Id = _userId };
            var newName = "NewFolderName";
            var folder = await _repository.Folder.GetFolderAsync(user, _folderId, true);
            folder.Name = newName;
            // Act
            _repository.Folder.UpdateFolder(folder);
            await _repository.SaveAsync();
            var result = await _repository.Folder.GetFoldersAsync(user, true);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Folder>>(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(newName, result.FirstOrDefault(f => f.FolderId == _folderId).Name);
        }
        [Fact]
        public async Task FolderRepository_CreateFolder_CreatesFolder()
        {
            // Arrange 
            var user = new User { Id = _userId };
            var newFolderName = "NewFolderName";
            var newFolderId = Guid.NewGuid();
            var newFolder = new Folder() { FolderId = newFolderId, UserId = _userId, Name = newFolderName }; 
            // Act
            _repository.Folder.CreateFolder(newFolder);
            await _repository.SaveAsync();
            var result = await _repository.Folder.GetFoldersAsync(user, true);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Folder>>(result);
            Assert.Equal(4, result.Count);
            Assert.Equal(newFolderName, result.FirstOrDefault(f => f.FolderId == newFolderId).Name);
        }
    }
}
