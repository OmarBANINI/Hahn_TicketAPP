using Moq;
using TicketApp.Controllers;
using TicketApp.Services;
using TicketApp.Models;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Exceptions;
using Humanizer;

public class TicketsControllerTests
{
    private readonly Mock<ITicketService> _mockTicketService;
    private readonly TicketsController _controller;

    public TicketsControllerTests()
    {
        _mockTicketService = new Mock<ITicketService>();
        _controller = new TicketsController(_mockTicketService.Object);
    }

    [Fact]
    public async Task GetTickets_ReturnsOkResult_WithListOfTickets()
    {
        // Arrange
        var tickets = new List<Ticket>
        {
            new Ticket { TicketID = 1, Description = "Test Ticket 1", Status = "Open" },
            new Ticket { TicketID = 2, Description = "Test Ticket 2", Status = "Closed" }
        };

        _mockTicketService.Setup(service => service.GetTickets(It.IsAny<int>(), It.IsAny<int>()))
                          .ReturnsAsync(tickets);

        // Act
        var result = await _controller.GetTickets();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTickets = Assert.IsAssignableFrom<IEnumerable<Ticket>>(okResult.Value);
        Assert.Equal(2, returnedTickets.Count());
    }

    [Fact]
    public async Task GetTicket_ReturnsNotFoundResult_WhenTicketDoesNotExist()
    {
        // Arrange
        _mockTicketService.Setup(service => service.GetTicket(It.IsAny<int>()))
                          .ReturnsAsync((Ticket)null);

        // Act
        var result = await _controller.GetTicket(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostTicket_ReturnsCreatedAtAction_WhenTicketIsCreated()
    {
        // Arrange
        var newTicket = new Ticket
        {
            TicketID = 3,
            Description = "New Ticket",
            Status = "Open"
        };

        _mockTicketService.Setup(service => service.CreateTicket(newTicket))
                          .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PostTicket(newTicket);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(newTicket, createdResult.Value);
    }


    [Fact]
    public async Task PutTicket_ReturnsNoContent_WhenTicketIsUpdated()
    {
        // Arrange
        var updatedTicket = new Ticket { TicketID = 1, Description = "Updated Ticket", Status = "Closed" };

        _mockTicketService.Setup(service => service.UpdateTicket(updatedTicket))
                          .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PutTicket(1, updatedTicket);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteTicket_ReturnsNoContent_WhenTicketIsDeleted()
    {
        // Arrange
        _mockTicketService.Setup(service => service.DeleteTicket(It.IsAny<int>()))
                          .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteTicket(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetTickets_ReturnsBadRequest_WhenAppExceptionIsThrown()
    {
        // Arrange
        _mockTicketService.Setup(service => service.GetTickets(It.IsAny<int>(), It.IsAny<int>()))
                          .ThrowsAsync(new AppException("Error occurred"));

        // Act
        var result = await _controller.GetTickets();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Error occurred", badRequestResult.Value);
    }

    [Fact]
    public async Task PostTicket_ReturnsBadRequest_WhenAppExceptionIsThrown()
    {
        // Arrange
        var newTicket = new Ticket
        {
            TicketID = 3,
            Description = "New Ticket",
            Status = "Open"
        };

        // Setup the mock to throw an AppException when CreateTicket is called
        _mockTicketService.Setup(service => service.CreateTicket(newTicket))
                          .ThrowsAsync(new AppException("Error occurred"));

        // Act
        var result = await _controller.PostTicket(newTicket);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Error occurred", badRequestResult.Value);
    }

}
