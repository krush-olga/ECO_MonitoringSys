import React, { useEffect, useState } from 'react';
import { Button, Dropdown, DropdownButton } from 'react-bootstrap';

import { get } from '../../utils/httpService.js';
import { roles, TASKS_URL } from '../../utils/constants.js';
import { TaskInfoModal } from '../modals/taskInfoModal';

import './filtrationByTask.css';
import { AddTaskModal } from '../addComponents/addTaskModal';

const initTitle = 'Список задач';

export const FiltarionByTasks = ({
  filteredPoints,
  setFilteredPoints,
  setFilteredItems,
  user,
}) => {
  const [Title, setTitle] = useState(initTitle);

  const [Chosen, setChosen] = useState(null);

  const [Tasks, setAllTasks] = useState([]);

  const [isEventModalShow, setIsEventModalShown] = useState(false);

  const [isTaskModalShow, setIsTaskModalShown] = useState(false);
  const [shouldFetchData, setShouldFetchData] = useState(false);
  const [isEditTaskMode, setIsEditTaskMode] = useState(false);

  const setTasks = () => {
    get(TASKS_URL).then(({ data }) => {
      let tempTasks = [];
      data.forEach((el) => {
        if (!tempTasks.some((elem) => elem.name === el.name)) {
          tempTasks.push({
            name: el.name,
            description: el.description,
            thema: el.Tema,
            issue_id: el.issue_id,
            object_arr: [],
          });
        }
      });
      // data.forEach((el) => {
      //   let obj = tempTasks.find((elem) => elem.name === el.name);
      //   if (obj) {
      //     obj.object_arr.push(el.id_of_object);
      //   }
      // });
      console.log(tempTasks);
      setAllTasks(tempTasks);
    });
  };

  const [shouldUpdate, setUpdate] = useState(false);

  useEffect(() => {
    if (Tasks.length === 0) {
      setTasks();
    }
  }, []);

  useEffect(() => {
    if (shouldFetchData) {
      setTasks();
      setShouldFetchData(false);
    }
  }, [shouldFetchData]);

  const DropHandlerClick = (element) => {
    setChosen(element.name);
    setTitle(
      element.name.length > 12
        ? element.name.substring(0, 10) + '...'
        : element.name
    );
    setUpdate(!shouldUpdate);

    /*let tempPoints = filteredPoints.filter((el) => {
      if (element.object_arr.some((id) => id === el.Id)) {
        return el;
      }
    });
    if (tempPoints.length > 0) {
      setFilteredPoints(tempPoints);
      setChosen(element.name);
      setTitle(
        element.name.length > 12
          ? element.name.substring(0, 10) + '...'
          : element.name
      );
      setUpdate(!shouldUpdate);
    } else {
      alert('По даній задачі нема маркерів');
    }*/
  };

  const ClearChosenList = () => {
    setFilteredItems({
      isMyObjectsSelectionChecked: false,
      items: [],
    });
    setTitle(initTitle);
    setChosen(null);
    setUpdate(!shouldUpdate);
  };

  return (
    <div>
      <hr />
      <b>Оберіть задачу</b>
      <DropdownButton
        onClick={() => {
          setTasks();
          setUpdate(!shouldUpdate);
        }}
        className='drop-tasks'
        title={Title}
      >
        {Tasks.length > 0 && (
          <>
            {Chosen && (
              <Dropdown.Item
                onClick={() => {
                  ClearChosenList();
                }}
              >
                Відміна
              </Dropdown.Item>
            )}

            {Tasks.map((el, index) => {
              if (!Chosen) {
                return (
                  <Dropdown.Item
                    key={index}
                    active={el.chosen}
                    onClick={() => {
                      DropHandlerClick(el);
                    }}
                  >
                    {el.name}
                  </Dropdown.Item>
                );
              } else {
                return <></>;
              }
            })}
          </>
        )}
        {Tasks.length === 0 ? <Dropdown.Item>Немає задач</Dropdown.Item> : ''}
      </DropdownButton>
      {Chosen && (
        <Button
          variant='primary'
          className='text-center mt-2'
          onClick={() => setIsEventModalShown(true)}
        >
          Переглянути деталі
        </Button>
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
      <TaskInfoModal
        user={user}
        show={isEventModalShow}
        onHide={() => setIsEventModalShown(false)}
        task={Tasks.find((task) => task.name === Chosen)}
      />
      <AddTaskModal
        user={user}
        show={isTaskModalShow}
        onHide={() => setIsTaskModalShown(false)}
        isEditTaskMode={isEditTaskMode}
        setIsEditTaskMode={setIsEditTaskMode}
        setShouldFetchData={setShouldFetchData}
      />
    </div>
  );
};
