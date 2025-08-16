import React, { useState, KeyboardEvent } from 'react';
import { Input, Button } from 'reactstrap';
import { TagsContainer, TagsInputGroup, Tag, TagsPresets } from './styled';
import { FaXmark, FaPlus } from 'react-icons/fa6';

interface TagsInputProps {
  value: string[];
  onChange: (tags: string[]) => void;
  label: string;
  presets?: string[];
}

export const TagsInput: React.FC<TagsInputProps> = ({ value, onChange, label, presets }) => {
  const [inputValue, setInputValue] = useState('');

  const addTag = (tag: string) => {
    const trimmedTag = tag.trim();
    if (trimmedTag && !value.includes(trimmedTag)) {
      onChange([...value, trimmedTag]);
    }
    setInputValue('');
  };

  const removeTag = (tagToRemove: string) => {
    onChange(value.filter((tag) => tag !== tagToRemove));
  };

  const handleKeyPress = (e: KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      addTag(inputValue);
    }
  };

  const handlePresetClick = (preset: string) => {
    if (!value.includes(preset)) {
      onChange([...value, preset]);
    }
  };

  return (
    <TagsContainer>
      <label>{label}</label>

      <TagsInputGroup>
        <Input
          type="text"
          placeholder="Type a tag and press Enter..."
          value={inputValue}
          onChange={(e) => setInputValue(e.target.value)}
          onKeyPress={handleKeyPress}
        />
        <Button
          type="button"
          color="outline-primary"
          onClick={() => addTag(inputValue)}
          disabled={!inputValue.trim()}>
          <FaPlus />
        </Button>
      </TagsInputGroup>

      {value.length > 0 && (
        <div className="d-flex flex-wrap gap-2 mt-2">
          {value.map((tag, index) => (
            <Tag key={index}>
              {tag}
              <button type="button" onClick={() => removeTag(tag)} aria-label={`Remove ${tag} tag`}>
                <FaXmark />
              </button>
            </Tag>
          ))}
        </div>
      )}

      {presets && presets.length > 0 && (
        <TagsPresets>
          <small className="text-muted mb-2">Popular tags:</small>
          <div className="d-flex flex-wrap gap-1">
            {presets
              .filter((preset) => !value.includes(preset))
              .map((preset, index) => (
                <Button
                  key={index}
                  size="sm"
                  outline
                  color="secondary"
                  onClick={() => handlePresetClick(preset)}
                  className="preset-tag">
                  {preset}
                </Button>
              ))}
          </div>
        </TagsPresets>
      )}
    </TagsContainer>
  );
};
