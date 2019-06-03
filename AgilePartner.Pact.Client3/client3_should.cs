using AgilePartner.Pact.Lib;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgilePartner.Pact.Client3
{
    public class client3_should : client_should<Client3>, IClassFixture<PactFixture>
    {
        public client3_should(PactFixture pactFixture)
            : base(pactFixture, "Client3", "HelloWorld") { }

        [Fact]
        public async Task get_helloworldv2()
        {
            mockProviderService
              .Given("Client3 get at helloworld")
              .UponReceiving("A message")
              .With(new ProviderServiceRequest
              {
                  Method = HttpVerb.Get,
                  Path = "/api/v2.0/helloworld"
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
                      message = "Hello world v2"
                  }
              });

            await consumer.GetHello();

            mockProviderService.VerifyInteractions();
        }

        protected override Client3 CreateConsumer() =>
            new Client3(new RestProxy(), pactFixture.MockProviderServiceBaseUri);
    }
}
