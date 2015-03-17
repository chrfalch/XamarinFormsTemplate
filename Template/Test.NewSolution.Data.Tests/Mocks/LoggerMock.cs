using System;
using System.Threading.Tasks;
using Test.NewSolution.Contracts.Services;

namespace Test.NewSolution.Data.Tests.Mocks
{
    public class LoggerMock: ILoggingService
    {
        public LoggerMock()
        {
        }

        #region ILoggingService implementation

        public Task InitializAsync()
        {
            return Task.FromResult(true);
        }

        public Task AddProviderAsync(ILoggingProvider provider)
        {
            return Task.FromResult(true);
        }

        public void AddProvider(ILoggingProvider provider)
        {

        }

        public void Log(LogLevel level, object sender, string message)
        {

        }

        public void Log(LogLevel level, object sender, string formatString, params object[] args)
        {

        }

        public LogLevel LogLevel { get; set; }

        public void BeginIgnore()
        {           
        }

        public void EndIgnore()
        {         
        }
        #endregion
    }
}

