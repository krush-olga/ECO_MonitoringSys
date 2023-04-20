const pool = require('../../db-config/mysql-config');

const tableName = 'resource';

const getResources = async (req, res) => {
  const getResourcesPromise = new Promise((resolve, reject) => {
    const query = `
      SELECT 
        *
      FROM 
        ??
      ;`;

    const values = [tableName];

    return pool.query(query, values, (error, rows) => {
      if (error) {
        return reject(error);
      }

      return resolve(rows);
    });
  });

  try {
    const rows = await getResourcesPromise;
    return res.send(JSON.stringify(rows));
  } catch (error) {
    return res.status(500).send({
      message: error,
    });
  }
};

const addResource = async (req, res) => {
  const addResourcePromise = new Promise((resolve, reject) => {
    const query = `
      INSERT INTO
        ??
      VALUES
        (?)
    `;

    pool.query(query, [tableName, Object.values(req.body)], (error, rows) => {
      if (error) {
        return reject(error);
      }

      if (rows.affectedRows === 1) {
        return resolve();
      }
    });
  });

  try {
    await addResourcePromise;
    return res.sendStatus(200);
  } catch (error) {
    return res.status(500).send({ message: error });
  }
};

const editResource = async (req, res) => {
  const editResourcePromise = new Promise((resolve, reject) => {
    const id = req.params.id;
    const { body: updatedValues } = req;

    const query = `
      UPDATE
      ??
      SET
      ?
      WHERE
      ?? = ?
    `;

    const values = [tableName, updatedValues, 'resource_id', id];

    pool.query(query, values, (error, rows) => {
      if (error) {
        return reject(error);
      }

      if (rows.affectedRows === 1) {
        return resolve();
      }
    });
  });

  try {
    await editResourcePromise;
    return res.sendStatus(200);
  } catch (error) {
    return res.status(500).send({ message: error });
  }
};

const removeResource = async (req, res) => {
  const removeResourcePromise = new Promise((resolve, reject) => {
    const id = req.params.id;

    const query = `
      DELETE FROM
      ??
      WHERE
      ?? = ?
    `;

    const values = [tableName, 'resource_id', id];

    pool.query(query, values, (error, rows) => {
      if (error) {
        return reject(error);
      }

      if (rows.affectedRows === 1) {
        return resolve();
      }
    });
  });

  try {
    await removeResourcePromise;
    return res.sendStatus(200);
  } catch (error) {
    return res.status(500).send({ message: error });
  }
};

module.exports = {
  getResources,
  addResource,
  editResource,
  removeResource,
};
