﻿var Usuario = (function ($, win, doc) {

    var $btnNuevoUsuario = $('#btnNuevoUsuario');
    var $btnGuardar = $('#btnGuardar');

    var $tblListadoUsuarios = $('#tblListadoUsuarios');

    // Modal
    var $modalUsuario = $('#modalUsuario');  
    var $txtModalUsuario = $('#txtModalUsuario');  
    var $txtModalClave = $('#txtModalClave');  
    var $cboModalTipo = $('#cboModalTipo');  
    var $cboModalEstado = $('#cboModalEstado');
    var $titleModalUsuario = $('#titleModalUsuario');

    var $divClave = $('#divClave');
                                                   
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    var Global = {
        Cod_Usu: null
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
        Global.Cod_Usu = null;
        $txtModalUsuario.val("");
        $txtModalClave.val("");
        $cboModalTipo.val(0);
        $cboModalEstado.val(1);
        app.Event.Disabled($cboModalEstado);
        $divClave.show();
    }

    function $btnGuardar_click() {
        InsertUpdateUsuario();
    }

    function InsertUpdateUsuario() {

        var obj = {
            "Cod_Usu": Global.Cod_Usu,
            "Nombre_Usu": $txtModalUsuario.val(),
            "Pass_Usu": $txtModalClave.val(),
            "Tipo_Usu": $cboModalTipo.val(),
            "Estado_Usu": $cboModalEstado.val()
        };

        var method = "POST";
        var data = obj;
        var url = "Usuario/InsertUpdateUsuario";

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
            { data: "Auditoria.TipoUsuario" }

        ];
        var columnDefs = [
            {
                "targets": [1],
                'render': function (data, type, full, meta) {
                    if (data === 1) {
                        return "Administrador";
                    } else return "Usuario";
                }
            },
            {
                "targets": [2],
                'render': function (data, type, full, meta) {
                    if (data === 1) {
                        return "Activo";
                    } else return "Inactivo";
                }
            },
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

    }

    function EditarUsuario(row) {
        var data = app.GetValueRowCellOfDataTable($tblListadoUsuarios, row);
        $titleModalUsuario.html("Editar Usuario");

        $modalUsuario.modal();
        Global.Cod_Usu = data.Cod_Usu;
        $txtModalUsuario.val(data.Nombre_Usu);
        $txtModalClave.val(data.Pass_Usu);
        app.Event.Enable($cboModalEstado);
        $cboModalEstado.val(data.Estado_Usu).trigger('change');
        $cboModalTipo.val(data.Tipo_Usu).trigger('change');
        $divClave.hide();
    }

    function EliminarUsuario(row) {
        var fnAceptarCallback = function () {
            var data = app.GetValueRowCellOfDataTable($tblListadoUsuarios, row);

            var obj = {
                "Cod_Usu": data.Cod_Usu
            };

            var method = "POST";
            var url = "Usuario/DeleteUsuario";
            var rsdata = obj;
            var fnDoneCallback = function (data) {
                GetUsuario();
            };
            app.CallAjax(method, url, rsdata, fnDoneCallback, null, null, null);
        };
        app.Message.Confirm("Aviso", "Esta seguro que desea eliminar el producto?", "Aceptar", "Cancelar", fnAceptarCallback, null);
    }


    return {
        EditarUsuario: EditarUsuario,
        EliminarUsuario: EliminarUsuario
    };


})(window.jQuery, window, document);