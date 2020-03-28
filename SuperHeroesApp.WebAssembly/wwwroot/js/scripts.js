window.storeUserDataToLocalStorage = function (key, userDataStr) {
    if(!!!userDataStr)
        return false;
    
    localStorage.setItem(key, userDataStr);
    return true;
};

window.clearUserDataInLocalStorage = function (key) {
    localStorage.removeItem(key);
}

window.getUserData = function (key) {
    return localStorage.getItem(key);
}