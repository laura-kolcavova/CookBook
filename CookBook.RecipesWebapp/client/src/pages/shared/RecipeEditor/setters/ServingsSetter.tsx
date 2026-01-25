import { useAtom } from 'jotai';
import { FormattedMessage } from 'react-intl';
import { servingsAtom } from '../atoms/recipeDataAtom';
import { FormExtendedNumberInput } from '~/pages/shared/forms/FormExtenedNumberInput';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { messages } from '../messages';

const MIN: number = 0;
const MAX: number = 255;

export const ServingsSetter = () => {
  const [servings, setServings] = useAtom(servingsAtom);

  const handleServingsChange = (newServings: number) => {
    setServings(newServings);
  };

  return (
    <>
      <FormLabel>
        <FormattedMessage {...messages.servingsLabel} />
      </FormLabel>

      <FormExtendedNumberInput
        value={servings}
        min={MIN}
        max={MAX}
        onChange={handleServingsChange}
      />

      <div className="mt-1">
        <small>
          <FormattedMessage {...messages.portionsCount} values={{ count: servings }} />
        </small>
      </div>
    </>
  );
};
