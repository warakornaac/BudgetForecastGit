﻿//var countList = $("#countList").val() || 0;
//var userType = @this.Session["UserType"].ToString();
//var monthInput = @formattedMonthInputCurrentIndex;
//console.log("userType = " + userType);

function linkTo(link) {
    window.location.href = link;
}

if (window.history.replaceState) {
    window.history.replaceState(null, null, window.location.href);
}
function hideBodySec(sec) {
    var x = document.getElementById("body_" + sec);
    if (x.style.display === "none") {
        $("#header_" + sec).toggleClass("fa-caret-up fa-caret-down");
        $("#body_" + sec).show('fast');
    } else {
        $("#header_" + sec).toggleClass("fa-caret-down fa-caret-up");
        $("#body_" + sec).hide('fast');
    }
}
//get all customer by sale
function getCustomerAllBySale() {
    var cusCodeAll = new Array();
    $('#cusCode option').each(function () {
        cusCodeAll.push(this.value);
    });
    $("#cusCodeAll_header").val(cusCodeAll);
}
//hide header
function hideHeader() {
    var x = document.getElementById("headerSearch");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
    var y = document.getElementById("headerSearchFooter");
    if (y.style.display === "none") {
        y.style.display = "block";
    } else {
        y.style.display = "none";
    }
}

function searchForecast(e) {
    $("#slmCode").prop('disabled', false);
    //LoadingShow();
    var msg = '';
    var error = 0;
    var slmCode = $("#slmCode option:selected").val();
    var cusCode = $("#cusCode").val();
    var stkSec = $("#stkSec").val();
    var prodMgr = $("#prodMgr option:selected").val();
    var year = $("#year option:selected").val();
    if (cusCode == undefined || cusCode == "") {
        ++error;
        msg += '<br>' + error + '. กรุณาเลือก CUSTOMER อย่างน้อย 1 รายการ';
    }
    if (stkSec == undefined) {
        ++error;
        msg += '<br>' + error + '. กรุณาเลือก SEC อย่างน้อย 1 รายการ';
    }
    if (error) {
        LoadingHide();
        Swal.fire({
            icon: 'warning',
            title: 'กรุณาตรวจสอบข้อมูลต่อไปนี้',
            html: msg,
            confirmButtonColor: '#d33',
            confirmButtonText: 'ปิด',
            allowOutsideClick: false,
            focusConfirm: false,
        });
        return false;
    } else {
        //main input
        //console.log("IndexslmCode = " + slmCode + "<br>");
        //console.log("IndexcusCode = " + cusCode);
        getDataForecastSale(slmCode, cusCode, stkSec, prodMgr, year, 1);
        $("#flagReloadByMonth").val('N');
        $("#flagReloadBySec").val('N');
    }
}

function getDataForecastSale(slmCode, cusCode, stkSec, prodMgr, year, flg) {
    var divShow = "";
    if (flg == 1) {//main
        divShow = "showDataInput";
    } else if (flg == 2) { //sum by month
        divShow = "showSumByMonth";
    }
    $.ajax({
        type: 'post',
        url: '/NB2023_Test/ForecastSale/SearchForecast', //@Url.Action("SearchForecast", "ForecastSale")',
        data: {
            slmCode: slmCode,
            cusCode: cusCode,
            stkSec: stkSec,
            prodMgr: prodMgr,
            year: year,
            flg: flg
        },
        dataType: 'html',
        cache: false,
        Async: true,
        beforeSend: function () {
            $('#' + divShow).empty();
            LoadingShow();
        },
        success: function (res) {
            LoadingHide();
            $('#' + divShow).html(res);
        }
    });
}

function resetSearch() {
    LoadingShow();
    location.reload();
    //LoadingHide();
}

function getCustomerBySlmCode(_slmCode) {
    //alert(_slmCode);
    let slmCode = "";
    if (_slmCode == "") {
        slmCode = $("#slmCode option:selected").val();
    } else {
        slmCode = _slmCode;
    }
    //alert(slmCode);
    $.ajax({
        url: '/NB2023_Test/ForecastSale/Getdatabyslm',
        data: {
            slmCode: slmCode,
        },
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            //console.log(data);
            var select = $("#cusCode");
            select.children().remove();
            //select.append($("<option>").val("ALL").text("CUSTOMER ALL"));
            $.each(data, function (key, value) {
                select.append('<option value=' + value.CUSCOD + '>' + value.CUSCOD + '|' + value.CUSNAM + /*"----ที่อยู่ " + value.ADDR_01 + " " + value.PRO +*/ '</option>');
            });
            if ($("#cusCode option:selected").val() == "") {
                $("#cusCode").val($("#cusCode option:first").val());
            }
            $('#cusCode').selectpicker('refresh');
            //customer selected
            $('#cusCode').selectpicker('val', JSON.parse($("#cusCode_header").val()));
        }
    });
}
function getStkgrpByProd(_prodCode) {
    var stkSecHeader = $("#stkSec_header").val();
    var stkArray = new Array();
    if (_prodCode == "") {
        prodCode = $("#prodMgr option:selected").val();
    } else {
        prodCode = _prodCode;
    }
    //console.log("getStkgrpByProd = " + prodCode);
    $.ajax({
        url: '/NB2023_Test/ForecastPm/GetSTKGRP',
        data: {
            ProdMRG: prodCode
        },
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            // console.log(data);
            var select = $("#stkSec");
            select.children().remove();
            select.append($("<option>").val("ALL").text("SEC ALL"));
            $(data).each(function (index, item) {
                select.append($("<option>").val(item.SEC).text(item.SEC + "/" + item.SECNAM));
                stkArray.push(item.SEC);
            });
            $('#stkSec').selectpicker('refresh');
            if (stkSecHeader != "[]") {
                $('#stkSec').selectpicker('val', JSON.parse(stkSecHeader));
            } else {
                stkArray.push('ALL');
                $('#stkSec').selectpicker('val', stkArray);
            }
        }
    });
}
//
//sum forecast sale by input
sumSaleForecast = async (_className, month, sec, cuscod) => {
    $(".iconLoading").html('<i class="fa fa-spinner fa-spin f-center"></i>');
    var monthSelect = $("#month_sec option:selected").val();
    //console.log("monthSelect = " + monthSelect);
    let sum_forecast_by_month = 0;
    await Promise.all($(".sale_forecast_" + sec + "_" + monthSelect).each(async function (i, obj) {
        let forecast_sale_key = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
        //console.log("forecast_sale_key = " + forecast_sale_key);
        forecast_sale_key = await getVowels(forecast_sale_key, 0);
        sum_forecast_by_month += Number(forecast_sale_key);
    }));
    $('.sum_sec_forecast_' + sec).text(numberWithCommas(sum_forecast_by_month.toFixed(0)));
    //sum total by cuscod sec
    let sum_total_forecast_by_cuscod = 0;
    await Promise.all($(".sum_sale_forecast_" + cuscod + "_" + sec).each(async function (i, obj) {
        let forecast_sale_key = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
        //console.log("sum_total_forecast_by_cuscod = " + forecast_sale_key);
        forecast_sale_key = await getVowels(forecast_sale_key, 0);
        sum_total_forecast_by_cuscod += Number(forecast_sale_key);
    }));
    $('.sum_total_forecast_' + cuscod + "_" + sec).text(numberWithCommas(sum_total_forecast_by_cuscod.toFixed(0)));
    $('.sum_total_forecast_input_' + cuscod + "_" + sec).val(numberWithCommas(sum_total_forecast_by_cuscod.toFixed(0)));

    //sum จากช่อง forecast
    await sumSaleForecastByCaption("sale_forecast");
    //sum by month
    await sumAllSaleForecastByCaption("total_forecast");

    //hide icon
    setTimeout(() => {
        $(".iconLoading").html('');
    }, "1000");
}
//for sum by sec
const sumSecAll = async (month) => {
    var flagTabBySecAll = $("#flagTabBySecAll").val();
    $(".iconLoading").html('<i class="fa fa-spinner fa-spin f-center"></i>');
    var sec;
    var monthSelect;
    var monthSelect = $("#month_sec option:selected").val();
    if (month === undefined) {
        month = monthSelect;
    }
    if (flagTabBySecAll == 'Y') {
        getDataForecastByMonth(slmCode, cusCodeAll, stkSec, prodMgr, year, 3, month)
    }

    $('.stkSecList_header').each(function () {
        sec = $(this).val();
        if (sec != null) {
            sumSecByMonth(sec, month);
        }
    });
    //hide icon
    setTimeout(() => {
        $(".iconLoading").html('');
    }, "1000");

}
//sum sec by month
const sumSecByMonth = async (sec, month) => {
    //sum sec forecast
    let sum_forecast_by_month = 0;
    await Promise.all($(".sale_forecast_" + sec + "_" + month).each(async function (i, obj) {
        let forecast_sale_key = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
        forecast_sale_key = await getVowels(forecast_sale_key, 0);
        sum_forecast_by_month += Number(forecast_sale_key);
    }));
    $('.sum_sec_forecast_' + sec).text(numberWithCommas(sum_forecast_by_month.toFixed(0)));
    //sum sec budget
    let sum_budget_by_month = 0;
    await Promise.all($(".sale_budget_" + sec + "_" + month).each(async function (i, obj) {
        let budget_sale_key = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
        budget_sale_key = await getVowels(budget_sale_key, 0);
        sum_budget_by_month += Number(budget_sale_key);
    }));
    $('.sum_sec_budget_' + sec).text(numberWithCommas(sum_budget_by_month.toFixed(0)));
    //sum sec actual
    let sum_actual_by_month = 0;
    await Promise.all($(".sale_actual_" + sec + "_" + month).each(async function (i, obj) {
        let actual_sale_key = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
        actual_sale_key = await getVowels(actual_sale_key, 0);
        sum_actual_by_month += Number(actual_sale_key);
    }));
    $('.sum_sec_actual_' + sec).text(numberWithCommas(sum_actual_by_month.toFixed(0)));
    //Budget, Actual, Forecast เป็น 0 ไม่แสดง
    if (sum_forecast_by_month == 0 && sum_budget_by_month == 0 && sum_actual_by_month == 0) {
        $("#sum_by_dec_" + sec).css("display", "none");
    } else {
        $("#sum_by_dec_" + sec).css("display", "block");
    }
}

const sumSaleForecastByCaption = async (inputName) => {
    //console.log("inputName = " + inputName);
    for (let numMonth = 1; numMonth <= 12; numMonth++) {
        let sum_by_caption = 0;
        await Promise.all($('.' + inputName + '_' + numMonth).each(async function (i, obj) {
            let val_by_caption = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
            val_by_caption = await getVowels(val_by_caption, 0);
            sum_by_caption += Number(val_by_caption);
            //console.log("sum_by_caption = " + sum_by_caption);
        }));
        if (sum_by_caption != 0 && sum_by_caption != null) {
            $('#sum_' + inputName + '_' + numMonth).text(numberWithCommas(sum_by_caption.toFixed(0)));
        }
    }
}

//sum all last year, total, under/over
const sumAllSaleForecastByCaption = async (className) => {
    //console.log("className = " + className);
    let sum_all_by_caption = 0;
    let sum_total_actual_before = 0;
    let sum_total_actual_after = 0;
    let sum_total_budget_before = 0;
    let sum_total_budget_after = 0;
    if (className == "over_actual") {
        sum_total_actual_before = $("#sum_total_actual").text();
        let sum_total_actual_after = sum_total_actual_before.toString().replace(/([[\]{}()*+?\\^$|%,])/g, '');
        sum_total_actual_after = await getVowels(sum_total_actual_after, 0);
        //console.log("sum_total_actual_after = " + sum_total_actual_after);

        sum_total_budget_before = $("#sum_total_budget").text();
        let sum_total_budget_after = sum_total_budget_before.toString().replace(/([[\]{}()*+?\\^$|%,])/g, '');
        sum_total_budget_after = await getVowels(sum_total_budget_after, 0);
        //console.log("sum_total_budget_after = " + sum_total_budget_after);

        if (sum_total_budget_after != 0 && sum_total_budget_after != null) {
            sum_all_by_caption = (sum_total_actual_after / sum_total_budget_after) * 100;
        }
        sum_all_by_caption = Math.floor(sum_all_by_caption * 100) / 100;
        sum_all_by_caption += "%";
    } else if (className == "total_forecast_all") {

    } else {
        await Promise.all($('.' + className).each(async function (i, obj) {
            let val_all_by_caption = obj.value.toString().replace(/([[\]{}()*+?\\^$|%,])/g, '');
            val_all_by_caption = await getVowels(val_all_by_caption, 0);
            sum_all_by_caption += Number(val_all_by_caption);
            //console.log("val_all_by_caption = " + val_all_by_caption);
        }));

        if (sum_all_by_caption != 0 && sum_all_by_caption != null && !isNaN(sum_all_by_caption)) {
            sum_all_by_caption = sum_all_by_caption.toFixed(0)
        }
    }
    $('#sum_' + className).text(numberWithCommas(sum_all_by_caption));
}

async function calculateForecastSale() {
    if (countList > 0) {
        $(".cardSummary").LoadingOverlay("show");
        await sumSecAll();
        await sumSaleForecastByCaption("sale_budget");
        await sumSaleForecastByCaption("sale_actual");
        await sumSaleForecastByCaption("sale_forecast");

        //Summary by sec
        await sumAllSaleForecastByCaption("last_year_budget");
        await sumAllSaleForecastByCaption("total_budget");
        await sumAllSaleForecastByCaption("under_budget");
        await sumAllSaleForecastByCaption("over_budget");

        await sumAllSaleForecastByCaption("last_year_actual");
        await sumAllSaleForecastByCaption("total_actual");
        await sumAllSaleForecastByCaption("under_actual");
        await sumAllSaleForecastByCaption("over_actual");

        await sumAllSaleForecastByCaption("last_year_forecast");
        await sumAllSaleForecastByCaption("total_forecast");
        await sumAllSaleForecastByCaption("under_forecast");
        await sumAllSaleForecastByCaption("over_forecast");

        $(".cardSummary").LoadingOverlay("hide");
    }
}
$(document).ready(function () {
    setTimeout(() => {
        calculateForecastSale();
    }, 2000);
    //flagInput = เลยเวลาคีย์แล้ว
    let flagInput = $("#flagInput").val();
    if (flagInput == 'NO') {
        $(window).unbind('beforeunload');
        $(".mask").prop('disabled', true);
    }
    //กรอกได้เฉพาะเดือนปัจจุบันเท่านั้น
    //$('input[name^="sale_forecast_110A0126_110_7"]').prop('readonly', true);

    $("[class*=sale_forecast_]").prop('readonly', true);
    $("[class=selectList]").prop('disabled', true);
    if (monthInput != "") { //กรอกได้เฉพาะเดือนปัจจุบันเท่านั้น
        $(".sale_forecast_" + monthInput).prop('readonly', false);
        $("input[data-month='" + monthInput + "']").prop("disabled", false)
    }

    if (userType == 3) {
        $("#slmCode").prop('disabled', true);
    }

    $('input.mask').keyup(function (event) {
        // skip for arrow keys
        if (event.which >= 37 && event.which <= 40) {
            event.preventDefault();
        }
        var $this = $(this);
        var num = $this.val().replace(/,/gi, "").split("").reverse().join("");
        var num2 = RemoveRougeChar(num.replace(/(.{3})/g, "$1,").split("").reverse().join(""));
        //console.log(num2);
        // the following line has been simplified. Revision history contains original.
        $this.val(num2);
    });
    //
    getCustomerAllBySale();
    //selectpicker stkgrp
    function toggleSelectAll(control) {
        var allOptionIsSelected = (control.val() || []).indexOf("ALL") > -1;
        function valuesOf(elements) {
            return $.map(elements, function (element) {
                return element.value;
            });
        }

        if (control.data('allOptionIsSelected') != allOptionIsSelected) {
            if (allOptionIsSelected) {
                control.selectpicker('val', valuesOf(control.find('option')));
            } else {
                control.selectpicker('val', []);
            }
        } else {
            if (allOptionIsSelected && control.val().length != control.find('option').length) {
                control.selectpicker('val', valuesOf(control.find('option:selected[value!=ALL]')));
                allOptionIsSelected = false;
            } else if (!allOptionIsSelected && control.val().length == control.find('option').length - 1) {
                control.selectpicker('val', valuesOf(control.find('option')));
                allOptionIsSelected = true;
            }
        }
        control.data('allOptionIsSelected', allOptionIsSelected);
    }
    $('#cusCode').selectpicker().change(function () { toggleSelectAll($(this)); }).trigger('change');
    $('#stkSec').selectpicker().change(function () { toggleSelectAll($(this)); }).trigger('change');
    //GET CUSTOMER
    $('#slmCode').change(function () {
        $.ajax({
            url: '/NB2023_Test/ForecastSale/Getdatabyslm',
            data: {
                slmCode: $(this).val(),
            },
            type: "POST",
            dataType: "JSON",
            success: function (data) {
                //console.log(data);
                $("#showCustomerSelected").css('display', 'none');
                $("#countCustomerSelected").html(0)

                var select = $("#cusCode");
                select.children().remove();
                //select.append($("<option>").val("ALL").text("CUSTOMER ALL"));
                $.each(data, function (key, value) {
                    select.append('<option value=' + value.CUSCOD + '>' + value.CUSCOD + '|' + value.CUSNAM + '</option>');
                });
                $('#cusCode').selectpicker('refresh');
                if ($("#cusCode option:selected").val() == "") {
                    $("#cusCode").val($("#cusCode option:first").val());
                }
                //get customerall
                getCustomerAllBySale();
            }
        });
    });
    //GET Stkgroup
    $('#prodMgr').change(function () {
        var stkSecHeader = $("#stkSec_header").val();
        var prodMgrHeader = $("#prodMgr_header").val();
        var prodMgrCurrent = $(this).val();
        //console.log("prodMgrHeader = " + prodMgrHeader + " prodMgrCurrent = " + prodMgrCurrent);
        var stkArray = new Array();
        var ProdMRG = $('#prodMgr').val();
        console.log("ProdMRG - " + ProdMRG);
        if (ProdMRG != 'ALL') { // PROD MGR != ALL
            ProdMRG = ProdMRG;
        } else {
            ProdMRG = "";
        }
        $.ajax({
            url: '/NB2023_Test/BudgetSale/GetSTKGRP',//'@Url.Action("GetSTKGRP", "BudgetSale")',
            data: {
                ProdMRG: ProdMRG
            },
            type: "POST",
            dataType: "JSON",
            success: function (data) {
                var select = $("#stkSec");
                select.children().remove();
                select.append($("<option>").val("ALL").text("SEC ALL"));
                $(data).each(function (index, item) {
                    select.append($("<option>").val(item.SEC).text(item.SEC + "/" + item.SECNAM));
                    stkArray.push(item.SEC);
                });
                //console.log("stkArray = " + stkArray);
                $('#stkSec').selectpicker('refresh');
                if (prodMgrHeader == prodMgrCurrent) {
                    console.log("1 = " + stkSecHeader);
                    $('#stkSec').selectpicker('val', JSON.parse(stkSecHeader));
                } else {
                    console.log("2 = " + stkArray);
                    stkArray.push('ALL');
                    $('#stkSec').selectpicker('val', stkArray);
                    //$('#stkSec').selectpicker('refresh');
                }
                $('#stkSec').trigger('change');
            }
        });
    });
    //เลือกลูกค้าได้ 10 ร้าน
    $("#cusCode").on("change", function () {
        var countCustomer = $(this).find('option:selected').length;
        if (countCustomer > 0) {
            //$("#showCustomerSelected").css('display', 'block');
            $("#showCustomerSelected").css('display', 'contents');
            $("#countCustomerSelected").html(countCustomer)
        } else {
            $("#showCustomerSelected").css('display', 'none');
            $("#countCustomerSelected").html(0)
        }
        if (countCustomer > 10) {
            Swal.fire({
                icon: 'warning',
                //title: 'กรุณาตรวจสอบข้อมูลต่อไปนี้',
                html: "เลือกลูกค้าได้ครั้งละ 10 ร้านเท่านั้น",
                confirmButtonColor: '#d33',
                confirmButtonText: 'ปิด',
                allowOutsideClick: false,
                focusConfirm: false,
            });
            return false;
        }
    });
});
//
document.onreadystatechange = function () {
    if (document.readyState === 'interactive') {
        LoadingShow();
    }
    if (document.readyState === "complete") {
        var stkSecHeader = $("#stkSec_header").val();
        var prodMgr = $("#prodMgr_header").val();
        var prodMgrCurrent = $("#prodMgr option:selected").val();
        var stkArray = new Array();

        $('#slmCode').val($("#slmCode_header").val());
        $('#cusCode').selectpicker('val', JSON.parse($("#cusCode_header").val()));
        //console.log("stkSecHeader = " + stkSecHeader);
        if (stkSecHeader != null) {
            if (stkSecHeader.includes("ALL")) {
                $("#stkSec").val('').trigger('change');
            }
        }
        if (stkSecHeader != "[]") {
            $('#stkSec').selectpicker('val', JSON.parse(stkSecHeader));
        }

        $('#year').val($("#year_header").val());
        //get list customer
        getCustomerBySlmCode($('#slmCode option:selected').val());

        if (prodMgr != "[]" && prodMgr != "ALL") {
            $('#prodMgr').selectpicker('val', prodMgr);
            getStkgrpByProd($("#prodMgr option:selected").val());
            //$('#stkSec').trigger('change');
        } else { //prod all
            $('#prodMgr').selectpicker('val', 'ALL');
            $('#stkSec').selectpicker('selectAll');
           // $('#slmCode').selectpicker('val', 'B101TNK');
           // $('#cusCode').selectpicker('val', '110A0126');
        }
        LoadingHide()
    }
}
////////////////////////////
//Script list forecastsale//
////////////////////////////
