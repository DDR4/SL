var Registro = (function ($, win, doc) {

    var $btnRegistro = $('#btnRegistro');
    var $btnRegistrar = $('#btnRegistrar');

    // Modal
    var $modalRegistro = $('#modalRegistro');  
    var $txtModalUsuario = $('#txtModalUsuario');  
    var $txtModalClave = $('#txtModalClave');  
                                                   
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    // Constructor
    $(Initialize);

    // Implementacion del constructor
    function Initialize() {

        $btnRegistro.click($btnRegistro_click);     
        $btnRegistrar.click($btnRegistrar_click);   

    }

    function $btnRegistro_click() {
        $modalRegistro.modal();
    }


    function $btnRegistrar_click() {

        var obj = {
            "Nombre_Usu": $txtModalUsuario.val(),
            "Pass_Usu": $txtModalClave.val(),
            "Tipo_Usu": 1
        };

        var method = "POST";
        var data = obj;
        var url = "Account/Registrar";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);           
        };
        app.CallAjax(method, url, data, fnDoneCallback);
    }


    return {

    };


})(window.jQuery, window, document);