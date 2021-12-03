function objectCharacteristicsIdMap(characteristics) {
  const result = {};
  result['generatingPower'] = {
    value: characteristics.generatingPower,
    id_characteristic: 1,
  };
  result['fuelType'] = {
    value: characteristics.fuelType,
    id_characteristic: 2,
  };
  result['typeOfObject'] = {
    value: characteristics.typeOfObject,
    id_characteristic: 3,
  };
  result['commissioningDate'] = {
    value: characteristics.commissioningDate,
    id_characteristic: 4,
  };
  result['state'] = {
    value: characteristics.state,
    id_characteristic: 5,
  };
  result['equipmentDescription'] = {
    value: characteristics.equipmentDescription,
    id_characteristic: 6,
  };
  return result;
}

module.exports = {
  objectCharacteristicsIdMap,
};
