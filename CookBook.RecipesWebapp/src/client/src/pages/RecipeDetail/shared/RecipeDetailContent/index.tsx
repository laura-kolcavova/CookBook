import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { useFormatServings } from './hooks/useFormatServings';
import { useFormatCookTime } from './hooks/useFormatCookTime';
import { MetaItem } from './shared/MetaItem';
import { MetaLabel } from './shared/MetaLabel';
import { MetaValue } from './shared/MetaValue';
import { Tag } from '~/pages/shared/Tag';
import { FormattedDate, FormattedMessage } from 'react-intl';
import { messages } from '../../messages';

export type RecipeDetailContentProps = {
  recipe: RecipeDetailDto;
};
export const RecipeDetailContent = ({ recipe }: RecipeDetailContentProps) => {
  const formatServings = useFormatServings();
  const formatCookTime = useFormatCookTime();

  return (
    <>
      <div className="mb-8 border-b-2 border-gray-200 pb-6">
        <div className="mb-6">
          <h2 className="text-2xl text-center text-text-color-primary font-semibold">
            {recipe.title}
          </h2>
        </div>

        <div className="text-center mb-2">
          <span className="text-sm text-text-color-secondary">
            <FormattedDate value={recipe.createdAt} />
          </span>
        </div>

        {recipe.description && (
          <p className="text-lg text-text-color-secondary mb-4">{recipe.description}</p>
        )}

        <div className="grid grid-cols-[repeat(auto-fit,minmax(150px,1fr))] gap-4 mb-6">
          <MetaItem>
            <MetaLabel>
              <FormattedMessage {...messages.servingsLabel} />
            </MetaLabel>
            <MetaValue>{formatServings(recipe.servings)}</MetaValue>
          </MetaItem>

          <MetaItem>
            <MetaLabel>
              <FormattedMessage {...messages.cookTimeLabel} />
            </MetaLabel>
            <MetaValue>{formatCookTime(recipe.cookTime)}</MetaValue>
          </MetaItem>
        </div>

        {recipe.tags.length > 0 && (
          <div className="mb-6">
            <div className="flex flex-wrap gap-2">
              {recipe.tags.map((tag, index) => (
                <Tag key={index} tag={tag} />
              ))}
            </div>
          </div>
        )}
      </div>

      <div className="mb-8">
        <h2 className="text-2xl font-semibold text-text-color-primary mb-4">
          <FormattedMessage {...messages.ingredientsTitle} />
        </h2>

        <ul className="list-none p-0 ml-4">
          {recipe.ingredients.map((ingredient) => (
            <li
              key={ingredient.localId}
              className="bg-gray-100 mb-2 p-4 rounded-md border-l-4 border-[var(--text-text-color-primary)] text-base text-text-color-primary leading-6">
              {ingredient.note}
            </li>
          ))}
        </ul>
      </div>

      <div className="mb-8">
        <h2 className="text-2xl font-semibold text-text-color-primary mb-4">
          <FormattedMessage {...messages.instructionsTitle} />
        </h2>

        <ol className="p-0 ml-4">
          {recipe.instructions.map((instruction, index) => (
            <li key={instruction.localId} className="bg-gray-100 mb-4 p-6 rounded-md relative">
              <div className="absolute w-6 h-6 -left-1 -top-1 rounded-full flex items-center justify-center text-sm leading-normal font-bold bg-button-background-color-primary text-button-color-primary">
                {index + 1}
              </div>

              <span className="text-base leading-relaxed">{instruction.note}</span>
            </li>
          ))}
        </ol>
      </div>

      {recipe.notes && (
        <div>
          <h2 className="text-2xl font-semibold text-text-color-primary mb-4">
            <FormattedMessage {...messages.notesTitle} />
          </h2>

          <p className="text-base text-text-color-secondary m-0 leading-relaxed">{recipe.notes}</p>
        </div>
      )}
    </>
  );
};
