/*Validar email y solucionar error de compilación*/
function valEmail(email) {
    return /^([a-zA-Z0-9_\.\-])+\@([a-zA-Z0-9\-])+\.([a-zA-Z0-9]{3})+$/.test(email);
}

/*Validar que solo se ingrese números en un campo*/
function Numbers(evt) { if (evt.which == 13) { return true; } if (!/^[0-9]+$/.test(String.fromCharCode((evt.which) ? evt.which : event.keyCode))) { return false; } }