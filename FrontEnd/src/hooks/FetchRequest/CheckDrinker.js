import { useGlobalState } from '../CurrentUser'
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
    const body = { surname: surname, name: name, patronymic: patronymic, birthDay: birthDay };
    
    return new Request(url, {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    });
}

// Checking somebody by their private info.
export async function CheckUser(name, surname, patronymic, birthDay){
    let response = await fetch(buildRequest(name, surname, patronymic, birthDay));
    if (response.ok){
        return await response.json();
    }

    throw new BadRequestError("Server returned exception during potential drinker checking!", await response.json());
}

// Checking somebody by their token.
export async function CheckLogonUser(){
    const [ private_info, ] = useGlobalState('name');
    return await CheckUser(private_info.name, private_info.surname, private_info.patronymic, private_info.birth_day);
}