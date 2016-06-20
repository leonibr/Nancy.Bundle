(function () {
    window.appObject = {
        name: funName,
        age: funAge
    };

    function funName(param1, param2, param3) {
        var internalParam1 = (param1 + param2) / param3;

        return internalParam1;
    }

    function funAge(year, month, day) {
        return (new Date(year, month, day)).toDateString();
    }

})();

(function ($) {
    $(document).ready(function () {
        var canvas = $("#canvas");
        var addItem = function (text) {
            var item = $(document.createElement("div"));
            var span = $(document.createElement("span"));
            span.text(text);
            item.append(span);
            canvas.prepend(item);

        }
        var btnOk = $("#btnOk");
        var txt = $("#textField");


        btnOk.on("click", function () {
            console.log("clicked button: " + this.value);
            var responseText =  txt.val()== ""? " null ": txt.val();
            console.log("textField text: " + responseText)
            addItem(responseText);
            txt.val('');
        });
    });


})($);

