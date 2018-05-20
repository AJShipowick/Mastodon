//Basic Site 1 JavaScript
(function () {
    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            if (request.status === 200) {
                document.body.className = 'ok';
                //console.log('OsO Easy Promo successfully loaded');
                if (request.responseText.includes('ERROR')) {
                    console.log('OsO Easy Promo: ' + request.responseText);
                    return;
                }

                //let osoPromoDiv = document.createElement('div');
                //osoPromoDiv.innerHTML = request.responseText;
                //document.getElementsByTagName('head')[0].appendChild(osoPromoDiv);

                var script = document.createElement('script');
                script.innerHTML = request.responseText;
                document.getElementsByTagName('head')[0].appendChild(script);

            } else {
                document.body.className = 'error';
                console.log('Unknown error receiving OsO Easy Promotion.')
            }
        }
    };
    request.open("GET", "http://localhost:51186/api/promo/eb11ffd7-a837-44dc-9102-0145baf92207", true);
    request.send(null);
})();