
export function tryStoreToken(id, token){
    try {
        window.sessionStorage.setItem(id, token);
        return true
      } catch (error) {
        console.error(error);
        return false;
      }
    
}

export function extractToken(id){
    return window.sessionStorage.getItem(id);
}

export function removeToken(id){
    window.sessionStorage.removeItem(id);
}
