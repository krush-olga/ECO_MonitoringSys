const isEmpty = require('is-empty');

const pool = require('../../db-config/mysql-config');

const getTaskDocuments = (req, res) => {
  const { taskId } = req.query;

  const query = `
    SELECT *
    FROM issues_documents
    WHERE issue_id = ${taskId};
  `;

  pool.query(query, (error, rows) => {
    if (error) {
      console.log(error);
      res.status(500).send({
        message: error,
      });
    } else {
      res.send(rows);
    }
  });
};

const addTaskDocument = (req, res) => {
  const { issue_id, document_code, description } = req.body;

  const query = `
    INSERT INTO issues_documents 
    (issue_id, document_code, description)
    VALUES ('${issue_id}', '${document_code}', '${description}');
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

const removeTaskDocument = (req, res) => {
  const { issue_id, document_code } = req.body;

  const query = `DELETE FROM issues_documents WHERE issue_id=${issue_id} AND document_code='${document_code}'`;

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
  getTaskDocuments,
  addTaskDocument,
  removeTaskDocument,
};
