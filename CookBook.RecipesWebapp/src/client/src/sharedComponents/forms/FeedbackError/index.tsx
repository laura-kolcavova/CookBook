import React from 'react';

type FeedbackErrorProps = {
  message: string;
};

export const FeedbackError: React.FC<FeedbackErrorProps> = ({ message }) => {
  return <div className="w-full mt-1 text-sm text-red-600">{message}</div>;
};
