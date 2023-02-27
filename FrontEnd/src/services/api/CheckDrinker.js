import { getBaseUrl } from '../../utilites/CommonUtils';
import BadRequestError from '../../model/errors/BadRequestError';
import Cookies from 'universal-cookie';

// Building request to check user in drinkers database.
function buildRequest(name, surname, patronymic, birthDay){
    let url = getBaseUrl() + "/drinkers";
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
  try{
    let response = await fetch(buildRequest(name, surname, patronymic, birthDay));
      if (response.ok){
          return await response.json();
      }
  
      throw new BadRequestError("Server returned exception during potential drinker checking!", await response.json());
  }
  catch (error){
    throw new BadRequestError("Can't requst drinkers data!", { fullError: error });
  }
}

// Checking somebody by their token.
export async function CheckLogonUser(){
  const cookies = new Cookies();
  const private_info = cookies.get('private');
  return await CheckUser(private_info.name, private_info.surname, private_info.patronymic, private_info.birth_day);
}