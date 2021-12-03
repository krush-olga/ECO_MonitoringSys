import React, { useEffect, useState, useContext } from 'react';

import { Dropdown, Form, Button, Accordion, Card } from 'react-bootstrap';
import { AgGridReact } from 'ag-grid-react';

import { RadiationBar } from '../../radiation-bar/radiation-bar';
import { RadiationRecordsContext } from '../../context/radiationRecordsContext';

import { mapRows, mapColumns } from '../utils/mappers';

import '../formGroup.css';
import { SOIL_COLUMNS } from './columns';
import { Lookup } from '../../lookup/lookup';

const initialState = {
  form: {
    element: {
      code: -1,
      short_name: 'Оберіть елемент',
      displayName: 'Оберіть елемент',
    },
    volumetricActivity: 0,
    soilAbsorptionRate: 0.12,
    duration: 365,
    radiationRisk: 0,
  },
};

export const SoilTable = ({ soilRecords, setSoilRecords, elements }) => {
  const { radiationRecords, setRadiationRecords } = useContext(
    RadiationRecordsContext
  );

  const [columns, setColumns] = useState(mapColumns(SOIL_COLUMNS));
  const [rows, setRows] = useState(mapRows(radiationRecords.soilRecords));
  const [selectedElement, setElement] = useState(initialState.form.element);
  const [gridOptions, setGridOptions] = useState({});
  const [volumetricActivity, setVolumetricActivity] = useState(
    initialState.form.volumetricActivity
  );
  const [soilAbsorptionRate, setSoilAbsorptionRate] = useState(
    initialState.form.soilAbsorptionRate
  );
  const [duration, setDuration] = useState(initialState.form.duration);
  const [radiationRisk, setRadiationRisk] = useState(
    initialState.form.radiationRisk
  );

  useEffect(() => {
    setRows(mapRows(radiationRecords.soilRecords));
    // setRadiationRisk((radiationRisk) => radiationRisk + 1);
  }, [radiationRecords]);

  const clearForm = () => {
    setElement(initialState.form.element);
    setSoilAbsorptionRate(initialState.form.soilAbsorptionRate);
    setVolumetricActivity(initialState.form.volumetricActivity);
    setDuration(initialState.form.duration);
  };

  const handleAddClick = () => {
    const isValid =
      selectedElement !== initialState.form.element &&
      volumetricActivity !== initialState.form.volumetricActivity;
    if (isValid) {
      const record = {
        element: selectedElement,
        soilAbsorptionRate,
        duration,
        volumetricActivity,
      };
      const newSoilRecords = [...radiationRecords.soilRecords, record];
      setRadiationRecords({ ...radiationRecords, soilRecords: newSoilRecords });
      clearForm();
    }
  };

  const selectElement = (element) => {
    setElement(element);
  };

  const onGridReady = (gridOptions) => {
    setGridOptions(gridOptions);
    gridOptions.api.sizeColumnsToFit();
  };

  return (
    <>
      <AgGridReact
        columnDefs={columns}
        rowSelection='single'
        onGridReady={onGridReady}
        rowData={rows}
      />
      <Form.Group
        style={{
          display: 'flex',
          flexDirection: 'row',
          alignItems: 'flex-end',
          justifyContent: 'space-between',
        }}
      >
        <Lookup
          data={elements.map((item) => ({
            ...item,
            displayName: item.short_name,
          }))}
          onClick={(element) => selectElement(element)}
          selected={selectedElement}
        />
        <Form.Group className='radiation-table-group'>
          <Form.Label>Швидкість поглинання грунту (г/добу)</Form.Label>
          <Form.Control
            type='number'
            min='0'
            step='0.1'
            value={soilAbsorptionRate}
            onChange={(e) => setSoilAbsorptionRate(+e.target.value)}
          />
        </Form.Group>
        <Form.Group className='radiation-table-group'>
          <Form.Label>Тривалість опромінення (діб)</Form.Label>
          <Form.Control
            type='number'
            min='0'
            value={duration}
            onChange={(e) => setDuration(+e.target.value)}
          />
        </Form.Group>
        <Form.Group className='radiation-table-group'>
          <Form.Label>
            Введіть питому активність радіонукліда у грунті (Бк/г)
          </Form.Label>
          <Form.Control
            type='number'
            min='0'
            value={volumetricActivity}
            onChange={(e) => setVolumetricActivity(+e.target.value)}
          />
        </Form.Group>
        <Button className='radiation-table-group' onClick={handleAddClick}>
          Додати Елемент
        </Button>
      </Form.Group>
    </>
  );
};
