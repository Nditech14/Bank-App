using Microsoft.VisualBasic;
using OneBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBankTest
{
    public class CreateAccountTest
    {
        [Fact]
        public void ValidateInput_ValidInput_ReturnTrue()
        {
            //Arrange
            string input = "Dominic";
            string pattern = "^[A-Z][a-zA-Z\\s]+$";

            //Act
            bool result = CreateAccount.ValidatingInput(input, pattern);

            //Assert
            Assert.True(result);

        }
    }
}
