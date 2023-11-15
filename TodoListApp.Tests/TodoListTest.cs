using Moq;
using TodoApp.Core;

namespace TodoListApp.Tests;

public class TodoListTest
{
    [Fact]
    public void CanAddTodosShouldReturnTrue()
    {
        // Arrange
        var title = "title";
        var description = "description";

        var mockTodoListStorage = new Mock<ITodoListStorage>();
        mockTodoListStorage.Setup(x => x.Save(It.IsAny<TodoItem>()));

        var sut = new TodoList(mockTodoListStorage.Object);
        // Act
        var actual = sut.Create(title, description);

        // Assert
        Assert.NotEqual(Guid.Empty, actual.Id);
        Assert.Equal(title, actual.Title);
        Assert.Equal(description, actual.Description);
        Assert.False(actual.IsComplete);
    }

    [Fact]
    public void WhenAddingTodoThenAddItToStorage()
    {
        // Arrange
        var title = "title";
        var description = "description";

        var mockTodoListStorage = new Mock<ITodoListStorage>();
        mockTodoListStorage.Setup(x => x.Save(It.IsAny<TodoItem>()));
        var sut = new TodoList(mockTodoListStorage.Object);

        // Act
        var actual = sut.Create(title, description);

        // Assert
        mockTodoListStorage.Verify(x => x.Save(actual), Times.Once);
    }

    [Fact]
    public void CanViewAllTodosShouldReturnSingle()
    {
        // Arrange
        var mockTodoListStorage = new Mock<ITodoListStorage>();
        mockTodoListStorage.Setup(x => x.Load())
            .Returns(
                new List<TodoItem>
                {
                    new TodoItem(),
                }
            );

        var sut = new TodoList(mockTodoListStorage.Object);

        // Act
        var actual = sut.GetAllTodos();
        // Assert
        Assert.Single(actual);
        mockTodoListStorage.Verify(x => x.Load(), Times.Once);
    }

    [Fact]
    public void CanViewAllCompletedTodos()
    {
        // Arrange
        var mockTodoListStorage = new Mock<ITodoListStorage>();
        mockTodoListStorage.Setup(x => x.Load()).Returns(
            new List<TodoItem>()
            {
                new TodoItem(){ IsComplete = true },
                new TodoItem() { IsComplete = true },
                new TodoItem() { IsComplete = false }
            });

        var sut = new TodoList(mockTodoListStorage.Object);

        // Act
        var actual = sut.GetCompletedTodos();

        // Assert
        Assert.Equal(2, actual.Count());

        Assert.True(actual.All(x => x.IsComplete));
    }

    [Fact]
    public void CanViewAllNotCompletedTodos()
    {
        // Arrange
        var mockTodoListStorage = new Mock<ITodoListStorage>();
        mockTodoListStorage.Setup(x => x.Load()).Returns(
            new List<TodoItem>()
            {
                new TodoItem(){ IsComplete = true },
                new TodoItem() { IsComplete = true },
                new TodoItem() { IsComplete = false }
            });

        var sut = new TodoList(mockTodoListStorage.Object);

        // Act
        var actual = sut.GetNotCompletedTodos();

        // Assert
        Assert.Equal(1, actual.Count());

        Assert.False(actual.All(x => x.IsComplete));
        
    }

    [Fact]
    public void CanMarkTodoAsCompleteShouldReturnTrue()
    {
        // Arrange
        var mockTodoStorage = new Mock<ITodoListStorage>();
        var sut = new TodoList(mockTodoStorage.Object);
        sut.TodoItems = new List<TodoItem>() { new TodoItem() };
        var todo = new TodoItem() { IsComplete = false };

        // Act
        sut.ChangeStatus(sut.TodoItems.First());
        sut.ChangeStatus(todo);

        // Assert
        Assert.True(sut.TodoItems.First().IsComplete);
        Assert.True(todo.IsComplete);
    }

    [Fact]
    public void CanDeleteTodoShouldReturnTrue()
    {
        // Arrange
        var mockTodoStorage = new Mock<ITodoListStorage>();
        var sut = new TodoList(mockTodoStorage.Object);
        var todo = new TodoItem();
        sut.TodoItems = new() { todo };

        // Act
        var actual = sut.Delete(todo);

        // Assert
        Assert.True(actual);

    }

    [Fact]
    public void CannotDeleteTodoIfTodoListIsNullShouldReturnFalse()
    {
        // Arrange
        var mockTodoStorage = new Mock<ITodoListStorage>();
        var sut = new TodoList(mockTodoStorage.Object);


        // Act
        var actual = sut.Delete(new TodoItem());


        // Assert
        Assert.False(actual);

    }

 
}
