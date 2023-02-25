import { useUserState } from '../CurrentUser'
import { getBaseUrl } from '../../utilites/CommonUtils'

// This error occurs when server returns response with any error.
class BadRequestError extends Error {
    constructor(message, errorJson) {
      super(message);
      this.name = "BadRequestError";
      this.errorDetails = errorJson;
    }

    // Get error details in JSON format.
    get Details(){
        return this.errorDetails;
    }
  }

// Building request to check user in drinkers database.
function buildRequest(name, surname, patronymic, birthDay){
    let url = getBaseUrl() + "/Drinkers";
    body = { surname: surname, name: name, patronymic: patronymic, birthDay: birthDay };
    
    return new Request(url, {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify()
    });
}

// Checking somebody by their private info.
export async function checkUser(name, surname, patronymic, birthDay){
    let response = await fetch(buildRequest(name, surname, patronymic, birthDay));
    if (response.ok){
        return await response.json();
    }

    throw new BadRequestError("Server returned exception during potential drinker checking!", await response.json());
}

// Checking somebody by their token.
export async function checkLogonUser(){
    const [ name, ] = useUserState('name');
    const [ surname, ] = useUserState('surname');
    const [ patronymic, ] = useUserState('patronymic');
    const [ birthDay, ] = useUserState('birth_day');

    return await checkUser(name, surname, patronymic, birthDay);
}