function cleanFilterURL() {
    var currentUrl = window.location.href; 
    var noFilterUrl = currentUrl.split('&')[0];

    window.history.replaceState({}, document.title, noFilterUrl);

    window.location.reload();
}

$(document).ready(function() {
    $('#btn-clean-filter').click(function(e) {
        e.preventDefault();
        cleanFilterURL();        
    });
});