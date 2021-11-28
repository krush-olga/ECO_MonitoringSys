import React, { useState, useEffect } from 'react';

import { Alert } from 'react-bootstrap';
import {
  RadiationRiskCategory,
  getRadiationRiskCategory,
} from '../../utils/radiationRiskCategory';

const recommendations = {
  [RadiationRiskCategory.Tiny]:
    'У встановленому порядку джерело іонізуючого опромінення може бути виведений із-під регулярного контролю. Захисні міри не передбачені. Рівень радіаційного ризику мінімальний й не несе жодних небезпечних наслідків для довкілля та здоров`я людини.',
  [RadiationRiskCategory.Small]:
    'Необхідні періодичні відбіри проб та виміри питомої й/або об’ємної активності радіонуклідів у компонентах природнього середовища та продуктах харчування для підтвердження неперевищення заданого радіаційного ризику. Захисні міри не передбачені. Рівень радіаційного ризику низький та не потребує жодних карантинних дій.',
  [RadiationRiskCategory.Medium]:
    'Необхідний постійний автоматизований моніторинг радіаційного стану: потужності дози гама-випромінювання, деяких компонентів природнього середовища, з можливістю сигналу тривоги. Також потрібен постійний відбір проб атмосферного повітря, атмосферних викидів і поверхневих вод з періодичними замірами питомої та об’ємної активності радіонуклідів у лабораторії. Додатково необхідно проводити періодичний відбір проб грунту, відкладень на дні, біоіндикаторів з подальшими замірами питомих та об’ємних активностей радіонуклідів у пробах лабораторії. Захисні міри передбачені по мірі необхідності.',
  [RadiationRiskCategory.Critical]:
    'Рівень радіаційного ризику критичний. Високий рівень небезпеки, потрібний моніторинг радіаційного стану у зоні обстеження, на радіаційно зараженій території, а також на контрольній ділянці по спеціальній програмі. Негайно потрібні захисні міри для забезпечення безпеки населення та довкілля. У разі необхідності потрібно розглянути карантинні дії та евакуацію з районів зараження. Висока йморівність радіаційного ураження органів. Необхідне підключення відповідних органів щодо питань радіаційної безпеки.',
};

const getRiskUserCategory = {
  [RadiationRiskCategory.Tiny]: 'Мінімальний',
  [RadiationRiskCategory.Small]: 'Низький',
  [RadiationRiskCategory.Medium]: 'Середній',
  [RadiationRiskCategory.Critical]: 'Критичний',
};

const alertVariants = {
  [RadiationRiskCategory.Tiny]: 'success',
  [RadiationRiskCategory.Small]: 'success',
  [RadiationRiskCategory.Medium]: 'warning',
  [RadiationRiskCategory.Critical]: 'danger',
};

const getAlertVariant = (radiationRiskCategory) => {
  return alertVariants[radiationRiskCategory];
};

const getRiskDescription = (radiationRiskCategory) => {
  return getRiskUserCategory[radiationRiskCategory];
};

export const RadiationRiskRecommendations = ({ radiationRisk }) => {
  const [recommendation, setRecommendation] = useState(null);
  const [alertVariant, setAlertVariant] = useState(null);
  const [radiationRiskCategory, setRadiationRiskCategory] = useState(null);

  useEffect(() => {
    const category = getRadiationRiskCategory(radiationRisk);
    setRadiationRiskCategory(category);
  }, [radiationRisk]);

  useEffect(() => {
    setRecommendation(recommendations[radiationRiskCategory]);
    setAlertVariant(getAlertVariant(radiationRiskCategory));
  }, [radiationRiskCategory]);

  return (
    <Alert variant={alertVariant}>
      <strong>
        Категорія ризику: {getRiskDescription(radiationRiskCategory)}
      </strong>
      <div>{recommendation}</div>
    </Alert>
  );
};
