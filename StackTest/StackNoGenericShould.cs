using FluentAssertions;
using System.Collections;

namespace StackTest
{
    public class StackNoGenericShould
    {
        /*
         * Metodos de creación de Stack<>
         */

        [Fact]
        public void CreateEmptyAndNonSpecificDataStack()
        {
            // Arrange
            Stack data;
            int count;

            // Act
            data = new Stack();
            count = data.Count;

            // Assert
            data.Should().NotBeNull()
                .And.BeOfType<Stack>();
            data.Count.Should().Be(0);
        }

        [Fact]
        public void CreateStackFromAnyICollection()
        {
            // Arrange
            Stack data;
            ICollection collection = new ArrayList() { 1, 2, 3 };

            // Act
            data = new Stack(collection);

            // Assert
            data.Should().NotBeNull()
                .And.BeOfType<Stack>()
                .As<Stack>().Count.Should().Be(collection.Count);
        }

        /*
         * Métodos de manipulación de datos
         */

        [Fact]
        public void StoreDifferentTypeDataUsingPush()
        {
            // Arrange
            Stack data = new Stack();

            // Act
            data.Push("Hello");
            data.Push(true);

            // Assert
            data.Count.Should().NotBe(0);
            data.Contains("Hello").Should().BeTrue();
            data.Contains(true).Should().BeTrue();
        }

        [Fact]
        public void RemoveAndReturnBeginningDataUsingPop()
        {
            // Arrange
            Stack data = new Stack();
            
            // Act
            data.Push("Texto");
            data.Push(432);
            var result = data.Pop();

            // Assert
            data.Count.Should().NotBe(0);
            data.Contains("Texto").Should().BeTrue();
            data.Contains(432).Should().BeFalse();
            result.Should().Be(432);
        }

        /*
         * Excepciones
         */

        [Fact]
        public void ThrowInvalidOperationExceptionnWhenPeekEmptyStack()
        {
            // Arrange
            Stack pila = new Stack();

            // Act
            Action act = () => pila.Peek();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Stack empty.");
        }
    }
}