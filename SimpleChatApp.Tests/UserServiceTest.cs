using AutoMapper;
using Moq;
using SimpleChatApp.BLL.Services;
using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.DAL.Repositories.Contracts;
using SimpleChatApp.Data.DTOs.UserDTOs;
using SimpleChatApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SimpleChatApp.Data.Responses.Enums;

namespace SimpleChatApp.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _unitOfWorkMock.SetupGet(u => u.userRepository).Returns(_userRepositoryMock.Object);

            _userService = new UserService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNotFound_WhenNoEntitiesExist()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<User>());

            // Act
            var result = await _userService.GetAsync();

            // Assert
            Assert.False(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("No entities found.", result.Description);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnEntities_WhenEntitiesExist()
        {
            // Arrange
            var users = new List<User> { new User { Id = Guid.NewGuid(), UserName = "Test User" } };
            var userDtos = new List<GetUserDto> { new GetUserDto { Id = Guid.NewGuid(), UserName = "Test User" } };

            _userRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(users);
            _mapperMock.Setup(mapper => mapper.Map<List<GetUserDto>>(users)).Returns(userDtos);

            // Act
            var result = await _userService.GetAsync();

            // Assert
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal(userDtos, result.Data);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBadRequest_WhenIdIsEmpty()
        {
            // Act
            var result = await _userService.GetByIdAsync(Guid.Empty);

            // Assert
            Assert.False(result.StatusCode.Equals(StatusCode.Ok));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNotFound_WhenEntityDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetByIdAsync(id);

            // Assert
            Assert.False(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal($"Entity with id {id} not found.", result.Description);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new User { Id = id, UserName = "Test User" };
            var userDto = new GetUserDto { Id = id, UserName = "Test User" };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<GetUserDto>(user)).Returns(userDto);

            // Act
            var result = await _userService.GetByIdAsync(id);

            // Assert
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal(userDto, result.Data);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnBadRequest_WhenEntityIsNull()
        {
            // Act
            var result = await _userService.InsertAsync(null);

            // Assert
            Assert.False(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Entity is empty.", result.Description);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnOk_WhenEntityIsAdded()
        {
            // Arrange
            var addUserDto = new AddUserDto { UserName = "Test User" };
            var user = new User { Id = Guid.NewGuid(), UserName = "Test User" };

            _mapperMock.Setup(mapper => mapper.Map<User>(addUserDto)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.InsertAsync(user)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.InsertAsync(addUserDto);

            // Assert
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Operation completed successfully.", result.Description);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnBadRequest_WhenEntityIsNull()
        {
            // Act
            var result = await _userService.UpdateAsync(null);

            // Assert
            Assert.False(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Entity is empty.", result.Description);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnOk_WhenEntityIsUpdated()
        {
            // Arrange
            var updateUserDto = new UpdateUserDto { Id = Guid.NewGuid(), UserName = "Updated User" };
            var user = new User { Id = updateUserDto.Id, UserName = "Updated User" };

            _mapperMock.Setup(mapper => mapper.Map<User>(updateUserDto)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(user)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.UpdateAsync(updateUserDto);

            // Assert
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Operation completed successfully.", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnBadRequest_WhenIdIsEmpty()
        {
            // Act
            var result = await _userService.DeleteAsync(Guid.Empty);

            // Assert
            Assert.False(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Id is empty.", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnOk_WhenEntityIsDeleted()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = "Test User" };

            _userRepositoryMock.Setup(repo => repo.InsertAsync(user)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(user.Id)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.DeleteAsync(user.Id);

            // Assert
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Operation completed successfully.", result.Description);
        }
    }
}
