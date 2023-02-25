// Build full address.
export function buildAddress(street, build, appartament){
    return street + ", build" + build + ", app " + appartament;
}

// Getting base url fron config.
export function getBaseUrl(){
    const config = require('../config.json');
    return config.use_tls ? config.server_url_ssl : config.server_url;
}
