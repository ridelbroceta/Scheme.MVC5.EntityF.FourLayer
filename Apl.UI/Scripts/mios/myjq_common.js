if (typeof jQuery === 'undefined') {
    throw new Error('JavaScript requiere jQuery');
}


/*!
 * funciones
 * Copyright 2015-2016.
 */


+function ($) {
    'use strict';
    
    var kgridClass = function () {

        this.onDataBound = function (e) {
            $('.link-barra-grid').addClass('hide');
        };

        this.onDataBinding = function (e) {
            $('.link-barra-grid').addClass('hide');
        };

        this.onChange = function (e) {
            var data = this.dataItem(this.select());
            var rowCellText = data.id; //IMP
            rowSelect(rowCellText);
        };

        this.showGlVoucherDetails = function (e) {
            e.preventDefault();

            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

            MyJsGeneral.OpenInNewWindow('/GlVoucher/Details/' + dataItem.IdVoucher);

        };

    };

    window.MyJqGeneral = {
        KGridInstance: new kgridClass(),
    };
    
}(window.jQuery);


/*!
 * Extensiones
 * Copyright 2015-2016.
 */


jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        
        var d = value.split("/");
        var value1 = (!/Invalid|NaN/.test(new Date(value)));
        var value2 = (!/Invalid|NaN/.test(new Date((/chrom(e|ium)/.test(navigator.userAgent.toLowerCase())) ? d[1] + "/" + d[0] + "/" + d[2] : value)));
        var value3 = (!/Invalid|NaN/.test(new Date( d[1] + "/" + d[0] + "/" + d[2])));
        return this.optional(element) ||
            value1 ||
            value2 || value3;
    }
});



/*!
 * eventos
 * Copyright 2015-2016.
 */


+function ($) {
    'use strict';
    
    function myBeforeImport() {
        var mal = $(".has-error");
        var fileName = $('#FileName').val();
        if ((!mal.length) && (fileName.length)) {
            var windowModal = $('#myModalLoading');
            if (windowModal.length) {
                windowModal.modal({
                    backdrop: 'static',
                    keyboard: false
                });
                windowModal.modal('show');
            }
        }
        return true;
    };

    function myBeforeSubmit() {
        var mal = $(".has-error");
        if ((!mal.length)) {
            var windowModal = $('#myModalLoading');
            if (windowModal.length) {
                windowModal.modal({
                    backdrop: 'static',
                    keyboard: false
                });
                windowModal.modal('show');
            }
        }
    };

    $(function () {
        
        /*!
            
         */

        $('.myjsevent-pform').validate({
            success: function () {
                myBeforeSubmit();
            }
        });

        $('.myjsevent-form').submit(myBeforeSubmit);

        $('.myjsevent-importform').submit(myBeforeImport);

        /*!
            
         */

        $('#file').change(function () {
            $('#divshowfile').html("<p>" + $('#file').val() + "</p>");
            $('#FileName').val($('#file').val());
        });

        $('#import').click(function () {
            $('#divshowfile').empty();
            $('#FileName').val('');
            $('#file').trigger('click');
        });

        $('#divshowfile').html("<p>" + $('#FileName').val() + "</p>");

        /*!
            
         */
        var languageGet = window.MyCode52.CultureInfo.getCulture();
        
        $('div .input-group.date.my-date').datepicker({ format: 'dd/mm/yyyy', language: languageGet });

        $('div .input-group.date.my-ymonth').datepicker({ format: 'mm/yyyy', language: languageGet });
         
        /*!
            
         */

        $('.barra-grid').tooltip();

    });
}(jQuery);

