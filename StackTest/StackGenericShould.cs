using FluentAssertions;

namespace StackTest
{
    public class StackGenericShould
    {
        [Fact]
        public void CountNumberOfElementWithCount()
        {
            // Arrange
            Stack<string> strings = new Stack<string>();

            // Act
            strings.Push("A");
            strings.Push("B");
            strings.Push("C");
            strings.Push("D");
            int count = strings.Count();

            // Assert
            count.Should().Be(4);
        }

        [Fact]
        public void StoreSameTypeDataUsingPush()
        {
            // Arrange
            Stack<string> strings = new Stack<string>();

            // Act
            strings.Push("!");
            strings.Push("Mundo");
            strings.Push("Hola");

            // Assert
            strings.Should().NotBeEmpty()
                .And.HaveCount(3)
                .And.ContainInOrder("Hola", "Mundo", "!");
        }

        [Fact]
        public void RemoveAndReturnBeginningDataUsingPop()
        {
            // Arrange
            Stack<string> strings = new Stack<string>();

            // Act
            strings.Push("4");
            strings.Push("B");
            string result = strings.Pop();

            // Assert
            result.Should().NotBeEmpty()
                .And.NotBe("A")
                .And.Be("B");
            strings.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainSingle("A");
        }

        [Fact]
        public void ReturnBeginningDataUsingPeekWithoutRemoveIt()
        {
            // Arrange
            Stack<string> strings = new Stack<string>();

            // Act
            strings.Push("A");
            strings.Push("B");
            string result = strings.Peek();

            // Assert
            result.Should().NotBeEmpty()
                .And.Be("B");
            strings.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainInOrder("B", "A");
        }
    }
}