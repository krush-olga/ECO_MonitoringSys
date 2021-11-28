import React, { useEffect, useState, useContext } from 'react';

import { Dropdown, Form, Button, Accordion, Card } from 'react-bootstrap';
import { AgGridReact } from 'ag-grid-react';
import { RadiationBar } from '../../radiation-bar/radiation-bar';
import { mapRows, mapColumns } from '../utils/mappers';
import { RadiationRecordsContext } from '../../context/radiationRecordsContext';

import '../formGroup.css';
import { WATER_COLUMNS } from './columns';
import { Lookup } from '../../lookup/lookup';

const initialState = {
  form: {
    element: {
      code: -1,
      short_name: 'Оберіть елемент',
      displayName: 'Оберіть елемент',
    },
    waterPerYear: 0,
    volumetricActivity: 0,
    radiationRisk: 0,
  },
};

export const WaterTable = ({ elements }) => {
  const { radiationRecords, setRadiationRecords } = useContext(
    RadiationRecordsContext
  );

  const [columns, setColumns] = useState(mapColumns(WATER_COLUMNS));
  const [rows, setRows] = useState(mapRows(radiationRecords.waterRecords));
  const [selectedElement, setElement] = useState(initialState.form.element);
  const [gridOptions, setGridOptions] = useState({});
  const [radiationRisk, setRadiationRisk] = useState(
    initialState.form.radiationRisk
  );

  const [waterPerYear, setWaterPerYear] = useState(
    initialState.form.waterPerYear
  );
  const [volumetricActivity, setVolumetricActivity] = useState(
    initialState.form.volumetricActivity
  );

  useEffect(() => {
    setRows(mapRows(radiationRecords.waterRecords));
    // setRadiationRisk((radiationRisk) => radiationRisk + 1);
  }, [radiationRecords]);

  const selectElement = (element) => {
    setElement(element);
  };

  const clearForm = () => {
    setElement(initialState.form.element);
    setWaterPerYear(initialState.form.waterPerYear);
    setVolumetricActivity(initialState.form.volumetricActivity);
  };

  const onGridReady = (gridOptions) => {
    setGridOptions(gridOptions);
    gridOptions.api.sizeColumnsToFit();
  };

  const handleAddClick = () => {
    const isValid =
      selectedElement !== initialState.form.element &&
      waterPerYear !== initialState.form.waterPerYear &&
      volumetricActivity !== initialState.form.volumetricActivity;
    if (isValid) {
      const record = {
        element: selectedElement,
        waterPerYear,
        volumetricActivity,
      };
      const newWaterRecords = [...radiationRecords.waterRecords, record];
      setRadiationRecords({
        ...radiationRecords,
        waterRecords: newWaterRecords,
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
          <Form.Label>Річне вживання води (дм3/рік)</Form.Label>
          <Form.Control
            type='number'
            min='0'
            value={waterPerYear}
            onChange={(e) => setWaterPerYear(+e.target.value)}
          />
        </Form.Group>
        <Form.Group className='radiation-table-group'>
          <Form.Label>
            Введіть об'ємну активність радіонукліда у воді (Бк/дм3)
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
