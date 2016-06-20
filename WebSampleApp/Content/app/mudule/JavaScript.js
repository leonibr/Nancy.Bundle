(function () {
    //test for order, depends of ../app.js
    if (window.appObject == null)
        console.log("appObject does not exists");
    

    try {
        appObject.name(23, 12, 34);
        appObject.age(2004, 2, 3);
    } catch (e) {
        throw e.message;
    }

})();