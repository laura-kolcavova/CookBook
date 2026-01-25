import React from 'react';

import { Link, type LinkProps } from 'react-router-dom';

export type StyledLinkProps = LinkProps;

export const StyledLink: React.FC<StyledLinkProps & { className?: string }> = ({
  className = '',
  ...props
}) => {
  return <Link className={`text-link-color hover:underline ${className}`} {...props} />;
};
