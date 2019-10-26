const PROXY_CONFIG = [
  {
      context: [
          "/api"
      ],
      target: "http://localhost:5001", // API GATEWAY HOST
      secure: false,
      //changeOrigin: true // GW will be on external IP that why is need this options
  }
]

module.exports = PROXY_CONFIG;
