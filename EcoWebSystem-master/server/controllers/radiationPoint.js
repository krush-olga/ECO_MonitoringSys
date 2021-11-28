const pool = require('../../db-config/mysql-config');

const { radiationPointService } = require('../services/radiationPointService');

const addPoint = async (req, res) => {
  const payload = req.body;
  try {
    const radiationPointId = await radiationPointService.addRadiationPoint(
      payload
    );
    return res.status(201).send({ radiationPointId });
  } catch (error) {
    console.log(error);
    res.status(500).send({
      message: error,
    });
  }
};

const getRadiationPointInfo = async (req, res) => {
  const { id } = req.params;
  if (id) {
    try {
      const radiationInfo = await radiationPointService.getRadiationPointInfo(
        +id
      );
      return res.status(200).send(radiationInfo);
    } catch (error) {
      console.log(error);
      res.status(500).send({
        message: error,
      });
    }
  }
};

module.exports = {
  addPoint,
  getRadiationPointInfo,
};
