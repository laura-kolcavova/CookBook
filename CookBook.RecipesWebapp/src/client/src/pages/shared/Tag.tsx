import { FaXmark } from 'react-icons/fa6';

type TagProps = {
  tag: string;
  removeTag?: (tag: string) => void;
};

export const Tag = ({ tag, removeTag }: TagProps) => {
  return (
    <div className="py-2 px-2.5 rounded-lg flex flex-row items-center button-background-color-secondary button-background-color-secondary-hover button-color-secondary transition-colors duration-150">
      <span className="text-sm font-normal align-middle break-all">{tag}</span>

      {removeTag && (
        <button
          type="button"
          className="inline-block align-middle cursor-pointer p-2 -mr-2 -mt-2 -mb-2"
          onClick={() => removeTag(tag)}>
          <FaXmark size="0.875rem" />
        </button>
      )}
    </div>
  );
};
