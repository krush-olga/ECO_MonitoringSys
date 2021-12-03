const pool = require('../../db-config/mysql-config');

const tableName = 'radiation_object_state';

const getAll = async (req, res) => {
  const getAllPromise = new Promise((resolve, reject) => {
    const query = `
      SELECT 
        *
      FROM 
        ??;
      `;

    const values = [tableName];

    return pool.query(query, values, (error, rows) => {
      if (error) {
        return reject(error);
      }
      return resolve(rows);
    });
  });

  try {
    const rows = await getAllPromise;
    return res.send(JSON.stringify(rows));
  } catch (error) {
    return res.status(500).send({
      message: error,
    });
  }
};

module.exports = {
  getAll,
};
