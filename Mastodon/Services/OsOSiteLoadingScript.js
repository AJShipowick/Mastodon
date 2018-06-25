(function () {
    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            if (request.status === 200) {
                document.body.className = 'ok';
                if (request.responseText.includes('ERROR')) {
                    console.log(request.responseText);
                    return;
                }

                var script = document.createElement('script');
                script.innerHTML = request.responseText;
                document.getElementsByTagName('head')[0].appendChild(script);

            } else {                
                console.log('OsO Easy Promo Error' + request.responseText)
            }
        }
    };
    request.open("GET", "http://localhost:51186/api/promo/USERID", true);
    request.send(null);
})();