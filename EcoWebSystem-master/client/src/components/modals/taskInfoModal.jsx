import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Card, Form, ListGroup } from 'react-bootstrap';
import {
  faEye,
  faPencilAlt,
  faTrashAlt,
} from '@fortawesome/free-solid-svg-icons';

import { deleteRequest, get, post } from '../../utils/httpService';
import { EVENTS_URl, TASK_DOCUMENT_URl } from '../../utils/constants';

import { VerticallyCenteredModal } from './modal';
import { AddEventModal } from '../addComponents/addEventModal';
import { EventInfoModal } from './eventInfoModal';

const filtrationOptions = [
  { value: 'all', label: 'Показати усі' },
  { value: 'lawyer', label: 'Схваленні юристом' },
  { value: 'dm', label: 'Схваленні аналітиком' },
  { value: 'budget', label: 'Не перевищують бюджет' },
];

export const TaskInfoModal = ({ show, onHide, user, task }) => {
  const [documentCode, setDocumentCode] = useState('');
  const [documentDescription, setDocumentDescription] = useState('');
  const [documents, setDocuments] = useState([]);
  const [events, setEvents] = useState([]);
  const [filteredEvents, setFilteredEvents] = useState([]);
  const [isAddEventModalShown, setIsAddEventModalShown] = useState(false);
  const [shouldFetchDocuments, setShouldFetchDocuments] = useState(false);
  const [shouldFetchEvents, setShouldFetchEvents] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [isEventInfoModalShown, setIsEventInfoModalShown] = useState(false);
  const [filters, setFilters] = useState(['all']);

  const hide = () => {
    onHide();
    setDocumentDescription('');
    setDocumentCode('');
  };

  const handleRemoveEvent = (event_id) => {
    deleteRequest(`${EVENTS_URl}/${event_id}`)
      .then(() => {
        setShouldFetchEvents(true);
      })
      .catch((error) => {
        alert('Помилка при видаленні даних.');
        console.log(error);
        setShouldFetchEvents(false);
      });
  };

  const handleAddDocument = () => {
    if (documentCode && documentDescription) {
      post(TASK_DOCUMENT_URl, {
        document_code: documentCode,
        description: documentDescription,
        issue_id: task?.issue_id,
      })
        .then(() => {
          setShouldFetchDocuments(true);
          setDocumentDescription('');
          setDocumentCode('');
        })
        .catch((error) => {
          alert('Помилка при додаванні даних.');
          console.log(error);
          setShouldFetchDocuments(false);
        });
    } else {
      alert('Заповніть такі поля:\n-код документу\n-опис\n');
    }
  };

  const handleRemoveDocument = (document_code) => {
    deleteRequest(TASK_DOCUMENT_URl, {
      document_code: document_code,
      issue_id: task?.issue_id,
    })
      .then(() => {
        setShouldFetchDocuments(true);
        setDocumentDescription('');
        setDocumentCode('');
      })
      .catch((error) => {
        alert('Помилка при видаленні даних.');
        console.log(error);
        setShouldFetchDocuments(false);
      });
  };

  const getEvents = () => {
    get(`${EVENTS_URl}?taskId=${task?.issue_id}`).then(({ data }) => {
      setEvents(data);
    });
  };

  const getDocuments = () => {
    get(`${TASK_DOCUMENT_URl}?taskId=${task?.issue_id}`).then(({ data }) => {
      setDocuments(data);
    });
  };

  const handleCheckboxChange = (event) => {
    const isChecked = event.target.checked;
    const filterValue = event.target.value;

    if (filterValue === 'all') {
      setFilters(['all']);
    } else {
      let newFilters;

      if (isChecked) {
        newFilters = [...filters, filterValue].filter(
          (value) => value !== 'all'
        );
      } else {
        newFilters = filters.filter((value) => value !== filterValue);

        if (newFilters.length === 0) {
          newFilters = ['all'];
        }
      }

      setFilters(newFilters);
    }
  };

  useEffect(() => {
    if (show) {
      getEvents();
      getDocuments();
    }
  }, [show]);

  useEffect(() => {
    if (shouldFetchEvents) {
      getEvents();
      setShouldFetchEvents(false);
    }
  }, [shouldFetchEvents]);

  useEffect(() => {
    if (shouldFetchDocuments) {
      getDocuments();
      setShouldFetchDocuments(false);
    }
  }, [shouldFetchDocuments]);

  useEffect(() => {
    let filtered = [];
    if (filters.includes('all')) {
      filtered = events;
    } else {
      filtered = events.filter((event) => {
        if (filters.includes('lawyer') && event.lawyer_vefirication !== 1) {
          return false;
        }

        if (filters.includes('dm') && event.dm_verification !== 1) {
          return false;
        }

        if (
          filters.includes('budget') &&
          event.resources.reduce(
            (acc, cur) => (acc += cur.price * cur.value),
            0
          ) > task?.budget
        ) {
          return false;
        }

        return true;
      });
    }
    setFilteredEvents(filtered);
  }, [events, filters]);

  console.log(filters);
  return (
    <>
      <VerticallyCenteredModal
        size='lg'
        show={show}
        onHide={() => hide()}
        header={task?.name || 'Деталі по задачі'}
      >
        {task?.thema && <p>Тема: {task?.thema}</p>}
        {task?.description && <p>Опис: {task?.description}</p>}
        {task?.budget && <p>Бюджет: {task?.budget}</p>}

        <p className='mb-0'>Застосувати фільтри</p>
        <Form className='mb-2'>
          {filtrationOptions.map((option) => (
            <Form.Check
              key={option.value}
              inline
              label={option.label}
              value={option.value}
              type='checkbox'
              checked={filters.includes(option.value)}
              onChange={handleCheckboxChange}
            />
          ))}
        </Form>

        {filteredEvents.length ? (
          <>
            <p className='text-center mb-1 fw-bold'>Заходи:</p>
            <Card>
              <ListGroup variant='flush'>
                {filteredEvents.map((event) => (
                  <ListGroup.Item
                    key={event?.event_id}
                    className='d-flex align-items-center justify-content-between'
                  >
                    <p className='mb-0'>{event.name}</p>
                    <div className='d-flex'>
                      <FontAwesomeIcon
                        icon={faEye}
                        className='cursor-pointer'
                        onClick={() => {
                          setSelectedEvent(event);
                          setIsEventInfoModalShown(true);
                        }}
                      />
                      <FontAwesomeIcon
                        icon={faPencilAlt}
                        className='mx-2 d-inline-block cursor-pointer'
                        onClick={() => {
                          setSelectedEvent(event);
                          setIsAddEventModalShown(true);
                        }}
                      />
                      <FontAwesomeIcon
                        onClick={() => handleRemoveEvent(event?.event_id)}
                        icon={faTrashAlt}
                        className='cursor-pointer'
                      />
                    </div>
                  </ListGroup.Item>
                ))}
              </ListGroup>
            </Card>
          </>
        ) : (
          <p className='text-center fw-bold'>Заходів поки що немає</p>
        )}
        <Button
          variant='primary'
          className='text-center mt-2'
          onClick={() => {
            setSelectedEvent(null);
            setIsAddEventModalShown(true);
          }}
        >
          Додати захід
        </Button>

        {documents.length ? (
          <>
            <p className='text-center mb-1 fw-bold'>Документи:</p>
            <Card>
              <ListGroup variant='flush'>
                {documents.map((doc, index) => (
                  <ListGroup.Item key={index}>
                    <div className='d-flex align-items-center justify-content-between'>
                      <p className='mb-1'>
                        Код документа: {doc?.document_code}
                      </p>
                      <FontAwesomeIcon
                        onClick={() => handleRemoveDocument(doc?.document_code)}
                        icon={faTrashAlt}
                        className='cursor-pointer'
                      />
                    </div>
                    {doc.description && (
                      <p className='mb-0'>Опис: {doc?.description}</p>
                    )}
                  </ListGroup.Item>
                ))}
              </ListGroup>
            </Card>
          </>
        ) : null}

        <p className='my-3 text-center fw-bold'>Додати документ</p>
        <Form>
          <Form.Group>
            <Form.Label>Введіть код документу</Form.Label>
            <Form.Control
              type='input'
              value={documentCode}
              onChange={(e) => setDocumentCode(e.target.value)}
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Додайте опис документу</Form.Label>
            <Form.Control
              type='input'
              value={documentDescription}
              onChange={(e) => setDocumentDescription(e.target.value)}
            />
          </Form.Group>
          <Button
            variant='primary'
            className='text-center mt-2'
            onClick={handleAddDocument}
          >
            Додати документ
          </Button>
        </Form>
      </VerticallyCenteredModal>
      <AddEventModal
        show={isAddEventModalShown}
        onHide={() => {
          setIsAddEventModalShown(false);
          setSelectedEvent(null);
        }}
        user={user}
        setShouldFetchData={setShouldFetchEvents}
        issue_id={task?.issue_id}
        event={selectedEvent}
      />
      <EventInfoModal
        show={isEventInfoModalShown}
        onHide={() => {
          setIsEventInfoModalShown(false);
          setSelectedEvent(null);
        }}
        setShouldFetchData={setShouldFetchEvents}
        event={selectedEvent}
        setEvent={setSelectedEvent}
      />
    </>
  );
};
