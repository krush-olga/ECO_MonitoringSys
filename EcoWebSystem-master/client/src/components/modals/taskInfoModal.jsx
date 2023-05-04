import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Card, ListGroup } from 'react-bootstrap';
import {
  faEye,
  faPencilAlt,
  faTrashAlt,
} from '@fortawesome/free-solid-svg-icons';

import { get, deleteRequest } from '../../utils/httpService';
import { EVENTS_URl } from '../../utils/constants';

import { VerticallyCenteredModal } from './modal';
import { AddEventModal } from '../addComponents/addEventModal';
import { EventInfoModal } from './eventInfoModal';

export const TaskInfoModal = ({ show, onHide, user, task }) => {
  const [events, setEvents] = useState([]);

  const [isAddEventModalShown, setIsAddEventModalShown] = useState(false);
  const [shouldFetchData, setShouldFetchData] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [isEventInfoModalShown, setIsEventInfoModalShown] = useState(false);

  const hide = () => {
    onHide();
  };

  const getEvents = () => {
    get(`${EVENTS_URl}?taskId=${task?.issue_id}`).then(({ data }) => {
      setEvents(data);
    });
  };

  useEffect(() => {
    if (show) {
      getEvents();
    }
  }, [show, shouldFetchData]);

  useEffect(() => {
    if (shouldFetchData) {
      getEvents();
      setShouldFetchData(false);
    }
  }, [shouldFetchData]);

  const handleRemoveEvent = (event_id) => {
    deleteRequest(`${EVENTS_URl}/${event_id}`)
      .then(() => {
        setShouldFetchData(true);
      })
      .catch((error) => {
        alert('Помилка при видаленні даних.');
        console.log(error);
        setShouldFetchData(false);
      });
  };

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
      </VerticallyCenteredModal>
      <AddEventModal
        show={isAddEventModalShown}
        onHide={() => {
          setIsAddEventModalShown(false);
          setSelectedEvent(null);
        }}
        user={user}
        setShouldFetchData={setShouldFetchData}
        issue_id={task?.issue_id}
        event={selectedEvent}
      />
      <EventInfoModal
        show={isEventInfoModalShown}
        onHide={() => {
          setIsEventInfoModalShown(false);
          setSelectedEvent(null);
        }}
        user={user}
        setShouldFetchData={setShouldFetchData}
        event={selectedEvent}
      />
    </>
  );
};
