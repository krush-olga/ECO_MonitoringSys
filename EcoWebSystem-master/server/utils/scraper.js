const axios = require('axios');
const cheerio = require('cheerio');

async function scrapDocument(id) {
  const scraper = axios
    .get('https://data.rada.gov.ua/laws/show/' + id + '#Text')
    .then((response) => {
      let map = new Map();
      const $ = cheerio.load(response.data);
      map.set('name', $('meta[property="og:title"]').attr('content'));
      $('#Text > .row > .col').each((index, element) => {
        map.set('body', $(element).html());
      });
      $('.doc > div > .dat0').each((index, element) => {
        map.set('date', $(element).text());
      });
      return map;
    })
    .catch((error) => {
      console.log(error);
    });

  try {
    return await scraper;
  } catch (e) {
    throw e;
  }
}

module.exports = {
  scrapDocument,
};
