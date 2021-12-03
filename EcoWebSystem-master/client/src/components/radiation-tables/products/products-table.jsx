import React, { useEffect, useState, useContext } from 'react';

import { Dropdown, Form, Button, Accordion, Card } from 'react-bootstrap';
import { AgGridReact } from 'ag-grid-react';
import { RadiationBar } from '../../radiation-bar/radiation-bar';
import { mapRows, mapColumns } from '../utils/mappers';
import { RadiationRecordsContext } from '../../context/radiationRecordsContext';

import '../formGroup.css';
import { PRODUCT_COLUMNS } from './columns';
import { Lookup } from '../../lookup/lookup';

const initialState = {
  form: {
    element: {
      code: -1,
      short_name: 'Оберіть елемент',
      displayName: 'Оберіть елемент',
    },
    productPerYear: 0,
    volumetricActivity: 0,
    radiationRisk: 0,
    radionuclideConsumptionFactor: 0,
  },
};

export const ProductsTable = ({ elements }) => {
  const { radiationRecords, setRadiationRecords } = useContext(
    RadiationRecordsContext
  );

  const [columns, setColumns] = useState(mapColumns(PRODUCT_COLUMNS));
  const [rows, setRows] = useState(mapRows(radiationRecords.productRecords));
  const [selectedElement, setElement] = useState(initialState.form.element);

  const [productPerYear, setProductPerYear] = useState(
    initialState.form.productPerYear
  );
  const [volumetricActivity, setVolumetricActivity] = useState(
    initialState.form.volumetricActivity
  );
  const [radionuclideConsumptionFactor, setRadionuclideConsumptionFactor] =
    useState(initialState.form.radionuclideConsumptionFactor);

  const [gridOptions, setGridOptions] = useState({});
  const [radiationRisk, setRadiationRisk] = useState(
    initialState.form.radiationRisk
  );

  useEffect(() => {
    setRows(mapRows(radiationRecords.productRecords));
    // setRadiationRisk((radiationRisk) => radiationRisk + 1);
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
    setProductPerYear(initialState.form.productPerYear);
    setVolumetricActivity(initialState.form.volumetricActivity);
    setRadionuclideConsumptionFactor(
      initialState.form.radionuclideConsumptionFactor
    );
  };

  const handleAddClick = () => {
    const isValid =
      selectedElement !== initialState.form.element &&
      productPerYear !== initialState.form.productPerYear &&
      volumetricActivity !== initialState.form.volumetricActivity &&
      radionuclideConsumptionFactor !==
        initialState.form.radionuclideConsumptionFactor;
    if (isValid) {
      const record = {
        element: selectedElement,
        productPerYear,
        volumetricActivity,
        radionuclideConsumptionFactor,
      };
      const newProductRecords = [...radiationRecords.productRecords, record];
      setRadiationRecords({
        ...radiationRecords,
        productRecords: newProductRecords,
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
          <Form.Label>Річне вживання продукту (кг/рік)</Form.Label>
          <Form.Control
            type='number'
            min='0'
            value={productPerYear}
            onChange={(e) => setProductPerYear(+e.target.value)}
          />
        </Form.Group>
        <Form.Group className='radiation-table-group'>
          <Form.Label>
            Введіть питому активність радіонукліда у продукті (Бк/кг)
          </Form.Label>
          <Form.Control
            type='number'
            min='0'
            value={volumetricActivity}
            onChange={(e) => setVolumetricActivity(+e.target.value)}
          />
        </Form.Group>
        <Form.Group className='radiation-table-group'>
          <Form.Label>
            Коефіцієнт, враховуючий втрати і-го радіонукліда при кулінарній
            обробці р-продукту
          </Form.Label>
          <Form.Control
            type='number'
            min='0'
            value={radionuclideConsumptionFactor}
            onChange={(e) => setRadionuclideConsumptionFactor(+e.target.value)}
          />
        </Form.Group>
        <Button className='radiation-table-group' onClick={handleAddClick}>
          Додати Елемент
        </Button>
      </Form.Group>
    </>
  );
};
