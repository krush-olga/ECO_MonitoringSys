using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calculations.Tests
{
    public class EcologTest
    {
        [Theory]
        [InlineData(1,1,1)]
        [InlineData(1,2,2)]
        [InlineData(3,2,6)]
        [InlineData(10,2,20)]
        [InlineData(0,2,0)]
        public void formula13_should_calculate_simple_numbers(double x, double y, double expected)
        {
            //Arrange
            EcologCalculator ecologCalc = new EcologCalculator();
            //Act
            //double actual = ecologCalc.Formula13(x, y);
            //Assert
            //Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(4, 2, 2)]
        [InlineData(8, 2, 4)]
        [InlineData(16, 2, 8)]
        public void formula3_should_calculate_simple_values(double x, double y, double expected)
        {
            //Arrange
            EcologCalculator ecologCalc = new EcologCalculator();
            //Act
            //double actual = ecologCalc.Formula3(x, y);
            //Assert
            //Assert.Equal(expected, actual);
        }

        [Fact]
        public void formula2_should_throw_exception()
        {
            //Arrange
            EcologCalculator ecologCalc = new EcologCalculator();
            //Act
            //Assert
            //Assert.Throws<DivideByZeroException>(()=> ecologCalc.Formula2(0,0,0));
        }

        [Fact]
        public void formula3_should_throw_exception()
        {
            //Arrange
            EcologCalculator ecologCalc = new EcologCalculator();
            //Act
            //Assert
            //Assert.Throws<DivideByZeroException>(()=> ecologCalc.Formula3(1,0));
        }

        [Fact]
        public void formula4_should_throw_exception()
        {
            //Arrange
            EcologCalculator ecologCalc = new EcologCalculator();
            //Act
            //Assert
            //Assert.Throws<DivideByZeroException>(()=> ecologCalc.Formula4(1,0,0));
            
        }




    }
}
