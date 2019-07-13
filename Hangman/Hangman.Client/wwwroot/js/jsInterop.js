const tokenKey = "auth_token";

window.tokenManager = {
    save: function (token) {
        window.localStorage.setItem(tokenKey, token);
        return true;
    },
    get: function (){
        var token = window.localStorage.getItem(tokenKey);
        return token;
    },
    remove: function () {
        window.localStorage.removeItem(tokenKey);
        return true;
    }
};

window.sessionStorageManager = {
    set: function (key, value) {
        window.sessionStorage.setItem(key, value);
        return true;
    },
    get: function (key) {
        var token = window.sessionStorage.getItem(key);
        return token;
    },
    remove: function (key) {
        window.sessionStorage.removeItem(key);
        return true;
    }
};