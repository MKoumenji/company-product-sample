using Company.Product.Infrastructure.Services.Logging;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

namespace Company.Product.Infrastructure.Tests.Integration.Services
{
    public class SecLoggerServiceTests
    {
        public SecLoggerServiceTests()
        {
            // Set up Serilog for tests
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true) // Lade Test-Config
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration) // Lies Konfiguration aus appsettings
                .Enrich.WithExceptionDetails(
                    new DestructuringOptionsBuilder()
                        .WithDefaultDestructurers()
                )
                .Enrich.WithAssemblyName()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithEnvironmentName()
                .CreateLogger();
        }
        
        [Fact]
        public void LogInformation_ShouldNotThrowException()
        {
            // Arrange
            var message = "Test Information Message";
            var secLoggerService = new SecLoggerService();

            // Act & Assert
            var exception = Record.Exception(() => secLoggerService.LogInformation(message));
            Assert.Null(exception);
        }

        [Fact]
        public void LogWarning_ShouldNotThrowException()
        {
            // Arrange
            var message = "Test Warning Message";
            var secLoggerService = new SecLoggerService();

            // Act & Assert
            var exception = Record.Exception(() => secLoggerService.LogWarning(message));
            Assert.Null(exception);
        }

        [Fact]
        public void LogError_WithException_ShouldNotThrowException()
        {
            // Arrange
            var message = "Test Error Message";
            var exceptionToLog = new Exception("Test Exception");
            var secLoggerService = new SecLoggerService();

            // Act & Assert
            var exception = Record.Exception(() => secLoggerService.LogError(message, exceptionToLog));
            Assert.Null(exception);
        }

        [Fact]
        public void LogError_WithoutException_ShouldNotThrowException()
        {
            // Arrange
            var message = "Test Error Message";
            var secLoggerService = new SecLoggerService();

            // Act & Assert
            var exception = Record.Exception(() => secLoggerService.LogError(message, null));
            Assert.Null(exception);
        }

    }
}