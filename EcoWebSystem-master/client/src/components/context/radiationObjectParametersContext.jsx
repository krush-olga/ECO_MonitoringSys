import React, { createContext } from 'react';

const now = new Date();
const year = now.getFullYear();

export const radiationObjectParametersInitialState = {
  generatingPower: 0,
  state: { state: '', id: null },
  equipmentDescription: '',
  commissioningDate: year,
  fuelType: 'Уран',
};

export const RadiationObjectParametersContext = createContext(
  radiationObjectParametersInitialState
);
