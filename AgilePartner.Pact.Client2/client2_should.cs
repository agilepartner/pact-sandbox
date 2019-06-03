using AgilePartner.Pact.Lib;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgilePartner.Pact.Client2
{
    public class client2_should : client_should<Client2>, IClassFixture<PactFixture>
    {
        public client2_should(PactFixture pactFixture)
            : base(pactFixture, "Client2", "HelloWorld") { }

        [Fact]
        public async Task get_helloworldv1()
        {
            mockProviderService
              .Given("Client2 get at helloworld")
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

        protected override Client2 CreateConsumer() => new Client2(new RestProxy(), pactFixture.MockProviderServiceBaseUri);
    }
}
