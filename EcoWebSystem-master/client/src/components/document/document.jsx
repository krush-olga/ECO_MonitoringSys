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
      for (let i = 0; i < response.data.length; i++) {
        const id = response.data[i].id;
        if (!hiddenRows[id]) {
          setHiddenRows((prevHiddenRows) => ({
            ...prevHiddenRows,
            [id]: !prevHiddenRows[id],
          }));
        }
      }
    } catch (error) {
      setErrorMessage('Error fetching documents');
    }
  };

  useEffect(() => {
    fetchDocuments();
  }, []);

  const handleAddDocument = async () => {
    try {
      const doc = encodeURIComponent(newDocument.id);
      console.log(doc);
      await axios.post(`/document/${doc}`);
      setNewDocument({ name: '', body: '' });
      fetchDocuments();
      setIdFilter(newDocument.id);
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
  const [hiddenRows, setHiddenRows] = useState({});
  const handleInsertHtml = async (id) => {
    try {
      let encodedId = encodeURIComponent(id);
      const response = await axios.get(`/document/body/${encodedId}`);
      setHtmlContent({ ...htmlContent, [id]: response.data[0].body });
      setHiddenRows((prevHiddenRows) => ({
        ...prevHiddenRows,
        [id]: !prevHiddenRows[id],
      }));
    } catch (error) {
      setErrorMessage('Error fetching document body');
    }
  };

  const [searchTerm, setSearchTerm] = useState('');
  function handleSearchTerm(searchTerm) {
    setSearchTerm(searchTerm);
    if (searchTerm === '') {
      fetchDocuments();
    }
  }
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
      <div>
        <h2>Додати новий документ</h2>
        <div>
          <input
            type='text'
            placeholder='Ідентифікаційний номер...'
            id='id'
            value={newDocument.id}
            onChange={(event) =>
              setNewDocument({ ...newDocument, id: event.target.value })
            }
          />
          <button onClick={handleAddDocument} style={{ outline: 'none' }}>
            Додати
          </button>
        </div>
        <br />
      </div>
      <div>
        <h2>Фільтрація</h2>
        <label htmlFor='idFilter'>За ідентифікаційним номером:</label>
        <input
          type='text'
          value={idFilter}
          onChange={handleIdFilterChange}
          id='idFilter'
        />
        <label htmlFor='nameFilter'>За назвою:</label>
        <input
          type='text'
          value={nameFilter}
          onChange={handleNameFilterChange}
          id='nameFilter'
        />
        <label htmlFor='dateFilter'>За датою створення:</label>
        <input
          type='date'
          value={dateFilter ? dateFilter : ''}
          onChange={handleDateFilterChange}
          id='dateFilter'
        />
      </div>
      <br />
      <div>
        <h2>Список документів</h2>
        <input
          type='text'
          placeholder='Фраза для пошуку...'
          value={searchTerm}
          onChange={(event) => handleSearchTerm(event.target.value)}
        />
        <button onClick={handleSearch} style={{ outline: 'none' }}>
          Знайти
        </button>
      </div>
      {filteredDocuments.length > 0 ? (
        <div>
          <table
            style={{
              borderCollapse: 'collapse',
              border: '1px solid black',
              margin: '0 20px 0 20px',
            }}
          >
            <thead>
              <tr>
                <th style={{ border: '1px solid black', padding: '8px' }}>#</th>
                <th style={{ border: '1px solid black', padding: '8px' }}>
                  Назва
                </th>
                <th style={{ border: '1px solid black', padding: '8px' }}>
                  Дата створення
                </th>
                <th style={{ border: '1px solid black', padding: '8px' }}>
                  Оновлено
                </th>
                <th style={{ border: '1px solid black', padding: '8px' }}>
                  Дія
                </th>
                <th style={{ border: '1px solid black', padding: '8px' }}>
                  Документ
                </th>
              </tr>
            </thead>
            <tbody>
              {filteredDocuments.map((document) => (
                <tr key={document.id}>
                  <td
                    style={{
                      border: '1px solid black',
                      padding: '8px',
                      width: '75px',
                    }}
                  >
                    {document.id}
                  </td>
                  <td
                    style={{
                      border: '1px solid black',
                      padding: '8px',
                      width: '225px',
                    }}
                  >
                    {document.name}
                  </td>
                  <td
                    style={{
                      border: '1px solid black',
                      padding: '8px',
                      width: '30px',
                    }}
                  >
                    {new Date(document.created_on).toLocaleDateString()}
                  </td>
                  <td
                    style={{
                      border: '1px solid black',
                      padding: '8px',
                      width: '95px',
                    }}
                  >
                    {new Date(document.updated_on).toLocaleString()}
                  </td>
                  <td style={{ border: '1px solid black', padding: '8px' }}>
                    <button
                      onClick={() => handleUpdateDocument(document.id)}
                      style={{ outline: 'none' }}
                    >
                      Оновити
                    </button>
                    <button
                      onClick={() => handleRemoveDocument(document.id)}
                      style={{ outline: 'none' }}
                    >
                      Видалити
                    </button>
                  </td>
                  <td
                    style={{
                      border: '1px solid black',
                      padding: '8px',
                      width: '676.8px',
                    }}
                  >
                    {htmlContent[document.id] && !hiddenRows[document.id] && (
                      <div
                        className='inner-html-content'
                        dangerouslySetInnerHTML={{
                          __html: htmlContent[document.id],
                        }}
                      />
                    )}
                    <button
                      onClick={() => handleInsertHtml(document.id)}
                      style={{ outline: 'none' }}
                    >
                      {hiddenRows[document.id]
                        ? 'Завантажити документ'
                        : 'Приховати документ'}
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <br />
        </div>
      ) : (
        <div>
          <br />
          <br />
          <h3 style={{ color: 'red' }}>Документи не знайдено</h3>
        </div>
      )}
    </div>
  );
}

export default Document;
