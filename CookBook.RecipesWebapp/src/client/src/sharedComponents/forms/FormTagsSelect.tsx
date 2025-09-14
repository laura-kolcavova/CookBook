import React, { useState } from 'react';
import { FormTextInput } from './FormTextInput';
import { FaPlus, FaXmark } from 'react-icons/fa6';
import { Button } from '../Button';

export type FormTagsSelectProps = {
  tags: string[];
  onChange: (newTags: string[]) => void;
};

export const FormTagsSelect: React.FC<FormTagsSelectProps> = ({ tags, onChange }) => {
  const [inputValue, setInputValue] = useState<string>('');

  const addTag = (tag: string) => {
    const trimmedTag = tag.trim();

    if (trimmedTag && !tags.includes(trimmedTag)) {
      const newTags = [...tags, trimmedTag];

      onChange(newTags);
    }

    setInputValue('');
  };

  const removeTag = (tagToRemove: string) => {
    const newTags = tags.filter((tag) => tag !== tagToRemove);

    onChange(newTags);
  };

  const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      addTag(inputValue);
    }
  };

  return (
    <>
      <div className="flex flex-row gap-2 mb-4">
        <FormTextInput
          type="text"
          placeholder="Type a tag and press Enter..."
          value={inputValue}
          onChange={(e) => setInputValue(e.target.value)}
          onKeyPress={handleKeyPress}
        />

        <Button onClick={() => addTag(inputValue)} disabled={!inputValue.trim()}>
          <FaPlus />
        </Button>
      </div>

      {tags.length > 0 && (
        <div className="flex flex-row flex-wrap gap-2">
          {tags.map((tag, index) => (
            <Tag key={index} tag={tag} removeTag={removeTag}></Tag>
          ))}
        </div>
      )}
    </>
  );
};

type TagProps = {
  tag: string;
  removeTag: (tag: string) => void;
};

const Tag: React.FC<TagProps> = ({ tag, removeTag }) => {
  return (
    <div className="py-2 px-2.5 rounded-lg flex flex-row items-center button-background-color-secondary button-background-color-secondary-hover button-color-secondary transition-colors duration-150">
      <span className="text-sm font-normal align-middle mr-2">{tag}</span>

      <button
        className="inline-block align-middle cursor-pointer p-2 -m-2"
        onClick={() => removeTag(tag)}>
        <FaXmark size="0.875rem" />
      </button>
    </div>
  );
};
