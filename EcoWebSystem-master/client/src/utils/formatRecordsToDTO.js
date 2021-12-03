export function formatRecordsToDTO(records) {
  const radiationRecords = { ...records };
  if (radiationRecords.inhalationRecords.length) {
    radiationRecords.inhalationRecords =
      radiationRecords.inhalationRecords.map(iteratorFunction);
  }
  if (radiationRecords.waterRecords.length) {
    radiationRecords.waterRecords =
      radiationRecords.waterRecords.map(iteratorFunction);
  }
  if (radiationRecords.productRecords.length) {
    radiationRecords.productRecords =
      radiationRecords.productRecords.map(iteratorFunction);
  }
  if (radiationRecords.soilRecords.length) {
    radiationRecords.soilRecords =
      radiationRecords.soilRecords.map(iteratorFunction);
  }
  if (radiationRecords.externalRecords.length) {
    radiationRecords.externalRecords =
      radiationRecords.externalRecords.map(iteratorFunction);
  }
  return radiationRecords;
}

function removeDisplayValue(element) {
  const { displayName, ...data } = element;
  return data;
}

function iteratorFunction(record) {
  const data = record;
  data.element = removeDisplayValue(data.element);
  return data;
}
