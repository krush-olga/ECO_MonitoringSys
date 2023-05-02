import React, { useEffect, useRef, useState } from 'react';
import { Button, Form } from 'react-bootstrap';

import { get } from '../../utils/httpService';
import { EXPERTS_URL, roles } from '../../utils/constants';
import { FiltarionByTasks } from '../filtrations/filtrationByTask';
import { useOnClickOutside } from '../helperComponents/outsideClick';
import { AddTaskModal } from '../addComponents/addTaskModal';
import { Pseudo } from '../helperComponents/pseudo/pseudo';

import './filtration.css';

export const Filtration = ({
  user,
  filteredPoints,
  setFilteredPoints,
  setFilteredItems,
  environmentsInfo,
  sideLeftFilterOpened,
  setLeftFilterOpened,
}) => {
  let filtrationForm;
  const [existingExperts, setExistingExperts] = useState([]);
  const [isTaskModalShow, setIsTaskModalShown] = useState(false);
  const [
    shouldFetchAddTaskModalData,
    setShouldFetchAddTaskModalData,
  ] = useState(false);
  const [isEditTaskMode, setIsEditTaskMode] = useState(false);

  const [environmentState, setenvironmentState] = useState('');

  const ref = useRef();
  useOnClickOutside(ref, () => {
    setLeftFilterOpened(false);
  });

  useEffect(() => {
    get(EXPERTS_URL).then(({ data }) => {
      setExistingExperts(data);
    });
    if (environmentsInfo.selected) {
      setenvironmentState(environmentsInfo.selected.name);
    }
  }, []);

  const submitHandler = (e) => {
    e.preventDefault();

    const { expertCheckbox: expertCheckboxes, myCheckbox } = filtrationForm;
    const selectedExperts = Array.from(expertCheckboxes)
      .filter(({ checked }) => checked)
      .map(({ value }) =>
        existingExperts.find(({ id_of_expert }) => +id_of_expert === +value)
      );

    if (myCheckbox && myCheckbox.checked) {
      setFilteredItems({
        isMyObjectsSelectionChecked: true,
        items: [...selectedExperts, user],
      });
    } else {
      setFilteredItems({
        isMyObjectsSelectionChecked: false,
        items: selectedExperts,
      });
    }
  };

  return (
    <div
      ref={ref}
      className={`filtration-form ${sideLeftFilterOpened ? '' : 'transLeft'}`}
    >
      <div>
        <div>
          <b>Обрана карта:</b>
          <p>{environmentState}</p>
          <hr />
        </div>
        <Form
          onSubmit={submitHandler}
          className='d-flex justify-content-center flex-column'
          ref={(form) => (filtrationForm = form)}
        >
          <Form.Group>
            <Form.Label>
              <b>Оберіть експерта</b>
            </Form.Label>
            {existingExperts.length &&
              existingExperts.map((expert) => (
                <Form.Check
                  label={expert.expert_name}
                  type='checkbox'
                  value={expert.id_of_expert}
                  key={expert.id_of_expert}
                  name='expertCheckbox'
                />
              ))}
            {user && (
              <Form.Check
                label="Мої об'єкти"
                type='checkbox'
                value={user.id_of_user}
                key={user.id_of_user}
                name='myCheckbox'
              />
            )}
          </Form.Group>

          <Button variant='primary' type='submit' className='text-center'>
            Застосувати
          </Button>
        </Form>
        {user && (
          <FiltarionByTasks
            filteredPoints={filteredPoints}
            setFilteredPoints={setFilteredPoints}
            setFilteredItems={setFilteredItems}
            user={user}
          />
        )}

        {(user?.id_of_expert === roles.admin ||
          user?.id_of_expert === roles.analyst) && (
          <>
            <hr />
            <Button
              variant='primary'
              className='text-center'
              onClick={() => setIsTaskModalShown(true)}
            >
              Створити задачу
            </Button>
          </>
        )}
      </div>
      <AddTaskModal
        user={user}
        show={isTaskModalShow}
        onHide={() => setIsTaskModalShown(false)}
        isEditTaskMode={isEditTaskMode}
        setIsEditTaskMode={setIsEditTaskMode}
        setShouldFetchData={setShouldFetchAddTaskModalData}
      />
      <Pseudo setOpened={setLeftFilterOpened} />
    </div>
  );
};
