import React, { createContext } from 'react';

export const radiationRecordsInitialState = {
  inhalationRecords: [],
  waterRecords: [],
  productRecords: [],
  soilRecords: [],
  externalRecords: [],
};

export const RadiationRecordsContext = createContext(
  radiationRecordsInitialState
);
