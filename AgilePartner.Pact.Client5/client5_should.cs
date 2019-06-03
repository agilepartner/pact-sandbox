using AgilePartner.Pact.Lib;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgilePartner.Pact.Client5
{
    public class client5_should : client_should<Client5>, IClassFixture<PactFixture>
    {
        private readonly string path = "/api/v4.0/helloworld";
        public client5_should(PactFixture pactFixture)
            : base(pactFixture, "Client5", "HelloWorld") { }

        [Fact]
        public async Task get_helloworldv4()
        {
            var name = "John";

            mockProviderService
              .Given("Client5 get at helloworld")
              .UponReceiving("A message")
              .With(new ProviderServiceRequest
              {
                  Method = HttpVerb.Get,
                  Path = path,
                  Query = $"name={ name }"
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
                      message = $"Hello world v4 -> you rock { name }"
                  }
              });

            await consumer.GetHello(name);

            mockProviderService.VerifyInteractions();
        }

        [Fact]
        public async Task return_not_found_when_name_is_null()
        {
            string name = null;

            mockProviderService
              .Given("Client5 get at helloworld with empty name")
              .UponReceiving("A Not Found")
              .With(new ProviderServiceRequest
              {
                  Method = HttpVerb.Get,
                  Path = path,
                  Query = $"name="
              })
              .WillRespondWith(new ProviderServiceResponse
              {
                  Status = 404
              });

            await consumer.GetHello(name);

            mockProviderService.VerifyInteractions();
        }

        protected override Client5 CreateConsumer() => 
            new Client5(new RestProxy(), pactFixture.MockProviderServiceBaseUri);
    }
}
