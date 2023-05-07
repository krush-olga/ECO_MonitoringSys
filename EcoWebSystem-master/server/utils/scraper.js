const axios = require('axios');
const cheerio = require('cheerio');

async function scrapDocument(id) {
  let url = 'https://data.rada.gov.ua/laws/show/' + id;
  const scraper = axios
    .get(url)
    .then((response) => {
      let map = new Map();
      const $ = cheerio.load(response.data);
      map.set('name', $('meta[property="og:title"]').attr('content'));
      $('#Text > .row > .col').each((index, element) => {
        map.set('body', $(element).html());
      });
      $('.doc > div > .dat0').each((index, element) => {
        let splitDate = $(element).text().split('.');
        map.set(
          'date',
          new Date(splitDate[1] + '/' + splitDate[0] + '/' + splitDate[2])
        );
      });

      if (map.size < 3) {
        throw new Error('Some fields are not parsed');
      }
      return map;
    })
    .catch((error) => {
      console.log(error);
    });
  try {
    return await scraper;
  } catch (e) {
    throw new Error(
      `Cannot retrieve date from the page: ${url}; try again later. ${e}`
    );
  }
}

module.exports = {
  scrapDocument,
};
