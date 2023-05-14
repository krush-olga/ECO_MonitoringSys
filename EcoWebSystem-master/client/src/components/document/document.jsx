import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './style.css';

function Document() {
  const [documents, setDocuments] = useState([]);
  const [newDocument, setNewDocument] = useState({ id: '' });
  const [errorMessage, setErrorMessage] = useState('');
  const [nameFilter, setNameFilter] = useState('');
  const [dateFilter, setDateFilter] = useState('');
  const [idFilter, setIdFilter] = useState('');

  const fetchDocuments = async () => {
    try {
      const response = await axios.get('/document/info/list');
      setDocuments(response.data.sort());
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

  async function handleUpdateDocument(id) {
    fetchDocuments();
    try {
      await axios.put(`/document/${id}`);
      fetchDocuments();
    } catch (error) {
      setErrorMessage('Error updating document');
    }
  }

  const handleRemoveDocument = async (id) => {
    try {
      await axios.delete(`/document/${id}`);
      fetchDocuments();
    } catch (error) {
      setErrorMessage('Error removing document');
    }
  };

  const handleNameFilterChange = (event) => {
    setNameFilter(event.target.value);
  };

  const handleDateFilterChange = (event) => {
    setDateFilter(event.target.value);
  };

  const handleIdFilterChange = (event) => {
    setIdFilter(event.target.value);
  };

  const filteredDocuments = documents.filter((document) => {
    const nameMatchesFilter = document.name
      .toLowerCase()
      .includes(nameFilter.toLowerCase());
    const dateMatchesFilter = dateFilter
      ? new Date(document.created_on)
          .toLocaleDateString()
          .includes(new Date(dateFilter).toLocaleDateString())
      : true;
    const idMatchesFilter = idFilter
      ? document.id.toString().includes(idFilter)
      : true;
    return nameMatchesFilter && dateMatchesFilter && idMatchesFilter;
  });

  const [htmlContent, setHtmlContent] = useState({});
  const [showHtml, setShowHtml] = useState(false);
  const handleInsertHtml = async (id) => {
    try {
      const response = await axios.get(`/document/body/${id}`);
      setHtmlContent({ ...htmlContent, [id]: response.data[0].body });
    } catch (error) {
      setErrorMessage('Error fetching document body');
    }
  };

  const [searchTerm, setSearchTerm] = useState('');
  const handleSearch = async () => {
    try {
      if (searchTerm) {
        const response = await axios.get(`/document/search/${searchTerm}`);
        setDocuments(response.data);
      } else {
        fetchDocuments();
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div>
      <h1>Document List</h1>
      {errorMessage && <p>{errorMessage}</p>}
      <label htmlFor='idFilter'>Filter by ID:</label>
      <input
        type='text'
        value={idFilter}
        onChange={handleIdFilterChange}
        id='idFilter'
      />
      <label htmlFor='nameFilter'>Filter by name:</label>
      <input
        type='text'
        value={nameFilter}
        onChange={handleNameFilterChange}
        id='nameFilter'
      />
      <label htmlFor='dateFilter'>Filter by created on:</label>
      <input
        type='date'
        value={dateFilter ? dateFilter : ''}
        onChange={handleDateFilterChange}
        id='dateFilter'
      />
      <br />
      <input
        type='text'
        placeholder='Search documents...'
        value={searchTerm}
        onChange={(event) => setSearchTerm(event.target.value)}
      />
      <button onClick={handleSearch}>Search</button>
      <table
        style={{
          borderCollapse: 'collapse',
          border: '1px solid black',
          margin: '0 20px 0 20px',
        }}
      >
        <thead>
          <tr>
            <th style={{ border: '1px solid black', padding: '8px' }}>Id</th>
            <th style={{ border: '1px solid black', padding: '8px' }}>Name</th>
            <th style={{ border: '1px solid black', padding: '8px' }}>
              Created On
            </th>
            <th style={{ border: '1px solid black', padding: '8px' }}>
              Updated On
            </th>
            <th style={{ border: '1px solid black', padding: '8px' }}>
              Action
            </th>
            <th style={{ border: '1px solid black', padding: '8px' }}>Body</th>
          </tr>
        </thead>
        <tbody>
          {filteredDocuments.map((document) => (
            <tr key={document.id}>
              <td
                style={{
                  border: '1px solid black',
                  padding: '8px',
                  width: '80px',
                }}
              >
                {document.id}
              </td>
              <td style={{ border: '1px solid black', padding: '8px' }}>
                {document.name}
              </td>
              <td
                style={{
                  border: '1px solid black',
                  padding: '8px',
                  width: '105px',
                }}
              >
                {new Date(document.created_on).toLocaleDateString()}
              </td>
              <td
                style={{
                  border: '1px solid black',
                  padding: '8px',
                  width: '110px',
                }}
              >
                {new Date(document.updated_on).toLocaleString()}
              </td>
              <td style={{ border: '1px solid black', padding: '8px' }}>
                <button onClick={() => handleUpdateDocument(document.id)}>
                  Update
                </button>
                <button onClick={() => handleRemoveDocument(document.id)}>
                  Remove
                </button>
              </td>
              <td style={{ border: '1px solid black', padding: '8px' }}>
                <button onClick={() => handleInsertHtml(document.id)}>
                  Insert HTML
                </button>
                {htmlContent[document.id] && (
                  <div
                    dangerouslySetInnerHTML={{
                      __html: htmlContent[document.id],
                    }}
                  />
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
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
