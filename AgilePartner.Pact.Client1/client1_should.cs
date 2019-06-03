using AgilePartner.Pact.Lib;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgilePartner.Pact.Client1
{
    public class client1_should : client_should<Client1>, IClassFixture<PactFixture>
    {
        public client1_should(PactFixture pactFixture)
            : base(pactFixture, "Client1", "HelloWorld") { }

        [Fact]
        public async Task get_helloworldv1()
        {
            mockProviderService
              .Given("Client1 get at helloworld")
              .UponReceiving("A message")
              .With(new ProviderServiceRequest
              {
                  Method = HttpVerb.Get,
                  Path = "/api/v1.0/helloworld"
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
                      message = "Hello world v1"
                  }
              });

            await consumer.GetHello();

            mockProviderService.VerifyInteractions();
        }

        protected override Client1 CreateConsumer() => new Client1(new RestProxy(), pactFixture.MockProviderServiceBaseUri);
    }
}
