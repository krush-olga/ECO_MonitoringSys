const inhalationImportMap = {
  Index: 'index',
  Element: 'elementName',
  inhalationIntensity: 'inhalationIntensity',
  volumetricActivity: 'volumetricActivity',
};

const waterImportMap = {
  Index: 'index',
  Element: 'elementName',
  waterPerYear: 'waterPerYear',
  volumetricActivity: 'volumetricActivity',
};

const productImportMap = {
  Index: 'index',
  Element: 'elementName',
  productPerYear: 'productPerYear',
  volumetricActivity: 'volumetricActivity',
  radionuclideConsumptionFactor: 'radionuclideConsumptionFactor',
};

const soilImportMap = {
  Index: 'index',
  Element: 'elementName',
  soilAbsorptionRate: 'soilAbsorptionRate',
  volumetricActivity: 'volumetricActivity',
  duration: 'duration',
};

const externalImportMap = {
  Index: 'index',
  Element: 'elementName',
  volumetricActivity: 'volumetricActivity',
  duration: 'duration',
};

const generalInfoImportMap = {
  Date: 'date',
  objectType: 'objectType',
  ownerType: 'ownerType',
  name: 'name',
  description: 'description',
};

const radiationObjectParametersImportMap = {
  state: 'state',
  generatingPower: 'generatingPower',
  equipmentDescription: 'equipmentDescription',
  commissioningDate: 'commissioningDate',
  fuelType: 'fuelType',
};

const getXlsxMapUsingSheetName = (sheetName) => {
  if (sheetName === 'Sheet1' || sheetName === 'GeneralInfo') {
    return generalInfoImportMap;
  }
  if (sheetName === 'Sheet2' || sheetName === 'RadiationObjectParameters') {
    return radiationObjectParametersImportMap;
  }
  if (sheetName === 'Sheet3' || sheetName === 'InhalationRecords') {
    return inhalationImportMap;
  }
  if (sheetName === 'Sheet4' || sheetName === 'WaterRecords') {
    return waterImportMap;
  }
  if (sheetName === 'Sheet5' || sheetName === 'ProductRecords') {
    return productImportMap;
  }
  if (sheetName === 'Sheet6' || sheetName === 'SoilRecords') {
    return soilImportMap;
  }
  if (sheetName === 'Sheet7' || sheetName === 'ExternalRecords') {
    return externalImportMap;
  }
};

const fillRadiationRecords = (sheetName, rows, accumulator) => {
  if (sheetName === 'Sheet3' || sheetName === 'InhalationRecords') {
    accumulator.inhalationRecords = rows;
    return accumulator;
  }
  if (sheetName === 'Sheet4' || sheetName === 'WaterRecords') {
    accumulator.waterRecords = rows;
    return accumulator;
  }
  if (sheetName === 'Sheet5' || sheetName === 'ProductRecords') {
    accumulator.productRecords = rows;
    return accumulator;
  }
  if (sheetName === 'Sheet6' || sheetName === 'SoilRecords') {
    accumulator.soilRecords = rows;
    return accumulator;
  }
  if (sheetName === 'Sheet7' || sheetName === 'ExternalRecords') {
    accumulator.externalRecords = rows;
    return accumulator;
  }
};

module.exports = {
  getXlsxMapUsingSheetName,
  fillRadiationRecords,
};
