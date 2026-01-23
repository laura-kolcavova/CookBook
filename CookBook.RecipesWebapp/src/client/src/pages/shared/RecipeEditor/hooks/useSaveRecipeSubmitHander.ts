import { useState } from 'react';
import { useRecipeValidator } from './useRecipeValidator';
import { useSaveRecipeMutation } from './useSaveRecipeMutation';
import type { FieldValidations } from '~/forms/FieldValidations';
import { areValid } from '~/utils/forms/fieldValidationUtils';

export const useSaveRecipeSubmitHandler = () => {
  const {
    mutate: saveRecipeMutate,
    isPending: saveRecipeIsPending,
    isSuccess: saveRecipeIsSuccess,
    isError: saveRecipeIsError,
    data: saveRecipeData,
    error: saveRecipeError,
  } = useSaveRecipeMutation();

  const { validate } = useRecipeValidator();

  const [validations, setValidations] = useState<FieldValidations>({});

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const validationResults = validate();

    setValidations(validationResults);

    console.log(validationResults);

    if (!areValid(validationResults)) {
      return;
    }

    saveRecipeMutate();
  };

  return {
    validations,
    saveRecipeIsPending,
    saveRecipeIsSuccess,
    saveRecipeIsError,
    saveRecipeData,
    saveRecipeError,
    handleSubmit,
  };
};
