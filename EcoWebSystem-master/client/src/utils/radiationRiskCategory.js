export const RadiationRiskCategory = {
  Critical: 'Critical',
  Medium: 'Medium',
  Small: 'Small',
  Tiny: 'Tiny',
};

export const getRadiationRiskCategory = (radiationRisk) => {
  if (radiationRisk > 5 * 1e-5) {
    return RadiationRiskCategory.Critical;
  }
  if (radiationRisk >= 1e-5 && radiationRisk < 5 * 1e-5) {
    return RadiationRiskCategory.Medium;
  }
  if (radiationRisk >= 1e-6 && radiationRisk < 1e-5) {
    return RadiationRiskCategory.Small;
  }
  if (radiationRisk > 0 && radiationRisk < 1e-6) {
    return RadiationRiskCategory.Tiny;
  }
};
