import { RadiationRiskCategory } from './radiationRiskCategory';

export const getRadiationBarColor = (radiationRiskCategory) => {
  switch (radiationRiskCategory) {
    case RadiationRiskCategory.Tiny:
      return 'lightgreen';
    case RadiationRiskCategory.Small:
      return 'green';
    case RadiationRiskCategory.Medium:
      return 'orange';
    case RadiationRiskCategory.Critical:
      return 'red';
  }
};
