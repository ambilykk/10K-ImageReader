var responseContainer;
var status;

function enableresult() {
    var resultCard = document.getElementById('resultCard');
    resultCard.style.display = "block";   

    status = document.getElementById('status');

    responseContainer = document.getElementById('result');
    responseContainer.innerText = "";
    var imageURL = document.getElementById('imageURL');
}

function read() {
    enableresult();
    status.innerText = "Extracting Text from Image in progress.....";

    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            responseContainer.innerText = JSON.parse(request.responseText);            
            status.innerText = "";
        }
    };
    request.open("get", "/api/ocr?imageURL=" + imageURL.value, true);
    request.send();      
}

function readImage()
{   
    var uploader = document.getElementById('uploader').files;
    if (uploader.length > 0) {
        enableresult();            
        status.innerText = "Extracting Text from Image in progress.....";

        var formData = new FormData();
        formData.append("opmlFile", uploader[0]);

        var request = new XMLHttpRequest();
        request.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                responseContainer.innerText = JSON.parse(request.responseText);
                status.innerText = "";
            }
        };
        request.open("post", "/api/ocr", true);
        request.send(formData);
    }
    else 
        alert("Select an image");    
}

