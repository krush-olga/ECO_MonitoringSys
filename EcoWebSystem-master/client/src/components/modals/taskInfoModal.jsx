import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Card, Form, ListGroup } from 'react-bootstrap';
import {
  faEye,
  faPencilAlt,
  faTrashAlt,
} from '@fortawesome/free-solid-svg-icons';

import { get, deleteRequest, post } from '../../utils/httpService';
import { EVENTS_URl, TASK_DOCUMENT_URl } from '../../utils/constants';

import { VerticallyCenteredModal } from './modal';
import { AddEventModal } from '../addComponents/addEventModal';
import { EventInfoModal } from './eventInfoModal';

export const TaskInfoModal = ({ show, onHide, user, task }) => {
  const [documentCode, setDocumentCode] = useState('');
  const [documentDescription, setDocumentDescription] = useState('');
  const [documents, setDocuments] = useState([]);
  const [events, setEvents] = useState([]);
  const [isAddEventModalShown, setIsAddEventModalShown] = useState(false);
  const [shouldFetchDocuments, setShouldFetchDocuments] = useState(false);
  const [shouldFetchEvents, setShouldFetchEvents] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [isEventInfoModalShown, setIsEventInfoModalShown] = useState(false);

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
        {events.length ? (
          <>
            <p className='text-center mb-1 bold'>Заходи:</p>
            <Card>
              <ListGroup variant='flush'>
                {events.map((event) => (
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
          <p className='text-center bold'>Заходів поки що немає</p>
        )}
        <Button
          variant='primary'
          className='text-center mt-2'
          onClick={() => setIsAddEventModalShown(true)}
        >
          Додати захід
        </Button>

        {documents.length ? (
          <>
            <p className='text-center mb-1 bold'>Документи:</p>
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

        <p className='my-3 text-center bold'>Додати документ</p>
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
