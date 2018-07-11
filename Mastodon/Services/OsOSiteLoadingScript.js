(function () {
    let request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            if (request.status === 200) {
                document.body.className = 'ok';
                if (!request.responseText || request.responseText.includes('ERROR') || request.responseText.includes('WARNING')) {
                    console.log(request.responseText);
                    return;
                }

                let script = document.createElement('script');
                script.innerHTML = request.responseText;
                document.getElementsByTagName('head')[0].appendChild(script);

            } else {                
                console.log('OsO Easy Promo Error' + request.responseText)
            }
        }
    };
    request.open("GET", "https://api.osoeasypromo.com/api/promo/USERID", true);
    request.send(null);
})();