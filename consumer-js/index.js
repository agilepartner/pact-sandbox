"use strict"

const axios = require("axios")

exports.helloworldv2 = endpoint => {
  const url = endpoint.url
  const port = endpoint.port

  return axios.request({
    method: "GET",
    baseURL: `${url}:${port}`,
    url: "/api/v2.0/helloworld",
    headers: { Accept: "application/json; charset=utf-8" },
  })
}
