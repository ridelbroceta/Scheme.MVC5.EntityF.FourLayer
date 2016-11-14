/***
 * @package mio
 *
 ***/

//strings
Object.defineProperty(String.prototype, 'isEmail', {
    get: function () {
        var a = this;
        var r = /^([\da-z_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$/.test(a);
        return r;
    }
});

Object.defineProperty(String.prototype, 'isUrl', {
    get: function () {
        var a = this;
        var r = /^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \?=.-]*)*\/?$/.test(a);
        return r;
    }
});

Object.defineProperty(String.prototype, 'isMyCad', {
    get: function () {
        var a = this;
        var r = /^([\S]|[\S][\S]|[\S].+[\S])$/.test(a);
        return r;
    }
});

Object.defineProperty(String.prototype, 'isPhone', {
    get: function () {
        var a = this;
        var r = /^[0-9]{2,3}-? ?[0-9]{4,12}$/.test(a);
        return r;
    }
});

Object.defineProperty(String.prototype, 'isMovil', {
    get: function () {
        var a = this;
        var r = /^[0-9]{2,3}-? ?[0-9]{6,7}$/.test(a);
        return r;
    }
});

String.prototype.getOnlyNumber = function () {
    var r = '';
    var a = this;
    for (var i = 0; i < a.length; i++) {
        if (/^([0-9])$/.test(a[i]))
            r += a[i];
    }
    return r;
};

(function (window) {
    var i18N = function (culture) {

        var pculture = culture || "en-US";

        this.getCulture = function () {
            return pculture;
        };

    };
    
    window.MyCode52 = {
        CultureInfo: new i18N(),
        Init: function (culture) {
            window.MyCode52.CultureInfo = new i18N(culture);
        }
    };
    
    window.MyJsGeneral = {
        OpenInNewWindow: function (url) {
            var wd, hg;
            wd = screen.availWidth - 8;
            hg = screen.availHeight - 50;
            window.open(url,
            "_blank", "left=0,top=0,border=0,toolbar=0,location=1,directories=0,status=1,menubar=0,scrollbars=1,resizable=1,width=" + wd + ",height=" + hg);
        }
    };

})(window);
