import React, { useEffect, useState, useContext } from 'react';

import { Dropdown, Form, Button, Accordion, Card } from 'react-bootstrap';
import { AgGridReact } from 'ag-grid-react';

import { mapRows, mapColumns } from '../utils/mappers';
import { RadiationBar } from '../../radiation-bar/radiation-bar';
import { RadiationRecordsContext } from '../../context/radiationRecordsContext';

import '../formGroup.css';
import { EXTERNAL_COLUMNS } from './columns';
import { Lookup } from '../../lookup/lookup';

const initialState = {
  form: {
    element: {
      code: -1,
      short_name: 'Оберіть елемент',
      name: 'Оберіть елемент',
      displayName: 'Оберіть елемент',
    },
    volumetricActivity: 0,
    duration: 365,
  },
};

export const ExternalTable = ({ elements }) => {
  const { radiationRecords, setRadiationRecords } = useContext(
    RadiationRecordsContext
  );
  const [columns, setColumns] = useState(mapColumns(EXTERNAL_COLUMNS));
  const [rows, setRows] = useState(mapRows(radiationRecords.inhalationRecords));
  const [selectedElement, setElement] = useState(initialState.form.element);
  const [volumetricActivity, setVolumetricActivity] = useState(
    initialState.form.volumetricActivity
  );
  const [duration, setDuration] = useState(initialState.form.duration);
  const [gridOptions, setGridOptions] = useState({});

  useEffect(() => {
    setRows(mapRows(radiationRecords.externalRecords));
  }, [radiationRecords]);

  const onGridReady = (gridOptions) => {
    setGridOptions(gridOptions);
    gridOptions.api.sizeColumnsToFit();
  };

  const selectElement = (element) => {
    setElement(element);
  };

  const clearForm = () => {
    setElement(initialState.form.element);
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
        duration,
        volumetricActivity,
      };
      const newExternalRecords = [...radiationRecords.externalRecords, record];
      setRadiationRecords({
        ...radiationRecords,
        externalRecords: newExternalRecords,
      });
      clearForm();
    }
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
            Введіть об'ємну активність радіонукліда у грунті (Бк/г)
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
