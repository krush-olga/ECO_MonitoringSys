const pool = require('../../db-config/mysql-config');
const { scrapDocument } = require('../utils/scraper');

const addDocument = async (req, res) => {
  let id = req.params.id;
  try {
    let document = await scrapDocument(id);
    const save = new Promise((resolve, reject) => {
      const query = `INSERT INTO documents
                           VALUES (?, ?, ?, ?, null);`;
      return pool.query(
        query,
        [id, document.get('name'), document.get('body'), document.get('date')],
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

const updateDocument = async (req, res) => {
  let id = req.params.id;
  try {
    let document = await scrapDocument(id);
    const update = new Promise((resolve, reject) => {
      const query = `UPDATE documents
                     SET name = ?,
                         body = ?,
                         created_on = ?,
                         updated_on = null
                     WHERE id = ?`;
      return pool.query(
        query,
        [document.get('name'), document.get('body'), document.get('date'), id],
        (error, rows) => {
          if (error) {
            return reject(error);
          }
          return resolve(rows);
        }
      );
    });
    await update;
    return res.sendStatus(200);
  } catch (error) {
    return res.status(500).send({ message: error });
  }
};

const getDocumentInfoList = async (req, res) => {
  const findAllDocuments = new Promise((resolve, reject) => {
    const query = `SELECT d.id, d.name, d.created_on, d.updated_on
                       FROM documents d;`;
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

const getDocumentBody = async (req, res) => {
  let id = req.params.id;
  try {
    let document = await scrapDocument(id);
    const getDocumentBody = new Promise((resolve, reject) => {
      const query = `SELECT d.body
                     FROM documents d 
                     WHERE d.id = ?;`;
      return pool.query(query, [id], (error, rows) => {
        if (error) {
          return reject(error);
        }
        return resolve(rows);
      });
    });

    const documentBody = await getDocumentBody;
    return res.send(JSON.stringify(documentBody));
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
  updateDocument,
  getDocumentInfoList,
  getDocumentBody,
  removeDocument,
};
