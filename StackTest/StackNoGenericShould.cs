using FluentAssertions;
using System.Collections;

namespace StackTest
{
    public class StackNoGenericShould
    {
        [Fact]
        public void StoreDifferentTypeDataUsingPush()
        {
            // Arrange
            Stack data = new Stack();

            // Act
            data.Push(1);
            data.Push("Hello");
            data.Push(3.14);

            // Assert
            data.Should().NotBeNull()
                .And.BeOfType<Stack>();
        }
    }
}