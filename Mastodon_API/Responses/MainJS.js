//***********************************************************************************************
//***********************************************************************************************
//This file will be minified during the bild process via bundleconfig.json
//Keep all JS pure here, no JQuery as this will be sent to clients that may not have JQuery
//***********************************************************************************************
//***********************************************************************************************

(function () {
    let sliderDiv = document.createElement('div');
    sliderDiv.setAttribute('id', 'osoContainer');
    document.body.appendChild(sliderDiv);
    document.getElementById('osoContainer').style.right = '-300px'

    loadSlickHTML();
    loadSlickCSS();
    loadSlickJS();

    function loadSlickHTML() {
        let htmlURL = 'https://api.osoeasypromo.com/api/promo/html/?';
        getSlickResource(htmlURL, handleHTMLCallback);
    }

    function handleHTMLCallback(data) {
        if (data.includes('Error')) { return; }
        document.getElementById('osoContainer').innerHTML = data;
    }

    function loadSlickCSS() {
        let cssURL = 'https://api.osoeasypromo.com/api/promo/css/?';
        getSlickResource(cssURL, handleCSSCallback);
    }

    function handleCSSCallback(data) {
        let style = document.createElement('style');
        style.type = 'text/css';
        style.innerHTML = data;
        document.getElementsByTagName('head')[0].appendChild(style);
    }

    function loadSlickJS() {
        let jsURL = 'https://api.osoeasypromo.com/api/promo/js/?';
        getSlickResource(jsURL, handleJSCallback);
    }

    function handleJSCallback(data) {
        let script = document.createElement('script');
        script.innerHTML = data;
        document.getElementsByTagName('head')[0].appendChild(script);
    }
})();

function submitOSOEasyPromotion() {
    document.getElementById('osoPromoResponseMessage').style.display = 'none'

    let name = document.getElementById('osoUserName').value;
    let email = document.getElementById('osoUserEmail').value;

    if (!name || !email) {
        let responseMsg = document.getElementById('osoPromoResponseMessage');
        responseMsg.innerHTML = 'Please fill out all form fields.';
        responseMsg.style.color = 'red';
        document.getElementById('osoPromoResponseMessage').style.display = 'block'
        return;
    }

    let submitURL = 'https://api.osoeasypromo.com/api/promo/submit/?/' + name + '/' + email + '/CLIENTID';
    getSlickResource(submitURL, handleSubmitCallback);
}

function getSlickResource(resourceURL, callback) {
    let xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {  //Use 4 && 200 for a server request
            callback(xhr.response); //Outputs a DOMString by default
        }
    }

    xhr.open('GET', resourceURL, true);
    xhr.send();
}

function handleSubmitCallback(submitResponse) {
    document.getElementById('osoFormInput').style.display = 'none';

    if (submitResponse !== 'SUCCESS') {
        document.getElementById('thankYou').innerHTML = 'Error sending coupon code, please verify email is correct and try again later.';
        console.log('Error submitting OsoEasyPromo for user: ' + submitResponse);
    }

    document.getElementById('thankYou').style.display = 'block';
}