/*This script is for showing a custom promotion on the site*/

/*For Dev testing change the below http get request from 
  * 'api.osoeasypromo.com......' to localhost:51186/api/promo/??YOUR_USER_ID??
  * This will allow you to debug the API locally and not get the response from the live API application
  * 
  * You also MUST enable CORS (Cross site request scripts)
  * This can be done with the Chrome extension 'Allow-Control-Allow-Origin: *'
 */

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
    request.open("GET", "http://localhost:51186/api/promo/7d052744-6a60-4b1f-90c5-b976bcad55bf", true);
    request.send(null);
})();

function contactUs() {
    if (!formValid()) { return; }

    axios.get('SendContactRequest' + "?userEmail=" + $('#userEmail').val() + "&userComment=" + $('#userComment').val())
        .then(function (response) {
            $('#sendConfirmation').show();
        })
        .catch(function (error) {
            //todo Handle errors
        });
}

function formValid() {
    $('#confirmationError').hide();

    if ($('#userEmail').val() && $('#userComment').val()) {
        return true;
    } else {
        $('#confirmationError').show();
        return false;
    }
}