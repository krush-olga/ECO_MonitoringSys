import React, { useState } from 'react';
import { Button, Form } from 'react-bootstrap';
import { TASK_URL } from '../../utils/constants';

import { post } from '../../utils/httpService';

import { VerticallyCenteredModal } from '../modals/modal';

const initialState = {
  form: {
    name: '',
    description: '',
    Tema: '',
  },
};

export const AddTaskModal = ({
  show,
  onHide,
  user,
  setShouldFetchData,
  isEditTaskMode,
  setIsEditTaskMode,
}) => {
  const [name, setName] = useState(initialState.form.name);
  const [Tema, setTema] = useState(initialState.form.Tema);
  const [description, setDescription] = useState(initialState.form.description);

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
      console.log(user, name, description, Tema);
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

  return (
    <VerticallyCenteredModal
      size='lg'
      show={show}
      onHide={() => hide()}
      header={!isEditTaskMode ? 'Додати задачу' : 'Редагувати задачу'}
    >
      <Form>
        <Form.Group>
          <Form.Label>Введіть ім'я задачі</Form.Label>
          <Form.Control
            type='input'
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </Form.Group>

        <Form.Group>
          <Form.Label>Додайте опис задачі</Form.Label>
          <Form.Control
            as='textarea'
            rows='3'
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
        </Form.Group>

        <Form.Group>
          <Form.Label>Додайте тему задачі</Form.Label>
          <Form.Control
            as='input'
            value={Tema}
            onChange={(e) => setTema(e.target.value)}
          />
        </Form.Group>
        {isEditTaskMode ? (
          <Button size={'sm'} onClick={() => editTask()}>
            Редагувати задачу
          </Button>
        ) : (
          <Button size={'sm'} onClick={() => addTask()}>
            Додати задачу
          </Button>
        )}
      </Form>
    </VerticallyCenteredModal>
  );
};
