function read() {

    var resultCard = document.getElementById('resultCard');
    resultCard.removeAttribute('hidden');

    var inprogress = document.getElementById("inprogress");
    inprogress.removeAttribute('hidden');

    var responseContainer = document.getElementById('result');

    var imageURL = document.getElementById('imageURL');

    var request = new XMLHttpRequest();
    request.open("get", "/api/ocr?imageURL=" + imageURL.value, false);
    request.send();

    responseContainer.innerText = JSON.parse(request.responseText);
    inprogress.setAttribute('hidden', 'hidden');

}