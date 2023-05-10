import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Card, Form, ListGroup } from 'react-bootstrap';
import {
  faEye,
  faPencilAlt,
  faTrashAlt,
} from '@fortawesome/free-solid-svg-icons';
import jsPDF from 'jspdf';
import 'jspdf-autotable';

import { deleteRequest, get, post } from '../../utils/httpService';
import { EVENTS_URl, roles, TASK_DOCUMENT_URl } from '../../utils/constants';

import { VerticallyCenteredModal } from './modal';
import { AddEventModal } from '../addComponents/addEventModal';
import { EventInfoModal } from './eventInfoModal';

import '../../fonts/Roboto-Regular-normal';

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

  const handleGeneratePdf = async () => {
    const doc = new jsPDF({
      orientation: 'portrait',
      format: 'a4',
      compress: true,
      margin: {
        top: 20,
        bottom: 20,
        left: 20,
        right: 20,
      },
    });

    doc.addFont('Roboto-Regular-normal.ttf', 'Roboto-Regular', 'normal');
    doc.setFont('Roboto-Regular', 'normal');

    doc.setFontSize(18);
    doc.text(
      task?.name || 'Детали по задаче',
      doc.internal.pageSize.width / 2,
      10,
      { align: 'center' }
    );
    doc.setFontSize(16);

    if (task?.thema) {
      doc.text(`Тема: ${task.thema}`, 10, 30);
    }

    if (task?.description) {
      doc.text(`Опис: ${task.description}`, 10, 40);
    }

    if (task?.budget) {
      doc.text(`Бюджет: ${task.budget} грн`, 10, 50);
    }

    const eventsStartY = 60;
    const eventsLineHeight = 10;
    const eventsMaxWidth = doc.internal.pageSize.width - 40;
    let count = 0;

    doc.setFontSize(17);
    doc.text('Заходи', doc.internal.pageSize.width / 2, eventsStartY, {
      align: 'center',
    });
    doc.setFontSize(16);

    let eventsY = eventsStartY + 5;

    filteredEvents.forEach((event) => {
      if (count > 1) {
        doc.addPage();
        eventsY = 10;
        count = 0;
      } else {
        count++;
      }

      const {
        name,
        description,
        expert_name,
        lawyer_vefirication,
        dm_verification,
        resources,
      } = event;

      doc.setFontSize(15);
      const nameLines = doc.splitTextToSize(name, eventsMaxWidth);
      const nameHeight = nameLines.length * eventsLineHeight;
      const nameStartY = eventsY + nameHeight / 2;
      doc.text(nameLines, doc.internal.pageSize.width / 2, nameStartY, {
        align: 'center',
      });

      let y = eventsY + nameHeight + 5;

      doc.setFontSize(12);
      const descriptionLines = doc.splitTextToSize(description, eventsMaxWidth);
      doc.text(`Опис: ${descriptionLines}`, 10, y);

      y += eventsLineHeight;

      // Выводим информацию об эксперте
      if (expert_name) {
        doc.setFontSize(12);
        doc.text(`Експерт: ${expert_name}`, 10, y);
        y += eventsLineHeight;
      }

      // Выводим информацию о решении юриста
      if (lawyer_vefirication !== null) {
        doc.setFontSize(12);
        doc.text('Рішення юриста:', 10, y);
        doc.setFontSize(11);
        doc.text(lawyer_vefirication ? 'Схвалено' : 'Не схвалено', 45, y);
        y += eventsLineHeight;
      }

      // Выводим информацию о решении аналитика
      if (dm_verification !== null) {
        doc.setFontSize(12);
        doc.text('Рішення аналітика:', 10, y);
        doc.setFontSize(11);
        doc.text(dm_verification ? 'Схвалено' : 'Не схвалено', 50, y);
        y += eventsLineHeight;
      }

      if (resources.length) {
        const columns = ['Назва', 'Одиниця', 'Кількість', 'Ціна', 'Вартість'];
        const rows = resources.map((resource) => [
          resource.name,
          resource?.units,
          resource.value,
          resource.price,
          resource.price * resource.value,
        ]);

        doc.autoTable(columns, rows, {
          startY: y,
          margin: { top: 20 },
          styles: { font: 'Roboto-Regular', fontSize: 10 },
          headStyles: {
            font: 'Roboto-Regular',
            fontSize: 12,
            fillColor: [191, 191, 191],
          },
          columnStyles: { 0: { cellWidth: 70 } },
          bodyStyles: {
            font: 'Roboto-Regular',
            fontSize: 10,
            textColor: [0, 0, 0],
          },
          drawCell: function (cell, opts) {
            if (opts.row.index === rows.length - 1) {
              if (opts.column.index === 0) {
                cell.styles.fontStyle = 'bold';
              } else if (opts.column.index === 4) {
                cell.styles.fontStyle = 'bold';
              }
            }
          },
        });

        y = doc.lastAutoTable.finalY + 10;

        // Total cost
        const totalCost = event.resources.reduce(
          (acc, cur) => (acc += cur.price * cur.value),
          0
        );
        doc.setFontSize(12);
        doc.text(`Загальна сума: ${totalCost} грн`, 30, y, { align: 'center' });
      }

      eventsY = y + 10;
    });

    const pdfDataUri = doc.output('bloburl', {
      filename: 'звіт.pdf',
      charset: 'utf-8',
    });

    window.open(pdfDataUri, '_blank');
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
        {task?.budget && <p>Бюджет: {task?.budget} грн</p>}

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
                    <p className='mb-0'>{`Експерт: ${event.expert_name}`}</p>
                    <div className='d-flex'>
                      <FontAwesomeIcon
                        icon={faEye}
                        className='cursor-pointer'
                        onClick={() => {
                          setSelectedEvent(event);
                          setIsEventInfoModalShown(true);
                        }}
                      />
                      {user.id_of_expert === roles.admin ||
                      user.id_of_user === event.id_of_user ? (
                        <FontAwesomeIcon
                          icon={faPencilAlt}
                          className='mx-2 d-inline-block cursor-pointer'
                          onClick={() => {
                            setSelectedEvent(event);
                            setIsAddEventModalShown(true);
                          }}
                        />
                      ) : null}
                      {user.id_of_expert === roles.admin ||
                      user.id_of_user === event.id_of_user ? (
                        <FontAwesomeIcon
                          onClick={() => handleRemoveEvent(event?.event_id)}
                          icon={faTrashAlt}
                          className='cursor-pointer'
                        />
                      ) : null}
                    </div>
                  </ListGroup.Item>
                ))}
              </ListGroup>
            </Card>
            {user.id_of_expert === roles.admin ||
            user.id_of_expert === roles.analyst ? (
              <Button
                variant='success'
                className='text-center mt-2 d-block'
                onClick={handleGeneratePdf}
              >
                Сгенерувати PDF
              </Button>
            ) : null}
          </>
        ) : (
          <p className='text-center fw-bold'>Заходів поки що немає</p>
        )}

        {user.id_of_expert !== roles.lawyer &&
        user.id_of_expert !== roles.analyst ? (
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
        ) : null}

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
                      {user.id_of_expert === roles.admin ||
                      user.id_of_expert === roles.lawyer ? (
                        <FontAwesomeIcon
                          onClick={() =>
                            handleRemoveDocument(doc?.document_code)
                          }
                          icon={faTrashAlt}
                          className='cursor-pointer'
                        />
                      ) : null}
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

        {user.id_of_expert === roles.admin ||
        user.id_of_expert === roles.lawyer ? (
          <>
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
          </>
        ) : null}
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
        user={user}
        setShouldFetchData={setShouldFetchEvents}
        event={selectedEvent}
        setEvent={setSelectedEvent}
      />
    </>
  );
};
