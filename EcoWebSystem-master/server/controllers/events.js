const isEmpty = require('is-empty');

const pool = require('../../db-config/mysql-config');

const getEvents = (req, res) => {
  const { taskId } = req.query;

  pool.query(
    `
   SELECT 
  event.event_id, 
  event.name, 
  event.description, 
  event.lawyer_vefirication, 
  event.dm_verification, 
  event.id_of_user, 
  event.issue_id, 
  JSON_ARRAYAGG(
    JSON_MERGE_PATCH(
      JSON_OBJECT(
        'resource_id', event_resource.resource_id,
        'value', event_resource.value,
        'description', event_resource.description
      ),
      JSON_OBJECT(
        'name', resource.name,
        'units', resource.units,
        'price', resource.price
      )
    )
  ) AS resources,
  JSON_ARRAYAGG(
    JSON_OBJECT(
      'document_code', 
      CASE WHEN event_documents.document_code IS NOT NULL THEN event_documents.document_code ELSE NULL END,
      'description', 
      CASE WHEN event_documents.description IS NOT NULL THEN event_documents.description ELSE NULL END
    )
  ) AS documents
FROM 
  event 
LEFT JOIN 
  event_resource 
ON 
  event.event_id = event_resource.event_id 
LEFT JOIN 
  event_documents 
ON 
  event.event_id = event_documents.event_id
LEFT JOIN 
  resource
ON 
  event_resource.resource_id = resource.resource_id
WHERE 
  event.issue_id = ${taskId}
GROUP BY 
  event.event_id;
  `,
    (err, rows) => {
      if (err) {
        console.log(err);
      } else {
        const parsedRows = rows.map((row) => ({
          ...row,
          documents: JSON.parse(row.documents).filter(
            (doc) => doc.document_code
          ),
          resources: JSON.parse(row.resources).filter(
            (resource) => resource.resource_id
          ),
        }));

        res.send(parsedRows);
      }
    }
  );
};

const addEvent = (req, res) => {
  const { name, description, id_of_user, resources, issue_id } = req.body;

  const query = `
    INSERT INTO event 
    (name, description, id_of_user, issue_id)
    VALUES ('${name}', '${description}', '${id_of_user}', '${issue_id}');
  `;

  const eventPromise = new Promise((resolve, reject) => {
    pool.query(query, (error, rows) => {
      if (error) {
        reject(error);
      } else if (isEmpty(rows)) {
        error = Error('Wrong input data');
        reject(error);
      } else {
        resolve(+rows.insertId);
      }
    });
  });

  return eventPromise
    .then((eventId) => {
      const promises = resources.map((resource) => {
        const { description, resource_id, value } = resource;
        const resourceQuery = `
          INSERT INTO event_resource
          (event_id, resource_id, description, value)
          VALUES ('${eventId}', '${resource_id}', '${description}', '${value}');
        `;
        return new Promise((resolve, reject) => {
          pool.query(resourceQuery, (error, rows) => {
            if (error) {
              reject(error);
            } else {
              resolve();
            }
          });
        });
      });

      return Promise.all(promises);
    })
    .then(() => res.sendStatus(200))
    .catch((error) => {
      console.log(error);
      res.status(500).send({
        message: error,
      });
    });
};

module.exports = {
  getEvents,
  addEvent,
};
