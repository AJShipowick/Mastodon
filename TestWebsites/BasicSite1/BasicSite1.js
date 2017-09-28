//Basic Site 1 JavaScript

(function () {
    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            if (request.status === 200) {
                document.body.className = 'ok';
                console.log(request.responseText);
            } else {
                document.body.className = 'error';
            }
        }
    };
    request.open("GET", "http://localhost:51186/api/slider/6666ddfdd", true);
    request.send(null);
})();