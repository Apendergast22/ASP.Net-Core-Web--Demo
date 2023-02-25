import { useUserState } from '../CurrentUser'
import { getBaseUrl } from '../../utilites/CommonUtils'

// Build registry new user request.
function BuildRequest(login, password){
    const [ name, ] = useUserState('name');
    const [ surname, ] = useUserState('surname');
    const [ patronymic, ] = useUserState('patronymic');
    const [ birth_day, ] = useUserState('birth_day');
    const [ city, ] = useUserState('city');
    const [ address, ] = useUserState('address');
    const body = {
      login: login,
      password: password,
      name: name,
      surname: surname,
      patronymic: patronymic,
      birth_day: birth_day,
      city: city,
      address: address
    }
    
    const url = getBaseUrl() + "/RegistryNew";
    return new Request(url, {
        method: 'POST',
        mode: 'cors',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
}

// Parse response body JSON.
function parse(json){
  if (!json.hasOwnProperty("token")){
    return null;
  }

  return json["token"];
}

// Send registry new user request.
async function registryNewUser(login, password){
    let request = BuildRequest(login, password);
    let response = await fetch(request);
    if (response.ok){
      return parse(await response.json());
    }

    return null;
}

export default registryNewUser