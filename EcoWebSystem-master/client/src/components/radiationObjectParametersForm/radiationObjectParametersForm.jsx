import React, { useContext, useState, useEffect } from 'react';
import { Dropdown, Form, Tabs, Tab, Button, Spinner } from 'react-bootstrap';
import { RadiationObjectParametersContext } from '../context/radiationObjectParametersContext';

const emptyState = {
  selectedRadiationObjectState: 'Оберіть стан об`єкту',
};

const now = new Date();
const year = now.getFullYear();

export const RadiationObjectParameters = ({ radiationObjectStates }) => {
  const { radiationObjectParameters, setRadiationObjectParameters } =
    useContext(RadiationObjectParametersContext);
  const [generatingPower, setGeneratingPower] = useState(
    radiationObjectParameters.generatingPower
  );
  const [selectedRadiationObjectState, setSelectedRadiationObjectState] =
    useState(radiationObjectParameters.state);
  const [equipmentDescription, setEquipmentDescription] = useState(
    radiationObjectParameters.equipmentDescription
  );
  const [commissioningDate, setCommissioningDate] = useState(
    radiationObjectParameters.commissioningDate
  );
  const [fuelType, setFuelType] = useState(radiationObjectParameters.fuelType);

  useEffect(() => {
    setGeneratingPower(radiationObjectParameters.generatingPower);
    setSelectedRadiationObjectState(radiationObjectParameters.state);
    setEquipmentDescription(radiationObjectParameters.equipmentDescription);
    setCommissioningDate(radiationObjectParameters.commissioningDate);
    setFuelType(radiationObjectParameters.fuelType);
  }, [radiationObjectParameters]);

  useEffect(() => {
    const isChanged =
      generatingPower !== radiationObjectParameters.generatingPower ||
      selectedRadiationObjectState !== radiationObjectParameters.state ||
      equipmentDescription !== radiationObjectParameters.equipmentDescription ||
      commissioningDate !== radiationObjectParameters.commissioningDate ||
      fuelType !== radiationObjectParameters.fuelType;
    if (isChanged) {
      const newRadiationObjectParameters = {
        generatingPower,
        state: selectedRadiationObjectState,
        equipmentDescription,
        commissioningDate,
        fuelType,
      };
      setRadiationObjectParameters(newRadiationObjectParameters);
    }
  }, [
    generatingPower,
    selectedRadiationObjectState,
    equipmentDescription,
    commissioningDate,
    fuelType,
  ]);

  return (
    <>
      <Form.Group className='radiation-table-group'>
        <Form.Label>Генеруюча потужність (МегаВат)</Form.Label>
        <Form.Control
          type='number'
          min='0'
          value={generatingPower}
          onChange={(e) => setGeneratingPower(+e.target.value)}
        />
      </Form.Group>
      <Form.Group>
        <Form.Label
          style={{
            marginTop: '8px',
          }}
        >
          Стан АЕС
        </Form.Label>
        <Dropdown>
          <Dropdown.Toggle size='sm' variant='success'>
            {selectedRadiationObjectState.state ||
              emptyState.selectedRadiationObjectState}
          </Dropdown.Toggle>
          <Dropdown.Menu
            styles={{
              overflowY: 'auto',
              height: '100px',
            }}
          >
            {radiationObjectStates.length &&
              radiationObjectStates.map((objectState) => (
                <Dropdown.Item
                  key={objectState.id}
                  active={objectState === selectedRadiationObjectState}
                  onClick={() => setSelectedRadiationObjectState(objectState)}
                >
                  {objectState.state}
                </Dropdown.Item>
              ))}
          </Dropdown.Menu>
        </Dropdown>
      </Form.Group>
      <Form.Group>
        <Form.Label>Введіть характеристики обладнання</Form.Label>
        <Form.Control
          as='textarea'
          rows='3'
          value={equipmentDescription}
          onChange={(e) => setEquipmentDescription(e.target.value)}
        />
      </Form.Group>
      <Form.Group>
        <Form.Label>Рік введення у експлуатацію</Form.Label>
        <Form.Control
          type='number'
          min='1700'
          max={year}
          value={commissioningDate}
          onChange={(e) => setCommissioningDate(+e.target.value)}
        />
      </Form.Group>
      <Form.Group>
        <Form.Label>Тип Палива</Form.Label>
        <Form.Control
          type='input'
          value={fuelType}
          onChange={(e) => setFuelType(e.target.value)}
        />
      </Form.Group>
    </>
  );
};
