const pool = require('../../db-config/mysql-config');
const { characteristicService } = require('./characteristicsService');

class RadiationInfoParametersService {
  async _getElementInfoUsingRisk(riskColumn, riskValue) {
    return new Promise((resolve, reject) => {
      const query = `
            SELECT
                elements.*
            FROM
                element_radiation_risk
            INNER JOIN elements ON element_radiation_risk.element_code = elements.code
            WHERE element_radiation_risk.${riskColumn} = ${riskValue}
        `;
      pool.query(query, (error, rows) => {
        if (error) {
          console.log(error);
          reject(error);
        }
        if (rows[0]) {
          resolve(rows[0]);
        }
      });
    });
  }

  async _reduceCalculations(data) {
    const dataCopy = [...data];
    const calculations = {
      inhalationRecords: [],
      waterRecords: [],
      productRecords: [],
      soilRecords: [],
      externalRecords: [],
    };
    while (dataCopy.length) {
      if (dataCopy.length && dataCopy[0].id_of_formula === 900) {
        let [element, inhalationIntensity, volumetricActivity] =
          dataCopy.splice(0, 3);
        element = await this._getElementInfoUsingRisk(
          'risk_from_inhalation',
          element.parameter_value
        );
        inhalationIntensity = Number(inhalationIntensity.parameter_value);
        volumetricActivity = Number(volumetricActivity.parameter_value);
        calculations.inhalationRecords.push({
          element,
          inhalationIntensity,
          volumetricActivity,
        });
      }
      if (dataCopy.length && dataCopy[0].id_of_formula === 800) {
        let [element, waterPerYear, volumetricActivity] = dataCopy.splice(0, 3);
        element = await this._getElementInfoUsingRisk(
          'risk_from_product_and_water',
          element.parameter_value
        );
        waterPerYear = Number(waterPerYear.parameter_value);
        volumetricActivity = Number(volumetricActivity.parameter_value);
        calculations.waterRecords.push({
          element,
          waterPerYear,
          volumetricActivity,
        });
      }
      if (dataCopy.length && dataCopy[0].id_of_formula === 700) {
        let [
          element,
          productPerYear,
          volumetricActivity,
          radionuclideConsumptionFactor,
        ] = dataCopy.splice(0, 4);
        element = await this._getElementInfoUsingRisk(
          'risk_from_product_and_water',
          element.parameter_value
        );
        productPerYear = Number(productPerYear.parameter_value);
        volumetricActivity = Number(volumetricActivity.parameter_value);
        radionuclideConsumptionFactor = Number(
          radionuclideConsumptionFactor.parameter_value
        );
        calculations.productRecords.push({
          element,
          productPerYear,
          volumetricActivity,
          radionuclideConsumptionFactor,
        });
      }
      if (dataCopy.length && dataCopy[0].id_of_formula === 600) {
        let [element, soilAbsorptionRate, duration, volumetricActivity] =
          dataCopy.splice(0, 4);
        element = await this._getElementInfoUsingRisk(
          'risk_from_external_irradiation',
          element.parameter_value
        );
        soilAbsorptionRate = Number(soilAbsorptionRate.parameter_value);
        volumetricActivity = Number(volumetricActivity.parameter_value);
        duration = Number(duration.parameter_value);
        calculations.soilRecords.push({
          element,
          soilAbsorptionRate,
          volumetricActivity,
          duration,
        });
      }
      if (dataCopy.length && dataCopy[0].id_of_formula === 500) {
        let [element, duration, volumetricActivity] = dataCopy.splice(0, 3);
        element = await this._getElementInfoUsingRisk(
          'risk_from_external_irradiation',
          element.parameter_value
        );
        volumetricActivity = Number(volumetricActivity.parameter_value);
        duration = Number(duration.parameter_value);
        calculations.externalRecords.push({
          element,
          volumetricActivity,
          duration,
        });
      }
    }
    return calculations;
  }

  async getParameters(radiationPointId) {
    const dataPromise = new Promise((resolve, reject) => {
      const query = `
        SELECT
            radiation_poi_calculation.calculation_number,
            parameters_value.id_of_formula,
            parameters_value.id_of_parameter,
            parameters_value.parameter_value
        FROM radiation_poi_calculation
        INNER JOIN parameters_value ON radiation_poi_calculation.calculation_number = parameters_value.calculation_number
        WHERE radiation_poi_calculation.idPoi = ${radiationPointId}
      `;
      pool.query(query, (error, rows) => {
        if (error) {
          console.log(error);
          reject(error);
        }

        resolve(rows);
      });
    });
    const data = await dataPromise;
    const calculations = await this._reduceCalculations(data);

    return calculations;
  }

  async getRadiationObjectParameters(radiationPointId) {
    const allCharacteristics = await characteristicService.getAll();
    const data = await new Promise((resolve, reject) => {
      const query = `
        SELECT
          value_,
          id_characteristic
        FROM
          characteristics_of_object
        WHERE
          id_poi = ${radiationPointId}
      `;
      pool.query(query, (error, rows) => {
        if (error) {
          console.log(error);
          reject(error);
        }
        if (rows.length) {
          resolve(rows);
        } else {
          resolve([]);
        }
      });
    });
    const mapped = data.map(async (item) => {
      const characteristics = allCharacteristics.find(
        (x) => x.id === item.id_characteristic
      );
      if (characteristics.id === 3 && characteristics.name_ === `Тип об'єкту`) {
        const typeOfObject = await new Promise((resolve, reject) => {
          const query = `
            SELECT
              Name
            FROM
              type_of_object
            WHERE
              Id = ${item.value_}
          `;
          pool.query(query, (error, rows) => {
            if (error) {
              console.log(error);
              reject(error);
            }
            if (rows.length) {
              resolve(rows[0].Name);
            }
          });
        });
        item.value_ = typeOfObject;
      }
      return {
        value: item.value_,
        description: characteristics.name_,
        measure: characteristics.measure,
      };
    });
    return Promise.all(mapped);
  }
}

const instance = new RadiationInfoParametersService();

module.exports = {
  radiationInfoParametersService: instance,
};
