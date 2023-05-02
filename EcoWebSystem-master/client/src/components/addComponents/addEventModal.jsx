import React, { useEffect, useState } from 'react';
import { Button, Dropdown, DropdownButton, Form } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';

import { EVENTS_URl, RESOURCES_URL } from '../../utils/constants';

import { get, post } from '../../utils/httpService';

import { VerticallyCenteredModal } from '../modals/modal';

const initialState = {
  form: {
    name: '',
    description: '',
    resources: [],
  },
};

export const AddEventModal = ({
  show,
  onHide,
  user,
  setShouldFetchData,
  isEditEventMode,
  setIsEditEventMode,
  issue_id,
}) => {
  const [name, setName] = useState(initialState.form.name);
  const [description, setDescription] = useState(initialState.form.description);
  const [selectedResources, setSelectedResources] = useState([]);
  const [allResources, setAllResources] = useState([]);

  const hide = () => {
    onHide();
    clearForm();
  };

  const clearForm = () => {
    setName(initialState.form.name);
    setDescription(initialState.form.description);
    setSelectedResources(initialState.form.resources);
    setIsEditEventMode(false);
  };

  // useEffect(() => {
  //   if (tubeId && isEditEventMode) {
  //     get(`${TUBE_URl}/${tubeId}`).then(({ data }) => {
  //       setName(data.name);
  //       setDescription(data.description);
  //     });
  //   }
  // }, [tubeId, isEditEventMode]);

  const addEvent = () => {
    if (name && description && selectedResources.length && user?.id_of_user) {
      console.log(
        name,
        description,
        selectedResources,
        issue_id,
        user.id_of_user
      );

      post(EVENTS_URl, {
        name,
        description,
        issue_id,
        id_of_user: user.id_of_user,
        resources: selectedResources,
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
        'Заповніть такі поля:\n-назва\n-опис\n-ресурси\nТа увійдіть в систему'
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
          setIsEditEventMode(false);
          settubeId(null);
        })
        .catch((error) => {
          alert('Помилка при редагуванні даних.');
          console.log(error);
          setIsEditEventMode(false);
          settubeId(null);
          setnewTubeCordinates([]);
          setShouldFetchData(false);
        });*/
  };

  useEffect(() => {
    if (show) {
      get(RESOURCES_URL).then(({ data }) => {
        setAllResources(data);
      });
    }
  }, [show]);

  return (
    <VerticallyCenteredModal
      size='lg'
      show={show}
      onHide={() => hide()}
      header={!isEditEventMode ? 'Додати захід' : 'Редагувати захід'}
    >
      <Form>
        <Form.Group>
          <Form.Label>Введіть ім'я заходу</Form.Label>
          <Form.Control
            type='input'
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </Form.Group>

        <Form.Group>
          <Form.Label>Додайте опис заходу</Form.Label>
          <Form.Control
            as='textarea'
            rows='3'
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
        </Form.Group>

        <DropdownButton id='Resources' className='my-2' title='Оберіть ресурси'>
          {allResources.length ? (
            allResources.map((resource, index) => (
              <Dropdown.Item
                key={index}
                onClick={() => {
                  setSelectedResources((prev) => {
                    const isResourceAlreadyAdded = prev.find(
                      (res) => res.resource_id === resource.resource_id
                    );
                    if (isResourceAlreadyAdded) {
                      return prev;
                    }

                    return [
                      ...prev,
                      {
                        resource_id: resource.resource_id,
                        name: resource.name,
                        description: resource.description,
                        value: '',
                      },
                    ];
                  });
                }}
              >
                {resource.name}
              </Dropdown.Item>
            ))
          ) : (
            <Dropdown.Item>Немає ресурсів</Dropdown.Item>
          )}
        </DropdownButton>

        {selectedResources.length
          ? selectedResources.map((resource) => (
              <div
                key={resource.resource_id}
                className='d-flex justify-content-between align-items-center'
              >
                <p className='mb-0'>{resource.name}</p>
                <Form.Group>
                  <Form.Label>Задайте кількість</Form.Label>
                  <Form.Control
                    value={resource.value}
                    onChange={(e) => {
                      e.persist();
                      setSelectedResources((prev) =>
                        prev.map((res) =>
                          res.resource_id === resource.resource_id
                            ? {
                                ...resource,
                                value:
                                  Number(e?.target?.value) &&
                                  Number(e?.target?.value > 0)
                                    ? Number(e?.target?.value)
                                    : resource.value,
                              }
                            : res
                        )
                      );
                    }}
                  />
                </Form.Group>
                <FontAwesomeIcon
                  icon={faTrashAlt}
                  className='cursor-pointer'
                  onClick={() => {
                    setSelectedResources((prev) =>
                      prev.filter(
                        (res) => res.resource_id !== resource.resource_id
                      )
                    );
                  }}
                />
              </div>
            ))
          : null}

        {isEditEventMode ? (
          <Button size={'sm'} onClick={() => editTask()}>
            Редагувати захід
          </Button>
        ) : (
          <Button size={'sm'} onClick={() => addEvent()}>
            Додати захід
          </Button>
        )}
      </Form>
    </VerticallyCenteredModal>
  );
};
