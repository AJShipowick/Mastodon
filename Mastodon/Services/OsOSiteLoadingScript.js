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

                let container;
                if (request.responseText.substring(0, 8).includes('function')) {
                    container = document.createElement('script');
                } else {
                    container = document.createElement('div');
                }
                container.innerHTML = request.responseText;
                document.body.appendChild(container);

            } else {
                console.log('Oso Easy Promo Error' + request.responseText);
            }
        }
    };
    request.open("GET", "https://api.osoeasypromo.com/api/promo/USERID", true);
    request.send(null);
})();