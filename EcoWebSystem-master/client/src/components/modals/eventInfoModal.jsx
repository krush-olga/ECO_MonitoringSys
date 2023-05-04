import React, { useEffect, useState } from 'react';
import { Button, Card, Form, ListGroup } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';

import { deleteRequest, post, put } from '../../utils/httpService';
import { VerticallyCenteredModal } from './modal';
import {
  DM_VERIFICATION_URl,
  EVENT_DOCUMENT_URl,
  LAWYER_VERIFICATION_URl,
} from '../../utils/constants';

export const EventInfoModal = ({
  show,
  onHide,
  setShouldFetchData,
  event,
  setEvent,
}) => {
  const [documentCode, setDocumentCode] = useState('');
  const [documentDescription, setDocumentDescription] = useState('');

  const [lawyerVerification, setLawyerVerification] = useState(null);
  const [dmVerification, setDmVerification] = useState(null);

  const hide = () => {
    onHide();
    setDocumentDescription('');
    setDocumentCode('');
    setEvent(null);
    setLawyerVerification(null);
    setDmVerification(null);
  };

  const handleAddDocument = () => {
    if (documentCode && documentDescription) {
      post(EVENT_DOCUMENT_URl, {
        document_code: documentCode,
        description: documentDescription,
        event_id: event?.event_id,
      })
        .then(() => {
          setShouldFetchData(true);
          setEvent((prev) => ({
            ...prev,
            documents: [
              ...prev.documents,
              {
                document_code: documentCode,
                description: documentDescription,
                event_id: event?.event_id,
              },
            ],
          }));
          setDocumentDescription('');
          setDocumentCode('');
        })
        .catch((error) => {
          alert('Помилка при додаванні даних.');
          console.log(error);
          setShouldFetchData(false);
        });
    } else {
      alert('Заповніть такі поля:\n-код документу\n-опис\n');
    }
  };

  const handleRemoveDocument = (document_code) => {
    deleteRequest(EVENT_DOCUMENT_URl, {
      document_code: document_code,
      event_id: event?.event_id,
    })
      .then(() => {
        setShouldFetchData(true);
        setEvent((prev) => ({
          ...prev,
          documents: prev.documents.filter(
            (doc) => doc.document_code !== document_code
          ),
        }));
        setDocumentDescription('');
        setDocumentCode('');
      })
      .catch((error) => {
        alert('Помилка при видаленні даних.');
        console.log(error);
        setShouldFetchData(false);
      });
  };

  const handleLawyerVerificationUpdate = (value) => {
    put(`${LAWYER_VERIFICATION_URl}/${event?.event_id}`, {
      lawyer_vefirication: value,
    }).then((res) => setShouldFetchData(true));
  };

  const handleDmVerificationUpdate = (value) => {
    put(`${DM_VERIFICATION_URl}/${event?.event_id}`, {
      dm_verification: value,
    }).then((res) => setShouldFetchData(true));
  };

  useEffect(() => {
    switch (event?.lawyer_vefirication) {
      case 0:
        setLawyerVerification(0);
        break;
      case 1:
        setLawyerVerification(1);
        break;
      default:
        setLawyerVerification(null);
    }

    switch (event?.dm_verification) {
      case 0:
        setDmVerification(0);
        break;
      case 1:
        setDmVerification(1);
        break;
      default:
        setDmVerification(null);
    }
  }, [event]);

  return (
    <>
      <VerticallyCenteredModal
        size='lg'
        show={show}
        onHide={() => hide()}
        header={event?.name || 'Деталі по задачі'}
      >
        {event?.description && <p>Опис: {event?.description}</p>}

        <p className='mb-0'>Рішення юриста</p>
        <Form className='mb-1'>
          <Form.Check
            inline
            label='Не розглянуто'
            name='lawyer'
            type='radio'
            checked={lawyerVerification === null}
            id='lawyer-unchecked'
          />
          <Form.Check
            inline
            label='Схвалено'
            name='lawyer'
            type='radio'
            checked={lawyerVerification === 1}
            id='lawyer-approved'
            onChange={() => {
              setLawyerVerification(1);
              handleLawyerVerificationUpdate(1);
            }}
          />
          <Form.Check
            inline
            label='Не схвалено'
            name='lawyer'
            type='radio'
            checked={lawyerVerification === 0}
            id='lawyer-rejected'
            onChange={() => {
              setLawyerVerification(0);
              handleLawyerVerificationUpdate(0);
            }}
          />
        </Form>

        <p className='mb-0'>Рішення аналітика</p>
        <Form className='mb-1'>
          <Form.Check
            inline
            label='Не розглянуто'
            name='dm'
            type='radio'
            checked={dmVerification === null}
            id='dm-unchecked'
          />
          <Form.Check
            inline
            label='Схвалено'
            name='dm'
            type='radio'
            checked={dmVerification === 1}
            id='dm-approved'
            onChange={() => {
              setDmVerification(1);
              handleDmVerificationUpdate(1);
            }}
          />
          <Form.Check
            inline
            label='Не схвалено'
            name='dm'
            type='radio'
            checked={dmVerification === 0}
            id='dm-rejected'
            onChange={() => {
              setDmVerification(0);
              handleDmVerificationUpdate(0);
            }}
          />
        </Form>

        {event?.resources.length ? (
          <>
            <p className='text-center mb-1 bold'>Ресурси:</p>
            <Card>
              <ListGroup variant='flush'>
                {event?.resources.map((resource, index) => (
                  <ListGroup.Item
                    key={index}
                    className='d-flex align-items-center justify-content-between'
                  >
                    <p className='mb-0'>
                      {resource.name}, {resource?.units}
                    </p>
                    <p className='mb-0'>Кількість: {resource.value}</p>
                    <p className='mb-0'>
                      Вартість: {resource.price * resource.value}
                    </p>
                  </ListGroup.Item>
                ))}
              </ListGroup>
            </Card>
            <p className='mt-1'>
              Сума:{' '}
              {event.resources.reduce(
                (acc, cur) => (acc += cur.price * cur.value),
                0
              )}{' '}
              грн
            </p>
          </>
        ) : null}
        {event?.documents.length ? (
          <>
            <p className='text-center mb-1 bold'>Документи:</p>
            <Card>
              <ListGroup variant='flush'>
                {event?.documents.map((doc, index) => (
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
    </>
  );
};
