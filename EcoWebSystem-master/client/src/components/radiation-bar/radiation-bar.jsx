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
import {
  getRadiationRiskCategory,
  RadiationRiskCategory,
} from '../../utils/radiationRiskCategory';

const mapToData = ({ title, radiationRisk }) => {
  return [
    {
      name: title,
      value: radiationRisk,
    },
  ];
};

export const RadiationBar = ({ radiationRisk, title }) => {
  const [data, setData] = useState([{ value: 0, name: '' }]);
  const [color, setColor] = useState('grey');
  const [radiationRiskCategory, setRadiationRiskCategory] = useState(null);

  useEffect(() => {
    setData(mapToData({ title, radiationRisk }));
  }, [radiationRisk, title]);

  useEffect(() => {
    const category = getRadiationRiskCategory(radiationRisk);
    setRadiationRiskCategory(category);
  }, [radiationRisk]);

  useEffect(() => {
    setColor(getRadiationBarColor(radiationRiskCategory));
  }, [radiationRiskCategory]);

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
        <Bar connectNulls type='monotone' dataKey='value' fill={color} />
      </BarChart>
    </ResponsiveContainer>
  );
};
