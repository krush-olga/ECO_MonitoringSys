import React, { useEffect, useState, useContext } from 'react';

import { Dropdown, Form, Button, Accordion, Card } from 'react-bootstrap';
import { AgGridReact } from 'ag-grid-react';

import { mapRows, mapColumns } from '../utils/mappers';
import { RadiationBar } from '../../radiation-bar/radiation-bar';
import { RadiationRecordsContext } from '../../context/radiationRecordsContext';

import '../formGroup.css';
import { INHALATION_COLUMNS } from './columns';
import { Lookup } from '../../lookup/lookup';

const initialState = {
  form: {
    element: {
      code: -1,
      short_name: 'Оберіть елемент',
      name: 'Оберіть елемент',
      displayName: 'Оберіть елемент',
    },
    inhalationIntensity: 0,
    volumetricActivity: 0,
    radiationRisk: 0,
  },
};

export const InhalationTable = ({ elements }) => {
  const { radiationRecords, setRadiationRecords } = useContext(
    RadiationRecordsContext
  );
  const [columns, setColumns] = useState(mapColumns(INHALATION_COLUMNS));
  const [rows, setRows] = useState(mapRows(radiationRecords.inhalationRecords));
  const [selectedElement, setElement] = useState(initialState.form.element);
  const [inhalationIntensity, setInhalationIntensity] = useState(
    initialState.form.inhalationIntensity
  );
  const [volumetricActivity, setVolumetricActivity] = useState(
    initialState.form.volumetricActivity
  );
  const [gridOptions, setGridOptions] = useState({});
  const [radiationRisk, setRadiationRisk] = useState(
    initialState.form.radiationRisk
  );

  useEffect(() => {
    setRows(mapRows(radiationRecords.inhalationRecords));
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
    setInhalationIntensity(initialState.form.inhalationIntensity);
    setVolumetricActivity(initialState.form.volumetricActivity);
  };

  const handleAddClick = () => {
    const isValid =
      selectedElement !== initialState.form.element &&
      inhalationIntensity !== initialState.form.inhalationIntensity &&
      volumetricActivity !== initialState.form.volumetricActivity;
    if (isValid) {
      const record = {
        element: selectedElement,
        inhalationIntensity,
        volumetricActivity,
      };
      // setInhalationRecords([...inhalationRecords, newRecord]);
      const newInhalationRecords = [
        ...radiationRecords.inhalationRecords,
        record,
      ];
      setRadiationRecords({
        ...radiationRecords,
        inhalationRecords: newInhalationRecords,
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
          <Form.Label>Введіть інтенсивність дихання (м3/рік)</Form.Label>
          <Form.Control
            type='number'
            min='0'
            value={inhalationIntensity}
            onChange={(e) => setInhalationIntensity(+e.target.value)}
          />
        </Form.Group>

        <Form.Group className='radiation-table-group'>
          <Form.Label>
            Введіть об’ємну активність радіонукліда у повітрі (Бк/м3)
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
