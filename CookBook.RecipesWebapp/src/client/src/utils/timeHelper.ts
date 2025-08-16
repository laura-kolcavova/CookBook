export const getTimeParts = (
  valueInMinutes: number,
): { days: number; hours: number; minutes: number } => {
  const days = Math.floor(valueInMinutes / (24 * 60));
  const hours = Math.floor((valueInMinutes / 60) % 24);
  const minutes = Math.floor(valueInMinutes % 60);

  return { days, hours, minutes };
};

export const getTotalMinutes = (days: number, hours: number, minutes: number): number => {
  const daysInMinutes = days * 24 * 60;
  const hoursInMinutes = hours * 60;

  return daysInMinutes + hoursInMinutes + minutes;
};
