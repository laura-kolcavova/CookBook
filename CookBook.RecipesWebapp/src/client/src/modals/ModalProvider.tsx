import type { PropsWithChildren, ReactNode } from 'react';
import { createContext, useCallback, useContext, useMemo, useState } from 'react';

type ModalHandler = {
  modal: ReactNode;
  key: string;
};

export type ModalContextValue = {
  openModal: (modal: ReactNode, key?: string) => void;
  hideModal: () => void;
  currentModalKey: string;
};

const ModalContext = createContext<ModalContextValue | null>(null);

export type ModalProviderProps = PropsWithChildren;

export const ModalProvider = ({ children }: ModalProviderProps) => {
  const [modalHandlers, setModalHandlers] = useState<ModalHandler[]>([]);

  const addModalHandler = useCallback(
    (modalHandler: ModalHandler): void => {
      const newModalHandlers = [...modalHandlers];
      newModalHandlers.push(modalHandler);

      setModalHandlers(newModalHandlers);
    },
    [modalHandlers],
  );

  const popModalHandler = useCallback((): void => {
    if (modalHandlers.length === 0) {
      return;
    }

    const newModalHandlers = [...modalHandlers];
    newModalHandlers.pop();

    setModalHandlers(newModalHandlers);
  }, [modalHandlers]);

  const currentModalKey = useMemo(() => {
    if (modalHandlers.length === 0) {
      return '';
    }

    return modalHandlers[modalHandlers.length - 1].key;
  }, [modalHandlers]);

  const openModal = useCallback(
    (modal: ReactNode, key?: string): void => {
      const modalHandler: ModalHandler = {
        modal: modal,
        key: key ?? '',
      };

      addModalHandler(modalHandler);
    },
    [addModalHandler],
  );

  const hideModal = useCallback((): void => {
    if (modalHandlers.length === 0) {
      return;
    }

    popModalHandler();
  }, [modalHandlers.length, popModalHandler]);

  return (
    <ModalContext.Provider value={{ openModal, hideModal, currentModalKey }}>
      {children}

      {modalHandlers.length > 0 && modalHandlers.map((modalHandlers) => modalHandlers.modal)}
    </ModalContext.Provider>
  );
};

export const useModals = () => {
  const contextValue = useContext(ModalContext);

  if (contextValue === null) {
    throw new Error('ModalProvider missing');
  }

  return contextValue;
};
