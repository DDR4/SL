var Usuario = (function ($, win, doc) {

    var $btnNuevoUsuario = $('#btnNuevoUsuario');
    var $btnGuardar = $('#btnGuardar');

    var $tblListadoUsuarios = $('#tblListadoUsuarios');

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

        $btnNuevoUsuario.click($btnNuevoUsuario_click);
        $btnGuardar.click($btnGuardar_click);
        GetUsuario();

    }

    function $btnNuevoUsuario_click() {
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
            "Tipo_Usu": $cboModalTipo.val(),
            "Estado_Usu": $cboModalEstado.val()
        };

        var method = "POST";
        var data = obj;
        var url = "Usuario/Registrar";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            $modalUsuario.modal('hide');
            GetUsuario();
        };
        app.CallAjax(method, url, data, fnDoneCallback);
    }

    function GetUsuario() {
        var parms = {
            Estado_Usu: 1
        };

        var url = "Usuario/GetUsuario";

        var columns = [
            { data: "Nombre_Usu" },
            { data: "Tipo_Usu" },
            { data: "Estado_Usu" },
            { data: "Estado_Usu" }

        ];
        var columnDefs = [
            {
                "targets": [3],
                "visible": true,
                "orderable": false,
                "className": "text-center",
                'render': function (data, type, full, meta) {
                    if (data === "1") {
                        return "<center>" +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Editar" href="javascript:Usuario.EditarUsuario(' + meta.row + ');"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>' +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Eliminar" href="javascript:Usuario.EliminarUsuario(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
                            "</center> ";
                    } else {
                        return "";
                    }
                }
            }

        ];

        var filters = {
            pageLength: app.Defaults.TablasPageLength
        };
        app.FillDataTableAjaxPaging($tblListadoUsuarios, url, parms, columns, columnDefs, filters, null, null);

        //var obj = {
        //    "Estado_Usu": 1
        //};

        //var method = "POST";
        //var data = obj;
        //var url = "Usuario/GetUsuario";

        //var fnDoneCallback = function (data) {
        //    console.log(data.Data);
        //};
        //app.CallAjax(method, url, data, fnDoneCallback);
    }


    return {

    };


})(window.jQuery, window, document);