import { createGlobalState } from 'react-hooks-global-state';

// See details on https://www.npmjs.com/package/react-hooks-global-state
const initial_user = { name: "", surname: "", patronymic: "", birth_day: "", city: "", address: "", tokenStorageKey: null };
const { useGlobalState, setGlobalState } = createGlobalState(initial_user);

// Erase information about user.
function ClearInfo(){
    setGlobalState('name', "");
    setGlobalState('surname', "");
    setGlobalState('patronymic', "");
    setGlobalState('birth_day', "");
    setGlobalState('city', "");
    setGlobalState('address', "");
    setGlobalState('tokenStorageKey', null);
}

export { ClearInfo, useGlobalState, setGlobalState }
