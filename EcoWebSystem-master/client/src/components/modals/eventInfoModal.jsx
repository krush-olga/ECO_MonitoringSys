import React, { useState } from 'react';
import { Button, Card, Form, ListGroup } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';

import { deleteRequest, post } from '../../utils/httpService';
import { VerticallyCenteredModal } from './modal';
import { EVENT_DOCUMENT_URl } from '../../utils/constants';

export const EventInfoModal = ({
  show,
  onHide,
  user,
  setShouldFetchData,
  event,
}) => {
  const [documentCode, setDocumentCode] = useState('');
  const [documentDescription, setDocumentDescription] = useState('');
  console.log(event);

  const hide = () => {
    onHide();
  };

  const handleAddDocument = () => {
    if (documentCode && documentDescription) {
      post(EVENT_DOCUMENT_URl, {
        document_code: documentCode,
        description: documentDescription,
      })
        .then(() => {
          setShouldFetchData(true);
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

  const handleRemoveDocument = (document_code, event_id) => {
    deleteRequest(EVENT_DOCUMENT_URl, {
      document_code: documentCode,
      description: documentDescription,
    })
      .then(() => {
        setShouldFetchData(true);
        setDocumentDescription('');
        setDocumentCode('');
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
        header={event?.name || 'Деталі по задачі'}
      >
        {event?.description && <p>Опис: {event?.description}</p>}
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
                        onClick={() =>
                          handleRemoveDocument(
                            doc?.document_code,
                            doc?.description
                          )
                        }
                        icon={faTrashAlt}
                        className='cursor-pointer'
                      />
                    </div>
                    <p className='mb-0'>Опис: {doc?.description}</p>
                  </ListGroup.Item>
                ))}
              </ListGroup>
            </Card>
          </>
        ) : null}

        <Form className='mt-3'>
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
