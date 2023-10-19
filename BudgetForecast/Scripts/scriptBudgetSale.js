var flagSup = $("#flagSup").val();
//var pathname = window.location.pathname.split("/");
//var pathId = pathname[1].replace('/', '');
//var hostName = "";
//if (pathId != 'BudgetSale') {
//    hostName = '/' + pathId;
//}
//console.log("hostName = " + hostName);
if (window.history.replaceState) {
    window.history.replaceState(null, null, window.location.href);
}

function searchBudget() {
    $("#slmCode").prop('disabled', false);
    var msg = '';
    var error = 0;
    var slmCode = $("#slmCode option:selected").val();
    var cusCode = $("#cusCode").val();
    var stkSec = $("#stkSec").val();
    var prodMgr = $("#prodMgr option:selected").val();
    var stkGroup = $("#stkGroup").val();
    var stkGroupSelectAll = $("#stkGroup").find('option:selected').length;
    var flgBudget = $("input:radio[name=flgBudget]:checked").val();
    var flagSelectCustomerAll = $("#flagSelectCustomerAll").val();
    var SecCountAll = $("#stkSecAllCount_header").val();
    var getSelectCustomerAll = $("#cusCode").find('option:selected').length;
    var getSelectSecAll = $("#stkSec").find('option:selected').length;

    if (flagSelectCustomerAll == 'Y') {
        cusCode = "ALL";
    }
    console.log('getSelectSecAll = ' + getSelectSecAll + " SecCountAll = " + SecCountAll);
    //return false;
    if (getSelectSecAll >= SecCountAll) {
        stkSec = "ALL";
    }

    if (flgBudget == 0 && flagSelectCustomerAll == 'Y' && getSelectSecAll > 1) {
        ++error;
        msg += '<br>' + error + '. เลือก VALUE เป็น NO สามารถเลือก SEC ได้ 1 รายการเท่านั้น';
    }
    if (slmCode == undefined) {
        ++error;
        msg += '<br>' + error + '. กรุณาเลือก SALEMAN อย่างน้อย 1 รายการ';
    }
    if (cusCode == undefined) {
        ++error;
        msg += '<br>' + error + '. กรุณาเลือก CUSTOMER อย่างน้อย 1 รายการ';
    }
    if (stkSec == undefined) {
        ++error;
        msg += '<br>' + error + '. กรุณาเลือก SEC อย่างน้อย 1 รายการ';
    }
    console.log("stkGroup = " + stkGroup);
    if (stkGroup == undefined || stkGroup == '') {
        stkGroup = "ALL";
    }
    //console.log("stksec = " + stksec);
    //return false;
    if (error) {
        Swal.fire({
            icon: 'warning',
            title: 'กรุณาตรวจสอบข้อมูลต่อไปนี้',
            html: msg,
            confirmButtonColor: '#d33',
            confirmButtonText: 'Close',
            allowOutsideClick: false,
            focusConfirm: false,
        });
        return false;
    } else {
        $.ajax({
            type: 'post',
            url: hostName + '/BudgetSale/SearchBudget',
            data: {
                slmCode: slmCode,
                cusCode: cusCode,
                stkSec: stkSec,
                prodMgr: prodMgr,
                stkGroup: stkGroup,
                flgBudget: flgBudget,
            },
            dataType: 'html',
            cache: false,
            Async: true,
            beforeSend: function () {
                $('#showDataBudgetInput').empty();
                LoadingShow();
            },
            success: function (res) {
                LoadingHide();
                $('#showDataBudgetInput').html(res);
            },
            error: function (er) {
                alert(er.error);
            }
        });
    }
}

$(document).ready(function () {
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
    $('#stkGroup').selectpicker().change(function () { toggleSelectAll($(this)); }).trigger('change');

    getCustomerAllBySale();
    //GET Stkgroup
    $('#prodMgr').change(function () {
        var stkSecHeader = $("#stkSec_header").val();
        var prodMgrHeader = $("#prodMgr_header").val();
        var prodMgrCurrent = $(this).val();
        var secCode = $("#stkSec option:selected").val();
        //console.log("prodMgrHeader = " + prodMgrHeader + " prodMgrCurrent = " + prodMgrCurrent);
        var stkArray = new Array();
        var ProdMRG = $('#prodMgr').val();
        //console.log("ProdMRG - " + ProdMRG);
        if (ProdMRG != 'ALL') { // PROD MGR != ALL
            ProdMRG = ProdMRG;
        } else {
            ProdMRG = "";
        }

        getStkgrp(ProdMRG, secCode);

        $.ajax({
            url: hostName + '/BudgetSale/GetSection',
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
                //getSecAllBySale();
            }
        });
    });

    $('#stkSec').change(function () {
        var secCode =  $(this).val();
        var prodCode = $("#prodMgr option:selected").val();
        getStkgrp(prodCode, secCode);

    });
});
//ดึงข้อมูล STOCK GROUP
function getStkgrp(prodCode, secCode) {
    if (prodCode == null || prodCode == '') {
        prodCode = 'ALL';
    }
    if (secCode == null || secCode == '') {
        secCode = 'ALL';
    }
    console.log("prodCode = " + prodCode + " secCode = " + secCode);
    $.ajax({
        url: hostName + '/BudgetSale/GetStkgrp',
        data: {
            prodCode: prodCode,
            secCode: secCode
        },
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            var select = $("#stkGroup");
            select.children().remove();
            select.append($("<option>").val("ALL").text("STOCK GROUP ALL"));
            $(data).each(function (index, item) {
                select.append($("<option>").val(item.STKGRP).text(item.STKGRP + "/" + item.GRPNAM));
            });
            $('#stkGroup').selectpicker('refresh');
            $('#stkGroup').selectpicker('selectAll');
            if (prodCode.includes("ALL") && secCode.includes("ALL")) {
               // $('#stkGroup').selectpicker('selectAll');
            }
            $('#stkGroup').trigger('change');
        }
    });
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
        url: hostName + '/BudgetSale/Getdatabyslm',
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

function resetSearch() {
    LoadingShow();
    location.reload();
    LoadingHide();
}

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

$('#slmCode').change(function () {
    $.ajax({
        url: hostName + '/BudgetSale/Getdatabyslm',
        data: {
            slmCode: $(this).val(),
        },
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            $("#showCustomerSelected").css('display', 'none');
            $("#countCustomerSelected").html(0)
            //console.log(data);
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
        }
    });
});

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
        url: hostName + '/BudgetSale/GetSection',
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

//เลือกลูกค้าได้ 10 ร้าน
$("#cusCode").on("change", function () {
    getCustomerAllBySale();
    setTimeout(() => {
        //console.log($(this).val());
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
//sum Budget By field name
const sumBudgetSale = async (inputName) => {
    let sum_budget = 0;
    await Promise.all($('.' + inputName).each(async function (i, obj) {
        let budget_by_pm = obj.value.toString().replace(/([-[\]{}()*+?\\^$|%,])/g, '');
        budget_by_pm = await getVowels(budget_by_pm, 0);
        sum_budget += Number(budget_by_pm);
        //console.log(sum_budget);
    }));
    //if (sum_budget != 0) {
    $('#div_sum_' + inputName).LoadingOverlay("show");
    $('#sum_' + inputName).text(numberWithCommas(sum_budget));
    $('#div_sum_' + inputName).LoadingOverlay("hide");
    //}
}
//btn reload
async function calculateBudgetPm() {
    //$("#cardSummary").LoadingOverlay("show");
    //sam LastYr
    await sumBudgetSale("last_year");
    await sumBudgetSale("ytd");
    await sumBudgetSale("quota");
    await sumBudgetSale("quota_next");
    await sumBudgetSale("f_slm");
    await sumBudgetSale("f_prod");
}

document.onreadystatechange = function () {
    if (document.readyState === 'interactive') {
        LoadingShow();
    }
    if (document.readyState === "complete") {
        var stkSecHeader = $("#stkSec_header").val();
        var prodMgr = $("#prodMgr_header").val();

        $('#slmCode').val($("#slmCode_header").val());
        $('#cusCode').selectpicker('val', JSON.parse($("#cusCode_header").val()));
        $('#prodMgr').selectpicker('val', $("#prodMgr_header").val());
        $('#stkSec').selectpicker('val', JSON.parse($("#stkSec_header").val()));
        //$('#year').val($("#year_header").val());
        //get list customer
        getCustomerBySlmCode($('#slmCode option:selected').val());
        //getStkgrpByProd($("#prodMgr option:selected").val());
        if (stkSecHeader != null) {
            if (stkSecHeader.includes("ALL")) {
                $("#stkSec").val('').trigger('change');
            }
        }
        if (stkSecHeader != "[]") {
            $('#stkSec').selectpicker('val', JSON.parse(stkSecHeader));
        }
        if (prodMgr != "[]" && prodMgr != "ALL") {
            // alert('getStkgrpByProd');
            $('#prodMgr').selectpicker('val', prodMgr);
            getStkgrpByProd($("#prodMgr option:selected").val());
            //$('#stkSec').trigger('change');

        } else {
            $('#prodMgr').selectpicker('val', 'ALL');
            $('#stkSec').selectpicker('selectAll');
            //$('#stkGroup').selectpicker('selectAll');
            //$('#stkSec').selectpicker('refresh');
        }
        //flagInput = เลยเวลาคีย์แล้ว
        let flagInput = $("#flagInput").val();
        if (flagInput == 'NO') {
            $(window).unbind('beforeunload');
            $("input[type=checkbox]").prop('disabled', true);
        }
        if (userType == 3 && flagSup == "") {
            $("#slmCode").prop('disabled', true);
        }

        LoadingHide()
    }
}