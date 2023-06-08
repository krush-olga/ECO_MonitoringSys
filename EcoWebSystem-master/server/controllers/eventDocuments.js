const isEmpty = require('is-empty');

const pool = require('../../db-config/mysql-config');

const addEventDocument = (req, res) => {
  const { event_id, document_code, description } = req.body;

  const query = `
    INSERT INTO event_documents 
    (event_id, document_code, description)
    VALUES ('${event_id}', '${document_code}', '${description}');
  `;

  pool.query(query, (error, rows) => {
    if (error) {
      console.log(error);
      res.status(500).send({
        message: error,
      });
    } else {
      res.sendStatus(200);
    }
  });
};

const removeEventDocument = (req, res) => {
  const { event_id, document_code } = req.body;

  const query = `DELETE FROM event_documents WHERE event_id=${event_id} AND document_code='${document_code}'`;

  pool.query(query, (error, rows) => {
    if (error) {
      console.log(error);
      res.status(500).send({
        message: error,
      });
    } else {
      res.sendStatus(200);
    }
  });
};

module.exports = {
  addEventDocument,
  removeEventDocument,
};
