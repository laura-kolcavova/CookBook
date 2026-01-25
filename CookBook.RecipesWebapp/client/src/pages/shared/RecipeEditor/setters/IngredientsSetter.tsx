import { FaPlus, FaTrash } from 'react-icons/fa6';
import { FormattedMessage, useIntl } from 'react-intl';
import { ingredientsAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTextInput } from '~/pages/shared/forms/FormTextInput';
import { Button } from '~/pages/shared/Button';
import type { RecipeIngredientData } from '../models/RecipeIngredientData';
import { messages } from '../messages';

export const IngredientsSetter = () => {
  const { formatMessage } = useIntl();

  const [ingredients, setIngredients] = useAtom(ingredientsAtom);

  const addIngredient = () => {
    const newIngredient: RecipeIngredientData = {
      localId: null,
      note: '',
    };

    const newIngredients = [...ingredients, newIngredient];

    setIngredients(newIngredients);
  };

  const removeIngredient = (indexToRemove: number) => {
    const newIngredients = ingredients.filter((_, index) => index !== indexToRemove);

    setIngredients(newIngredients);
  };

  const updateIngredient = (indexToUpdate: number, note: string) => {
    const newIngredients = ingredients.map((ingredient, index) =>
      index === indexToUpdate ? { ...ingredient, note } : ingredient,
    );

    setIngredients(newIngredients);
  };

  return (
    <>
      <FormLabel>
        <FormattedMessage {...messages.ingredientsLabel} />
      </FormLabel>

      <div className="mb-4">
        {ingredients.map((ingredient, index) => (
          <div key={index} className="flex flex-row items-center gap-2 mb-4">
            <div className="flex flex-col justify-center h-10">
              <span className="text-base">{index + 1}.</span>
            </div>

            <FormTextInput
              type="text"
              className="h-10"
              placeholder={formatMessage(messages.ingredientPlaceholder)}
              value={ingredient.note}
              onChange={(e) => updateIngredient(index, e.target.value)}
              onKeyDown={(e) => {
                if (e.key === 'Enter') {
                  e.preventDefault();
                  addIngredient();
                }
              }}
            />

            <Button className="h-10" onClick={() => removeIngredient(index)}>
              <FaTrash size="0.875rem" />
            </Button>
          </div>
        ))}
      </div>

      <Button onClick={addIngredient} className="flex justify-center items-center">
        <FaPlus className="mr-1" />
        <span>
          <FormattedMessage {...messages.addIngredientButton} />
        </span>
      </Button>

      <div className="mt-2">
        <small>
          <FormattedMessage {...messages.ingredientsHelpText} />
        </small>
      </div>
    </>
  );
};
