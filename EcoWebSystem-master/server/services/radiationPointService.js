const pool = require('../../db-config/mysql-config');
const {
  objectCharacteristicsIdMap,
} = require('../utils/objectCharacteristicsIdMap');
const { calculationService } = require('./calculationsService');
const { formulasService } = require('./formulasService');
const {
  radiationInfoParametersService,
} = require('./radiationInfoParametersService.js');

class RadiationPointService {
  async _insertPoi(generalInfo) {
    const {
      Name_object,
      type,
      coordinates,
      description,
      id_of_user,
      owner_type_id: owner_type,
    } = generalInfo;
    return new Promise((resolve, reject) => {
      const query = `
          INSERT INTO poi
          (id_of_user, Type, owner_type, Coord_Lat, Coord_Lng, Description, Name_object)
          VALUES ('${id_of_user}', '${type}', '${owner_type}','${coordinates[0]}', '${coordinates[1]}', '${description}', '${Name_object}');
          `;
      pool.query(query, (error, rows) => {
        if (error) {
          return reject(error);
        }

        if (rows.affectedRows === 1) {
          return resolve(+rows.insertId);
        }
      });
    });
  }

  async _insertRadiationObjectParameters(idPoi, payload, typeOfObject) {
    const {
      commissioningDate,
      equipmentDescription,
      generatingPower,
      stateId,
      fuelType,
    } = payload;
    const state = await new Promise((resolve, reject) => {
      const query = `
        SELECT state
        FROM radiation_object_state 
        WHERE id = ${stateId}
      `;
      pool.query(query, (error, rows) => {
        if (error) {
          reject(error);
        }
        if (rows.length) {
          resolve(rows[0].state);
        }
      });
    });
    const tableName = 'characteristics_of_object';
    const characteristics = {
      generatingPower,
      fuelType,
      typeOfObject,
      commissioningDate,
      state,
      equipmentDescription,
    };
    const mapped = objectCharacteristicsIdMap(characteristics);
    const insertionPromises = Object.values(mapped).map((characteristic) => {
      return new Promise((resolve, reject) => {
        const query = `
          INSERT INTO 
              ??
              (??)
          VALUES
              (?)`;
        const columnNames = ['id_poi', 'value_', 'id_characteristic'];
        const values = [
          idPoi,
          characteristic.value,
          characteristic.id_characteristic,
        ];
        const parameters = [tableName, columnNames, values];
        pool.query(query, parameters, (error) => {
          if (error) {
            reject(error);
          }
          resolve();
        });
      });
    });
    return Promise.all(insertionPromises);
  }

  async _getRisksUsingElementCode(code, requiredColumn) {
    const tableName = 'element_radiation_risk';
    const columnNames = [requiredColumn];
    const query = `
      SELECT 
        ??
      FROM 
        ??
      WHERE 
        ?? = ?
      ;`;

    const values = [columnNames, tableName, 'element_code', code];
    return new Promise((resolve, reject) => {
      pool.query(query, values, (error, rows) => {
        if (error) {
          return reject(error);
        }
        if (rows[0]) {
          return resolve(rows[0]);
        }
      });
    });
  }

  async _processInhalationRecords(
    inhalationRecords,
    user,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const id_of_formula = 900;
    const requiredColumn = 'risk_from_inhalation';
    for (const record of inhalationRecords) {
      const { element } = record;
      const code = element.code;
      const elementRisk = await this._getRisksUsingElementCode(
        code,
        requiredColumn
      );
      await this._setParameters({
        id_of_formula,
        record,
        user,
        risk: elementRisk.risk_from_inhalation,
        idRadiationPoint,
        radiationEmissionsDate,
      });
    }
  }

  async _processWaterRecords(
    waterRecords,
    user,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const id_of_formula = 800;
    const requiredColumn = 'risk_from_product_and_water';

    for (const record of waterRecords) {
      const { element } = record;
      const code = element.code;
      const elementRisk = await this._getRisksUsingElementCode(
        code,
        requiredColumn
      );
      await this._setParameters({
        id_of_formula,
        record,
        user,
        risk: elementRisk.risk_from_product_and_water,
        idRadiationPoint,
        radiationEmissionsDate,
      });
    }
  }

  async _processProductRecords(
    productRecords,
    user,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const id_of_formula = 700;
    const requiredColumn = 'risk_from_product_and_water';
    for (const record of productRecords) {
      const { element } = record;
      const code = element.code;
      const elementRisk = await this._getRisksUsingElementCode(
        code,
        requiredColumn
      );
      await this._setParameters({
        id_of_formula,
        record,
        user,
        risk: elementRisk.risk_from_product_and_water,
        idRadiationPoint,
        radiationEmissionsDate,
      });
    }
  }

  async _processSoilRecords(
    soilRecords,
    user,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const id_of_formula = 600;
    const requiredColumn = 'risk_from_external_irradiation';

    for (const record of soilRecords) {
      const { element } = record;
      const code = element.code;
      const elementRisk = await this._getRisksUsingElementCode(
        code,
        requiredColumn
      );
      await this._setParameters({
        id_of_formula,
        record,
        user,
        risk: elementRisk.risk_from_external_irradiation,
        idRadiationPoint,
        radiationEmissionsDate,
      });
    }
  }

  async _processExternalRecords(
    externalRecords,
    user,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const id_of_formula = 500;
    const requiredColumn = 'risk_from_external_irradiation';

    for (const record of externalRecords) {
      const { element } = record;
      const code = element.code;
      const elementRisk = await this._getRisksUsingElementCode(
        code,
        requiredColumn
      );
      await this._setParameters({
        id_of_formula,
        record,
        user,
        risk: elementRisk.risk_from_external_irradiation,
        idRadiationPoint,
        radiationEmissionsDate,
      });
    }
  }

  async _getFormulaParameters({ id_of_formula, id_of_expert }) {
    return new Promise((resolve, reject) => {
      const tableName = 'formula_compound';
      const query = `
            SELECT 
              *
            FROM 
              ??
            WHERE 
              ?? = ?
            AND
              ?? = ?
            ;`;

      const values = [
        tableName,
        'id_of_formula',
        id_of_formula,
        'id_of_expert',
        id_of_expert,
      ];

      pool.query(query, values, (error, rows) => {
        if (error) {
          return reject(error);
        }
        if (rows && rows.length) {
          return resolve(rows);
        }
      });
    });
  }

  async _setDescription({
    calculation_number,
    id_of_expert,
    calculation_name,
    description_of_calculation,
  }) {
    const tableName = 'calculations_description';
    return new Promise((resolve, reject) => {
      const query = `
          INSERT INTO 
              ??
              (??)
          VALUES
              (?)`;

      const columnNames = [
        'calculation_number',
        'calculation_name',
        'description_of_calculation',
        'id_of_expert',
      ];

      const values = [
        calculation_number,
        calculation_name,
        description_of_calculation,
        id_of_expert,
      ];
      const parameters = [tableName, columnNames, values];
      pool.query(query, parameters, (error) => {
        if (error) {
          reject(error);
        }
        resolve();
      });
    });
  }

  async _setInhalationResult({
    calculation_number,
    id_of_formula,
    id_of_expert,
    parametersEntries,
    radiationEmissionsDate,
  }) {
    const tableName = 'calculations_result';
    return new Promise((resolve, reject) => {
      const query = `
          INSERT INTO 
              ??
              (??)
          VALUES
              (?)`;
      const [year, month, day] = radiationEmissionsDate.split('-');
      const date_of_calculation = new Date(year, month - 1, day)
        .toISOString()
        .slice(0, 19)
        .replace('T', ' ');
      const columnNames = [
        'calculation_number',
        'date_of_calculation',
        'id_of_formula',
        'result',
        'id_of_expert',
        'desciption',
      ];
      const result = formulasService.calculate(
        id_of_formula,
        parametersEntries
      );
      const values = [
        calculation_number,
        date_of_calculation,
        id_of_formula,
        result,
        id_of_expert,
        'Inhalation risk',
      ];

      const parameters = [tableName, columnNames, values];
      pool.query(query, parameters, (error) => {
        if (error) {
          reject(error);
        }
        resolve();
      });
    });
  }

  async _setWaterResult({
    calculation_number,
    id_of_formula,
    id_of_expert,
    parametersEntries,
    radiationEmissionsDate,
  }) {
    const tableName = 'calculations_result';
    return new Promise((resolve, reject) => {
      const query = `
          INSERT INTO 
              ??
              (??)
          VALUES
              (?)`;
      const [year, month, day] = radiationEmissionsDate.split('-');
      const date_of_calculation = new Date(year, month - 1, day)
        .toISOString()
        .slice(0, 19)
        .replace('T', ' ');
      const columnNames = [
        'calculation_number',
        'date_of_calculation',
        'id_of_formula',
        'result',
        'id_of_expert',
        'desciption',
      ];
      const result = formulasService.calculate(
        id_of_formula,
        parametersEntries
      );

      const values = [
        calculation_number,
        date_of_calculation,
        id_of_formula,
        result,
        id_of_expert,
        'Water risk',
      ];

      const parameters = [tableName, columnNames, values];
      pool.query(query, parameters, (error) => {
        if (error) {
          reject(error);
        }
        resolve();
      });
    });
  }

  async _setProductResult({
    calculation_number,
    id_of_formula,
    id_of_expert,
    parametersEntries,
    radiationEmissionsDate,
  }) {
    const tableName = 'calculations_result';
    return new Promise((resolve, reject) => {
      const query = `
          INSERT INTO 
              ??
              (??)
          VALUES
              (?)`;
      const [year, month, day] = radiationEmissionsDate.split('-');
      const date_of_calculation = new Date(year, month - 1, day)
        .toISOString()
        .slice(0, 19)
        .replace('T', ' ');
      const columnNames = [
        'calculation_number',
        'date_of_calculation',
        'id_of_formula',
        'result',
        'id_of_expert',
        'desciption',
      ];
      const result = formulasService.calculate(
        id_of_formula,
        parametersEntries
      );
      const values = [
        calculation_number,
        date_of_calculation,
        id_of_formula,
        result,
        id_of_expert,
        'Product risk',
      ];

      const parameters = [tableName, columnNames, values];
      pool.query(query, parameters, (error) => {
        if (error) {
          reject(error);
        }
        resolve();
      });
    });
  }

  async _setSoilResult({
    calculation_number,
    id_of_formula,
    id_of_expert,
    parametersEntries,
    radiationEmissionsDate,
  }) {
    const tableName = 'calculations_result';
    return new Promise((resolve, reject) => {
      const query = `
        INSERT INTO 
            ??
            (??)
        VALUES
            (?)`;
      const [year, month, day] = radiationEmissionsDate.split('-');
      const date_of_calculation = new Date(year, month - 1, day)
        .toISOString()
        .slice(0, 19)
        .replace('T', ' ');
      const columnNames = [
        'calculation_number',
        'date_of_calculation',
        'id_of_formula',
        'result',
        'id_of_expert',
        'desciption',
      ];
      const result = formulasService.calculate(
        id_of_formula,
        parametersEntries
      );
      const values = [
        calculation_number,
        date_of_calculation,
        id_of_formula,
        result,
        id_of_expert,
        'Soil risk',
      ];
      const parameters = [tableName, columnNames, values];
      pool.query(query, parameters, (error) => {
        if (error) {
          reject(error);
        }
        resolve();
      });
    });
  }

  async _setExternalResult({
    calculation_number,
    id_of_formula,
    id_of_expert,
    parametersEntries,
    radiationEmissionsDate,
  }) {
    const tableName = 'calculations_result';
    return new Promise((resolve, reject) => {
      const query = `
        INSERT INTO 
            ??
            (??)
        VALUES
            (?)`;
      const [year, month, day] = radiationEmissionsDate.split('-');
      const date_of_calculation = new Date(year, month - 1, day)
        .toISOString()
        .slice(0, 19)
        .replace('T', ' ');
      const columnNames = [
        'calculation_number',
        'date_of_calculation',
        'id_of_formula',
        'result',
        'id_of_expert',
        'desciption',
      ];
      const result = formulasService.calculate(
        id_of_formula,
        parametersEntries
      );
      const values = [
        calculation_number,
        date_of_calculation,
        id_of_formula,
        result,
        id_of_expert,
        'External risk',
      ];
      const parameters = [tableName, columnNames, values];
      pool.query(query, parameters, (error) => {
        if (error) {
          reject(error);
        }
        resolve();
      });
    });
  }

  async _setInhalationParameters(
    id_of_formula,
    record,
    id_of_expert,
    risk,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const calculationNumber =
      await calculationService.generateNewCalculationNumber();
    const tableName = 'parameters_value';
    const getParameterValueUsingIdOfParameter = (
      id_of_parameter,
      record,
      risk
    ) => {
      switch (Number(id_of_parameter)) {
        case 901:
          return risk;
        case 902:
          return record.inhalationIntensity;
        case 903:
          return record.volumetricActivity;
      }
    };

    const idsOfParameters = (
      await this._getFormulaParameters({
        id_of_formula,
        id_of_expert,
      })
    ).map(({ id_of_parameter }) => id_of_parameter);

    const parametersEntries = idsOfParameters.map((id_of_parameter) => {
      return {
        id_of_parameter,
      };
    });

    const insertionPromises = idsOfParameters.map(
      (id_of_parameter, index_of_parameter) => {
        const parameter_value = getParameterValueUsingIdOfParameter(
          id_of_parameter,
          record,
          risk
        );
        const parameter = parametersEntries.find(
          (x) => x.id_of_parameter === id_of_parameter
        );
        parameter.parameter_value = parameter_value;
        return new Promise((resolve, reject) => {
          const query = `
            INSERT INTO 
                ??
                (??)
            VALUES
                (?)`;

          const columnNames = [
            'calculation_number',
            'id_of_parameter',
            'parameter_value',
            'index_of_parameter',
            'id_of_expert',
            'id_of_formula',
          ];
          const values = [
            calculationNumber,
            id_of_parameter,
            parameter_value,
            index_of_parameter,
            id_of_expert,
            id_of_formula,
          ];

          const parameters = [tableName, columnNames, values];
          pool.query(query, parameters, (error) => {
            if (error) {
              reject(error);
            }
            resolve();
          });
        });
      }
    );

    return Promise.all(insertionPromises)
      .then(() =>
        this._setDescription({
          calculation_number: calculationNumber,
          id_of_expert,
          calculation_name: 'Inhalation calculation',
          description_of_calculation: 'Inhalation calculation',
        })
      )
      .then(() =>
        this._setInhalationResult({
          calculation_number: calculationNumber,
          id_of_formula,
          id_of_expert,
          parametersEntries,
          radiationEmissionsDate,
        })
      )
      .then(() =>
        calculationService.setCalculationNumberToPoi({
          idPoi: idRadiationPoint,
          calculationNumber,
        })
      );
  }

  async _setWaterParameters(
    id_of_formula,
    record,
    id_of_expert,
    risk,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const calculationNumber =
      await calculationService.generateNewCalculationNumber();
    const tableName = 'parameters_value';
    const getParameterValueUsingIdOfParameter = (
      id_of_parameter,
      record,
      risk
    ) => {
      switch (Number(id_of_parameter)) {
        case 801:
          return risk;
        case 802:
          return record.waterPerYear;
        case 803:
          return record.volumetricActivity;
      }
    };

    const idsOfParameters = (
      await this._getFormulaParameters({
        id_of_formula,
        id_of_expert,
      })
    ).map(({ id_of_parameter }) => id_of_parameter);

    const parametersEntries = idsOfParameters.map((id_of_parameter) => {
      return {
        id_of_parameter,
      };
    });

    const waterPromises = idsOfParameters.map(
      (id_of_parameter, index_of_parameter) => {
        const parameter_value = getParameterValueUsingIdOfParameter(
          id_of_parameter,
          record,
          risk
        );
        const parameter = parametersEntries.find(
          (x) => x.id_of_parameter === id_of_parameter
        );
        parameter.parameter_value = parameter_value;
        return new Promise((resolve, reject) => {
          const query = `
            INSERT INTO 
                ??
                (??)
            VALUES
                (?)`;

          const columnNames = [
            'calculation_number',
            'id_of_parameter',
            'parameter_value',
            'index_of_parameter',
            'id_of_expert',
            'id_of_formula',
          ];
          const values = [
            calculationNumber,
            id_of_parameter,
            parameter_value,
            index_of_parameter,
            id_of_expert,
            id_of_formula,
          ];

          const parameters = [tableName, columnNames, values];
          pool.query(query, parameters, (error) => {
            if (error) {
              reject(error);
            }
            resolve();
          });
        });
      }
    );

    return Promise.all(waterPromises)
      .then(() =>
        this._setDescription({
          calculation_number: calculationNumber,
          id_of_expert,
          calculation_name: 'Water calculation',
          description_of_calculation: 'Water calculation',
        })
      )
      .then(() =>
        this._setWaterResult({
          calculation_number: calculationNumber,
          id_of_formula,
          id_of_expert,
          parametersEntries,
          radiationEmissionsDate,
        })
      )
      .then(() =>
        calculationService.setCalculationNumberToPoi({
          idPoi: idRadiationPoint,
          calculationNumber,
        })
      );
  }

  async _setProductParameters(
    id_of_formula,
    record,
    id_of_expert,
    risk,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const calculationNumber =
      await calculationService.generateNewCalculationNumber();
    const tableName = 'parameters_value';
    const getParameterValueUsingIdOfParameter = (
      id_of_parameter,
      record,
      risk
    ) => {
      switch (Number(id_of_parameter)) {
        case 701:
          return risk;
        case 702:
          return record.productPerYear;
        case 703:
          return record.volumetricActivity;
        case 704:
          return record.radionuclideConsumptionFactor;
      }
    };

    const idsOfParameters = (
      await this._getFormulaParameters({
        id_of_formula,
        id_of_expert,
      })
    ).map(({ id_of_parameter }) => id_of_parameter);
    const parametersEntries = idsOfParameters.map((id_of_parameter) => {
      return {
        id_of_parameter,
      };
    });
    const productPromises = idsOfParameters.map(
      (id_of_parameter, index_of_parameter) => {
        const parameter_value = getParameterValueUsingIdOfParameter(
          id_of_parameter,
          record,
          risk
        );
        const parameter = parametersEntries.find(
          (x) => x.id_of_parameter === id_of_parameter
        );
        parameter.parameter_value = parameter_value;
        return new Promise((resolve, reject) => {
          const query = `
            INSERT INTO 
                ??
                (??)
            VALUES
                (?)`;

          const columnNames = [
            'calculation_number',
            'id_of_parameter',
            'parameter_value',
            'index_of_parameter',
            'id_of_expert',
            'id_of_formula',
          ];
          const values = [
            calculationNumber,
            id_of_parameter,
            parameter_value,
            index_of_parameter,
            id_of_expert,
            id_of_formula,
          ];

          const parameters = [tableName, columnNames, values];
          pool.query(query, parameters, (error) => {
            if (error) {
              reject(error);
            }
            resolve();
          });
        });
      }
    );

    return await Promise.all(productPromises)
      .then(() =>
        this._setDescription({
          calculation_number: calculationNumber,
          id_of_expert,
          calculation_name: 'Product calculation',
          description_of_calculation: 'Product calculation',
        })
      )
      .then(() =>
        this._setProductResult({
          calculation_number: calculationNumber,
          id_of_formula,
          id_of_expert,
          parametersEntries,
          radiationEmissionsDate,
        })
      )
      .then(() =>
        calculationService.setCalculationNumberToPoi({
          idPoi: idRadiationPoint,
          calculationNumber,
        })
      );
  }

  async _setSoilParameters(
    id_of_formula,
    record,
    id_of_expert,
    risk,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const calculationNumber =
      await calculationService.generateNewCalculationNumber();
    const tableName = 'parameters_value';
    const getParameterValueUsingIdOfParameter = (
      id_of_parameter,
      record,
      risk
    ) => {
      switch (Number(id_of_parameter)) {
        case 601:
          return risk;
        case 602:
          return record.soilAbsorptionRate;
        case 603:
          return record.duration;
        case 604:
          return record.volumetricActivity;
      }
    };

    const idsOfParameters = (
      await this._getFormulaParameters({
        id_of_formula,
        id_of_expert,
      })
    ).map(({ id_of_parameter }) => id_of_parameter);
    const parametersEntries = idsOfParameters.map((id_of_parameter) => {
      return {
        id_of_parameter,
      };
    });
    const soilPromises = idsOfParameters.map(
      (id_of_parameter, index_of_parameter) => {
        const parameter_value = getParameterValueUsingIdOfParameter(
          id_of_parameter,
          record,
          risk
        );
        const parameter = parametersEntries.find(
          (x) => x.id_of_parameter === id_of_parameter
        );
        parameter.parameter_value = parameter_value;
        return new Promise((resolve, reject) => {
          const query = `
            INSERT INTO 
                ??
                (??)
            VALUES
                (?)`;

          const columnNames = [
            'calculation_number',
            'id_of_parameter',
            'parameter_value',
            'index_of_parameter',
            'id_of_expert',
            'id_of_formula',
          ];
          const values = [
            calculationNumber,
            id_of_parameter,
            parameter_value,
            index_of_parameter,
            id_of_expert,
            id_of_formula,
          ];

          const parameters = [tableName, columnNames, values];
          pool.query(query, parameters, (error) => {
            if (error) {
              reject(error);
            }
            resolve();
          });
        });
      }
    );

    return await Promise.all(soilPromises)
      .then(() =>
        this._setDescription({
          calculation_number: calculationNumber,
          id_of_expert,
          calculation_name: 'Soil calculation',
          description_of_calculation: 'Soil calculation',
        })
      )
      .then(() =>
        this._setSoilResult({
          calculation_number: calculationNumber,
          id_of_formula,
          id_of_expert,
          parametersEntries,
          radiationEmissionsDate,
        })
      )
      .then(() =>
        calculationService.setCalculationNumberToPoi({
          idPoi: idRadiationPoint,
          calculationNumber,
        })
      );
  }

  async _setExternalParameters(
    id_of_formula,
    record,
    id_of_expert,
    risk,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const calculationNumber =
      await calculationService.generateNewCalculationNumber();
    const tableName = 'parameters_value';
    const getParameterValueUsingIdOfParameter = (
      id_of_parameter,
      record,
      risk
    ) => {
      switch (Number(id_of_parameter)) {
        case 501:
          return risk;
        case 502:
          return record.duration;
        case 503:
          return record.volumetricActivity;
      }
    };

    const idsOfParameters = (
      await this._getFormulaParameters({
        id_of_formula,
        id_of_expert,
      })
    ).map(({ id_of_parameter }) => id_of_parameter);
    const parametersEntries = idsOfParameters.map((id_of_parameter) => {
      return {
        id_of_parameter,
      };
    });
    const externalPromises = idsOfParameters.map(
      (id_of_parameter, index_of_parameter) => {
        const parameter_value = getParameterValueUsingIdOfParameter(
          id_of_parameter,
          record,
          risk
        );
        const parameter = parametersEntries.find(
          (x) => x.id_of_parameter === id_of_parameter
        );
        parameter.parameter_value = parameter_value;
        return new Promise((resolve, reject) => {
          const query = `
            INSERT INTO 
                ??
                (??)
            VALUES
                (?)`;

          const columnNames = [
            'calculation_number',
            'id_of_parameter',
            'parameter_value',
            'index_of_parameter',
            'id_of_expert',
            'id_of_formula',
          ];
          const values = [
            calculationNumber,
            id_of_parameter,
            parameter_value,
            index_of_parameter,
            id_of_expert,
            id_of_formula,
          ];

          const parameters = [tableName, columnNames, values];
          pool.query(query, parameters, (error) => {
            if (error) {
              reject(error);
            }
            resolve();
          });
        });
      }
    );

    return await Promise.all(externalPromises)
      .then(() =>
        this._setDescription({
          calculation_number: calculationNumber,
          id_of_expert,
          calculation_name: 'External calculation',
          description_of_calculation: 'External calculation',
        })
      )
      .then(() =>
        this._setExternalResult({
          calculation_number: calculationNumber,
          id_of_formula,
          id_of_expert,
          parametersEntries,
          radiationEmissionsDate,
        })
      )
      .then(() =>
        calculationService.setCalculationNumberToPoi({
          idPoi: idRadiationPoint,
          calculationNumber,
        })
      );
  }

  async _setParameters({
    id_of_formula,
    record,
    user,
    risk,
    idRadiationPoint,
    radiationEmissionsDate,
  }) {
    switch (id_of_formula) {
      case 900:
        return this._setInhalationParameters(
          id_of_formula,
          record,
          user.id_of_expert,
          risk,
          idRadiationPoint,
          radiationEmissionsDate
        );
      case 800:
        return this._setWaterParameters(
          id_of_formula,
          record,
          user.id_of_expert,
          risk,
          idRadiationPoint,
          radiationEmissionsDate
        );
      case 700:
        return this._setProductParameters(
          id_of_formula,
          record,
          user.id_of_expert,
          risk,
          idRadiationPoint,
          radiationEmissionsDate
        );
      case 600:
        return this._setSoilParameters(
          id_of_formula,
          record,
          user.id_of_expert,
          risk,
          idRadiationPoint,
          radiationEmissionsDate
        );
      case 500:
        return this._setExternalParameters(
          id_of_formula,
          record,
          user.id_of_expert,
          risk,
          idRadiationPoint,
          radiationEmissionsDate
        );
    }
  }

  async _processRadiationEmissions(
    radiationEmissions,
    user,
    idRadiationPoint,
    radiationEmissionsDate
  ) {
    const shouldProcess =
      radiationEmissions.inhalationRecords.length ||
      radiationEmissions.waterRecords.length ||
      radiationEmissions.productRecords.length ||
      radiationEmissions.soilRecords.length ||
      radiationEmissions.externalRecords.length;

    if (!shouldProcess) {
      return;
    }

    const shouldProcessInhalationRecords =
      radiationEmissions.inhalationRecords &&
      radiationEmissions.inhalationRecords.length;

    const shouldProcessWaterRecords =
      radiationEmissions.waterRecords && radiationEmissions.waterRecords.length;

    const shouldProcessProductRecords =
      radiationEmissions.productRecords &&
      radiationEmissions.productRecords.length;

    const shouldProcessSoilRecords =
      radiationEmissions.soilRecords && radiationEmissions.soilRecords.length;

    const shouldProcessExternalRecords =
      radiationEmissions.externalRecords &&
      radiationEmissions.externalRecords.length;

    if (shouldProcessInhalationRecords) {
      await this._processInhalationRecords(
        radiationEmissions.inhalationRecords,
        user,
        idRadiationPoint,
        radiationEmissionsDate
      );
    }

    if (shouldProcessWaterRecords) {
      await this._processWaterRecords(
        radiationEmissions.waterRecords,
        user,
        idRadiationPoint,
        radiationEmissionsDate
      );
    }

    if (shouldProcessProductRecords) {
      await this._processProductRecords(
        radiationEmissions.productRecords,
        user,
        idRadiationPoint,
        radiationEmissionsDate
      );
    }

    if (shouldProcessSoilRecords) {
      await this._processSoilRecords(
        radiationEmissions.soilRecords,
        user,
        idRadiationPoint,
        radiationEmissionsDate
      );
    }

    if (shouldProcessExternalRecords) {
      await this._processExternalRecords(
        radiationEmissions.externalRecords,
        user,
        idRadiationPoint,
        radiationEmissionsDate
      );
    }
  }

  async _getIdOfExpert(id_of_user) {
    const query = `
        SELECT 
        *
        FROM 
        ??
        WHERE
        ?? = ?
        ;`;

    const values = ['user', 'id_of_user', id_of_user];

    return new Promise((resolve, reject) => {
      pool.query(query, values, (error, rows) => {
        if (error) {
          return reject(error);
        }

        if (rows[0]) {
          return resolve(rows[0]);
        }
      });
    });
  }

  async _getRadiationPointInfo(radiationPointId) {
    const dataPromise = new Promise((resolve, reject) => {
      const query = `
        SELECT
          radiation_poi_calculation.calculation_number,
          SUM(calculations_result.result) as result,
            calculations_result.id_of_formula
        FROM radiation_poi_calculation
        INNER JOIN calculations_result ON radiation_poi_calculation.calculation_number = calculations_result.calculation_number
        WHERE radiation_poi_calculation.idPoi = ${radiationPointId}
        GROUP BY calculations_result.id_of_formula
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
    const reducedResults = data.reduce((acc, cur) => {
      switch (+cur.id_of_formula) {
        case 900:
          return {
            ...acc,
            inhalationResult: cur.result,
          };
        case 800:
          return {
            ...acc,
            waterResult: cur.result,
          };
        case 700:
          return {
            ...acc,
            productResult: cur.result,
          };
        case 600:
          return {
            ...acc,
            soilResult: cur.result,
          };
        case 500:
          return {
            ...acc,
            externalResult: cur.result,
          };
      }
    }, {});
    const calculations = await radiationInfoParametersService.getParameters(
      radiationPointId
    );
    const radiationObjectParameters =
      await radiationInfoParametersService.getRadiationObjectParameters(
        radiationPointId
      );
    return {
      results: reducedResults,
      calculations,
      radiationObjectParameters,
    };
  }

  async addRadiationPoint(data) {
    const user = await this._getIdOfExpert(data.generalInfo.id_of_user);
    const idRadiationPoint = await this._insertPoi(data.generalInfo);
    await this._insertRadiationObjectParameters(
      idRadiationPoint,
      data.radiationObjectParameters,
      data.generalInfo.type
    );
    await this._processRadiationEmissions(
      data.radiationEmissions,
      user,
      idRadiationPoint,
      data.radiationEmissionsDate
    );
    return +idRadiationPoint;
  }

  async getRadiationPointInfo(radiationPointId) {
    const info = await this._getRadiationPointInfo(radiationPointId);
    return info;
  }
}

const instance = new RadiationPointService();

module.exports = {
  radiationPointService: instance,
};
