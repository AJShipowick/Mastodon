//***********************************************************************************************
//***********************************************************************************************
//Use this file to generate a minified version to be used in MainJS.cs class
//Using single quotes instead of double quotes to help with the string minification process
//***********************************************************************************************
//***********************************************************************************************

(function () {
    //create parent element
    let sliderDiv = document.createElement('div');
    sliderDiv.setAttribute('id', 'osoContainer');
    document.body.appendChild(sliderDiv);

    loadSlickHTML();
    loadSlickCSS();
    loadSlickJS();

    function loadSlickHTML() {
        let htmlURL = 'http://localhost:51186/api/promo/html/?';
        getSlickResource(htmlURL, handleHTMLCallback);
    }

    function handleHTMLCallback(data) {
        document.getElementById('osoContainer').innerHTML = data;
    }

    function loadSlickCSS() {
        let cssURL = 'http://localhost:51186/api/promo/css/?';
        getSlickResource(cssURL, handleCSSCallback);
    }

    function handleCSSCallback(data) {
        let style = document.createElement('style');
        style.type = 'text/css';
        style.innerHTML = data;
        document.getElementsByTagName('head')[0].appendChild(style);
    }

    function loadSlickJS() {
        let jsURL = 'http://localhost:51186/api/promo/js/?';
        getSlickResource(jsURL, handleJSCallback);
    }

    function handleJSCallback(data) {
        let script = document.createElement('script');
        script.innerHTML = data;
        document.getElementsByTagName('head')[0].appendChild(script);
    }
})();

function submitSlider() {
    let name = document.getElementById('osoUserName').value;
    let email = document.getElementById('osoUserEmail').value;

    //todo perform some validation on these inputs......

    if (!name || !email) {
        let responseMsg = document.getElementById('osoPromoResponseMessage');
        responseMsg.innerHTML = 'Please fill out all form fields.';
        responseMsg.style.color = 'red';
        return;
    }

    let submitURL = 'http://localhost:51186/api/promo/submit/?/' + name + '/' + email;
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

function handleSubmitCallback(data) {
    console.log(data);
    //    let responseMsg = document.getElementById('osoPromoResponseMessage');

    //    if (data === 'SUCCESS') {
    //        responseMsg.innerHTML = 'You message has been sent!'
    //        responseMsg.style.color = 'green';
    //    } else {

    //        //todo show msg response from server....success/failure and some general reasons why??

    //        responseMsg.innerHTML = '???????????????'
    //        responseMsg.style.color = 'red';
    //    }
}