//console.log("hostName = " + hostName);
var flagSup = $("#flagSup").val();

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
    var cusCodeAllCount = 0;
    $('#cusCode option').each(function () {
        ++cusCodeAllCount;
        cusCodeAll.push(this.value);
    });
    $("#cusCodeAll_header").val(cusCodeAll);
    if (cusCodeAllCount > 0) {
        $("#cusCodeAllCount_header").val(cusCodeAllCount);
    }
}
//get all sec by sale
function getSecAllBySale() {
    var stkSecAll = new Array();
    var stkSecAllCount = 0;
    $('#stkSec option').each(function () {
        ++stkSecAllCount;
        stkSecAll.push(this.value);
    });
    $("#stkSecAll_header").val(stkSecAll);
    if (stkSecAllCount > 0) {
        $("#stkSeceAllCount_header").val(stkSecAllCount);
    }
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
    $("#flgShowCustomer2").removeClass("btn-light");
    $("#flgShowCustomer2").addClass("btn-info");
    $("#flgShowCustomer1").removeClass("btn-info");
    $("#flgShowCustomer1").addClass("btn-light");
    //LoadingShow();
    var msg = '';
    var error = 0;
    var slmCode = $("#slmCode option:selected").val();
    var cusCode = $("#cusCode").val();
    var stkSec = $("#stkSec").val();
    var prodMgr = $("#prodMgr option:selected").val();
    var year = $("#year option:selected").val();
    var flgBudget = $("input:radio[name=flgBudget]:checked").val();
    var flagSelectCustomerAll = $("#flagSelectCustomerAll").val();
    var SecCountAll = $("#stkSecAllCount_header").val();
    var getSelectCustomerAll = $("#cusCode").find('option:selected').length;
    var getSelectSecAll = $("#stkSec").find('option:selected').length;

    if (flagSelectCustomerAll == 'Y') {
        cusCode.push("ALLCUS");
    }
    if (getSelectSecAll >= SecCountAll) {
        stkSec.push("ALLSEC");
    }
    //ถ้าเลือก value ทั้งหมด, เลือกลูกค้าทั้งหมด, เลือก sec ทั้งหมด
    if (flgBudget == 0 && flagSelectCustomerAll == 'Y' && getSelectSecAll > 1) {
        ++error;
        msg += '<br>' + error + '. เลือก VALUE เป็น NO สามารถเลือก SEC ได้ 1 รายการเท่านั้น';
    }
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
        getDataForecastSale(slmCode, cusCode, stkSec, prodMgr, year, 1, flgBudget);

        $("#flagReloadByMonth").val('N');
        $("#flagReloadBySec").val('N');
    }
}

function getDataForecastSale(slmCode, cusCode, stkSec, prodMgr, year, flg, flgBudget) {
    //flgBudget = 0 Show all, 1 = Show only figure
    var divShow = "";
    if (flg == 1) {//main
        divShow = "showDataInput";
    } else if (flg == 2) { //sum by month
        divShow = "showSumByMonth";
    }

    $.ajax({
        type: 'post',
        url: hostName + '/ForecastMidMonthSale/SearchForecast',/*url: hostName + '/ForecastSale/SearchForecast',*/ //@Url.Action("SearchForecast", "ForecastSale")',
        data: {
            slmCode: slmCode,
            cusCode: cusCode,
            stkSec: stkSec,
            prodMgr: prodMgr,
            year: year,
            flg: flg,
            flgBudget: flgBudget
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
            //get note
            getNotefromMonthSelector();
            //alert("Note 2");
        }
    });
}

function resetSearch() {
    LoadingShow();
    location.reload();
    //LoadingHide();
}

//AddNote
//function addsecNote(_seccode) {
//    (async () => {
//        const { value: text } = await Swal.fire({
//            input: "textarea",
//            title: "เพิ่ม Note Sec " + _seccode,
//            inputPlaceholder: "Type your message here...",
//            inputAttributes: {
//                "aria-label": "Type your message here"
//            },
//            showCancelButton: true,

//            confirmButtonText: "เพิ่ม",
//            cancelButtonText: "ยกเลิก",
//        });
//        if (text) {
//            Swal.fire({
//                icon: "success",
//                title: "Note Updated!!!"
//            });
//        }
//    })();
//}

function getCustomerBySlmCode(_slmCode) {
    //alert(_slmCode);
    var slmCode = "";
    if (_slmCode == "") {
        slmCode = $("#slmCode option:selected").val();
    } else {
        slmCode = _slmCode;
    }
    //alert(slmCode);
    $.ajax({
        //url: hostName + '/Getdatabyslm',
        url: hostName + '/ForecastMidMonthSale/Getdatabyslm',
        data: {
            slmCode: slmCode,
        },
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            //console.log(data);
            var select = $("#cusCode");
            select.children().remove();
            select.append($("<option>").val("ALL").text("CUSTOMER ALL"));
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
    console.log("getStkgrpByProd = " + prodCode);
    $.ajax({
        url: hostName + '/ForecastPm/GetSTKGRP',
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



//for sum by sec
const sumSecAll = async (month) => {
    var flagTabBySecAll = $("#flagTabBySecAll").val();
    // $(".iconLoading").html('<i class="fa fa-spinner fa-spin f-center"></i>');
    var sec;
    var monthSelect;
    var monthSelect = $("#month_sec option:selected").val();
    if (month === undefined) {
        month = monthSelect;
    }
    //if (flagTabBySecAll == 'Y') {
    //    getDataForecastByMonth(slmCode, cusCodeAll, stkSec, prodMgr, year, 3, month)
    //}

    $('.stkSecList_header').each(function () {
        sec = $(this).val();
        if (sec != null) {
            sumSecByMonth(sec, month);
        }
    });

    //hide icon
    //setTimeout(() => {
    //    $(".iconLoading").html('');
    //}, "1000");

}
//func Get Note
function getNotefromMonthSelector() {
    setTimeout(function () {
        $('[id^="note_input_"]').each(function () {
            let monthSelect = $("#month_sec option:selected").val().padStart(2, '0');
            let slmCode = $("#slmCode option:selected").val();
            let year = $("#year option:selected").val();
            let secValue = $(this).data('sec');
            console.log("NOTE 2");
            //let noteVal;
            $.ajax({
                url: hostName + '/ForecastMidMonthSale/GetNotebysec',
                type: 'post',
                data: {
                    MONTH_INPUT: monthSelect,
                    SEC: secValue,
                    SLMCOD: slmCode,
                    YEAR: year
                },
                dataType: 'json',
                beforeSend: function () {
                    //LoadingShow();
                    $(".iconLoading_" + secValue).html('<i class="fa fa-spinner fa-spin f-center"></i>');
                    $('.note_show_' + secValue).hide();
                },
                success: function (data) {
                    $('[class^="note_show_"]').each(function () {
                        //var secValue = $(this).data("sec");
                        var noteVal = "";
                        $.each(data, function (index, item) {
                            noteVal = item.Note;
                        });
                        if (noteVal) {

                            $('.note_show_' + secValue).html(noteVal);
                            $(this).val(noteVal);
                            console.log("Sec : " + secValue + " = " + noteVal);
                            if (noteVal.trim() != "") {
                                $('#note_btn_pic_' + secValue).removeClass("fa-plus");
                                $('#note_btn_pic_' + secValue).addClass("fa-pencil");
                            }
                        }
                        else {
                            if (noteVal.trim() == "") {
                                $('.note_show_' + secValue).text("");
                                $(this).val("");
                                $('#note_btn_pic_' + secValue).addClass("fa-plus");
                                $('#note_btn_pic_' + secValue).removeClass("fa-pencil");
                            }
                        }
                    });
                },
                error: function () {
                    alert('Error fetching data.');
                    console.log("fail!!! ");
                },
                complete: function (res) {
                    //LoadingHide();
                    $(".iconLoading_" + secValue).html('');
                    $('.note_show_' + secValue).show();
                }
            });//ajax
        });//$func
    }, 500);//settime
}//func



//sum sec by month
const sumSecByMonth = async (sec, month) => {
    //sum sec forecast
    let sum_forecast_by_month = 0;
    await Promise.all($(".sale_forecastm_" + sec + "_" + month).each(async function (i, obj) {
        let forecast_sale_key = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
        forecast_sale_key = await getVowels(forecast_sale_key, 0);
        sum_forecast_by_month += Number(forecast_sale_key);
    }));
    $('.sum_sec_forecastm_' + sec).text(numberWithCommas(sum_forecast_by_month.toFixed(0)));

    //sum sec New Forecast
    let new_sum_forecast_by_month = 0;
    await Promise.all($(".sale_forecast_" + sec + "_" + month).each(async function (i, obj) {
        let new_forecast_sale_key = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
        new_forecast_sale_key = await getVowels(new_forecast_sale_key, 0);
        new_sum_forecast_by_month += Number(new_forecast_sale_key);
    }));
    $('.sum_sec_forecast_' + sec).text(numberWithCommas(new_sum_forecast_by_month.toFixed(0)));

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

    //Remain
    let remain = 0;
    if (new_sum_forecast_by_month > 0) {
        remain = new_sum_forecast_by_month - sum_actual_by_month;
    }
    else {
        remain = sum_forecast_by_month - sum_actual_by_month;
    }
    $('.sum_sec_remain' + sec).text(numberWithCommas(remain))

    //Budget, Actual, Forecast เป็น 0 ไม่แสดง
    //console.log("sum_forecast_by_month = " + sum_forecast_by_month + " sum_budget_by_month = " + sum_budget_by_month + " sum_actual_by_month = " + sum_actual_by_month);
    if (sum_forecast_by_month == 0 && sum_budget_by_month == 0 && sum_actual_by_month == 0) {
        $("#sum_by_sec_" + sec).css("display", "none");
    } else {
        $("#sum_by_sec_" + sec).css("display", "block");
    }
}

const sumSaleForecastByCaption = async (inputName) => {
    console.log("inputName = " + inputName);
    for (let numMonth = 1; numMonth <= 12; numMonth++) {
        let sum_by_caption = 0;
        await Promise.all($('.' + inputName + '_' + numMonth).each(async function (i, obj) {
            let val_by_caption = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
            val_by_caption = await getVowels(val_by_caption, 0);
            sum_by_caption += Number(val_by_caption);
            //console.log("sum_by_caption = " + sum_by_caption);
        }));
        if (sum_by_caption != null) {
            $('#sum_' + inputName + '_' + numMonth).text(numberWithCommas(sum_by_caption.toFixed(0)));
            $('#sum_' + "input" + "_" + inputName + '_' + numMonth).val(sum_by_caption);
        }
    }
}

//sum all last year, total, under/over
const sumAllSaleForecastByCaption = async (className) => {
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
    } else if (className == "total_forecastm_all") {

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

//sum by month remain
const sumRemainBycaption = async () => {
    $('[id^="sum_input_sale_forecastm_"]').each(function () {
        let month = $(this).data('month');
        let sum_actual = $("#sum_input_sale_actual_" + month).val() || 0;
        let sum_new_forecast = $("#sum_input_sale_forecast_" + month).val() || 0;
        let sum_forecast = $(this).val() || 0;
        let remain_res = 0;

        if (sum_new_forecast > 0) {
            remain_res = sum_new_forecast - sum_actual;
        }
        //else if (sum_new_forecast <= 0) {
        //    remain_res = sum_forecast - sum_actual;
        //}
        else
            remain_res = sum_forecast - sum_actual;

        //console.log("REsult : " + sum_new_forecast + " month : " + month);
        $("#sum_sale_remain_" + month).text(numberWithCommas(remain_res.toFixed(0)));
    })
}

async function calculateForecastSale() {
    //if (countList > 0) {
    $(".cardSummary").LoadingOverlay("show");
    await sumSecAll();
    await sumSaleForecastByCaption("sale_budget");
    await sumSaleForecastByCaption("sale_actual");
    await sumSaleForecastByCaption("sale_forecast");
    await sumSaleForecastByCaption("sale_forecastm");
    await sumRemainBycaption();
    // await sumRemainByMonthAll();

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


    await sumAllSaleForecastByCaption("last_year_forecastm");
    await sumAllSaleForecastByCaption("total_forecastm");
    await sumAllSaleForecastByCaption("under_forecastm");
    await sumAllSaleForecastByCaption("over_forecastm");



    $(".cardSummary").LoadingOverlay("hide");
    //}
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

    $("[class*=sale_forecast_]").prop('disabled', true);
    if (monthInput != "") { //กรอกได้เฉพาะเดือนปัจจุบันเท่านั้น
        $(".sale_forecast_" + monthInput).attr("disabled", false);
        $("input[data-month='" + monthInput + "']").prop("disabled", false)
    }

    $("[class*=sale_forecastm_]").prop('disabled', true);
    if (monthInput != "") { //กรอกได้เฉพาะเดือนปัจจุบันเท่านั้น
        $(".sale_forecastm_" + monthInput).attr("disabled", false);
        $("input[data-month='" + monthInput + "']").prop("disabled", false)
    }

    if (userType == 3 && flagSup == "") {
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
    getSecAllBySale();
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
            url: hostName + '/ForecastMidMonthSale/Getdatabyslm',
            //url: hostName + '/ForecastSale/Getdatabyslm',
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
                select.append($("<option>").val("ALL").text("CUSTOMER ALL"));
                $.each(data, function (key, value) {
                    select.append('<option value=' + value.CUSCOD + '>' + value.CUSCOD + '|' + value.CUSNAM + '</option>');
                });
                $('#cusCode').selectpicker('refresh');
                if ($("#cusCode option:selected").val() == "") {
                    $("#cusCode").val($("#cusCode option:first").val());
                }
                $('#cusCode').selectpicker('selectAll');
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
        console.log("prodMgrHeader = " + prodMgrHeader + " prodMgrCurrent = " + prodMgrCurrent);
        var stkArray = new Array();
        var ProdMRG = $('#prodMgr').val();
        console.log("ProdMRG - " + ProdMRG);
        if (ProdMRG != 'ALL') { // PROD MGR != ALL
            ProdMRG = ProdMRG;
        } else {
            ProdMRG = "";
        }
        $.ajax({
            url: hostName + '/BudgetSale/GetSection',//'@Url.Action("GetSTKGRP", "BudgetSale")',
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
                    //console.log("1 = " + stkSecHeader);
                    $('#stkSec').selectpicker('val', JSON.parse(stkSecHeader));
                } else {
                    //console.log("2 = " + stkArray);
                    stkArray.push('ALL');
                    $('#stkSec').selectpicker('val', stkArray);
                    //$('#stkSec').selectpicker('refresh');
                }
                $('#stkSec').trigger('change');
                getSecAllBySale();
            }
        });
    });
    //เลือกลูกค้าได้ 10 ร้าน
    $("#cusCode").on("change", function () {
        getCustomerAllBySale();
        setTimeout(() => {
            var countCustomer = $(this).find('option:selected').length;
            var cusCodeAllCount_header = $("#cusCodeAllCount_header").val() || 0;
            //console.log("countCustomer = " + countCustomer);
            //console.log("cusCodeAllCount_header = " + cusCodeAllCount_header);
            if (countCustomer > 0) {
                $("#showCustomerSelected").css('display', 'contents');
                $("#countCustomerSelected").html(countCustomer)
            } else {
                $("#showCustomerSelected").css('display', 'none');
                $("#countCustomerSelected").html(0)
            }
            if (countCustomer == cusCodeAllCount_header) { //เลือกลูกค้าทั้งหมด
                $("#flagSelectCustomerAll").val('Y');
            } else {
                $("#flagSelectCustomerAll").val('N');
            }
        }, 800);
    });
    //เลือก sec
    $("#stkSec").on("change", function () {
        var countSec = $(this).find('option:selected').length;
        console.log("countSec = " + countSec);
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
        } else { //prod all
            //console.log("getStkgrpByProd");
            $('#prodMgr').selectpicker('val', 'ALL');
            $('#stkSec').selectpicker('selectAll');
            getSecAllBySale();
            // $('#slmCode').selectpicker('val', 'B101TNK');
            // $('#cusCode').selectpicker('val', '110A0126');
        }
        LoadingHide()
    }
}
////////////////////////////
//Script list forecast mid month sale//
////////////////////////////
