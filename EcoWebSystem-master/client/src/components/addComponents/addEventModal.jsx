import React, { useEffect, useState } from 'react';
import { Button, Dropdown, DropdownButton, Form } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';

import { EVENTS_URl, RESOURCES_URL } from '../../utils/constants';

import { get, post, put } from '../../utils/httpService';

import { VerticallyCenteredModal } from '../modals/modal';

const initialState = {
  form: {
    name: '',
    description: '',
    resources: [],
    weight: 0,
  },
};

export const AddEventModal = ({
  show,
  onHide,
  user,
  setShouldFetchData,
  issue_id,
  event,
}) => {
  const [name, setName] = useState(initialState.form.name);
  const [description, setDescription] = useState(initialState.form.description);
  const [selectedResources, setSelectedResources] = useState(
    initialState.form.resources
  );
  const [weight, setWeight] = useState(initialState.form.weight);
  const [allResources, setAllResources] = useState([]);
  const [isEditMode, setIsEditMode] = useState(false);

  const hide = () => {
    onHide();
    clearForm();
  };

  const clearForm = () => {
    setName(initialState.form.name);
    setDescription(initialState.form.description);
    setSelectedResources(initialState.form.resources);
    setWeight(initialState.form.weight);
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
    if (
      name &&
      description &&
      selectedResources.length &&
      weight >= 0 &&
      weight <= 1 &&
      user?.id_of_user
    ) {
      post(EVENTS_URl, {
        name,
        description,
        issue_id,
        id_of_user: user.id_of_user,
        id_of_expert: user.id_of_expert,
        resources: selectedResources,
        weight,
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
        'Заповніть такі поля:\n-назва\n-опис\n-ресурси\n-вага\nТа увійдіть в систему'
      );
    }
  };

  const editTask = () => {
    if (
      name &&
      description &&
      selectedResources.length &&
      weight >= 0 &&
      weight <= 1 &&
      user?.id_of_user &&
      event?.event_id
    ) {
      put(`${EVENTS_URl}/${event?.event_id}`, {
        name,
        description,
        resources: selectedResources,
        weight,
      })
        .then(() => {
          hide();
          setShouldFetchData(true);
        })
        .catch((error) => {
          alert('Помилка при редагуванні даних.');
          console.log(error);
          setShouldFetchData(false);
        });
    } else {
      alert(
        'Заповніть такі поля:\n-назва\n-опис\n-ресурси\nТа увійдіть в систему'
      );
    }
  };

  useEffect(() => {
    if (show) {
      get(RESOURCES_URL).then(({ data }) => {
        setAllResources(data);
      });
    }
  }, [show]);

  useEffect(() => {
    if (event) {
      setIsEditMode(true);
      setName(event.name);
      setDescription(event.description);
      setSelectedResources(event.resources);
      setWeight(event.weight);
    } else {
      clearForm();
      setIsEditMode(false);
    }
  }, [event]);

  return (
    <VerticallyCenteredModal
      size='lg'
      show={show}
      onHide={() => hide()}
      header={isEditMode ? 'Редагувати захід' : 'Додати захід'}
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

        <Form.Group>
          <Form.Label>Встановіть вагу заходу від 0 до 1</Form.Label>
          <Form.Control
            value={weight}
            onChange={(e) => setWeight(e.target.value)}
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
                        price: resource.price,
                        units: resource.units,
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
                <p className='mb-0'>
                  {resource.name}, {resource?.units}. Ціна: {resource.price}
                </p>
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
        <Button size={'sm'} onClick={isEditMode ? editTask : addEvent}>
          {isEditMode ? 'Редагувати захід' : 'Додати захід'}
        </Button>
      </Form>
    </VerticallyCenteredModal>
  );
};
