using FluentAssertions;

namespace StackTest
{
    public class StackGenericShould
    {

        /*
         * Metodos constructores de creaci�n de Stack<>
         */

        [Fact]
        public void CreateEmptyAndSpecificDataStack()
        {
            // Arrange
            Stack<string> strings;

            // Act
            strings = new Stack<string>();

            // Assert
            strings.Should().NotBeNull()
                .And.BeEmpty()
                .And.BeOfType<Stack<string>>();
        }

        [Fact]
        public void CreateEmptyStackOfSpecificSizeAndResizeItAutomatically()
        {
            // Arrange
            Stack<string> strings = GenerateStack(200);

            // Act
            var initialCapacity = GetCapacity(strings);
            strings.Push("S200");
            var finalCapacity = GetCapacity(strings);

            // Assert
            strings.Should().NotBeNullOrEmpty()
                .And.HaveCount(201)
                .And.BeOfType<Stack<string>>();
            finalCapacity.Should().BeGreaterThan(initialCapacity);
            finalCapacity.Should().Be(initialCapacity * 2);
        }

        [Fact]
        public void CreateStackFromAnyIEnumerable()
        {
            // Arrange
            Stack<string> strings;
            IEnumerable<string> enumerable = new List<string>() { "A", "B", "C" };

            // Act
            strings = new Stack<string>(enumerable);

            // Assert
            strings.Should().NotBeEmpty()
                .And.HaveCount(3)
                .And.ContainInOrder("C", "B", "A");
        }

        /*
         * M�todos de manipulaci�n de datos
         */
        // usando clear
        [Fact]
        public void ClearStackData()
        {
            // Arrange
            Stack<string> strings = GenerateStack(100);

            // Act
            strings.Clear();

            // Assert
            strings.Should().NotBeNull()
                .And.BeEmpty();
        }

        [Fact]
        public void CountNumberOfElementUsingCount()
        {
            // Arrange
            var strings = GenerateStack(100);

            // Act
            int count = strings.Count();

            // Assert
            count.Should().Be(100);
        }

        [Fact]
        public void TrimExcessStackCapacity()
        {
            // Arrange
            Stack<string> strings = GenerateStack(100);

            // Act
            var capacityBeforePush = GetCapacity(strings);
            strings.Push("S100");
            var capacityAfterPush = GetCapacity(strings);
            strings.TrimExcess();
            var capacityAfterTrim = GetCapacity(strings);

            // Assert
            capacityBeforePush.Should().Be(100);
            capacityAfterPush.Should().Be(200);
            capacityAfterTrim.Should().Be(101);
        }

        [Fact]
        public void StoreSameTypeDataInReverseOrderUsingPush()
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
            strings.Push("A");
            strings.Push("B");
            string result = strings.Pop();

            // Assert
            result.Should().NotBeEmpty()
                .And.NotBe("A")
                .And.Be("B");
            strings.Should().NotBeEmpty()
                .And.ContainMatch("A")
                .And.HaveCount(1);
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

        /*
         * Metodos heredados de la clase Object
        */

        /// <summary>
        /// Stack<> no sobreescribe el m�todo toString de la clase Object
        /// por eso no se imprimen los valores sino el tipo de objeto
        /// </summary>
        [Fact]
        public void ReturnStringRepresentationOfStackUsingToString()
        {
            // Arrange
            Stack<string> strings = GenerateStack(3);

            // Act
            string? result = strings.ToString();

            // Assert
            result.Should().NotBeEmpty()
                .And.NotBe("S0,S1,S2")
                .And.Be("System.Collections.Generic.Stack`1[System.String]");
        }

        

        /*
         * Excepciones
         */

        [Fact]
        public void ThrowArgumentOutOfRangeExceptionWhenCreateStackWithNegativeSize()
        {
            // Arrange
            int size = -1;

            // Act
            Action act = () => new Stack<string>(size);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenCreateStackWithNull()
        {
            // Arrange
            IEnumerable<string> enumerable = null;

            // Act
            Action act = () => new Stack<string>(enumerable);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ThrowInvalidOperationExceptionnWhenPopEmptyStack()
        {
            // Arrange
            Stack<string> strings = new Stack<string>();

            // Act
            Action act = () => strings.Pop();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Stack empty.");
        }

        /*
         * Metodos privados que nos ayudan a generar datos de prueba y obtenerlos
         */

        private static Stack<string> GenerateStack(int l)
        {
            Stack<string> strings = new Stack<string>(l);
            for (int i = 0; i < l; i++)
            {
                strings.Push("S" + i);
            }

            return strings;
        }

        private int GetCapacity<T>(Stack<T> stack)
        {
            // Obtenemos el valor del campo _array de la pila que pasamos por par�metro
            var field = typeof(Stack<T>).GetField("_array", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            // Covnertimos el objeto de tipo field a un array para saber su longitud
            T[] array = (T[])field.GetValue(stack);
            // retornamos la longitud del array que es el dato que nos interesa
            return array.Length;
        }
    }
}