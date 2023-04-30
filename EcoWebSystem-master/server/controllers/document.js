const pool = require('../../db-config/mysql-config');
const { scrapDocument } = require('../utils/scraper');

const addDocument = async (req, res) => {
  let id = req.params.id;
  try {
    let document = await scrapDocument(id);
    const save = new Promise((resolve, reject) => {
      const query = `INSERT INTO documents
                           VALUES (?, ?, ?, ?);`;
      return pool.query(
        query,
        [
          id,
          document.get('name'),
          document.get('body'),
          new Date(document.get('date').replaceAll('.', '/')),
        ],
        (error, rows) => {
          if (error) {
            return reject(error);
          }
          return resolve(rows);
        }
      );
    });
    await save;
    return res.sendStatus(200);
  } catch (error) {
    return res.status(500).send({ message: error });
  }
};

const getDocumentList = async (req, res) => {
  const findAllDocuments = new Promise((resolve, reject) => {
    const query = `SELECT *
                       FROM documents;`;
    return pool.query(query, [], (error, rows) => {
      if (error) {
        return reject(error);
      }
      return resolve(rows);
    });
  });

  try {
    const documents = await findAllDocuments;
    return res.send(JSON.stringify(documents));
  } catch (error) {
    return res.status(500).send({ message: error });
  }
};

const removeDocument = async (req, res) => {
  let id = req.params.id;
  const deleteDocumentById = new Promise((resolve, reject) => {
    const query = `DELETE
                       FROM documents
                       WHERE id = ?`;

    return pool.query(query, [id], (error, rows) => {
      if (error) {
        return reject(error);
      }

      return resolve(rows);
    });
  });

  try {
    await deleteDocumentById;
    return res.sendStatus(200);
  } catch (error) {
    return res.status(500).send({ message: error });
  }
};

module.exports = {
  addDocument,
  getDocumentList,
  removeDocument,
};
