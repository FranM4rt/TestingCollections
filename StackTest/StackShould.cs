using FluentAssertions;
using System.Collections;

namespace StackTest
{
    public class StackShould
    {

        /*
         * Metodos de creación de Stack<>
         */



        /*
         * Métodos de manipulación de datos
         */

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