import React, { useEffect, useState, useContext } from 'react';
import { EnvironmentsInfoContext } from '../context/environmentsInfoContext';
import { InhalationTable } from '../radiation-tables/inhalation/inhalation-table';
import { post, get } from '../../utils/httpService';
import { ELEMENTS_URL } from '../../utils/constants';
import {
  Button,
  Dropdown,
  Form,
  Spinner,
  Accordion,
  Card,
  Tab,
  Tabs,
} from 'react-bootstrap';
import { WaterTable } from '../radiation-tables/water-table/water-table';
import { ProductsTable } from '../radiation-tables/products/products-table';
import { SoilTable } from '../radiation-tables/soil/soil-table';
import { ExternalTable } from '../radiation-tables/external/external-table';
import { RadiationRecordsContext } from '../context/radiationRecordsContext';

export const cardStyles = {
  maxHeight: '700px',
  height: '40vh',
  overflow: 'auto',
  width: '99%',
};

export const SubmitRadiationForm = ({ elements }) => {
  return (
    <>
      <>
        <Form.Group>
          <Form.Label>
            Введіть показники для розрахунку радіаційного ризику по:
          </Form.Label>
          <Tabs
            defaultActiveKey='general'
            id='uncontrolled-tab-example'
            style={{
              padding: '10px 0',
            }}
          >
            <Tab eventKey='inhalation' title='Вдихання (інгаляція)'>
              <Card>
                <Card.Body style={cardStyles} className='ag-theme-alpine'>
                  <InhalationTable elements={elements} />
                </Card.Body>
              </Card>
            </Tab>
            <Tab eventKey='water' title='Вживання води'>
              <Card>
                <Card.Body style={cardStyles} className='ag-theme-alpine'>
                  <WaterTable elements={elements} />
                </Card.Body>
              </Card>
            </Tab>
            <Tab eventKey='products' title='Вживання продуктів'>
              <Card>
                <Card.Body style={cardStyles} className='ag-theme-alpine'>
                  <ProductsTable elements={elements} />
                </Card.Body>
              </Card>
            </Tab>
            <Tab eventKey='soil' title='Пероральне вживання частинок грунту'>
              <Card>
                <Card.Body style={cardStyles} className='ag-theme-alpine'>
                  <SoilTable elements={elements} />
                </Card.Body>
              </Card>
            </Tab>
            <Tab eventKey='external' title='Зовнішнє опромінення від грунту'>
              <Card>
                <Card.Body style={cardStyles} className='ag-theme-alpine'>
                  <ExternalTable elements={elements} />
                </Card.Body>
              </Card>
            </Tab>
          </Tabs>
        </Form.Group>
      </>
    </>
  );
};
