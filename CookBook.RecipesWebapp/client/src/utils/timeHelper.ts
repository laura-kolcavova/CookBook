export const getTimeParts = (valueInMinutes: number): { hours: number; minutes: number } => {
  const hours = Math.floor(valueInMinutes / 60);
  const minutes = Math.floor(valueInMinutes % 60);

  return { hours, minutes };
};

export const getTotalMinutes = (hours: number, minutes: number): number => {
  const hoursInMinutes = hours * 60;

  return hoursInMinutes + minutes;
};
