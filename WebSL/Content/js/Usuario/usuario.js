var Usuario = (function ($, win, doc) {

    var $btnNuevaProducto = $('#btnNuevaProducto');
    var $btnGuardar = $('#btnGuardar');

    // Modal
    var $modalUsuario = $('#modalUsuario');  
    var $txtModalUsuario = $('#txtModalUsuario');  
    var $txtModalClave = $('#txtModalClave');  
    var $cboModalTipo = $('#cboModalTipo');  
    var $cboModalEstado = $('#cboModalEstado');  
                                                   
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    // Constructor
    $(Initialize);

    // Implementacion del constructor
    function Initialize() {

        $btnNuevaProducto.click($btnNuevaProducto_click);     
        $btnGuardar.click($btnGuardar_click);   

    }

    function $btnNuevaProducto_click() {
        $modalUsuario.modal();
        $txtModalUsuario.val("");
        $txtModalClave.val("");
        $cboModalTipo.val(0);
        $cboModalEstado.val(1);
        app.Event.Disabled($cboModalEstado);
    }


    function $btnGuardar_click() {

        var obj = {
            "Nombre_Usu": $txtModalUsuario.val(),
            "Pass_Usu": $txtModalClave.val(),
            "Tipo_Usu": $cboModalTipo.val()
        };

        var method = "POST";
        var data = obj;
        var url = "Usuario/Registrar";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);           
        };
        app.CallAjax(method, url, data, fnDoneCallback);
    }


    return {

    };


})(window.jQuery, window, document);