import React, { useState, useEffect } from 'react';

import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from 'recharts';
import { getRadiationBarColor } from '../../utils/getRadiationBarColor';
import { getRadiationRiskCategory } from '../../utils/radiationRiskCategory';

const mapToData = (radiationResults) => {
  const data = [];
  if (radiationResults.inhalationResult) {
    data.push({
      name: 'Інгаляція',
      value: radiationResults.inhalationResult,
      color: getRadiationBarColor(
        getRadiationRiskCategory(radiationResults.inhalationResult)
      ),
    });
  }
  if (radiationResults.waterResult) {
    data.push({
      name: 'Вода',
      value: radiationResults.waterResult,
      color: getRadiationBarColor(
        getRadiationRiskCategory(radiationResults.waterResult)
      ),
    });
  }
  if (radiationResults.productResult) {
    data.push({
      name: 'Продукти',
      value: radiationResults.productResult,
      color: getRadiationBarColor(
        getRadiationRiskCategory(radiationResults.productResult)
      ),
    });
  }
  if (radiationResults.soilResult) {
    data.push({
      name: 'Грунт (пероральне вживання)',
      value: radiationResults.soilResult,
      color: getRadiationBarColor(
        getRadiationRiskCategory(radiationResults.soilResult)
      ),
    });
  }
  if (radiationResults.externalResult) {
    data.push({
      name: 'Зовнішнє опромінення від грунту',
      value: radiationResults.external,
      color: getRadiationBarColor(
        getRadiationRiskCategory(radiationResults.external)
      ),
    });
  }
  return data;
};

export const RadiationBarComparison = ({ radiationResults }) => {
  const [data, setData] = useState([]);

  useEffect(() => {
    setData(mapToData(radiationResults));
  }, [radiationResults]);

  return (
    <ResponsiveContainer width='100%' height={300}>
      <BarChart
        width={500}
        height={300}
        data={data}
        margin={{
          top: 5,
          right: 30,
          left: 20,
          bottom: 5,
        }}
      >
        <CartesianGrid strokeDasharray='3 3' />
        <XAxis dataKey='name' />
        <YAxis />
        <Tooltip cursor={false} />
        <Bar connectNulls type='monotone' dataKey='value' fill={'silver'} />
      </BarChart>
    </ResponsiveContainer>
  );
};
