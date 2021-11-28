class FormulasService {
  _calculateInhalation(parameterValues) {
    return parameterValues[0] * parameterValues[1] * parameterValues[2];
  }

  _calculateWater(parameterValues) {
    return parameterValues[0] * parameterValues[1] * parameterValues[2];
  }

  _calculateProduct(parameterValues) {
    return (
      parameterValues[0] *
      parameterValues[1] *
      parameterValues[2] *
      parameterValues[3]
    );
  }

  _calculateSoil(parameterValues) {
    return (
      parameterValues[0] *
      parameterValues[1] *
      parameterValues[2] *
      parameterValues[3]
    );
  }

  _calculateExternal(parameterValues) {
    return parameterValues[0] * parameterValues[1] * parameterValues[2];
  }

  calculate(id_of_formula, parametersEntries) {
    const parameterValues = parametersEntries.map(
      ({ parameter_value }) => parameter_value
    );
    switch (id_of_formula) {
      case 900:
        return this._calculateInhalation(parameterValues);
      case 800:
        return this._calculateWater(parameterValues);
      case 700:
        return this._calculateProduct(parameterValues);
      case 600:
        return this._calculateSoil(parameterValues);
      case 500:
        return this._calculateExternal(parameterValues);
    }
  }
}

const instance = new FormulasService();

module.exports = {
  formulasService: instance,
};
