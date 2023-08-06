using OneBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBankTest
{
    public class DashboardTest
    {
        [Fact]
        public void Menu_InvalidOptionEntered_CallsMenuAgain()
        {
            // Arrange
            var bank = new Bank();
            var input = "invalid input";
            var expectedOutput = "Invalid input. Please enter a valid menu option (1-8).";

            // Mock Console.ReadLine to return invalid input
            using (var consoleInput = new ConsoleInputMock(input))
            {
                // Mock Console.WriteLine to capture output
                using (var consoleOutput = new ConsoleOutputMock())
                {
                    // Act
                    bank.Menu();

                    // Assert
                    Assert.Equal(expectedOutput, consoleOutput.GetOutput());
                }
            }
        }
    }
}
