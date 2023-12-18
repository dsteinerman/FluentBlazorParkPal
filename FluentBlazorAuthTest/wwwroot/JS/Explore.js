let map;

function initMap() {
    // Creates a new Bing Maps instance in the 'map' div
    map = new Microsoft.Maps.Map(document.getElementById('map'), {
        credentials: 'AtqNLHA6sIAE94ZXG6Nzm1tNb_igZu-VlDagoYFnsPc0LL1-v96Pju5pS8VyPTF8',
        center: new Microsoft.Maps.Location(0, 0),
        zoom: 2
    });
}

window.onload = function () {
    initMap();
    GetMap();
};

// Event listener for the Find Location button
document.getElementById('findBtn').addEventListener('click', function () {
    // gets Bing Maps data based on the  address
    let address = document.getElementById('addressInput').value;
    if (address.trim() !== '') {
        fetch(`https://dev.virtualearth.net/REST/v1/Locations?query=${encodeURIComponent(address)}&key=AtqNLHA6sIAE94ZXG6Nzm1tNb_igZu-VlDagoYFnsPc0LL1-v96Pju5pS8VyPTF8`)
            .then(response => response.json())
            .then(data => {
                if (data.resourceSets.length > 0 && data.resourceSets[0].resources.length > 0) {
                    let location = data.resourceSets[0].resources[0].point.coordinates;
                    map.setView({ center: new Microsoft.Maps.Location(location[0], location[1]), zoom: 14 });
                    const pin = new Microsoft.Maps.Pushpin(map.getCenter(), {
                        color: 'blue',
                        draggable: false
                    });
                    map.entities.push(pin);
                } else {
                    alert('Location not found');
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });
    } else {
        alert('Please enter an address');
    }
});

function GetMap() {
    const addressInput = document.getElementById('addressInput');
    const options = { maxResults: 5 };
    const manager = new Microsoft.Maps.AutosuggestManager(options);
    manager.attachAutosuggest('#addressInput', '#addressForm', selectedSuggestion);
    addressInput.addEventListener('input', function () {
        if (this.value.length > 0) {
            manager.getSuggestions({ query: this.value, count: 5, countryCode: 'US' }, function (suggestions) {
                console.log(suggestions);
            });
        }
    });
}

function selectedSuggestion(suggestionResult) {
    document.getElementById('addressInput').value = suggestionResult.formattedSuggestion;
}

