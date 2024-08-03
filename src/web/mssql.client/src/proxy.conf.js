const { env } = require('process');

const target1 = 'http://localhost:5166/';
const target2 = 'http://localhost:5105';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: target1,
    secure: false 
  },
  {
    context: [
      "/Tables",
    ],
    target: target2,
    secure: false 
 
  }
]

module.exports = PROXY_CONFIG;
