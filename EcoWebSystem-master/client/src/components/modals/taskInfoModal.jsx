import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Card, ListGroup } from 'react-bootstrap';
import {
  faEye,
  faPencilAlt,
  faTrashAlt,
} from '@fortawesome/free-solid-svg-icons';

import { get, post } from '../../utils/httpService';
import { EVENTS_URl, TASK_URL } from '../../utils/constants';

import { VerticallyCenteredModal } from './modal';
import { AddEventModal } from '../addComponents/addEventModal';

const initialState = {
  form: {
    name: '',
    description: '',
    Tema: '',
  },
};

export const TaskInfoModal = ({
  show,
  onHide,
  user,
  setShouldFetchData,
  isEditTaskMode,
  setIsEditTaskMode,
  task,
}) => {
  const [name, setName] = useState(initialState.form.name);
  const [Tema, setTema] = useState(initialState.form.Tema);
  const [description, setDescription] = useState(initialState.form.description);
  const [events, setEvents] = useState([]);

  const [isAddEventModalShown, setIsAddEventModalShown] = useState(false);
  const [isAddEventModalEditMode, setIsEventModalEditMode] = useState(false);

  const hide = () => {
    onHide();
    clearForm();
  };

  const clearForm = () => {
    setName(initialState.form.name);
    setDescription(initialState.form.description);
    setTema(initialState.form.Tema);
    setIsEditTaskMode(false);
  };

  // useEffect(() => {
  //   if (tubeId && isEditTaskMode) {
  //     get(`${TUBE_URl}/${tubeId}`).then(({ data }) => {
  //       setName(data.name);
  //       setDescription(data.description);
  //     });
  //   }
  // }, [tubeId, isEditTaskMode]);

  const addTask = () => {
    if (name && description && Tema && user?.id_of_user) {
      post(TASK_URL, {
        name,
        description,
        Tema,
      })
        .then(() => {
          hide();
          setShouldFetchData(true);
        })
        .catch((error) => {
          alert('Помилка при додаванні даних.');
          console.log(error);
          setShouldFetchData(false);
        });
    } else {
      alert(
        'Заповніть такі поля:\n-назва\n-опис\n-тема\nТа увійдіть в систему'
      );
    }
  };

  const editTask = () => {
    /*
      put(`${TUBE_URl}/${tubeId}`, {
        brush_color_r: color.r,
        bruch_color_g: color.g,
        brush_color_b: color.b,
        brush_alfa: color.a,
        line_collor_r: color.r,
        line_color_g: color.g,
        line_color_b: color.b,
        line_alfa: color.a,
        line_thickness: Number(lineThickness),
        name,
        description,
      })
        .then(() => {
          clearForm();
          onHide();
          setnewTubeCordinates([]);
          setShouldFetchData(true);
          setIsEditTaskMode(false);
          settubeId(null);
        })
        .catch((error) => {
          alert('Помилка при редагуванні даних.');
          console.log(error);
          setIsEditTaskMode(false);
          settubeId(null);
          setnewTubeCordinates([]);
          setShouldFetchData(false);
        });*/
  };

  useEffect(() => {
    if (show) {
      get(`${EVENTS_URl}?taskId=${task?.issue_id}`).then(({ data }) => {
        setEvents(data);
      });
    }
  }, [show]);

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
                      />
                      <FontAwesomeIcon
                        icon={faPencilAlt}
                        className='mx-2 d-inline-block cursor-pointer'
                      />
                      <FontAwesomeIcon
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
        onHide={() => setIsAddEventModalShown(false)}
        user={user}
        setShouldFetchData={setShouldFetchData}
        isEditEventMode={isAddEventModalEditMode}
        setIsEditEventMode={setIsEventModalEditMode}
        issue_id={task?.issue_id}
      />
    </>
  );
};
