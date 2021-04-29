using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentAssertions;
using ImageGram.API.Controllers;
using ImageGram.API.Interfaces;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ImageGram.Test
{
    public class UserControllerTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkStub = new();
        private readonly Mock<IHttpContextAccessor> http = new();
        private readonly Random randomNumber = new();

        [Fact]
        public async Task GetUserAsync_WIthUnexistingUser_ReturnsBadRequest()
        {
            //Given (Arange)
            unitOfWorkStub.Setup(unit => unit.AccountRepository.GetAccountByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Account)null);
            
            var controller = new UserController(unitOfWorkStub.Object, http.Object);
            
            //When (Act)
            var actualUser = await controller.GetUserAsync(randomNumber.Next(10));
            
            //Then (Assert)
            actualUser.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetUserAsync_WIthExistingUser_ReturnsOkObjectResult()
        {
            // Give (Arange)
            var expectedUser = CreateRandomAccount();

            unitOfWorkStub.Setup(unit => unit.AccountRepository.GetAccountByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedUser);
            
            var controller = new UserController(unitOfWorkStub.Object, http.Object);
            
            //When (Act)
            var actualUser = await controller.GetUserAsync(randomNumber.Next(10));
            
            //Then (Assert)
            actualUser.Result.Should().BeOfType<OkObjectResult>();
        }

        private Account CreateRandomAccount()
        {
            return new Account
            {
                AccountId = randomNumber.Next(10),
                Name = Guid.NewGuid().ToString(),
                Posts = new Collection<Post>(),
                Comments = new Collection<Comment>()
            };
        }
    }
}