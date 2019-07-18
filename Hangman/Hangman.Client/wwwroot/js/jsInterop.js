const tokenKey = "auth_token";

window.tokenManager = {
    save: function (token) {
        window.localStorage.setItem(tokenKey, token);
        return true;
    },
    get: function () {
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

// File upload logic
window.fileManager = {
    readUploadedFileAsText: async function (inputFileId) {
        let input = document.getElementById(inputFileId);
        let file = input.files[0];

        let data = await readFileContent(file);
        return data;
    }
};

function readFileContent(file) {
    const reader = new FileReader();
    return new Promise((resolve, reject) => {
        reader.onload = event => resolve(event.target.result);
        reader.onerror = error => reject(error);
        reader.readAsText(file);
    });
};

