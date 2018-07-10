const uri = 'api/Destinations';

function getData(){
    //fetch api default method is GET  
    fetch(uri)
    .then(response => response.json())
    .then(function(data){
        data.forEach(value => {
            document.getElementById('destinations').innerHTML += '<li id="' + value.id + '">' + value.id + ': ' + value.cityName + ' - ' + value.airport + '</li>';
        });
    })
}


