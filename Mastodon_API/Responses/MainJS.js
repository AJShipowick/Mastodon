//***********************************************************************************************
//***********************************************************************************************
//Use this file to generate a minified version to be used in MainJS.cs class
//Using single quotes instead of double quotes to help with the string minification process
//***********************************************************************************************
//***********************************************************************************************

(function () {
    //create parent element
    var sliderDiv = document.createElement('div');
    sliderDiv.setAttribute('id', 'sliderContainer');
    document.body.appendChild(sliderDiv);

    loadSlickHTML();
    loadSlickCSS();
    loadSlickJS();

    //todo, this timeout is not the best solution.
    //But...the image will not assign to the HTML property unless there is a timelapse in this call......sometimes....
    setTimeout(
        function () {
            loadSlickImage();
        }, 250);


    function loadSlickHTML() {
        var htmlURL = 'http://localhost:51186/api/slider/html/?';
        getSlickResource(htmlURL, handleHTMLCallback);
    }

    function handleHTMLCallback(data) {
        document.getElementById('sliderContainer').innerHTML = data;
    }

    function loadSlickCSS() {
        var cssURL = 'http://localhost:51186/api/slider/css/?';
        getSlickResource(cssURL, handleCSSCallback);
    }

    function handleCSSCallback(data) {
        var style = document.createElement('style');
        style.type = 'text/css';
        style.innerHTML = data;
        document.getElementsByTagName('head')[0].appendChild(style);
    }

    function loadSlickJS() {
        var jsURL = 'http://localhost:51186/api/slider/js/?';
        getSlickResource(jsURL, handleJSCallback);
    }

    function handleJSCallback(data) {
        var script = document.createElement('script');
        script.innerHTML = data;
        document.getElementsByTagName('head')[0].appendChild(script);
    }

    //https://stackoverflow.com/questions/35367830/load-an-image-from-a-xhr-request-and-then-pass-it-to-the-server
    function loadSlickImage() {
        var imageURL = 'http://localhost:51186/api/slider/image/?';
        getImageResource(imageURL, handleImageCallback);
    }

    function handleImageCallback(data) {
        showSlickImageOnScreen(data);
    }

    function getSlickResource(resourceURL, callback) {
        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {  //Use 4 && 200 for a server request
                callback(xhr.response); //Outputs a DOMString by default
            } else {
                //todo handle errors
            }
        }

        xhr.open('GET', resourceURL, true);
        xhr.send();

    }

    function getImageResource(imageURL, callback) {
        var oReq = new XMLHttpRequest();
        oReq.open('GET', imageURL, true);
        oReq.responseType = 'blob';

        oReq.onload = function (oEvent) {
            callback(oReq.response);
        };
        oReq.send();
    }

    function showSlickImageOnScreen(blobData) {
        var urlCreator = window.URL || window.webkitURL;
        var imageUrl = urlCreator.createObjectURL(blobData);
        document.querySelector('#slickImage').src = imageUrl;
    }

})();