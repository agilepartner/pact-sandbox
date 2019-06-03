using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace AgilePartner.Pact.Providers.Tests
{
    public class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper output;

        public XUnitOutput(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void WriteLine(string line)
        {
            output.WriteLine(line);
        }
    }
}