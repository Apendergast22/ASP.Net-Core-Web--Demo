import { createGlobalState } from 'react-hooks-global-state';

// See details on https://www.npmjs.com/package/react-hooks-global-state
const initial_user = { name: "", surname: "", patronymic: "", birth_day: "", city: "", address: "", tokenStorageKey: null };
export const { useUserState } = createGlobalState(initial_user);

// Erase information about user.
export function clearInfo(){
    const [ , setName ] = useUserState('name');
    const [ , setSurname ] = useUserState('surname');
    const [ , setPatronymic] = useUserState('patronymic');
    const [ , setBirthDay] = useUserState('birth_day');
    const [ , setCity] = useUserState('city');
    const [ , setAddress] = useUserState('address');
    const [ , setTokenKey] = useUserState('tokenStorageKey');

    setName("");
    setSurname("");
    setPatronymic("");
    setPatronymic("");
    setBirthDay("");
    setCity("");
    setAddress("");
    setTokenKey(null);
}
