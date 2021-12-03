const pool = require('../../db-config/mysql-config');

class CharacteristicService {
  async getAll() {
    return new Promise((resolve, reject) => {
      const query = `
                SELECT
                    *
                FROM 
                    characteristics`;
      pool.query(query, (error, rows) => {
        if (error) {
          reject(error);
        }
        resolve(rows);
      });
    });
  }
}

const instance = new CharacteristicService();

module.exports = {
  characteristicService: instance,
};
