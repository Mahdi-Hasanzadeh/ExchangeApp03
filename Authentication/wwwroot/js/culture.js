window.setCultureCookie = function (culture) {
    document.cookie = `.AspNetCore.Culture=c=${culture}|uic=${culture}; path=/`;
};
