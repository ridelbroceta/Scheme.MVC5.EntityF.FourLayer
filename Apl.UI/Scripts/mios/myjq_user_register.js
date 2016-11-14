if (typeof jQuery === 'undefined') {
    throw new Error('JavaScript requiere jQuery');
}



+function($) {
    'use strict';

    function limpiarDropDownList(dropDownList) {
        $('#prefijoContainerTelf').hide();
        $('#telPrefijo').text('-1');
        var cadena = "--seleccione--";
        dropDownList.empty();
        dropDownList.append
        (
            $('<option/>')
                .attr('value', 0)
                .text(cadena)
        );
    };

    function llenarDropDownList(dropDownList, datos) {
        limpiarDropDownList(dropDownList);
        $.each(datos, function (index, item) {
            dropDownList.append(
                $('<option/>')
                    .attr('value', item.Id)
                    .text(item.Nombre)
            );
        });
    };

    $(function () {
        
        $("#Continentes").removeClass('form-control');
        $("#Continentes").addClass('form-control');
        
        $("#Continentes").change(function () {
            limpiarDropDownList($("#IdPais"));
            $.ajax(
            {
                type: 'POST',
                dataType: 'json',
                data: { idContinente: $("#Continentes").val() },
                url: '/Pais/List',
                success: function (paises) {
                    llenarDropDownList($("#IdPais"), paises);
                    //limpiar todos los otros para abajo
                }
            });
        });

        $("#IdPais").change(function () {
            var valIdPais = $("#IdPais").val();
            if (valIdPais > 0) {
                $.ajax(
                    {
                        type: 'POST',
                        dataType: 'json',
                        data: { idPais: valIdPais },
                        url: '/Pais/Prefijo',
                        success: function (prefijo) {
                            $('#prefijoContainerTelf').show();
                            $('#telPrefijo').text(prefijo);
                        }
                    });
            }
            else {
                $('#prefijoContainerTelf').hide();
                $('#telPrefijo').text('-1');
            }
        });

    });
    
}(jQuery)