import React, { useEffect, useState } from 'react';
import axios from 'axios';

function Document() {
  const [documents, setDocuments] = useState([]);
  const [newDocument, setNewDocument] = useState({ id: '' });
  const [errorMessage, setErrorMessage] = useState('');

  const fetchDocuments = async () => {
    try {
      const response = await axios.get('/document/list');
      setDocuments(response.data);
    } catch (error) {
      setErrorMessage('Error fetching documents');
    }
  };

  useEffect(() => {
    fetchDocuments();
  }, []);

  const handleAddDocument = async () => {
    try {
      await axios.post(`/document/${newDocument.id}`);
      setNewDocument({ name: '', body: '' });
      fetchDocuments();
    } catch (error) {
      setErrorMessage('Error adding document');
    }
  };

  const handleRemoveDocument = async (id) => {
    try {
      await axios.delete(`/document/${id}`);
      fetchDocuments();
    } catch (error) {
      setErrorMessage('Error removing document');
    }
  };

  return (
    <div>
      <h1>Document List</h1>
      {errorMessage && <p>{errorMessage}</p>}
      <ul>
        {documents.map((document) => (
          <li key={document.id}>
            <p>{document.name}</p>
            <div dangerouslySetInnerHTML={{ __html: document.body }} />
            <p>{document.created_on}</p>
            <button onClick={() => handleRemoveDocument(document.id)}>
              Remove
            </button>
          </li>
        ))}
      </ul>
      <h2>Add New Document</h2>
      <div>
        <label htmlFor='id'>Id:</label>
        <input
          type='text'
          id='id'
          value={newDocument.id}
          onChange={(event) =>
            setNewDocument({ ...newDocument, id: event.target.value })
          }
        />
      </div>
      <button onClick={handleAddDocument}>Add</button>
    </div>
  );
}

export default Document;
