if (typeof jQuery === 'undefined') {
    throw new Error('JavaScript requiere jQuery');
}


/*!
 * eventos
 * Copyright 2015-2016.
 */


+ function ($) {
    'use strict';

    var methodsClass = function () {

        var self = this;

        var datos = {
            qty: 0,
            price: 0,
            descuento: 0,
            importe: 0,
            idConcepto: 0,
            base: 0,
            taxes: []
        };

        var fillTaxsContainer = function (arregloTaxs) {

            $('#taxContainer').empty();
            for (var i = 0; i < arregloTaxs.length; i++) {
                $('#taxContainer').append('<div class="row">' +
                                              '<div class="col-md-3 text-right">' +
                                                  '<div class="editor-label">' +
                                                     '<label>' + 'Base' + arregloTaxs[i].description1 + '</label>' +
                                                  '</div>' +
                                              '</div>' +
                                              '<div class="col-md-2">' +
                                                  '<div class="editor-label m_bottom_8">' +
                                                     '<strong><label class="label-info">' + arregloTaxs[i].base.toFixed(2) + '</label></strong>' +
                                                  '</div>' +
                                              '</div>' +
                                          '</div>' +
                                          '<div class="row">' +
                                              '<div class="col-md-3 text-right">' +
                                                  '<div class="editor-label">' +
                                                     '<label>' + arregloTaxs[i].description1 + '</label>' +
                                                  '</div>' +
                                              '</div>' +
                                              '<div class="col-md-2">' +
                                                  '<div class="editor-label m_bottom_8">' +
                                                     '<strong><label class="label-info">' + arregloTaxs[i].result.toFixed(2) + '</label></strong>' +
                                                  '</div>' +
                                              '</div>' +
                                          '</div>');

            }


        };
            
        this.calcTotal =  function () {
            var subTotal2 = parseFloat($('#subtotal2').text());
            //var count = $('.mytaxdiv').length;
            
            var textAreaIdTaxs = $('#HIdTaxs').val();
            var textAreaTaxs = $('#HTaxs').val();
            var textAreaBaseTaxs = $('#HBaseTaxs').val();
            var arreglo1 = textAreaIdTaxs.split(';');
            var arreglo2 = textAreaTaxs.split(';');
            var arreglo3 = textAreaBaseTaxs.split(';');

            if (arreglo1[0] == "") {
                arreglo1 = [];
            }

            var arregloTaxs = [];
            var sumTaxs = 0;
            for (var k = 0; k < arreglo1.length; k++) {
                
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

                tax.id = parseInt(arreglo1[k]);
                tax.result = parseFloat(arreglo2[k]);
                tax.base = parseFloat(arreglo3[k]);

                sumTaxs += tax.result;
                
                tax.nombre = $("#nombre_" + arreglo1[k]).val();
                tax.description = $("#desc_" + arreglo1[k]).val();
                tax.description1 = $("#desc1_" + arreglo1[k]).val();
                tax.description2 = $("#desc2_" + arreglo1[k]).val();
                tax.valor = parseFloat($("#valor_" + arreglo1[k]).val());
                tax.isPercentage = $("#isPercentage_" + arreglo1[k]).val() == 'True';
                tax.isIva = $("#isIva_" + arreglo1[k]).val() == 'True';


                if (tax.isIva) {
                    arregloTaxs.unshift(tax);
                } else {
                    arregloTaxs.push(tax);
                }
            }

            var baseIva = 0;

            if ((arregloTaxs.length > 0) && (arregloTaxs[0].isIva)) {
                baseIva = arregloTaxs[0].base;
            }
            var base0 = subTotal2 - baseIva;
            $('#base0').text(base0.toFixed(2));

            fillTaxsContainer(arregloTaxs);
            
            $('#Total').val((subTotal2 + sumTaxs).toFixed(2));
        };

        var calcDeduc = function (baseCalc, deducs) {
            var tDeducs = 0;
            $.each(deducs, function (index, item) {
                var valor = item.Valor;
                if (item.IsPercentage) {
                    valor = (baseCalc * valor) / 100;
                }
                tDeducs += valor;
            });
            $('#deduc').val(tDeducs.toFixed(2));
        };

        this.myChangeDeducs = function () {
            var myIdCDeduc = parseInt($("#Deducciones").val());
            var valor = parseFloat($("#valor").text());
            if (!isNaN(valor) && (valor > 0) && (!isNaN(myIdCDeduc))) {
                //busca la clase de este concepto
                $.ajax(
                    {
                        type: 'POST',
                        dataType: 'json',
                        data: { idCDeduc: myIdCDeduc },
                        url: '/CiClassDeduction/Deducs',
                        success: function (deducs) {
                            calcDeduc(valor, deducs);
                        }
                    });
            };
        };
        
        this.myChangeConceptos = function () {
            var myIdIgEgSubCat = parseInt($("#Conceptos").val());
            if (!isNaN(myIdIgEgSubCat) && (myIdIgEgSubCat > 0)) {
                //busca la clase de este concepto
                $.ajax(
                    {
                        type: 'POST',
                        dataType: 'json',
                        data: { idIgEgSubCat: myIdIgEgSubCat },
                        url: '/IgEgSubCat/IdsCDeducCTaxPrice',
                        success: function (datos) {
                            //hacer
                            $("#precio").val(datos.Price.toFixed(2));

                            var cant = parseFloat($("#cantidad").val());

                            if (!isNaN(cant) && (cant > 0)) {

                                var valor = cant * datos.Price;
                                $("#valor").text(valor.toFixed(2));

                                if (valor > 0) {
                                    $("#btn-add").show();
                                }
                            }
                            if (datos.CDeduc > 0) {
                                $("#Deducciones").val(idDeduc);
                                self.myChangeDeducs();
                            }

                        }
                    });
            } else {
                $("#btn-add").hide();
            }
        };

        var limpiar = function () {
            var conceptos = $("#Conceptos").data("kendoComboBox");
            conceptos.value("");

            $("#Deducciones").val(0);
            $("#Impuestos").val(0);
            $("#deduc").val("0.00");
            $("#cantidad").val("1.00");
            $("#precio").val("1.00");
            $("#valor").text("0.00");
            $("#btn-add").hide();

            datos.qty = 0;
            datos.price = 0;
            datos.descuento = 0;
            datos.importe = 0;
            datos.idConcepto = 0;
            datos.base = 0;
            datos.taxes = [];
                
            self.myChangeConceptos();
            $(".my-alert").addClass('hide');
        };

        this.myChangeValor = function () {
            var valor = parseFloat($("#valor").text());
            if (!isNaN(valor) && (valor > 0)) {
                self.myChangeDeducs();
            }
            var myIdIgEgSubCat = parseInt($("#Conceptos").val());
            if (!isNaN(myIdIgEgSubCat) && (!isNaN(valor) && (valor > 0))) {
                $("#btn-add").show();
            } else $("#btn-add").hide();
        };

        this.myChangePrecio = function () {
            var cant = parseFloat($("#cantidad").val());
            var precio = parseFloat($("#precio").val());

            if (!isNaN(cant) && (cant > 0) && (!isNaN(precio)) && (precio > 0)) {

                var valor = cant * precio;
                $("#valor").text(valor.toFixed(2));

                 self.myChangeValor();
            }
        };

        var myToConceptContainer = function (sumTaxs) {
            
            var conceptos = $("#Conceptos").data("kendoComboBox");
            var cadena = conceptos.text();

            $('#conceptContainer').append(
                '<tr class="info">' +
                    '<td style="width: 0.5%;">' +
                    '<button id="btn-del-' + datos.idConcepto + '" class="btn btn-link glyphicon glyphicon-remove" type="button" title="Quitar" onclick="ApInvoiceInstance.delConcept(this);">' + '</button>' +
                    '</td>' +
                    '<td style="width: 69%;">' +
                    cadena +
                    '</td>' +
                    '<td>' +
                    datos.qty.toFixed(2) +
                    '</td>' +
                    '<td>' +
                    datos.price.toFixed(2) +
                    '</td>' +
                    '<td>' +
                    datos.importe.toFixed(2) +
                    '</td>' +
                    '<td>' +
                    datos.descuento.toFixed(2) +
                    '</td>' +
                    '<td>' +
                    sumTaxs.toFixed(2) +
                    '</td>' +
                    '</tr>');

        };

        var myAddToHides2 = function (conceptosVacios) {
            //concepto
            var textAreaConceptTaxs = $('#HConceptTaxs').val();
            var textAreaConceptBaseTaxs = $('#HConceptBaseTaxs').val();


            //del
            var textAreaDelId = $('#HDelId').val();
            var textAreaDelIdTaxs = $('#HDelIdTaxs').val();
            var textAreaDelBaseTaxs = $('#HDelBaseTaxs').val();
            var textAreaDelTaxs = $('#HDelTaxs').val();


            //taxs
            var textAreaIdTaxs = $('#HIdTaxs').val();
            var textAreaTaxs = $('#HTaxs').val();
            var textAreaBaseTaxs = $('#HBaseTaxs').val();

            if (!conceptosVacios) {
                textAreaConceptTaxs += ";";
                textAreaConceptBaseTaxs += ";";
            }

            var sumTaxs = 0;
            var arreglo1 = textAreaIdTaxs.split(';');
            var arreglo2 = textAreaTaxs.split(';');
            var arreglo3 = textAreaBaseTaxs.split(';');

            if (arreglo1[0] == "") {
                arreglo1 = [];
                arreglo2 = [];
                arreglo3 = [];
            }

            var delVacios = textAreaDelId == "";
            var mybase = datos.base;
            for (var j = 0; j < datos.taxes.length; j++) {

                //aqui lleno los del
                if (!delVacios) {
                    textAreaDelId += ";";
                    textAreaDelIdTaxs += ";";
                    textAreaDelBaseTaxs += ";";
                    textAreaDelTaxs += ";";
                }
                
                textAreaDelId += datos.idConcepto;
                textAreaDelIdTaxs += datos.taxes[j].id;
                textAreaDelTaxs += datos.taxes[j].result.toFixed(2);
                textAreaDelBaseTaxs += datos.taxes[j].base.toFixed(2);
                

                delVacios = false;
                
                if (j == 0) {
                    mybase = ((mybase > datos.taxes[j].base)) ? datos.taxes[j].base : mybase;
                } else {
                    mybase = ((mybase < datos.taxes[j].base)) ? datos.taxes[j].base : mybase;
                }
                sumTaxs += datos.taxes[j].result;

                var i = 0;
                while ((i < arreglo1.length) && (arreglo1[i] != datos.taxes[j].id)) {
                    i++;
                }

                if (i == arreglo1.length) {
                    //nuevo
                    arreglo1.push(datos.taxes[j].id.toFixed());
                    arreglo2.push((datos.taxes[j].result).toFixed(2));
                    arreglo3.push((datos.taxes[j].base).toFixed(2));

                } else {
                    //encontrado

                    arreglo2[i] = (parseFloat(arreglo2[i]) + datos.taxes[j].result).toFixed(2);
                    arreglo3[i] = (parseFloat(arreglo3[i]) + datos.taxes[j].base).toFixed(2);

                }
            }

            var cadena1 = "";
            var cadena2 = "";
            var cadena3 = "";

            for (var k = 0; k < arreglo1.length; k++) {
                if (cadena1 != "") {
                    cadena1 += ";";
                    cadena2 += ";";
                    cadena3 += ";";
                }
                cadena1 += arreglo1[k];
                cadena2 += arreglo2[k];
                cadena3 += arreglo3[k];

            }
            $('#HIdTaxs').val(cadena1);
            $('#HTaxs').val(cadena2);
            $('#HBaseTaxs').val(cadena3);


            textAreaConceptTaxs += sumTaxs.toFixed(2);
            $('#HConceptTaxs').val(textAreaConceptTaxs);

            textAreaConceptBaseTaxs += mybase.toFixed(2);
            $('#HConceptBaseTaxs').val(textAreaConceptBaseTaxs);

            //del
            $('#HDelId').val(textAreaDelId);
            $('#HDelIdTaxs').val(textAreaDelIdTaxs);
            $('#HDelBaseTaxs').val(textAreaDelBaseTaxs);
            $('#HDelTaxs').val(textAreaDelTaxs);

            return sumTaxs;

        };
        
        var myAddToHides = function(principal) {

            var textAreaQties = $('#HQties').val();
            var textAreaPrecios = $('#HPrices').val();
            var textAreaValues = $('#HValues').val();
            var textAreaDeducs = $('#HDescounts').val();

            var conceptosVacios = principal == "";

            if (!conceptosVacios) {
                principal += ";";
                textAreaQties += ";";
                textAreaPrecios += ";";
                textAreaValues += ";";
                textAreaDeducs += ";";
            }

            principal += datos.idConcepto;
            $('#HIds').val(principal);

            textAreaQties += datos.qty.toFixed(2);
            $('#HQties').val(textAreaQties);

            textAreaPrecios += datos.price.toFixed(2);
            $('#HPrices').val(textAreaPrecios);

            textAreaValues += datos.importe.toFixed(2);
            $('#HValues').val(textAreaValues);

            textAreaDeducs += datos.descuento.toFixed(2);
            $('#HDescounts').val(textAreaDeducs);

            return myAddToHides2(conceptosVacios);
        };

        var myAddConcept = function () {

            var textArea = $('#HIds').val();
            var arreglo = textArea.split(';');

            var i = 0;
            while ((i < arreglo.length) && (arreglo[i] != datos.idConcepto)) {
                i++;
            }
            
            if (i == arreglo.length) {

                var subTotal = parseFloat($('#subtotal').text());
                var descuento = parseFloat($('#descuento').text());
                subTotal += datos.importe;
                descuento += datos.descuento;
                $('#subtotal').text(subTotal.toFixed(2));
                $('#descuento').text(descuento.toFixed(2));

                var subTotal2 = subTotal - descuento;
                $('#subtotal2').text(subTotal2.toFixed(2));


                var sumTaxs = myAddToHides(textArea);

                myToConceptContainer(sumTaxs); //por ahora

                self.calcTotal();

            }
            limpiar();

        };

        this.mySaveModalResult = function () {
            
            for (var i = 0; i < datos.taxes.length; i++) {
                datos.taxes[i].base = parseFloat($('#base_' + datos.taxes[i].id).val());
                datos.taxes[i].result = parseFloat($('#tax_' + datos.taxes[i].id).val());
            }
            myAddConcept();
        };

        var myFillModal = function () {
            
            var divs = $('.mytaxdiv');
            divs.addClass('hide');

            $('#base').text(datos.base.toFixed(2));
            
            for (var i = 0; i < datos.taxes.length; i++) {
                datos.taxes[i].result = datos.taxes[i].valor;
                if (datos.taxes[i].isPercentage) {
                    datos.taxes[i].result = parseFloat(((datos.taxes[i].base * datos.taxes[i].valor) / 100).toFixed(2));
                }
                
                $('#taxdiv_' + datos.taxes[i].id).removeClass('hide');
                $('#base_' + datos.taxes[i].id).val(datos.taxes[i].base.toFixed(2));
                $('#tax_' + datos.taxes[i].id).val(datos.taxes[i].result.toFixed(2));
            }
        };

        this.myBtnAdd = function () {
            // recojo datos y muestro en la modal
            
            var importe = parseFloat($("#valor").text());
            if (isNaN(importe)) { importe = 0; }
            datos.importe = importe;

            var descuento = parseFloat($("#deduc").val());
            if (isNaN(descuento)) { descuento = 0; }
            datos.descuento = descuento;
            

            var qty = parseFloat($("#cantidad").val());
            if (isNaN(qty)) { qty = 0; }
            datos.qty = qty;
            
            var price = parseFloat($("#precio").val());
            if (isNaN(price)) { price = 0; }
            datos.price = price;

            var idIgEgSubCat = parseInt($("#Conceptos").val());
            if (isNaN(idIgEgSubCat)) { idIgEgSubCat = 0; }
            datos.idConcepto = idIgEgSubCat;

            if (!((qty == 0) || (price == 0) || (importe == 0) || (idIgEgSubCat == 0) || (descuento >= importe))) {
                
                datos.base = importe - descuento;
                var myIdCTax = parseInt($("#Impuestos").val());
                if (!isNaN(myIdCTax) && (myIdCTax > 0)) {

                    invoiceInstance.createArrTaxsByClass(myIdCTax, datos.base, datos.taxes);
                    myFillModal();
                    
                    $("#myTaxsByConceptModal").modal({
                        backdrop: 'static',
                        keyboard: false
                    });


                } else {
                    myAddConcept();
                }
            }
        };
        
        this.myIsOkReviewBases = function () {
            var any = false;
            for (var i = 0; i < datos.taxes.length; i++) {
                var baseInput = parseFloat($('#base_' + datos.taxes[i].id).val());
                if (datos.base < baseInput) {
                    if (!any) any = true;
                    $('#error_' + datos.taxes[i].id).removeClass('hide');
                }
            }
            return !any;
        };

    };


    var methodsClassInstance = new methodsClass();

    var apInvoiceClass = function() {

        var crearArregloFactTaxs = function() {

            var textAreaIdTaxs = $('#HIdTaxs').val();
            var textAreaTaxs = $('#HTaxs').val();
            var textAreaBaseTaxs = $('#HBaseTaxs').val();

            var arreglo1 = textAreaIdTaxs.split(';');
            var arreglo2 = textAreaBaseTaxs.split(';');
            var arreglo3 = textAreaTaxs.split(';');

            var factTaxs = [];

            for (var j = 0; j < arreglo1.length; j++) {

                var tax = {
                    id: 0,
                    tax: 0,
                    base: 0
                };

                tax.id = parseInt(arreglo1[j]);
                tax.base = parseFloat(arreglo2[j]);
                tax.tax = parseFloat(arreglo3[j]);

                factTaxs.push(tax);
            }

            return factTaxs;
        };
        
        var ponerFactTaxs = function (facTaxs) {
            
            var cadena1 = "";
            var cadena2 = "";
            var cadena3 = "";
            for (var j = 0; j < facTaxs.length; j++) {
                if (cadena1 != "") {
                    cadena1 += ";";
                    cadena2 += ";";
                    cadena3 += ";";
                }
                if ((facTaxs[j].base > 0.01) && (facTaxs[j].tax > 0.01)) {
                    cadena1 += facTaxs[j].id;
                    cadena2 += facTaxs[j].base.toFixed(2);
                    cadena3 += facTaxs[j].tax.toFixed(2);
                }
            };
            
            $('#HIdTaxs').val(cadena1);
            $('#HTaxs').val(cadena3);
            $('#HBaseTaxs').val(cadena2);

        };
        
        var delHidesDel = function (id) {
            var textAreaDelId = $('#HDelId').val();
            var arreglo1 = textAreaDelId.split(';');

            if (!arreglo1[0] == "") {
                
                var k = 0;
                while ((k < arreglo1.length) && (arreglo1[k] != id)) {
                    k++;
                }
                if (k < arreglo1.length) {
                    var textAreaDelIdTaxs = $('#HDelIdTaxs').val();
                    var textAreaDelBaseTaxs = $('#HDelBaseTaxs').val();
                    var textAreaDelTaxs = $('#HDelTaxs').val();

                    var arreglo2 = textAreaDelIdTaxs.split(';');
                    var arreglo3 = textAreaDelBaseTaxs.split(';');
                    var arreglo4 = textAreaDelTaxs.split(';');


                    var factTaxs = crearArregloFactTaxs();

                    var cadena1 = "";
                    var cadena2 = "";
                    var cadena3 = "";
                    var cadena4 = "";

                    for (var j = 0; j < arreglo1.length; j++) {
                        if (arreglo1[j] != id) {

                            if (cadena1 != "") {
                                cadena1 += ";";
                                cadena2 += ";";
                                cadena3 += ";";
                                cadena4 += ";";
                            }
                            cadena1 += arreglo1[j];
                            cadena2 += arreglo2[j];
                            cadena3 += arreglo3[j];
                            cadena4 += arreglo4[j];
                        } else {
                            //aqui borro lo de la factura si es 0
                            var idTax = parseInt(arreglo2[j]);
                            var i = 0;
                            while ((i < factTaxs.length) && (factTaxs[i].id != idTax)) {
                                i++;
                            }
                            if (i < factTaxs.length) {
                                factTaxs[i].tax -= parseFloat(arreglo4[j]);
                                factTaxs[i].base -= parseFloat(arreglo3[j]);
                            }
                        }
                    }

                    $('#HDelId').val(cadena1);
                    $('#HDelIdTaxs').val(cadena2);
                    $('#HDelBaseTaxs').val(cadena3);
                    $('#HDelTaxs').val(cadena4);
                    ponerFactTaxs(factTaxs);

                }
            }
        };

        this.recharge = function () {
            
            methodsClassInstance.calcTotal();
        };

        this.delConcept = function (btnDel) {
            var idcomplex = btnDel.id;
            var subTipoDel = idcomplex.substring(8);
            var textArea = $('#HIds').val();
            var textAreaQties = $('#HQties').val();
            var textAreaPrecios = $('#HPrices').val();
            var textAreaValues = $('#HValues').val();
            var textAreaDeducs = $('#HDescounts').val();
            
            var textAreaConceptTaxs = $('#HConceptTaxs').val();
            var textAreaConceptBaseTaxs = $('#HConceptBaseTaxs').val();



            var arreglo = textArea.split(';');
            var arregloQties = textAreaQties.split(';');
            var arregloPrices = textAreaPrecios.split(';');
            var arregloValues = textAreaValues.split(';');
            var arregloDeducs = textAreaDeducs.split(';');
            
            var arregloConceptTaxs = textAreaConceptTaxs.split(';');
            var arregloConceptBaseTaxs = textAreaConceptBaseTaxs.split(';');

            var i = 0;
            while ((i < arreglo.length) && (arreglo[i] != subTipoDel)) {
                i++;
            }
            
            if (i < arreglo.length) {
                var cadena = "";
                var cadena2 = "";
                var cadena3 = "";
                var cadena4 = "";
                var cadena5 = "";
                var cadena6 = "";
                var cadena7 = "";

                for (var j = 0; j < arreglo.length; j++) {
                    if (j != i) {
                        if (cadena != "") {
                            cadena += ";";
                            cadena2 += ";";
                            cadena3 += ";";
                            cadena4 += ";";
                            cadena5 += ";";
                            cadena6 += ";";
                            cadena7 += ";";
                        }
                        cadena += arreglo[j];
                        cadena2 += arregloQties[j];
                        cadena3 += arregloPrices[j];
                        cadena4 += arregloValues[j];
                        cadena5 += arregloDeducs[j];
                        cadena6 += arregloConceptTaxs[j];
                        cadena7 += arregloConceptBaseTaxs[j];
                    }
                }
                $('#HIds').val(cadena);
                $('#HQties').val(cadena2);
                $('#HPrices').val(cadena3);
                $('#HValues').val(cadena4);
                $('#HDescounts').val(cadena5);
                
                $('#HConceptTaxs').val(cadena6);
                $('#HConceptBaseTaxs').val(cadena7);


                var value = parseFloat(arregloValues[i]);
                var deduc = parseFloat(arregloDeducs[i]);

                var subTotal = parseFloat($('#subtotal').text());
                var descuento = parseFloat($('#descuento').text());

                subTotal -= value;
                descuento -= deduc;
                $('#subtotal').text(subTotal.toFixed(2));
                $('#descuento').text(descuento.toFixed(2));

                var subTotal2 = subTotal - descuento;
                $('#subtotal2').text(subTotal2.toFixed(2));

                delHidesDel(subTipoDel);
                methodsClassInstance.calcTotal();

                var mybtn = $(btnDel);
                var tr = mybtn.parent("td").parent("tr");
                tr.remove();
            }

        };
        
    };

    window.ApInvoiceInstance = new apInvoiceClass();


    $(function () {

        $('.myjsevent-proveedores').change(function () {
            // busco la clase y la cuenta
            var myIdProveedor = parseInt($("#IdProveedor").val());
            if (!isNaN(myIdProveedor)) {

                $.ajax(
                    {
                        type: 'POST',
                        dataType: 'json',
                        data: { id: myIdProveedor },
                        url: '/ApProveedor/Class',
                        success: function (datos) {
                            //hacer
                            if (datos.IdClass > 0) {
                                $("#IdCProveedor").val(datos.IdClass);
                                $("#IdGlAcct").val(datos.IdGlAcct);
                            }
                        }
                    });
            } else {

                $("#IdCProveedor").val("");
                $("#IdGlAcct").val("");
            }
        });

        $('.myjsevent-deducs').change(methodsClassInstance.myChangeDeducs);

        $('.myjsevent-cproveedores').change(function () {
            // busco la clase y la cuenta
            var myIdCProveedor = parseInt($("#IdCProveedor").val());
            if (!isNaN(myIdCProveedor)) {

                $.ajax(
                    {
                        type: 'POST',
                        dataType: 'json',
                        data: { id: myIdCProveedor },
                        url: '/ApCProveedor/Account',
                        success: function (datos) {
                            //hacer
                            if (datos.IdGlAcct > 0) {
                                $("#IdGlAcct").val(datos.IdGlAcct);
                            }
                        }
                    });
            } else {

                $("#IdGlAcct").val("");
            }
        });

        $('#valor').change(methodsClassInstance.myChangeValor);
        
        $('#precio').change(methodsClassInstance.myChangePrecio);

        $('#cantidad').change(methodsClassInstance.myChangePrecio);

        $('.myjsevent-conceptos').change(methodsClassInstance.myChangeConceptos);
        
          $
        ('#btn-add').click(methodsClassInstance.myBtnAdd);
        
        //$('#btnSalvarModal').click(methodsClassInstance.mySaveModalResult);

        $('#formSection').validate({
            submitHandler: function (form, event) {
                event.preventDefault();
                //aqui deberia revisar yo las base q no se pasen de la Base

                if (methodsClassInstance.myIsOkReviewBases()) {
                    $("#myTaxsByConceptModal").modal('hide');
                    methodsClassInstance.mySaveModalResult();
                }
                //submit via ajax
            }
        });


    });
    
}(window.jQuery);
