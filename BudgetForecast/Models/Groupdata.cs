using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace BudgetForecast.Models
{
    public class STKGRPList
    {
        public string STKGRP { get; set; }
        public string GRPNAM { get; set; }
    }
    public class SECList
    {
        public string SEC { get; set; }
        public string SECNAM { get; set; }
    }
    public class LoginUserViewModel
    {
        [Required]
        [StringLength(150, MinimumLength = 10)]
        [Display(Name = "User: ")]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 2)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
    public class CatProductGroup
    {
        public string Company { get; set; }
        public string ProductGroup { get; set; }
        public string ProductLine { get; set; }
      
    }
    public class LookupVehicle
    {  
       public string Type { get; set; }
       public string Code { get; set; }
       public string Description{ get; set; }
       public string SearchDescription{ get; set; }
       public string CodeRelation{ get; set; }
       public string YrStart{ get; set; }
       public string YrEnd{ get; set; }
       public string EngineType{ get; set; }
       public string CC{ get; set; }
       public string Picture { get; set; }
       public string sort { get; set; }
    }
    public class Stkgrop
    {
        public string STKGRP { get; set; }
        public string GRPNAM { get; set; }
        public string SEC { get; set; }
        public string PROD { get; set; }
        public string DEP { get; set; }
        public string COMPANY { get; set; }
    }
    public class logincutomer
    {
        public string EmpID { get; set; }
        public string company { get; set; }
        public string UsrID { get; set; }
        public string initials { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string EMail { get; set; }
        public string CUSCOD { get; set; }
        public string SUP { get; set; }
        public string UsrTyp { get; set; }
        public string SLMCOD { get; set; }
        public string SLMNAM { get; set; }
        public string PasswordExpiredDate { get; set; }
        public string DatetoExpire { get; set; }
        public string SLMPhone{ get; set; }
        public string SalesCo { get; set; }
        public string SalesCoPhone { get; set; }
    }
    public class Brabdgrop
    {
        public string CODE { get; set; }
        public string Description { get; set; }
      
    }
    public class Segmentgrop
    {
        public string CODE { get; set; }
        public string segment { get; set; }
        public string SearchDes { get; set; }
        public string sort { get; set; }
    }
    public class Prod
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
      

    }
    public class Searchitem
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
       
    }
    public class SearchitemDetailGetdata
    {
        public Searchitem val { get; set; }

    }
    public class PricelistpageingSearch
    {
        // [Display(Name = "Product")]
        //public int? Page { get; set; }
        public string CCheck_date { get; set; }
        public string PRCLST_NO { get; set; }
        public string PEOPLE { get; set; }
        public string CUSNAM { get; set; }
        public string SLMCOD { get; set; }
        public string STKCOD { get; set; }
        public string STKDES { get; set; }
        public string STKGRP_PRC { get; set; }
        public string minord { get; set; }
        public string Promotion { get; set; }
        public string LastInvUnitPric { get; set; }
        public string LastInvDisc { get; set; }
        public string LastInvPrice { get; set; }
        public string LastInvdate { get; set; }
        public string TOTBAL { get; set; }
        public string BackOrder { get; set; }
        public string UOM { get; set; }
        public string PCDES { get; set; }
        public string Price0 { get; set; }
        public string Price { get; set; }
        public string SalePrice { get; set; }
        public string Special_Price { get; set; }
        public string spc_moq { get; set; }
        public string spc_start_date { get; set; }
        public string spc_end_date { get; set; }
        public string spc_remark { get; set; }
        public string spc_PRODAPP { get; set; }
        public string PRODNAM { get; set; }
        public string ORD_ID { get; set; }
        public string ORD_AMT { get; set; }
        public string ORD_DISCOUNT { get; set; }
        public string ORD_ExpectPrice { get; set; }
        public string ORD_Price { get; set; }
        public string ORD_QTY { get; set; }
        public string ORD_SalePrice { get; set; }
        public string ORDDAT { get; set; }
        public string ORD_LineNote { get; set; }
        public string company { get; set; }
        public string PromotionCode { get; set; }
        public string PromoDesc { get; set; }
        public string PromoPrice { get; set; }
        public string PromoMOQ { get; set; }
        public string PF { get; set; }
        public string Rack { get; set; }
        public string Rcw { get; set; }
        public string Totbck { get; set; }
        public string SPackUOM { get; set; }
        public string expired { get; set; }
        public string itemblock { get; set; }
        public string PATH { get; set; }
        public string Expected_Receipt_Date { get; set; }
        public string maxord { get; set; }
        public List<PricelistpageingSearch> PricelistpageingSearch_Grid { get; set; }
    }
    public class vehicle_PlusItem
    {
        public string Company	  { get; set; }
        public string STKCOD	  { get; set; }
        public string Description	 { get; set; }
        public string Stock	 { get; set; }
        public string EndPrice	 { get; set; }
        public string IMAGE_NAME { get; set; }

    }
    public class Listvehicle_PlusItem
    {
        public vehicle_PlusItem val { get; set; }
    }
    public class ListPagedList
    {
        public PricelistpageingSearch val { get; set; }
    }
    public class ItemListGetdata
    {
        public ItemOrdering val { get; set; }

    }
    public class ItemOrdering
    {
        public string CartID { get; set; }
        public string PRCLST_NO { get; set; }
        public string CUSCOD { get; set; }
        public string STKCOD { get; set; }
        public string Company { get; set; }
        public string STKDES { get; set; }
        public string STKGRP { get; set; }
        public string STKGRPNam { get; set; }
        public string MINORD { get; set; }
        public string Price { get; set; }
        public string SalePrice { get; set; }
        public string SpecialPrice { get; set; }
        public string ExpectPrice { get; set; }
        public string Qty { get; set; }
        public string User { get; set; }
        public string Amt { get; set; }
        public string ORDDAT { get; set; }
        public string LineNote { get; set; }
        public string Status { get; set; }
        public string Discount { get; set; }
        public int QtyAmt { get; set; }
        public string AmtQty { get; set; }
        public string AmtSalePrices { get; set; }
        public string TotalPrice { get; set; }
        public string TotalDiscount { get; set; }
        public string TotalAmt { get; set; }
        public string amtCredit { get; set; }
        public string AmtDiscount { get; set; }
        public string UOM { get; set; }
        public string SLMID { get; set; }
        public string PromotionDesc { get; set; }
        public string Promotion { get; set; }
        public string LastInvdate { get; set; }
        public string LastInvPrice { get; set; }
        public string InStock { get; set; }
        public string Item_Type { get; set; }
        public string PrcApproveBy { get; set; }
        public string Stock { get; set; }
        public string Backorder { get; set; }
        public string Type_Cal { get; set; }
        public string Special_Discount { get; set; }
        public string DiscountPercent { get; set; }
        public string ORDMOD_Type { get; set; }
        public string ORD_Type { get; set; }
        public string GenID { get; set; }
        public string Ready_Status { get; set; }
        public string maxord { get; set; }

        public string PrcRemark { get; set; }
        public string Promotion_Foc { get; set; }
    }
    public class SLM
    {

        public string SLMCOD { get; set; }
        public string SLMNAM { get; set; }
    }
    public class CUS
    {
      public string CUSCOD{ get; set; }
      public string CUSNAM { get; set; }
      public string PRO { get; set; }
      public string ADDR_01{ get; set; }
      public string ADDR_02 { get; set; }
      public string CUSTYP { get; set; }
      public string AACCrlimit { get; set; }
      public string AACBalance  { get; set; }
      public string TACCrlimit  { get; set; }
      public string TACBalance { get; set; }
      public string SLMCOD { get; set; }
      public string INACTIVE { get; set; }
      public string BLOCKED { get; set; }
      public string AACPAYTRM { get; set; }
      public string TACPAYTRM { get; set; }
      public string TELNUM { get; set; }

      public string Hierarchy1_Market_Segment { get; set; }
      public string Hierarchy2_Channel { get; set; }
      public string Hierarchy3_Bussiness_Type { get; set; }

    }
    public class shipto
    {
        public string customer { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string name2 { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
    }
    public class ItemListshipto
    {
        public shipto val { get; set; }

    }
    public class Transport_DenyK99
    {
        public string Code { get; set; }
        public string Name { get; set; }


    }
    public class ItemListTransport_DenyK99
    {
        public Transport_DenyK99 val { get; set; }

    }
    public class Itemordertype
    {
        public string ORD_Type { get; set; }
        public string ORD_TypeName { get; set; }
        public string ORD_Time { get; set; }
        public string DateCal { get; set; }

    }
    public class ItemConfirmPro
    {
        public string chk { get; set; }
        public string VQty { get; set; }
        public string VSTKCOD { get; set; }
        public string VCompany { get; set; }
        public string VSTKDES { get; set; }
        public string VSTKGRP { get; set; }
        public string VPrice { get; set; }
        public string VSalePrice { get; set; }
        public string VDiscount { get; set; }
        public string VAmt { get; set; }
        public string VORDDAT { get; set; }
        public string VLineNote { get; set; }
        public string VPromotion { get; set; }
        public string CUSCOD { get; set; }
        public string AmtQty { get; set; }
        public string AmtSalePrices { get; set; }
        public string TotalAmt { get; set; }
        public string Credit { get; set; }
        public string Ordertype { get; set; }
        public string AmtDiscount { get; set; }
        public string PRCLST_NO { get; set; }
        public string Uom { get; set; }
        public string SLMID { get; set; }
        public string PromotionDesc { get; set; }
        public string Type_Cal { get; set; }
        public string Special_Discount { get; set; }
        public string DiscountPercent { get; set; }
    }
    public class ItemConfirm
    {
        public string Vidorder { get; set; }
        public string VQty { get; set; }
        public string VSTKCOD { get; set; }
        public string Vstock { get; set; }
        public string Vtype { get; set; }
        public string VCompany { get; set; }
        public string VSTKDES { get; set; }
        public string VSTKGRP { get; set; }
        public string VPrice { get; set; }
        public string VSalePrice { get; set; }
        public string VDiscount { get; set; }
        public string VAmt { get; set; }
        public string VORDDAT { get; set; }
        public string VLineNote { get; set; }
        public string VPromotion { get; set; }
        public string CUSCOD { get; set; }
        public string AmtQty { get; set; }
        public string AmtSalePrices { get; set; }
        public string TotalAmt { get; set; }
        public string Credit { get; set; }
        public string Ordertype { get; set; }
        public string AmtDiscount { get; set; }
        public string PRCLST_NO { get; set; }
        public string Uom { get; set; }
        public string SLMID { get; set; }
        public string PromotionDesc { get; set; }
        public string Remark { get; set; }
        public string Vbackorder { get; set; }
        public string VGenID { get; set; }

    }

    public class SLMc
    {

        public string SumQty { get; set; }
        public string CountPN { get; set; }
        public string Countrow { get; set; }
        public string Status { get; set; }
    }
    public class Listslmcount
    {
        public SLMc val { get; set; }
    }
    public class DetailSLM
    {

        public string salmman { get; set; }
        public string salmmanname { get; set; }
        public string customer { get; set; }
        public string customername { get; set; }
        public string Countrow { get; set; }
        public string sumqty { get; set; }

    }
    public class ListsDetailSLM
    {
        public DetailSLM val { get; set; }
    }
    public class SaleOrderList
    {

        public string RowNo { get; set; }
        public string ORD_ID { get; set; }
        public string ORD_DocNo { get; set; }
        public string ORD_Date { get; set; }
        public string ORD_TotalAmt { get; set; }
        public string QTY { get; set; }

        public string Picking { get; set; }
        public string Picking_Date { get; set; }
        public string Invoice { get; set; }
        public string Invoice_Date { get; set; }

        public string InsertDate { get; set; }
        public string DType { get; set; }
        public string Customer { get; set; }
        public string SlmCod { get; set; }
        public string Prn_ORD { get; set; }
        public string Order_Status { get; set; }
        public string PrintID { get; set; }
        public string StatusID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string DeliveryDate { get; set; }
    }
    public class OrderListGetdata
    {
        public SaleOrderList val { get; set; }

    }
    public class OrderListDetailGetdata
    {
        public SaleOrderDetail val { get; set; }

    }
    public class SaleOrderDetail
    {
        public string RowNo { get; set; }
        public string chk { get; set; }
        public string VQty { get; set; }
        public string VSTKCOD { get; set; }
        public string VSTKDES { get; set; }
        public string VSTKGRP { get; set; }
        public string VPrice { get; set; }
        public string VSalePrice { get; set; }
        public string VDiscount { get; set; }
        public string VAmt { get; set; }
        public string VORDDAT { get; set; }
        public string VLineNote { get; set; }
        public string VPromotion { get; set; }
        public string CUSCOD { get; set; }
        public string AmtQty { get; set; }
        public string AmtSalePrices { get; set; }
        public string TotalAmt { get; set; }
        public string Credit { get; set; }
        public string Ordertype { get; set; }
        public string AmtDiscount { get; set; }
        public string PRCLST_NO { get; set; }
        public string Uom { get; set; }
        public string SLMID { get; set; }
        public string PromotionDesc { get; set; }
        public string Item_Type { get; set; }
    }
    public class ItemListshop
    {
        public string Sop { get; set; }
        public string PRCLST_NO { get; set; }
        public string CUSCOD { get; set; }
        public string STKCOD { get; set; }
        public string PrHot { get; set; }
        public string Price0 { get; set; }
        public string Price { get; set; }
        public string SalePrice { get; set; }
        public string ExpectPrice { get; set; }
        public string Qty { get; set; }
        public string Moq { get; set; }
        public string User { get; set; }
        public string Amt { get; set; }
        public string LineNote { get; set; }
        public string ID { get; set; }
        public string UOM { get; set; }
        public string STKDES { get; set; }
        public string STKGRP { get; set; }
        public string company { get; set; }
        public string PromotionCode { get; set; }
        public string PromoDesc { get; set; }
        public string PromoPrice { get; set; }
        public string PromoMOQ { get; set; }
        public string Special_Price { get; set; }
        public string Spc_moq { get; set; }
        public string LastInvPrice { get; set; }
        public string LastInvdate { get; set; }
        public string CSalePrice { get; set; }
        public string Qtybo { get; set; }
        public string PlcPrice { get; set; }
        public string SpcPrice { get; set; }
      
    }
    public class ItemFoc
    {
        public string chk { get; set; }
        public string VQty { get; set; }
        public string VSTKCOD { get; set; }
        public string VCompany { get; set; }
        public string VSTKDES { get; set; }
        public string VSTKGRP { get; set; }
        public string VPrice { get; set; }
        public string VSalePrice { get; set; }
        public string VDiscount { get; set; }
        public string VAmt { get; set; }
        public string VORDDAT { get; set; }
        public string VLineNote { get; set; }
        public string VPromotion { get; set; }
        public string CUSCOD { get; set; }
        public string AmtQty { get; set; }
        public string AmtSalePrices { get; set; }
        public string TotalAmt { get; set; }
        public string Credit { get; set; }
        public string Ordertype { get; set; }
        public string AmtDiscount { get; set; }
        public string PRCLST_NO { get; set; }
        public string Uom { get; set; }
        public string SLMID { get; set; }
        public string PromotionDesc { get; set; }
        public string VCUSCOD { get; set; }
        public string Backorderfoc { get; set; }
    }

    public class historyorder
    {
        public string Company { get; set; }
        public string CUSCOD { get; set; }
        public string CUSNAM { get; set; }
        public string CUSKEY { get; set; }
        public string STKCOD { get; set; }
        public string STKDES { get; set; }
        public string Qty { get; set; }
        public string Sp { get; set; }
        public string Amt { get; set; }
        public string Docdat { get; set; }
        public string NoofOrder { get; set; }
        public string TotalQty { get; set; }
        public string TotalAmt { get; set; }
        public string Qty01 { get; set; }
        public string Qty02 { get; set; }
        public string Qty03 { get; set; }
        public string Qty04 { get; set; }
        public string Qty05 { get; set; }
        public string Qty06 { get; set; }
        public string Qty07 { get; set; }
        public string Qty08 { get; set; }
        public string Qty09 { get; set; }
        public string Qty10 { get; set; }
        public string Qty11 { get; set; }
        public string Qty12 { get; set; }
    


    }

    public class HistoryorderList
    {
        public historyorder val { get; set; }

    }
    public class ItemListQno
    {
        public string QuatationNo { get; set; }
        public string Detail { get; set; }


    }
    public class ItemListdropList
    {
        public ItemListdrop val { get; set; }

    }
    public class ItemListdrop
    {
        public string No { get; set; }
        public string STKDES { get; set; }


    }
    public class Listloc
    {
        public string CUSCOD { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Remark { get; set; }
        public string CUSNAM { get; set; }
        public string ShipCode { get; set; }
        public string ShipStatus { get; set; }

    }
    public class CrmOrder
    {
        public string ScCrmNo { get; set; }
        public string ScCrm_DocNo { get; set; }
        public string STKCOD { get; set; }
        public string STKDES { get; set; }
        public string STKGRP { get; set; }
        public string Qty { get; set; }
        public string UnitPrice { get; set; }
        public string Amount { get; set; }
        public string StatusItem { get; set; }
        public string StatusItemName { get; set; }
        public string LineNote { get; set; }
        public string company { get; set; }
    }
    public class PricelistpageingSearchTemp
    {
        // [Display(Name = "Product")]
        public int? Page { get; set; }
        public string CCheck_date { get; set; }
        public string PRCLST_NO { get; set; }
        public string PEOPLE { get; set; }
        public string CUSNAM { get; set; }
        public string SLMCOD { get; set; }
        public string STKCOD { get; set; }
        public string STKDES { get; set; }
        public string STKGRP_PRC { get; set; }
        public string minord { get; set; }
        public string Promotion { get; set; }
        public string LastInvPrice { get; set; }
        public string LastInvdate { get; set; }
        public string TOTBAL { get; set; }
        public string BackOrder { get; set; }
        public string UOM { get; set; }
        public string PCDES { get; set; }
        public string Price0 { get; set; }
        public string Price { get; set; }
        public string SalePrice { get; set; }
        public string Special_Price { get; set; }
        public string spc_moq { get; set; }
        public string spc_start_date { get; set; }
        public string spc_end_date { get; set; }
        public string spc_remark { get; set; }
        public string spc_PRODAPP { get; set; }
        public string PRODNAM { get; set; }
        public string ORD_ID { get; set; }
        public string ORD_AMT { get; set; }
        public string ORD_DISCOUNT { get; set; }
        public string ORD_ExpectPrice { get; set; }
        public string ORD_Price { get; set; }
        public string ORD_QTY { get; set; }
        public string ORD_SalePrice { get; set; }
        public string ORDDAT { get; set; }
        public string ORD_LineNote { get; set; }
        public string company { get; set; }
        public string PromotionCode { get; set; }
        public string PromoDesc { get; set; }
        public string PromoPrice { get; set; }
        public string PromoMOQ { get; set; }
        public string Rcw { get; set; }
        public string Totbck { get; set; }
    }
    public class ListPagedListtemp
    {
        public PricelistpageingSearchTemp val { get; set; }
    }
    public class Crm
    {

        public string ScCrm_ID { get; set; }
        public string ScCrm_DocNo { get; set; }
        public string Document_Order { get; set; }
        public string ScCrm_Orderdate { get; set; }
        public string ScCrm_DocType { get; set; }
        public string ScCrm_RequeDelivery { get; set; }
        public string ScCrm_RequeDeliverytime { get; set; }
        public string ScCrm_ByCall { get; set; }
        public string ScCrm_PhoneStatus { get; set; }
        public string ScCrm_CUSCOD { get; set; }
        public string ScCrm_CUSNAM { get; set; }
        public string ScCrm_SLMCODE { get; set; }
        public string ScCrm_SLMNAM { get; set; }
        public string ScCrm_Status { get; set; }
        public string ScCrm_Step1 { get; set; }
        public string ScCrm_Step2 { get; set; }
        public string ScCrm_Step3 { get; set; }
        public string ScCrm_Step4 { get; set; }
        public string ScCrm_Step5 { get; set; }
        public string ScCrm_Linenote { get; set; }
        public string ScCrm_UserCreate { get; set; }
        public string ScCrm_Createdate { get; set; }
        public string ScCrm_Createdatetime { get; set; }
        public string ScCrm_UseClosed { get; set; }
        public string ScCrm_UseCloseddate { get; set; }
        public string ScCrm_UseCloseddatetime { get; set; }


    }
    public class CusDoc
    {
        public string Cus { get; set; }
        public string ScCrm_DocNo { get; set; }
        public string Document_Order { get; set; }
        public string Slm { get; set; }
        public string CusNam { get; set; }
        public string ORD_Date { get; set; }
        public string InsertBy { get; set; }
    }
    public class ListCusDoc
    {
        public CusDoc val { get; set; }
    }
    public class ItemListtemp
    {
        public string item_t { get; set; }
    }

    public class BackOrderbyItem
    {
        public string ckrow { get; set; }
        public string Qtybackrow { get; set; }
        public string Qtyclearrow { get; set; }
        public string Qtyleftoverkrow { get; set; }
        public string SaleOrder_No { get; set; }
        public string SaleOrder_Date { get; set; }
        public string STKCOD { get; set; }
        public string STKDES { get; set; }
        public string STKGRP { get; set; }
        public string Price { get; set; }
        public string SalePrice { get; set; }
        public string Discount { get; set; }
        public string Qty { get; set; }
        public string Amt { get; set; }
        public string LineNote { get; set; }
        public string CUSCOD { get; set; }
        public string CUSNAM { get; set; }
        public string SLMCOD { get; set; }
        public string SLMNAM { get; set; }
        public string TOTBAL { get; set; }
        public string PromotionCode { get; set; }
        public string ConfirmOrderQty { get; set; }
        public string SeqNum { get; set; }
        public string display { get; set; }
        public string ORDQTY { get; set; }
        public string TOTRES { get; set; }
        public string Allocated { get; set; }
        public string REVQTY { get; set; }
        public string InStock { get; set; }
        public string DLVDAT { get; set; }
        public string COM { get; set; }
        public string Promo { get; set; }
        public string Comment { get; set; }
        public string Totbck { get; set; }
        public string shipto { get; set; }
        public string CUSPO { get; set; }
        public string Servicepart { get; set; }
        public string OrdTyp { get; set; }
        public List<BackOrderbyItem> BackOrderbyItem_Grid { get; set; }
    }
    public class BackOrderbyDoc
    {
        public string ckrow { get; set; }
        public string Qtybackrow { get; set; }
        public string Qtyclearrow { get; set; }
        public string Qtyleftoverkrow { get; set; }
        public string SaleOrder_No { get; set; }
        public string SaleOrder_Date { get; set; }
        public string Qty { get; set; }
        public string Amt { get; set; }
        public string Inventory { get; set; }
        public string ConfirmOrderQty { get; set; }
        public string SeqNum { get; set; }
        public string Cuscod { get; set; }
        public string Slm { get; set; }
        public string CUSPO { get; set; }
        public List<BackOrderbyDoc> BackOrderbyDoc_Grid { get; set; }
    }
    public class MainPageModel
    {
        public BackOrderbyItem Model1 { get; set; }
        public BackOrderbyDoc Model2 { get; set; }
    }
    public class ItemBackOrder
    {
        public string DocNo { get; set; }
        public string SeqNo { get; set; }
        public string VQty { get; set; }
        public string VSTKCOD { get; set; }
        public string VSTKDES { get; set; }
        public string VSTKGRP { get; set; }
        public string VPrice { get; set; }
        public string VSalePrice { get; set; }
        public string VDiscount { get; set; }
        public string VAmt { get; set; }
        public string VORDDAT { get; set; }
        public string VPromotion { get; set; }
        public string CUSCOD { get; set; }
        public string User { get; set; }
        public string ValueQty { get; set; }
        public string Vlinenote { get; set; }
        public string promo { get; set; }

    }
    public partial class BackOrder
    {
        public string ORDBck_DocNo { get; set; }
        public string Company { get; set; }
        public string DocNo { get; set; }
        public string SEQNUM { get; set; }
        public System.DateTime DocDate { get; set; }
        public string STKCOD { get; set; }
        public string STKDES { get; set; }
        public string STKGRP { get; set; }
        public Nullable<int> ORDQTY { get; set; }
        public Nullable<int> REMQTY { get; set; }
        public Nullable<decimal> SELLPR { get; set; }
        public Nullable<int> ConfirmOrderQty { get; set; }
        public string CUSPO { get; set; }
        public string SLMCOD { get; set; }
        public string CUSCOD { get; set; }
        public Nullable<int> TOTBAL { get; set; }
        public Nullable<int> PrintID { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public string InsertBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string linenote { get; set; }
        public Nullable<int> Prn_ORD { get; set; }
        public Nullable<System.DateTime> IF_Date { get; set; }
        public string IF_Status { get; set; }
        public string IF_Desc { get; set; }
        public string ORDMOD_Type { get; set; }
        public Nullable<System.DateTime> ORDMOD_Date { get; set; }
        public string ORDMOD_Time { get; set; }
        public string ORD_Type { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string DeliveryTime { get; set; }
        public string Ship_Code { get; set; }
        public string Ship_Customer { get; set; }
        public string Transport_Code { get; set; }
        public string Customer_Po { get; set; }
        public string Remark { get; set; }
        public Nullable<int> TotalBack { get; set; }
        public Nullable<System.DateTime> ORDBck_Date { get; set; }
        public string PromotionCode { get; set; }
    }
    public class SaleOrder_History
    {
        public string CUSCOD { get; set; }
        public string CUSNAM { get; set; }
        public string STKCOD { get; set; }
        public string FullDES { get; set; }
        public string QTY { get; set; }
        public string SALPRICE { get; set; }
        public string AMT { get; set; }
        public string DISCOUNT { get; set; }
        public string ORDDAT { get; set; }
        public string SONUM { get; set; }
        public string PINUM { get; set; }
        public string INVNUM { get; set; }
        public string SLMCOD { get; set; }
        public List<SaleOrder_History> SaleOrder_History_Grid { get; set; }
    }
    public class Salesamt
    {
        public string Cuscod { get; set; }	
        public string Company { get; set; }	
        public string Amt    { get; set; }	
        public string YTD	 { get; set; }	
        public string Jan	 { get; set; }	
        public string Feb	 { get; set; }	
        public string Mar	 { get; set; }	
        public string Apr	 { get; set; }	
        public string May  { get; set; }	
        public string Jun	{ get; set; }	
        public string Jul	 { get; set; }	
        public string Aug	{ get; set; }	
        public string Sep   { get; set; }	
        public string Oct	{ get; set; }	
        public string Nov	{ get; set; }
        public string Dec  { get; set; }	

    }
    public class Upload_History
    {
        public string Reference_No { get; set; }	
	    public string ID { get; set; }	
	    public string CUSCOD { get; set; }	
	    public string STKCOD { get; set; }	
	    public string Qty { get; set; }	
	    public string Status { get; set; }	
	    public string Status_Message { get; set; }	
	    public string Inserted_Date { get; set; }	
	    public string Inserted_By { get; set; }
        public string UOM { get; set; }
    }
}