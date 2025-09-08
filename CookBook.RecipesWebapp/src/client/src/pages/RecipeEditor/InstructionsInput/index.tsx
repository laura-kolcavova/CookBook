import React from 'react';
import {
  InstructionsContainer,
  InstructionItem,
  AddButton,
  RemoveButton,
  StyledInputGroupText,
  StyledTextArea,
} from './styled';
import { FaPlus, FaTrash } from 'react-icons/fa6';
import type { InstructionItemData } from '~/pages/RecipeEditor/models/InstructionItemData';
import { InputGroup } from '~/sharedComponents/forms/InputGroup';

interface InstructionsInputProps {
  instructions: InstructionItemData[];
  onChange: (instructions: InstructionItemData[]) => void;
  label: string;
}

export const InstructionsInput: React.FC<InstructionsInputProps> = ({
  instructions,
  onChange,
  label,
}) => {
  const addInstruction = () => {
    const newInstruction: InstructionItemData = {
      note: '',
    };

    const newInstructions = [...instructions, newInstruction];

    onChange(newInstructions);
  };

  const removeInstruction = (indexToRemove: number) => {
    const newInstructions = instructions.filter((_, index) => index !== indexToRemove);

    onChange(newInstructions);
  };

  const updateInstruction = (indexToUpdate: number, note: string) => {
    const newInstructions = instructions.map((instruction, index) =>
      index === indexToUpdate ? { ...instruction, note } : instruction,
    );

    onChange(newInstructions);
  };

  return (
    <InstructionsContainer>
      <label>{label}</label>

      {instructions.map((instruction, index) => (
        <InstructionItem key={index}>
          <InputGroup>
            <StyledInputGroupText>{index + 1}.</StyledInputGroupText>

            <StyledTextArea
              type="textarea"
              // rows={2}
              placeholder="Describe this step in detail..."
              value={instruction.note}
              onChange={(e) => updateInstruction(index, e.target.value)}
            />

            <RemoveButton
              type="button"
              color="outline-danger"
              onClick={() => removeInstruction(index)}>
              <FaTrash />
            </RemoveButton>
          </InputGroup>
        </InstructionItem>
      ))}

      <AddButton type="button" color="outline-primary" onClick={addInstruction}>
        <FaPlus className="me-1" />
        Add Step
      </AddButton>

      <small className="text-muted mt-1">
        Be specific with temperatures, times, and techniques for best results
      </small>
    </InstructionsContainer>
  );
};
