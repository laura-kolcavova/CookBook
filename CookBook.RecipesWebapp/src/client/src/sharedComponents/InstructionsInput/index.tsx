import React, { useState } from 'react';
import { Input, InputGroup, InputGroupText } from 'reactstrap';
import { InstructionsContainer, InstructionItem, AddButton, RemoveButton } from './styled';
import { FaPlus, FaTrash } from 'react-icons/fa6';

export interface Instruction {
  localId?: number;
  note: string;
}

interface InstructionsInputProps {
  value: Instruction[];
  onChange: (instructions: Instruction[]) => void;
  label: string;
}

export const InstructionsInput: React.FC<InstructionsInputProps> = ({ value, onChange, label }) => {
  const [nextId, setNextId] = useState(Math.max(0, ...value.map((inst) => inst.localId || 0)) + 1);

  const addInstruction = () => {
    const newInstruction: Instruction = {
      localId: nextId,
      note: '',
    };
    onChange([...value, newInstruction]);
    setNextId(nextId + 1);
  };

  const removeInstruction = (localId: number) => {
    onChange(value.filter((inst) => inst.localId !== localId));
  };

  const updateInstruction = (localId: number, note: string) => {
    onChange(value.map((inst) => (inst.localId === localId ? { ...inst, note } : inst)));
  };

  return (
    <InstructionsContainer>
      <label>{label}</label>

      {value.map((instruction, index) => (
        <InstructionItem key={instruction.localId}>
          <InputGroup>
            <InputGroupText>Step {index + 1}</InputGroupText>
            <Input
              type="textarea"
              rows={2}
              placeholder="Describe this step in detail..."
              value={instruction.note}
              onChange={(e) => updateInstruction(instruction.localId!, e.target.value)}
            />
            <RemoveButton
              type="button"
              color="outline-danger"
              onClick={() => removeInstruction(instruction.localId!)}
              disabled={value.length === 1}>
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
