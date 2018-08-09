/*This script is for showing a custom promotion on the site*/

/*For Dev testing change the below http get request from 
  * 'api.osoeasypromo.com......' to localhost:51186/api/promo/??YOUR_USER_ID??
  * This will allow you to debug the API locally and not get the response from the live API application
  * 
  * You also MUST enable CORS (Cross site request scripts)
  * This can be done with the Chrome extension 'Allow-Control-Allow-Origin: *'
 */

/*Begin Oso Easy Promo Script*/(function () { let a = new XMLHttpRequest; a.onreadystatechange = function () { if (4 === a.readyState) if (200 === a.status) { if (document.body.className = 'ok', !a.responseText || a.responseText.includes('ERROR') || a.responseText.includes('WARNING')) return void console.log(a.responseText); let b = document.createElement('script'); b.innerHTML = a.responseText, document.getElementsByTagName('head')[0].appendChild(b) } else console.log('OsO Easy Promo Error' + a.responseText) }, a.open('GET', 'http://localhost:51186/api/promo/a6593502-8f7d-4656-9d79-9cfc695dff9e', !0), a.send(null) })();/*End Oso Easy Promo Script*/

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