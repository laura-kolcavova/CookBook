import styled from 'styled-components';

export const LoadingSpinnerWrapper = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`;

export const RecipeContainer = styled.div`
  max-width: 800px;
  margin: 0 auto;
  padding: 2rem;
`;

export const RecipeHeader = styled.div`
  margin-bottom: 2rem;
  border-bottom: 2px solid #f0f0f0;
  padding-bottom: 1.5rem;
`;

export const RecipeTitle = styled.h1`
  color: var(--text-primary-color);
  margin-bottom: 1rem;
  font-size: 2.5rem;
  font-weight: 700;
`;

export const RecipeDescription = styled.p`
  color: #666;
  font-size: 1.1rem;
  line-height: 1.6;
  margin-bottom: 1rem;
`;

export const RecipeMetaInfo = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
  gap: 1rem;
  margin-bottom: 1.5rem;
`;

export const MetaItem = styled.div`
  background: #f8f9fa;
  padding: 1rem;
  border-radius: 8px;
  text-align: center;
`;

export const MetaLabel = styled.div`
  font-weight: 600;
  color: var(--text-primary-color);
  margin-bottom: 0.5rem;
  font-size: 0.9rem;
  text-transform: uppercase;
  letter-spacing: 0.5px;
`;

export const MetaValue = styled.div`
  font-size: 1.2rem;
  color: #666;
`;

export const TagsContainer = styled.div`
  margin-bottom: 2rem;
`;

export const TagsList = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
`;

export const Tag = styled.span`
  background: var(--navbar-background-color);
  color: var(--text-primary-color);
  padding: 0.5rem 1rem;
  border-radius: 20px;
  font-size: 0.9rem;
  font-weight: 500;
`;

export const Section = styled.div`
  margin-bottom: 2rem;
`;

export const SectionTitle = styled.h2`
  color: var(--text-primary-color);
  margin-bottom: 1rem;
  font-size: 1.8rem;
  font-weight: 600;
  border-left: 4px solid var(--navbar-background-color);
  padding-left: 1rem;
`;

export const IngredientsList = styled.ul`
  list-style: none;
  padding: 0;
`;

export const IngredientItem = styled.li`
  background: #f8f9fa;
  margin-bottom: 0.5rem;
  padding: 1rem;
  border-radius: 8px;
  border-left: 4px solid var(--navbar-background-color);
  font-size: 1rem;
  line-height: 1.5;
`;

export const InstructionsList = styled.ol`
  padding: 0;
  counter-reset: instruction-counter;
`;

export const InstructionItem = styled.li`
  background: #f8f9fa;
  margin-bottom: 1rem;
  padding: 1.5rem;
  border-radius: 8px;
  border-left: 4px solid var(--navbar-background-color);
  font-size: 1rem;
  line-height: 1.6;
  list-style: none;
  counter-increment: instruction-counter;
  position: relative;

  &::before {
    content: counter(instruction-counter);
    position: absolute;
    left: -0.5rem;
    top: 0.5rem;
    background: var(--navbar-background-color);
    color: var(--text-primary-color);
    width: 2rem;
    height: 2rem;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    font-weight: 700;
    font-size: 0.9rem;
  }
`;

export const NotesContainer = styled.div`
  background: #fff3cd;
  border: 1px solid #ffeaa7;
  border-radius: 8px;
  padding: 1.5rem;
  margin-top: 2rem;
`;

export const NotesTitle = styled.h3`
  color: #856404;
  margin-bottom: 1rem;
  font-size: 1.2rem;
  font-weight: 600;
`;

export const NotesText = styled.p`
  color: #856404;
  margin: 0;
  line-height: 1.6;
`;
