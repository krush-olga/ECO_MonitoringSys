const payload = {
  generalPoiInfo: {
    id_of_user,
    Type,
    coordinates,
  },
  radioEmissions: {
    inhalationRecords: [
      {
        element: { code: 1, name: '1', short_name: '2' },
        inhalationIntensity: 1,
        volumetricActivity: 2,
      },
      {
        element: { code: 2, name: '1', short_name: '2' },
        inhalationIntensity: 1,
        volumetricActivity: 2,
      },
      {
        element: { code: 3, name: '1', short_name: '2' },
        inhalationIntensity: 1,
        volumetricActivity: 2,
      },
    ],
    waterRecords: [
      {
        element: { code: 1, name: '1', short_name: '2' },
        waterPerYear: 1,
        volumetricActivity: 4,
      },
      {
        element: { code: 2, name: '1', short_name: '2' },
        waterPerYear: 2,
        volumetricActivity: 2,
      },
      {
        element: { code: 3, name: '1', short_name: '2' },
        waterPerYear: 3,
        volumetricActivity: 5,
      },
    ],
    productRecords: [
      {
        element: { code: 1, name: '1', short_name: '2' },
        productPerYear: 1,
        volumetricActivity: 4,
      },
      {
        element: { code: 2, name: '1', short_name: '2' },
        productPerYear: 2,
        volumetricActivity: 2,
      },
      {
        element: { code: 3, name: '1', short_name: '2' },
        productPerYear: 3,
        volumetricActivity: 5,
      },
    ],
    soilRecords: [
      {
        element: { code: 1, name: '1', short_name: '2' },
        soilAbsorptionRate: 0.12,
        duration: 365,
        volumetricActivity: 4,
      },
      {
        element: { code: 2, name: '1', short_name: '2' },
        soilAbsorptionRate: 0.12,
        duration: 365,
        volumetricActivity: 2,
      },
      {
        element: { code: 3, name: '1', short_name: '2' },
        soilAbsorptionRate: 0.12,
        duration: 365,
        volumetricActivity: 5,
      },
    ],
  },
};
