import { createGlobalState } from 'react-hooks-global-state';

// See details on https://www.npmjs.com/package/react-hooks-global-state
const initial_user_private = { name: "", surname: "", patronymic: "", birth_day: "", city: "", address: "" };
const initial_user = { private: initial_user_private, tokenStorageKey: null };
const { useGlobalState, setGlobalState } = createGlobalState(initial_user);

// Erase information about user.
function ClearInfo(){
    setGlobalState('private', initial_user_private);
    setGlobalState('tokenStorageKey', null);
}

export { ClearInfo, useGlobalState, setGlobalState }
