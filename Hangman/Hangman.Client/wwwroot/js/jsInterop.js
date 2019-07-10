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