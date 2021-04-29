using System;
using System.Collections;
using System.Collections.Generic;
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
    public class CommentControllerTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkStub = new();
        private readonly Mock<IHttpContextAccessor> http = new();
        private readonly Random randomNumber = new();

        [Fact]
        public async Task GetCommentAsync_WIthUnexistingComment_ReturnsBadRequest()
        {
            //Given (Arange)
            unitOfWorkStub.Setup(unit => unit.CommentRepository.GetCommentAsync(It.IsAny<int>()))
                .ReturnsAsync((Comment)null);
            
            var controller = new CommentController(unitOfWorkStub.Object, http.Object);
            
            //When (Act)
            var actualComment = await controller.GetCommentAsync(randomNumber.Next(10));
            
            //Then (Assert)
            actualComment.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetCommentAsync_WithExistingComment_ReturnsOkObjectResult()
        {
            // Give (Arange)
            var expectedComment = CreateRandomComment();

            unitOfWorkStub.Setup(unit => unit.CommentRepository
                .GetCommentAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedComment);

            var controller = new CommentController(unitOfWorkStub.Object, http.Object);

            // When (Act)
            var actualComment = await controller.GetCommentAsync(randomNumber.Next(10));

            // Then (Assert)
            actualComment.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetCommentsAsync_WithExistingComments_ReturnsAllComments()
        {
            // Given (Arange)
            var expectedComments = new Comment[] 
            { 
                CreateRandomComment(), 
                CreateRandomComment(), 
                CreateRandomComment() 
            };

            unitOfWorkStub.Setup(unit => unit.CommentRepository.GetCommentsAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedComments);

            var controller = new CommentController(unitOfWorkStub.Object, http.Object);

            // When (Act)
            var actualComments = await controller.GetCommnetsAsync(randomNumber.Next(10));

            // Then (Assert)
            actualComments.Should().BeEquivalentTo(
                expectedComments, 
                option => option.ComparingByMembers<Comment>());
        }
        
        private Comment CreateRandomComment()
        {
            return new Comment
            {
                CommentId = randomNumber.Next(10),
                Content = Guid.NewGuid().ToString(),
                CreateAt = DateTime.UtcNow,
                AccountId = randomNumber.Next(10),
                PostId = randomNumber.Next(10)
            };
        }
    }
}