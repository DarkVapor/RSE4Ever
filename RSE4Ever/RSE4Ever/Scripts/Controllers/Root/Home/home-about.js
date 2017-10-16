//require([
//    'bforms-initUI',
//], function () {

//    var HomeIndex = function (options) {
//        this.options = $.extend(true, {}, options);
//    };

//    HomeIndex.prototype.init = function () { 
//        this.$demoForm = $(".js-demoForm");
//        this.$demoForm.bsInitUI(this.options.styleInputs);
//    };

//    $(document).ready(function () {
//        var ctrl = new HomeIndex(requireConfig.pageOptions);
//        ctrl.init();
//    });
//    });

require([
    'bforms-grid'
], function () { });

$('.employee-grid').bsGrid({
    uniqueName: 'TestGrid'
})