const pool = require('../../db-config/mysql-config');

class CalculationService {
  async generateNewCalculationNumber() {
    return new Promise((resolve, reject) => {
      const tableName = 'calculations_description';
      const query = `
        SELECT
            MAX(??) as maxId
        FROM ??
      ;`;
      pool.query(query, ['calculation_number', tableName], (error, rows) => {
        if (error) {
          reject(error);
        }
        resolve(rows[0].maxId + 1);
      });
    });
  }

  async setCalculationNumberToPoi({ idPoi, calculationNumber }) {
    return new Promise((resolve, reject) => {
      const tableName = 'radiation_poi_calculation';
      const query = `
        INSERT INTO 
            ??
            (??)
        VALUES
            (?)`;

      const columnNames = ['calculation_number', 'idPoi'];
      const values = [calculationNumber, idPoi];
      const parameters = [tableName, columnNames, values];
      pool.query(query, parameters, (error, rows) => {
        if (error) {
          reject(error);
        }
        if (rows) {
          resolve();
        }
      });
    });
  }
}

const instance = new CalculationService();

module.exports = {
  calculationService: instance,
};
