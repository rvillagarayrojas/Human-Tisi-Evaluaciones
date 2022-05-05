function Login() {
    var urlDestino = $("#Id_URL_DESTINO").val();
    window.location.href = urlDestino;
}

$("#password").keypress(function (event) {
    var teclaPulsada = (document.all) ? event.keyCode : event.which;
    if (teclaPulsada == 13) {
        validarLogin();
    }
});

function validarLogin() {
    var usuario = $('#email').val();
    var contrasena = $('#password').val();

    $("#mensaje").html("");

    if (usuario == "" && contrasena == "") {
        $("#mensaje").html("Ingrese usuario y contraseña");
        return false;
    }

    if (usuario == "") {
        $("#mensaje").html("Ingrese usuario");
        return false;
    }
    if (contrasena == "") {
        $("#mensaje").html("Ingrese contraseña");
        return false;
    }

    enviarLogin(usuario, contrasena);

    return true;
}


function enviarLogin(usuario, contrasenia) {
    var url = $("#Id_URL").val();

    $.ajax({
        url: url,
        data: {
            usuario: usuario,
            contrasenia: contrasenia
        },
        type: "post",
        cache: false,
        success: function (data) {
            if (data.result) {
                setTimeout("Login()", 100);
            } else {
                $("#mensaje").html("Usuario o contraseña incorrecto");
                return false;
            }
        },
        error: function (jqXHR, status, error) {

        }
    });
}