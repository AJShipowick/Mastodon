//Basic Site 1 JavaScript
(function () {
    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            if (request.status === 200) {
                document.body.className = 'ok';
                console.log(request.responseText);

                var script = document.createElement('script');
                script.innerHTML = request.responseText;
                document.getElementsByTagName('head')[0].appendChild(script);

            } else {
                document.body.className = 'error';
            }
        }
    };
    request.open("GET", "http://localhost:51186/api/slider/74198dc4-95a7-45a7-8512-84a900adf4f5", true);
    request.send(null);
})();