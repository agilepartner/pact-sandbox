using AgilePartner.Pact.Lib;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgilePartner.Pact.Consumers.Tests
{
    public class client4_should : client_should<Client4>, IClassFixture<PactFixture>
    {
        public client4_should(PactFixture pactFixture)
            : base(pactFixture, "Client4", "HelloWorld") { }

        [Fact]
        public async Task get_helloworldv3()
        {
            mockProviderService
              .Given("Client4 get at helloworld")
              .UponReceiving("A message")
              .With(new ProviderServiceRequest
              {
                  Method = HttpVerb.Get,
                  Path = "/api/v3.0/helloworld"
              })
              .WillRespondWith(new ProviderServiceResponse
              {
                  Status = 200,
                  Headers = new Dictionary<string, object>
                  {
                    { "Content-Type", "application/json; charset=utf-8" },
                  },
                  Body = new
                  {
                      message = "Hello world v3"
                  }
              });

            await consumer.GetHello();

            mockProviderService.VerifyInteractions();
        }

        protected override Client4 CreateConsumer() => 
            new Client4(new RestProxy(), pactFixture.MockProviderServiceBaseUri);
    }
}
