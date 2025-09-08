import React, { useMemo } from 'react';
import { useParams } from 'react-router-dom';
import { useRecipeDetailQuery } from './hooks/useGetRecipeDetailQuery';
import {
  LoadingSpinnerWrapper,
  RecipeContainer,
  RecipeHeader,
  RecipeTitle,
  RecipeDescription,
  RecipeMetaInfo,
  MetaItem,
  MetaLabel,
  MetaValue,
  TagsContainer,
  TagsList,
  Tag,
  Section,
  SectionTitle,
  IngredientsList,
  IngredientItem,
  InstructionsList,
  InstructionItem,
  NotesContainer,
  NotesTitle,
  NotesText,
} from './styled';
import { LoadingSpinner } from '~/sharedComponents/LoadingSpinner';
import { ErrorAlert } from '~/sharedComponents/alerts/ErrorAlert';
import { Alert } from '~/sharedComponents/alerts/Alert';

export const RecipeDetail: React.FC = () => {
  const { recipeId: recipeIdParam } = useParams();

  if (recipeIdParam === undefined) {
    throw new Error('Recipe ID is missing in the URL.');
  }

  const recipeId = useMemo(() => Number.parseInt(recipeIdParam, 10), [recipeIdParam]);

  const { isLoading, isError, data, error } = useRecipeDetailQuery(recipeId);

  const formatTime = (minutes: number): string => {
    if (minutes < 60) {
      return `${minutes} min`;
    }
    const hours = Math.floor(minutes / 60);
    const remainingMinutes = minutes % 60;
    return remainingMinutes > 0 ? `${hours}h ${remainingMinutes}min` : `${hours}h`;
  };

  return isLoading ? (
    <LoadingSpinnerWrapper>
      <LoadingSpinner />
    </LoadingSpinnerWrapper>
  ) : isError ? (
    <ErrorAlert error={error} />
  ) : !data ? (
    <Alert color="warning">No recipe found.</Alert>
  ) : (
    <RecipeContainer>
      <RecipeHeader>
        <RecipeTitle>{data.recipeDetail.title}</RecipeTitle>

        {data.recipeDetail.description && (
          <RecipeDescription>{data.recipeDetail.description}</RecipeDescription>
        )}

        <RecipeMetaInfo>
          <MetaItem>
            <MetaLabel>Servings</MetaLabel>
            <MetaValue>{data.recipeDetail.servings}</MetaValue>
          </MetaItem>

          <MetaItem>
            <MetaLabel>Prep Time</MetaLabel>
            <MetaValue>{formatTime(data.recipeDetail.preparationTime)}</MetaValue>
          </MetaItem>

          <MetaItem>
            <MetaLabel>Cook Time</MetaLabel>
            <MetaValue>{formatTime(data.recipeDetail.cookTime)}</MetaValue>
          </MetaItem>

          <MetaItem>
            <MetaLabel>Total Time</MetaLabel>
            <MetaValue>
              {formatTime(data.recipeDetail.preparationTime + data.recipeDetail.cookTime)}
            </MetaValue>
          </MetaItem>
        </RecipeMetaInfo>

        {data.recipeDetail.tags.length > 0 && (
          <TagsContainer>
            <TagsList>
              {data.recipeDetail.tags.map((tag, index) => (
                <Tag key={index}>{tag}</Tag>
              ))}
            </TagsList>
          </TagsContainer>
        )}
      </RecipeHeader>

      <Section>
        <SectionTitle>Ingredients</SectionTitle>
        <IngredientsList>
          {data.recipeDetail.ingredients.map((ingredient) => (
            <IngredientItem key={ingredient.localId}>{ingredient.note}</IngredientItem>
          ))}
        </IngredientsList>
      </Section>

      <Section>
        <SectionTitle>Instructions</SectionTitle>
        <InstructionsList>
          {data.recipeDetail.instructions.map((instruction) => (
            <InstructionItem key={instruction.localId}>{instruction.note}</InstructionItem>
          ))}
        </InstructionsList>
      </Section>

      {data.recipeDetail.notes && (
        <NotesContainer>
          <NotesTitle>Notes</NotesTitle>
          <NotesText>{data.recipeDetail.notes}</NotesText>
        </NotesContainer>
      )}
    </RecipeContainer>
  );
};
