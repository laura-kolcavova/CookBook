import { useAtomValue } from 'jotai';
import { useCallback, useState } from 'react';
import { titleAtom } from '../atoms/recipeDataAtom';
import { FieldValidation } from '~/models/forms/FieldValidation';

const validateTitle = (title: string): true | string => {
  if (title.length < 3 || title.length > 256) {
    return 'The recipe title must be in between 3 and 256 characters';
  }

  return true;
};

type FieldValidations = {
  [key in string]: FieldValidation;
};

export const useRecipeValidator = () => {
  const title = useAtomValue(titleAtom);

  const [validations, setValidations] = useState<FieldValidations>({});

  const validate = useCallback(() => {
    let isValid = true;

    const newValidations: FieldValidations = {
      title: {
        isValid: true,
      },
    };

    const titleValidationResult = validateTitle(title);

    if (titleValidationResult !== true) {
      newValidations.title = {
        isValid: false,
        invalidMessage: titleValidationResult,
      };

      isValid = false;
    }

    setValidations(newValidations);

    return isValid;
  }, [title]);

  const resetValidations = useCallback(() => {
    setValidations({});
  }, []);

  return {
    validate,
    resetValidations,
    validations,
  };
};
