function read() {
    if (navigator.onLine) {
        enableresult();
     
        var imageURL = document.getElementById('imageURL');

        var request = new XMLHttpRequest();
        request.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('result').innerText = JSON.parse(request.responseText);
                document.getElementById("status").textContent = "";
            }
        };
        request.open("get", "/api/ocr?imageURL=" + imageURL.value, true);
        request.send();
    }
    else
    {
        offlinemsg();
    }
}

function readImage()
{
    if (navigator.onLine) {
        var uploader = document.getElementById('uploader').files;
        if (uploader.length > 0) {
            enableresult();         

            var formData = new FormData();
            formData.append("opmlFile", uploader[0]);

            var request = new XMLHttpRequest();
            request.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    document.getElementById('result').innerText = JSON.parse(request.responseText);
                    document.getElementById("status").textContent = "";
                }
            };
            request.open("post", "/api/ocr", true);
            request.send(formData);
        }
        else
            alert("Select an image");
    }
    else
    {
        offlinemsg();
    }
}

function enableresult() {
    var resultCard = document.getElementById('resultCard');
    resultCard.style.display = "block";

    document.getElementById("status").textContent = "Extracting Text from Image in progress.....";
    document.getElementById('result').innerText = "";    
}

function offlinemsg()
{
    var resultCard = document.getElementById('resultCard');
    resultCard.style.display = "block";

    document.getElementById("status").textContent =  "Application is Offline. Please save your pictures and feed to the application when Online.";
    document.getElementById("read").disabled = true;
    document.getElementById("upload").disabled = true;
}
