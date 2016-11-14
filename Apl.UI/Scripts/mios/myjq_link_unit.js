if (typeof jQuery === 'undefined') {
    throw new Error('JavaScript requiere jQuery');
}



+function ($) {
    'use strict';
    
    window.DocInstance = {
        delUnit: function (btnDel) {
            var idcomplex = btnDel.id;
            var subTipoDel = idcomplex.substring(8);
            var textArea = $('#HIds').val();
            var arreglo = textArea.split(';');
            var i = 0;
            while ((i < arreglo.length) && (arreglo[i] != subTipoDel)) { i++; }
            if (i < arreglo.length) {
                var cadena = "";
                for (var j = 0; j < arreglo.length; j++) {
                    if (j != i) {
                        if (cadena != "") {
                            cadena += ";";
                        }
                        cadena += arreglo[j];
                    }
                }
                $('#HIds').val(cadena);
                var mybtn = $(btnDel);
                var tr = mybtn.parent("td").parent("tr");
                tr.remove();
            }
        },
        
    };
}(window.jQuery);



+ function ($) {
    'use strict';

    $(function () {

        $("#Unidades").change(function() {
            if (($('#Unidades').val() != 0)) $('#btn-add').show();
            else $('#btn-add').hide();

        });

        $('#btn-add').click(function()
        {
            var textArea = $('#HIds').val();
            var arreglo = textArea.split(';');
            var newAdd = $('#Unidades').val();
            var combobox = $("#Unidades").data("kendoComboBox");
            var cadena = combobox.text();
            var i = 0;
            while ((i < arreglo.length) && (arreglo[i] != newAdd)) { i++; }
            if (i == arreglo.length) {
                if (textArea != "") {
                    textArea += ";";
                }
                textArea += newAdd;
                $('#HIds').val(textArea);
                $('#unitContainer').append('<tr class="info">' +
                                                            '<td style="width: 1%;">' +
                                                                '<button id="btn-del-' + newAdd + '" class="btn btn-link glyphicon glyphicon-remove" type="button" title="Quitar" onclick="DocInstance.delUnit(this);">' + '</button>' +
                                                            '</td>' +
                                                            '<td style="width: 99%;">' +
                                                                cadena +
                                                            '</td>' +
                                                      '</tr>');
            }

        });
        
        $("#AllUnits").click(function () {
           
            $('#unitContainer').empty();
            $('#HIds').val('');
            var textArea = $('#HIds').val();
            
            $("#Unidades").data("kendoComboBox").dataSource.data().forEach(function (element) {
                
                var newAdd = element.Value;
                var cadena = element.Text;
                if (newAdd != 0) {
                    if (textArea != "") {
                        textArea += ";";
                    }
                    textArea += newAdd;
                    $('#unitContainer').append('<tr class="info">' +
                                                                '<td style="width: 1%;">' +
                                                                    '<button id="btn-del-' + newAdd + '" class="btn btn-link glyphicon glyphicon-remove" type="button" title="Quitar" onclick="DocInstance.delUnit(this);">' + '</button>' +
                                                                '</td>' +
                                                                '<td style="width: 99%;">' +
                                                                    cadena +
                                                                '</td>' +
                                                          '</tr>');
                }

            });
            
            
            $('#HIds').val(textArea);

        });

    });
}(jQuery);