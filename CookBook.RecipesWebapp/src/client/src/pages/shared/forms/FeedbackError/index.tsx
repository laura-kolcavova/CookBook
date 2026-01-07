import React from 'react';

type FeedbackErrorProps = {
  message: string;
};

export const FeedbackError: React.FC<FeedbackErrorProps> = ({ message }) => {
  return <div className="mt-2 text-sm text-red-700">{message}</div>;
};
