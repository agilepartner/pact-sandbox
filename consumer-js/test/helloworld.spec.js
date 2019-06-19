"use strict"

const expect = require("chai").expect;
const path = require("path");
const { Pact } = require('@pact-foundation/pact');
const { helloworldv2 } = require("../index");

describe("The Helloworld API", () => {
  let url = "http://localhost"
  const port = 8992
  const contractPath = path.resolve(process.cwd(), "pacts");

  const provider = new Pact({
    port: port,
    log: path.resolve(process.cwd(), "logs", "mockserver-integration.log"),
    dir: contractPath,
    spec: 2,
    consumer: "consumer-js",
    provider: "HelloWorld",
    pactfileWriteMode: "merge",
    publishPacts: true,
    publishVerificationResult: true
  })

  const EXPECTED_BODY = {
    message: "Hello world v2"
  }

  // Setup the provider
  before(() => provider.setup())

  // Write Pact when all tests done
  after(() => provider.finalize())

  // verify with Pact, and reset expectations
  afterEach(() => provider.verify())

  describe("get /api/v2.0/helloworld", () => {
    before(done => {
      const interaction = {
        state: "consumer-js get at helloworld",
        uponReceiving: "A message",
        withRequest: {
          method: "GET",
          path: "/api/v2.0/helloworld"
        },
        willRespondWith: {
          status: 200,
          headers: {
            "Content-Type": "application/json; charset=utf-8",
          },
          body: EXPECTED_BODY,
        },
      }
      provider.addInteraction(interaction).then(() => {
        done()
      })
    })

    it("returns the correct response", done => {
      const urlAndPort = {
        url: url,
        port: port,
      }
      helloworldv2(urlAndPort).then(response => {
        expect(response.data).to.eql(EXPECTED_BODY)
        publish(contractPath)

        done()
      }, done
      )
    })
  })
})

let publish = contractDirectory => {
  let pact = require('@pact-foundation/pact-node');
  let opts = {
    pactBroker: "http://localhost/",
    pactFilesOrDirs: [contractDirectory],
    consumerVersion: "0.0.1",
    publishVerificationResult: true
  };
  pact.publishPacts(opts);
}