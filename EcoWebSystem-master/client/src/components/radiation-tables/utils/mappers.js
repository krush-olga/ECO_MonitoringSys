export const mapRows = (rows) => {
  if (!rows.length) {
    return [];
  }
  const newRows = rows.map((row) => ({
    ...row,
    element: row.element.short_name,
  }));
  return newRows;
};

export const mapColumns = (columns) => {
  return Object.entries(columns).map(([key, value]) => ({
    headerName: value,
    field: key,
    sortable: true,
    filter: true,
    resizable: true,
  }));
};
