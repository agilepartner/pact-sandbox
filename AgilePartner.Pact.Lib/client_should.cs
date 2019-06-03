using PactNet.Mocks.MockHttpService;
using System;
using Xunit;

namespace AgilePartner.Pact.Lib
{
    public abstract class client_should<TConsumer>
    {
        protected readonly TConsumer consumer;
        protected readonly PactFixture pactFixture;
        protected abstract TConsumer CreateConsumer();

        protected IMockProviderService mockProviderService => pactFixture.MockProviderService;

        public client_should(
            PactFixture pactFixture,
            string consumerName,
            string providerName,
            string clientVersion = "1.0.0")
        {
            this.pactFixture = pactFixture;
            this.pactFixture.SetUp(
                consumerName,
                providerName,
                clientVersion);

            mockProviderService.ClearInteractions();
            consumer = CreateConsumer();
        }
    }
}