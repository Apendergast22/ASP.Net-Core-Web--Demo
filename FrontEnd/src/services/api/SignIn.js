import { getBaseUrl } from '../../utilites/CommonUtils'

// Registred user info.
class UserInfo {
    constructor(name, surname, patronymic, birthDay, city, address, token){
        this.name = name;
        this.surname = surname;
        this.patronymic = patronymic;
        this.birthDay = birthDay;
        this.city = city;
        this.address = address;
        this.token = token;
    }

    // Get user's name.
    get Name(){
        return this.name;
    }

    // Get user's surname.
    get Surname(){
        return this.surname;
    }

    // Get user's patronymic.
    get Patronymic(){
        return this.patronymic;
    }

    // Get user's birth day.
    get BirthDay(){
        return this.birthDay;
    }

    // Get city where user was born..
    get City(){
        return this.city;
    }

    // Get address where user was born.
    get Address(){
        return this.address;
    }

    // Get access token.
    get Token(){
        return this.token;
    }
}


// Build registry new user request.
function buildRequest(login, password, keepUser){
    const body = {
      login: login,
      password: password,
      keepUser: keepUser
    }
    
    const url = getBaseUrl() + "/signin";
    return new Request(url, {
        method: 'GET',
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
  
    return new UserInfo(json["name"], json["surname"], json["patronymic"], json["birth_day"], json["city"], json["address"], json["token"]);
  }
  
// Send registry new user request.
async function signIn(login, password){
    let request = buildRequest(login, password);
    let response = await fetch(request);
    if (response.ok){
      return parse(await response.json());
    }

    return null;
}

export default signIn