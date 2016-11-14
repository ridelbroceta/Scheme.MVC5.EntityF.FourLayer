if (typeof jQuery === 'undefined') {
    throw new Error('JavaScript requiere jQuery');
}


// ReSharper disable once WrongExpressionStatement
+function($) {
    'use strict';


    var invoiceClass = function() {


        this.createArrTaxsByClass = function (classTax, baseCalc, arregloTax) {
            
            $.ajax(
                {
                    type: 'POST',
                    dataType: 'json',
                    async: false,
                    data: { idClassTax: classTax },
                    url: '/ClassTax/GetTaxesByClass',
                    success: function (data) {
                        if (data.success) {
                            //log.success(data.message);
                            $.each(data.taxes, function (index, item) {

                                var tax = {
                                    id: 0,
                                    nombre: "",
                                    description: "",
                                    description1: "",
                                    description2: "",
                                    valor: 0,
                                    isPercentage: false,
                                    isIva: false,
                                    base: 0,
                                    result: 0
                                };
                                tax.base = baseCalc;

                                tax.id = item.Id;
                                tax.nombre = item.Nombre;
                                tax.description = item.Description;
                                tax.description1 = item.Description1;
                                tax.description2 = item.Description2;
                                tax.valor = item.Valor;
                                tax.isPercentage = item.IsPercentage;
                                tax.isIva = item.IsIva;
                                var i = 0;
                                while ((i < arregloTax.length) && (arregloTax[i].id !== tax.id)) {
                                    i++;
                                }
                                if (i < arregloTax.length) {
                                    //encontrado
                                    arregloTax[i].base += tax.base;
                                } else {
                                    if (tax.isIva) {
                                        arregloTax.unshift(tax);
                                    } else {
                                        arregloTax.push(tax);
                                    }
                                }
                            });

                        } else {
                            //log.error(data.message);
                        }
                    },
                    error: function () {
                        //log.fatal()
                    }
                });
        };


    };

    window.invoiceInstance = new invoiceClass();

}(window.jQuery);