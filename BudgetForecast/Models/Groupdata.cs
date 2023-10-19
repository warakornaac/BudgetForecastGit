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
    public class CusomerListKey
    {
        public string CUSKEY { get; set; }
        public string CUSNAM { get; set; }
    }
    public class SecListKey
    {
        public string SEC { get; set; }
        public string SEC_NAME { get; set; }

    }

}