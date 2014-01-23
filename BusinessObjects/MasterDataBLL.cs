using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.MasterDataTableAdapters;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects.QueryDSTableAdapters;

namespace BusinessObjects {
    [System.ComponentModel.DataObject]
    public class MasterDataBLL {

        #region Tableadapters

        private BulletinTableAdapter _BulletinTableAdapter;
        public BulletinTableAdapter BulletinTableAdapter {
            get {
                if (this._BulletinTableAdapter == null) {
                    this._BulletinTableAdapter = new BulletinTableAdapter();
                }
                return this._BulletinTableAdapter;
            }
        }

        private RejectReasonTableAdapter _RejectReasonAdapter;
        public RejectReasonTableAdapter RejectReasonAdapter {
            get {
                if (this._RejectReasonAdapter == null) {
                    this._RejectReasonAdapter = new MasterDataTableAdapters.RejectReasonTableAdapter();
                }
                return this._RejectReasonAdapter;
            }
        }

        private CostCenterTableAdapter _CostCenterTableAdapter;
        public CostCenterTableAdapter CostCenterAdapter {
            get {
                if (this._CostCenterTableAdapter == null) {
                    this._CostCenterTableAdapter = new MasterDataTableAdapters.CostCenterTableAdapter();
                }
                return this._CostCenterTableAdapter;
            }
        }

        private ProvinceTableAdapter _TAProvince;
        public ProvinceTableAdapter TAProvince {
            get {
                if (this._TAProvince == null) {
                    this._TAProvince = new ProvinceTableAdapter();
                }
                return this._TAProvince;
            }
        }

        private CityTableAdapter _TACity;
        public CityTableAdapter TACity {
            get {
                if (this._TACity == null) {
                    this._TACity = new CityTableAdapter();
                }
                return this._TACity;
            }
        }

        private CityTypeTableAdapter _TACityType;
        public CityTypeTableAdapter TACityType {
            get {
                if (this._TACityType == null) {
                    this._TACityType = new CityTypeTableAdapter();
                }
                return this._TACityType;
            }
        }

        private CurrencyTableAdapter _TACurrency;
        public CurrencyTableAdapter TACurrency {
            get {
                if (this._TACurrency == null) {
                    this._TACurrency = new CurrencyTableAdapter();
                }
                return this._TACurrency;
            }
        }

        private ManageExpenseCategoyTableAdapter _TAManageExpenseCategoy;
        public ManageExpenseCategoyTableAdapter TAManageExpenseCategoy {
            get {
                if (this._TAManageExpenseCategoy == null) {
                    this._TAManageExpenseCategoy = new ManageExpenseCategoyTableAdapter();
                }
                return this._TAManageExpenseCategoy;
            }
        }

        private ManageExpenseItemTableAdapter _TAManageExpenseItem;
        public ManageExpenseItemTableAdapter TAManageExpenseItem {
            get {
                if (this._TAManageExpenseItem == null) {
                    this._TAManageExpenseItem = new ManageExpenseItemTableAdapter();
                }
                return this._TAManageExpenseItem;
            }
        }

        private ManageExpenseAccountingTableAdapter _TAManageExpenseAccounting;
        public ManageExpenseAccountingTableAdapter TAManageExpenseAccounting {
            get {
                if (this._TAManageExpenseAccounting == null) {
                    this._TAManageExpenseAccounting = new ManageExpenseAccountingTableAdapter();
                }
                return this._TAManageExpenseAccounting;
            }
        }

        private CostLimitTableAdapter _TACostLimit;
        public CostLimitTableAdapter TACostLimit {
            get {
                if (this._TACostLimit == null) {
                    this._TACostLimit = new CostLimitTableAdapter();
                }
                return this._TACostLimit;
            }
        }

        private PeriodReimburseTableAdapter _TAPeriodReimburse;
        public PeriodReimburseTableAdapter TAPeriodReimburse {
            get {
                if (this._TAPeriodReimburse == null) {
                    this._TAPeriodReimburse = new PeriodReimburseTableAdapter();
                }
                return this._TAPeriodReimburse;
            }
        }

        private ExpenseCategoryTableAdapter _TAExpenseCategory;
        public ExpenseCategoryTableAdapter TAExpenseCategory {
            get {
                if (this._TAExpenseCategory == null) {
                    this._TAExpenseCategory = new ExpenseCategoryTableAdapter();
                }
                return this._TAExpenseCategory;
            }
        }

        private ExpenseSubCategoryTableAdapter _TAExpenseSubCategory;
        public ExpenseSubCategoryTableAdapter TAExpenseSubCategory {
            get {
                if (this._TAExpenseSubCategory == null) {
                    this._TAExpenseSubCategory = new ExpenseSubCategoryTableAdapter();
                }
                return this._TAExpenseSubCategory;
            }
        }

        private ExpenseItemTableAdapter _TAExpenseItem;
        public ExpenseItemTableAdapter TAExpenseItem {
            get {
                if (this._TAExpenseItem == null) {
                    this._TAExpenseItem = new ExpenseItemTableAdapter();
                }
                return this._TAExpenseItem;
            }
        }

        private MarketingProjectTableAdapter _TAMarketingProject;
        public MarketingProjectTableAdapter TAMarketingProject {
            get {
                if (this._TAMarketingProject == null) {
                    this._TAMarketingProject = new MarketingProjectTableAdapter();
                }
                return this._TAMarketingProject;
            }
        }

        private DisplayTypeTableAdapter _TADisplayType;
        public DisplayTypeTableAdapter TADisplayType {
            get {
                if (this._TADisplayType == null) {
                    this._TADisplayType = new DisplayTypeTableAdapter();
                }
                return this._TADisplayType;
            }
        }

        private DiscountTypeTableAdapter _TADiscountType;
        public DiscountTypeTableAdapter TADiscountType {
            get {
                if (this._TADiscountType == null) {
                    this._TADiscountType = new DiscountTypeTableAdapter();
                }
                return this._TADiscountType;
            }
        }

        private PeriodSaleTableAdapter _TAPeriodSale;
        public PeriodSaleTableAdapter TAPeriodSale {
            get {
                if (this._TAPeriodSale == null) {
                    this._TAPeriodSale = new PeriodSaleTableAdapter();
                }
                return this._TAPeriodSale;
            }
        }

        private ExchangeRateTableAdapter _TAExchangeRate;
        public ExchangeRateTableAdapter TAExchangeRate {
            get {
                if (this._TAExchangeRate == null) {
                    this._TAExchangeRate = new ExchangeRateTableAdapter();
                }
                return this._TAExchangeRate;
            }
        }

        private ItemCategoryTableAdapter _TAItemCategory;
        public ItemCategoryTableAdapter TAItemCategory {
            get {
                if (this._TAItemCategory == null) {
                    this._TAItemCategory = new ItemCategoryTableAdapter();
                }
                return this._TAItemCategory;
            }
        }

        private ItemTableAdapter _TAItem;
        public ItemTableAdapter TAItem {
            get {
                if (this._TAItem == null) {
                    this._TAItem = new ItemTableAdapter();
                }
                return this._TAItem;
            }
        }

        private GLAccountTableAdapter _TAGLAccount;
        public GLAccountTableAdapter TAGLAccount {
            get {
                if (this._TAGLAccount == null) {
                    this._TAGLAccount = new GLAccountTableAdapter();
                }
                return this._TAGLAccount;
            }
        }

        private VendorTypeTableAdapter _TAVendorType;
        public VendorTypeTableAdapter TAVendorType {
            get {
                if (this._TAVendorType == null) {
                    this._TAVendorType = new VendorTypeTableAdapter();
                }
                return this._TAVendorType;
            }
        }

        private ShippingTermTableAdapter _TAShippingTerm;
        public ShippingTermTableAdapter TAShippingTerm {
            get {
                if (this._TAShippingTerm == null) {
                    this._TAShippingTerm = new ShippingTermTableAdapter();
                }
                return this._TAShippingTerm;
            }
        }

        private PurchaseBudgetTypeTableAdapter _TAPurchaseBudgetType;
        public PurchaseBudgetTypeTableAdapter TAPurchaseBudgetType {
            get {
                if (this._TAPurchaseBudgetType == null) {
                    this._TAPurchaseBudgetType = new PurchaseBudgetTypeTableAdapter();
                }
                return this._TAPurchaseBudgetType;
            }
        }

        private PurchaseTypeTableAdapter _TAPurchaseType;
        public PurchaseTypeTableAdapter TAPurchaseType {
            get {
                if (this._TAPurchaseType == null) {
                    this._TAPurchaseType = new PurchaseTypeTableAdapter();
                }
                return this._TAPurchaseType;
            }
        }

        private PeriodPurchaseTableAdapter _TAPeriodPurchase;
        public PeriodPurchaseTableAdapter TAPeriodPurchase {
            get {
                if (this._TAPeriodPurchase == null) {
                    this._TAPeriodPurchase = new PeriodPurchaseTableAdapter();
                }
                return this._TAPeriodPurchase;
            }
        }

        private InvoiceStatusTableAdapter _TAInvoiceStatus;
        public InvoiceStatusTableAdapter TAInvoiceStatus {
            get {
                if (this._TAInvoiceStatus == null) {
                    this._TAInvoiceStatus = new InvoiceStatusTableAdapter();
                }
                return this._TAInvoiceStatus;
            }
        }

        private CompanyTableAdapter _TACompany;
        public CompanyTableAdapter TACompany {
            get {
                if (this._TACompany == null) {
                    this._TACompany = new CompanyTableAdapter();
                }
                return this._TACompany;
            }
        }

        private CustomerTypeTableAdapter _TACustomerType;
        public CustomerTypeTableAdapter TACustomerType {
            get {
                if (this._TACustomerType == null) {
                    this._TACustomerType = new CustomerTypeTableAdapter();
                }
                return this._TACustomerType;
            }
        }

        private CustomerChannelTableAdapter _TACustomerChannel;
        public CustomerChannelTableAdapter TACustomerChannel {
            get {
                if (this._TACustomerChannel == null) {
                    this._TACustomerChannel = new CustomerChannelTableAdapter();
                }
                return this._TACustomerChannel;
            }
        }

        private CustomerRegionTableAdapter _TACustomerRegion;
        public CustomerRegionTableAdapter TACustomerRegion {
            get {
                if (this._TACustomerRegion == null) {
                    this._TACustomerRegion = new CustomerRegionTableAdapter();
                }
                return this._TACustomerRegion;
            }
        }

        private CustomerTableAdapter _TACustomer;
        public CustomerTableAdapter TACustomer {
            get {
                if (this._TACustomer == null) {
                    this._TACustomer = new CustomerTableAdapter();
                }
                return this._TACustomer;
            }
        }

        private BrandTableAdapter _TABrand;
        public BrandTableAdapter TABrand {
            get {
                if (this._TABrand == null) {
                    this._TABrand = new BrandTableAdapter();
                }
                return this._TABrand;
            }
        }

        private SKUCategoryTableAdapter _TASKUCategory;
        public SKUCategoryTableAdapter TASKUCategory {
            get {
                if (this._TASKUCategory == null) {
                    this._TASKUCategory = new SKUCategoryTableAdapter();
                }
                return this._TASKUCategory;
            }
        }

        private SKUTypeTableAdapter _TASKUType;
        public SKUTypeTableAdapter TASKUType {
            get {
                if (this._TASKUType == null) {
                    this._TASKUType = new SKUTypeTableAdapter();
                }
                return this._TASKUType;
            }
        }

        private SKUTableAdapter _TASKU;
        public SKUTableAdapter TASKU {
            get {
                if (this._TASKU == null) {
                    this._TASKU = new SKUTableAdapter();
                }
                return this._TASKU;
            }
        }

        private SKUPriceTableAdapter _TASKUPrice;
        public SKUPriceTableAdapter TASKUPrice {
            get {
                if (this._TASKUPrice == null) {
                    this._TASKUPrice = new SKUPriceTableAdapter();
                }
                return this._TASKUPrice;
            }
        }

        private VendorTableAdapter _TAVendor;
        public VendorTableAdapter TAVendor {
            get {
                if (this._TAVendor == null) {
                    this._TAVendor = new VendorTableAdapter();
                }
                return this._TAVendor;
            }
        }

        private Export_VendorTableAdapter _TAVendorView;
        public Export_VendorTableAdapter TAVendorView {
            get {
                if (this._TAVendorView == null) {
                    this._TAVendorView = new Export_VendorTableAdapter();
                }
                return this._TAVendorView;
            }
        }

        private PaymentTypeTableAdapter _TAPaymentType;
        public PaymentTypeTableAdapter TAPaymentType {
            get {
                if (this._TAPaymentType == null) {
                    this._TAPaymentType = new PaymentTypeTableAdapter();
                }
                return this._TAPaymentType;
            }
        }
        // add guoxj
        private ACTypeTableAdapter _TAACType;
        public ACTypeTableAdapter TAACType {
            get {
                if (this._TAACType == null) {
                    this._TAACType = new ACTypeTableAdapter();
                }
                return this._TAACType;
            }
        }

        private BankCodeTableAdapter _TABankCode;
        public BankCodeTableAdapter TABankCode {
            get {
                if (this._TABankCode == null) {
                    this._TABankCode = new BankCodeTableAdapter();
                }
                return this._TABankCode;
            }
        }

        private MethodPaymentTableAdapter _TAMethodPayment;
        public MethodPaymentTableAdapter TAMethodPayment {
            get {
                if (this._TAMethodPayment == null) {
                    this._TAMethodPayment = new MethodPaymentTableAdapter();
                }
                return this._TAMethodPayment;
            }
        }

        private PaymentTermTableAdapter _TAPaymentTerm;
        public PaymentTermTableAdapter TAPaymentTerm {
            get {
                if (this._TAPaymentTerm == null) {
                    this._TAPaymentTerm = new PaymentTermTableAdapter();
                }
                return this._TAPaymentTerm;
            }
        }
        private TransTypeTableAdapter _TATransType;
        public TransTypeTableAdapter TATransType {
            get {
                if (this._TATransType == null) {
                    this._TATransType = new TransTypeTableAdapter();
                }
                return this._TATransType;
            }
        }
        private VatTypeTableAdapter _TAVatType;
        public VatTypeTableAdapter TAVatType {
            get {
                if (this._TAVatType == null) {
                    this._TAVatType = new VatTypeTableAdapter();
                }
                return this._TAVatType;
            }
        }

        private UserAndRegionTableAdapter _TAUserAndRegion;
        public UserAndRegionTableAdapter TAUserAndRegion {
            get {
                if (this._TAUserAndRegion == null) {
                    this._TAUserAndRegion = new UserAndRegionTableAdapter();
                }
                return this._TAUserAndRegion;
            }
        }
        //end guoxj
        #endregion

        #region Bulletin Operate

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.BulletinDataTable GetBulletin() {
            return this.BulletinTableAdapter.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.BulletinDataTable GetBulletinById(int BulletinId) {
            return this.BulletinTableAdapter.GetDataByID(BulletinId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertBulletin(string bulletinTitle, string bulletinContent, bool isHot, string attachFileName, string realAttachFileName) {
            this.BulletinTableAdapter.Insert(bulletinTitle, bulletinContent, "", DateTime.Now, true, isHot, attachFileName, realAttachFileName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateBulletin(int bulletinId, string bulletinTitle, string bulletinContent, bool isHot, bool isActive, string attachFileName, string realAttachFileName) {
            MasterData.BulletinDataTable bulletins = this.BulletinTableAdapter.GetDataByID(bulletinId);
            MasterData.BulletinRow bulletinRow = bulletins[0];
            bulletinRow.BulletinTitle = bulletinTitle;
            bulletinRow.BulletinContent = bulletinContent;
            bulletinRow.CreateTime = DateTime.Now;
            bulletinRow.IsActive = isActive;
            bulletinRow.IsHot = isHot;
            bulletinRow.AttachFileName = attachFileName;
            bulletinRow.RealAttachFileName = realAttachFileName;
            this.BulletinTableAdapter.Update(bulletinRow);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public void DeleteBulletin(int bulletinId) {
            this.BulletinTableAdapter.DeleteByID(bulletinId);
        }

        // 获取总行数
        public int TotalCount(string queryExpression) {
            return (int)this.BulletinTableAdapter.QueryDataCount("Bulletin", queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.BulletinDataTable GetPage(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CreateTime desc";
            }
            return this.BulletinTableAdapter.GetDataPaged("Bulletin", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.BulletinDataTable GetPageInActive(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "IsHot Desc,CreateTime desc";
            }
            queryExpression = "IsActive = 1";
            return this.BulletinTableAdapter.GetDataPaged("Bulletin", sortExpression, startRowIndex, maximumRows, queryExpression);
        }
        #endregion

        #region  RejectReason Operate

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public MasterData.RejectReasonDataTable GetRejectReason() {
            return this.RejectReasonAdapter.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public MasterData.RejectReasonDataTable GetRejectReasonById(int id) {
            return this.RejectReasonAdapter.GetDataByID(id);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public bool InsertRejectReason(string RejectReasonTitle, int RejectReasonIndex, string RejectReasonContent, bool IsActive) {
            int rowsAffected = 0;

            // 进行数据新增处理
            MasterData.RejectReasonDataTable tab = new MasterData.RejectReasonDataTable();
            MasterData.RejectReasonRow row = tab.NewRejectReasonRow();

            try {
                // 进行传值
                row.RejectReasonTitle = RejectReasonTitle;
                row.RejectReasonIndex = RejectReasonIndex;
                row.RejectReasonContent = RejectReasonContent;
                row.IsActive = IsActive;
                // 填加行并进行更新处理
                tab.AddRejectReasonRow(row);
                rowsAffected = this.RejectReasonAdapter.Update(tab);
            } catch (Exception e) {
                // put errors 
                throw e;
            }
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public bool UpdateRejectReason(string RejectReasonTitle, int RejectReasonId, int RejectReasonIndex, string RejectReasonContent, bool IsActive) {
            int rowsAffected = 0;
            MasterData.RejectReasonDataTable tab = this.RejectReasonAdapter.GetDataByID(RejectReasonId);
            if (tab.Count == 0) {
                return false;
            }
            try {
                // 进行传值
                MasterData.RejectReasonRow row = tab[0];
                row.RejectReasonTitle = RejectReasonTitle;
                row.RejectReasonIndex = RejectReasonIndex;
                row.RejectReasonContent = RejectReasonContent;
                row.IsActive = IsActive;

                // 更新数据
                rowsAffected = this.RejectReasonAdapter.Update(row);
            } catch (Exception e) {
                throw e;
            }
            return rowsAffected == 1;
        }

        public MasterData.RejectReasonDataTable GetRejectReasonPaged(int startRowIndex, int maximumRows, string sortExpression) {
            return this.RejectReasonAdapter.GetDataPaged("RejectReason", sortExpression, startRowIndex, maximumRows, "1=1");
        }

        public int QueryTotalCount() {
            return (int)this.RejectReasonAdapter.QueryDataCount("RejectReason", "1=1");
        }

        #endregion

        #region CostCenter  Operate

        public MasterData.CostCenterRow GetCostCenterById(int Id) {
            return this.CostCenterAdapter.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.CostCenterDataTable GetCostCenterPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CostCenterID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "CostCenterID is not null";
            }
            return this.CostCenterAdapter.GetDataPaged("CostCenter ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCostCenter(string CostCenterName, string CostCenterCode, int CompanyID, string Region, bool IsMAA, bool IsActive, string AccrualCode) {
            MasterData.CostCenterDataTable CostCenterTab = new MasterData.CostCenterDataTable();
            MasterData.CostCenterRow CostCenterRow = CostCenterTab.NewCostCenterRow();
            CostCenterRow.CostCenterName = CostCenterName;
            CostCenterRow.CostCenterCode = CostCenterCode;
            CostCenterRow.AccrualCode = AccrualCode;
            CostCenterRow.CompanyID = CompanyID;
            CostCenterRow.Region = Region;
            CostCenterRow.IsMAA = IsMAA;
            CostCenterRow.IsActive = IsActive;
            CostCenterTab.AddCostCenterRow(CostCenterRow);
            this.CostCenterAdapter.Update(CostCenterTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCostCenter(int CostCenterID, string CostCenterName, string CostCenterCode, int CompanyID, string Region, bool IsMAA, bool IsActive, int UserID, int PositionID, string AccrualCode) {
            MasterData.CostCenterDataTable CostCenterTab = this.CostCenterAdapter.GetDataByID(CostCenterID);
            MasterData.CostCenterRow CostCenterRow = CostCenterTab[0];
            CostCenterRow.CostCenterName = CostCenterName;
            CostCenterRow.CostCenterCode = CostCenterCode;
            CostCenterRow.AccrualCode = AccrualCode;
            CostCenterRow.CompanyID = CompanyID;
            CostCenterRow.Region = Region;
            CostCenterRow.IsMAA = IsMAA;
            CostCenterRow.IsActive = IsActive;
            this.CostCenterAdapter.Update(CostCenterRow);
        }

        public int CostCenterTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "CostCenterID is not null";
            }
            return (int)this.CostCenterAdapter.QueryDataCount("CostCenter", queryExpression);
        }
        #endregion

        #region Province  Operate

        public MasterData.ProvinceRow GetProvinceById(int Id) {
            return this.TAProvince.GetDataByID(Id)[0];
        }

        public MasterData.ProvinceDataTable GetProvinceByName(string Name) {
            return this.TAProvince.GetDataByName(Name);
        }

        #endregion

        #region CityType  Operate

        public MasterData.CityTypeRow GetCityTypeById(int Id) {
            return this.TACityType.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.CityTypeDataTable GetCityTypePaged() {
            return this.TACityType.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCityType(string CityTypeName, bool IsActive) {
            MasterData.CityTypeDataTable CityTypeTab = new MasterData.CityTypeDataTable();
            MasterData.CityTypeRow CityTypeRow = CityTypeTab.NewCityTypeRow();
            CityTypeRow.CityTypeName = CityTypeName;
            CityTypeRow.IsActive = IsActive;
            CityTypeTab.AddCityTypeRow(CityTypeRow);
            this.TACityType.Update(CityTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCityType(int CityTypeID, string CityTypeName, bool IsActive) {
            MasterData.CityTypeDataTable CityTypeTab = this.TACityType.GetDataByID(CityTypeID);
            MasterData.CityTypeRow CityTypeRow = CityTypeTab[0];
            CityTypeRow.CityTypeName = CityTypeName;
            CityTypeRow.IsActive = IsActive;
            this.TACityType.Update(CityTypeRow);
        }

        public int CityTypeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "CityTypeID is not null";
            }
            return (int)this.TACityType.QueryDataCount("CityType", queryExpression);
        }

        #endregion

        #region City  Operate

        public MasterData.CityRow GetCityById(int Id) {
            return this.TACity.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.CityDataTable GetCityPaged(int startRowIndex, int maximumRows, string sortExpression, int CityTypeID) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CityID Desc";
            }
            return this.TACity.GetDataPaged("City ", sortExpression, startRowIndex, maximumRows, "CityTypeID=" + CityTypeID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCity(string CityName, int CityTypeId, bool IsAutoComplete, bool IsActive) {
            MasterData.CityDataTable CityTab = new MasterData.CityDataTable();
            MasterData.CityRow CityRow = CityTab.NewCityRow();
            CityRow.CityName = CityName;
            CityRow.CityTypeID = CityTypeId;
            CityRow.IsAutoComplete = IsAutoComplete;
            CityRow.IsActive = IsActive;
            CityTab.AddCityRow(CityRow);
            this.TACity.Update(CityTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCity(int CityID, string CityName, bool IsAutoComplete, bool IsActive) {
            MasterData.CityDataTable CityTab = this.TACity.GetDataByID(CityID);
            MasterData.CityRow CityRow = CityTab[0];
            CityRow.CityName = CityName;
            CityRow.IsAutoComplete = IsAutoComplete;
            CityRow.IsActive = IsActive;
            this.TACity.Update(CityRow);
        }

        public int CityTotalCount(int CityTypeId) {
            return (int)this.TACity.QueryDataCount("City", "CityTypeID=" + CityTypeId);
        }

        #endregion

        #region Currency  Operate

        public MasterData.CurrencyRow GetCurrencyByID(int ID) {
            return this.TACurrency.GetDataByID(ID)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.CurrencyDataTable GetCurrency() {

            return this.TACurrency.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCurrency(string CurrencyFullName, string CurrencyShortName, string CurrencySymbol, bool IsActive) {
            MasterData.CurrencyDataTable tbCurrency = new MasterData.CurrencyDataTable();
            MasterData.CurrencyRow rowCurrency = tbCurrency.NewCurrencyRow();

            rowCurrency.CurrencyShortName = CurrencyShortName;
            rowCurrency.CurrencyFullName = CurrencyFullName;
            rowCurrency.CurrencySymbol = CurrencySymbol;
            rowCurrency.IsActive = IsActive;

            tbCurrency.AddCurrencyRow(rowCurrency);
            this.TACurrency.Update(rowCurrency);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCurrency(int CurrencyID, string CurrencyFullName, string CurrencyShortName, string CurrencySymbol, bool IsActive) {

            MasterData.CurrencyDataTable CurrencyTab = this.TACurrency.GetDataByID(CurrencyID);
            MasterData.CurrencyRow CurrencyRow = CurrencyTab[0];
            CurrencyRow.CurrencyFullName = CurrencyFullName;
            CurrencyRow.CurrencyShortName = CurrencyShortName;
            CurrencyRow.CurrencySymbol = CurrencySymbol;
            CurrencyRow.IsActive = IsActive;
            this.TACurrency.Update(CurrencyRow);
        }

        public decimal GetExchangeRateByPeriod(int CurrencyID, DateTime Period) {
            if (this.TACurrency.GetExchangeRateByPeriod(CurrencyID, Period) == null) {
                return 0;
            } else {
                return (decimal)this.TACurrency.GetExchangeRateByPeriod(CurrencyID, Period);
            }
        }

        #endregion

        #region StaffLevel  Operate
        #endregion

        #region ManageExpenseCategoy  Operate

        public MasterData.ManageExpenseCategoyRow GetManageExpenseCategoyById(int Id) {
            return this.TAManageExpenseCategoy.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.ManageExpenseCategoyDataTable GetManageExpenseCategoy() {

            return this.TAManageExpenseCategoy.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertManageExpenseCategoy(string ManageExpenseCategoyName, int FormTypeID, bool IsActive) {

            int iCount = (int)this.TAManageExpenseCategoy.CheckDataOnly(ManageExpenseCategoyName, 0);
            if (iCount > 0) {
                throw new ApplicationException("费用大类不能重复！");
            }

            MasterData.ManageExpenseCategoyDataTable ManageExpenseTypeTab = new MasterData.ManageExpenseCategoyDataTable();
            MasterData.ManageExpenseCategoyRow ManageExpenseCategoyRow = ManageExpenseTypeTab.NewManageExpenseCategoyRow();
            ManageExpenseCategoyRow.ManageExpenseCategoyName = ManageExpenseCategoyName;
            ManageExpenseCategoyRow.FormTypeID = FormTypeID;
            ManageExpenseCategoyRow.IsActive = IsActive;
            ManageExpenseTypeTab.AddManageExpenseCategoyRow(ManageExpenseCategoyRow);
            this.TAManageExpenseCategoy.Update(ManageExpenseTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateManageExpenseCategoy(int ManageExpenseCategoyID, string ManageExpenseCategoyName, int FormTypeID, bool IsActive) {

            int iCount = (int)this.TAManageExpenseCategoy.CheckDataOnly(ManageExpenseCategoyName, ManageExpenseCategoyID);
            if (iCount > 0) {
                throw new ApplicationException("费用大类不能重复！");
            }
            MasterData.ManageExpenseCategoyDataTable ManageExpenseTypeTab = this.TAManageExpenseCategoy.GetDataByID(ManageExpenseCategoyID);
            MasterData.ManageExpenseCategoyRow ManageExpenseCategoyRow = ManageExpenseTypeTab[0];
            ManageExpenseCategoyRow.ManageExpenseCategoyName = ManageExpenseCategoyName;
            ManageExpenseCategoyRow.FormTypeID = FormTypeID;
            ManageExpenseCategoyRow.IsActive = IsActive;
            this.TAManageExpenseCategoy.Update(ManageExpenseCategoyRow);
        }
        #endregion

        #region ManageExpenseItem  Operate

        public MasterData.ManageExpenseItemRow GetManageExpenseItemById(int Id) {
            return this.TAManageExpenseItem.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public MasterData.ManageExpenseItemDataTable GetManageExpenseItemPaged(int startRowIndex, int maximumRows, string sortExpression, int ManageExpenseCategoyID) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ManageExpenseItemID Desc";
            }
            return this.TAManageExpenseItem.GetDataPaged("ManageExpenseItem ", sortExpression, startRowIndex, maximumRows, "ManageExpenseCategoyID=" + ManageExpenseCategoyID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertManageExpenseItem(string ManageExpenseItemName, int ManageExpenseCategoyID, string AccountingCode, string AccountingName, bool IsActive, bool IsTicket) {
            int iCount = (int)this.TAManageExpenseItem.CheckDataOnly(ManageExpenseItemName, ManageExpenseCategoyID, 0);
            if (iCount > 0) {
                throw new ApplicationException("同一费用大类下，费用项不能重复！");
            }
            this.TAManageExpenseItem.Insert(ManageExpenseItemName, AccountingCode, AccountingName, ManageExpenseCategoyID, IsActive, IsTicket);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateManageExpenseItem(int ManageExpenseItemID, int ManageExpenseCategoyID, string ManageExpenseItemName, string AccountingCode, string AccountingName, bool IsActive, bool IsTicket) {
            int iCount = (int)this.TAManageExpenseItem.CheckDataOnly(ManageExpenseItemName, ManageExpenseCategoyID, ManageExpenseItemID);
            if (iCount > 0) {
                throw new ApplicationException("同一费用大类下，费用项不能重复！");
            }
            MasterData.ManageExpenseItemDataTable ManageExpenseItemTab = this.TAManageExpenseItem.GetDataByID(ManageExpenseItemID);
            MasterData.ManageExpenseItemRow ManageExpenseItemRow = ManageExpenseItemTab[0];
            ManageExpenseItemRow.ManageExpenseItemName = ManageExpenseItemName;
            ManageExpenseItemRow.AccountingCode = AccountingCode;
            ManageExpenseItemRow.AccountingName = AccountingName;
            ManageExpenseItemRow.IsTicket = IsTicket;
            ManageExpenseItemRow.IsActive = IsActive;
            this.TAManageExpenseItem.Update(ManageExpenseItemRow);
        }

        public int ManageExpenseItemTotalCount(int ManageExpenseCategoyID) {
            return (int)this.TAManageExpenseItem.QueryDataCount("ManageExpenseItem", "ManageExpenseCategoyID=" + ManageExpenseCategoyID);
        }

        #endregion

        #region ManageExpenseAccounting  Operate

        public MasterData.ManageExpenseAccountingRow GetManageExpenseAccountingByID(int ManageExpenseAccountingID) {
            return this.TAManageExpenseAccounting.GetDataByID(ManageExpenseAccountingID)[0];
        }

        public MasterData.ManageExpenseAccountingDataTable GetManageExpenseAccountingByManageExpenseItemID(int ManageExpenseItemID) {
            return this.TAManageExpenseAccounting.GetDataByManageExpenseItemID(ManageExpenseItemID);
        }

        public void InsertManageExpenseAccounting(int ManageExpenseItemID, int CostCenterID, string AccountingCode, string AccountingName) {
            this.TAManageExpenseAccounting.Insert(ManageExpenseItemID, CostCenterID, AccountingCode, AccountingName);
        }

        public void UpdateManageExpenseAccounting(int ManageExpenseAccountingID, int CostCenterID, string AccountingCode, string AccountingName) {
            MasterData.ManageExpenseAccountingRow ManageExpenseAccountingRow = this.TAManageExpenseAccounting.GetDataByID(ManageExpenseAccountingID)[0];
            ManageExpenseAccountingRow.CostCenterID = CostCenterID;
            ManageExpenseAccountingRow.AccountingCode = AccountingCode;
            ManageExpenseAccountingRow.AccountingName = AccountingName;
            this.TAManageExpenseAccounting.Update(ManageExpenseAccountingRow);
        }

        public void DeleteManageExpenseAccountingByID(int ManageExpenseAccountingID) {
            this.TAManageExpenseAccounting.DeleteByID(ManageExpenseAccountingID);
        }

        public string GetAccountingCodeByExpenseItemAndCostCenter(int ManageExpenseItemID, int CostCenterID) {
            //返回空值，代表没有找到
            return this.TAManageExpenseAccounting.GetAccountingCodeByExpenseItemAndCostCenter(ManageExpenseItemID, CostCenterID);
        }

        #endregion

        #region CostLimit  Operate

        public MasterData.CostLimitRow GetCostLimitById(int Id) {
            return this.TACostLimit.GetDataByID(Id)[0];
        }

        public MasterData.CostLimitDataTable GetPagedCostLimit(int startRowIndex, int maximumRows, string sortExpression, string queryExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CostLimitID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "1=1";
            }
            return this.TACostLimit.GetDataPaged("CostLimit", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryCostLimitCount(string queryExpression) {
            return (int)this.TACostLimit.QueryDataCount("CostLimit", queryExpression);
        }

        public void InsertCostLimit(int CityTypeID, int StaffLevelID, int ManageExpenseItemID, decimal LimitCost) {
            this.TACostLimit.Insert(CityTypeID, StaffLevelID, ManageExpenseItemID, LimitCost);
        }

        public void UpdateCostLimit(int CostLimitID, int CityTypeID, int StaffLevelID, int ManageExpenseItemID, decimal LimitCost) {
            MasterData.CostLimitDataTable CostLimitTab = this.TACostLimit.GetDataByID(CostLimitID);
            MasterData.CostLimitRow CostLimitRow = CostLimitTab[0];
            CostLimitRow.CityTypeID = CityTypeID;
            CostLimitRow.StaffLevelID = StaffLevelID;
            CostLimitRow.ManageExpenseItemID = ManageExpenseItemID;
            CostLimitRow.LimitCost = LimitCost;
            this.TACostLimit.Update(CostLimitRow);
        }

        public void DeleteCostLimitByID(int CostLimitID) {
            this.TACostLimit.DeleteByID(CostLimitID);
        }

        public decimal GetLimitForOverStandard(int CityTypeID, int StaffLevelID, int ManageExpenseItemID, decimal UnitPrice) {
            decimal? limit = (decimal?)this.TACostLimit.GetLimitForOverStandard(CityTypeID, StaffLevelID, ManageExpenseItemID, UnitPrice);
            if (limit == null) {
                return 0;
            } else {
                return limit.GetValueOrDefault();
            }
        }

        #endregion

        #region ExpenseCategory Operate
        public MasterData.ExpenseCategoryRow GetExpenseCategoryById(int Id) {
            return this.TAExpenseCategory.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.ExpenseCategoryDataTable GetExpenseCategory() {
            return this.TAExpenseCategory.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertExpenseCategory(string ExpenseCategoryName, int BusinessType, bool NeedPO) {

            MasterData.ExpenseCategoryDataTable ExpenseCategoryTab = new MasterData.ExpenseCategoryDataTable();
            MasterData.ExpenseCategoryRow ExpenseCategoryRow = ExpenseCategoryTab.NewExpenseCategoryRow();

            ExpenseCategoryRow.ExpenseCategoryName = ExpenseCategoryName;
            ExpenseCategoryRow.BusinessType = BusinessType;
            ExpenseCategoryRow.NeedPO = NeedPO;

            ExpenseCategoryTab.AddExpenseCategoryRow(ExpenseCategoryRow);
            this.TAExpenseCategory.Update(ExpenseCategoryTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateExpenseCategory(int ExpenseCategoryID, string ExpenseCategoryName, int BusinessType, bool NeedPO) {

            MasterData.ExpenseCategoryDataTable ExpenseCategoryTab = this.TAExpenseCategory.GetDataByID(ExpenseCategoryID);
            MasterData.ExpenseCategoryRow ExpenseCategoryRow = ExpenseCategoryTab[0];

            ExpenseCategoryRow.BusinessType = BusinessType;
            ExpenseCategoryRow.ExpenseCategoryName = ExpenseCategoryName;
            ExpenseCategoryRow.NeedPO = NeedPO;

            this.TAExpenseCategory.Update(ExpenseCategoryRow);
        }

        #endregion

        #region ExpenseSubCategory Operate
        public MasterData.ExpenseSubCategoryRow GetExpenseSubCategoryById(int Id) {
            return this.TAExpenseSubCategory.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.ExpenseSubCategoryDataTable GetPagedExpenseSubCategory(int startRowIndex, int maximumRows, string sortExpression, int ExpenseCategoryID) {

            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ExpenseSubCategoryID Desc";
            }
            return this.TAExpenseSubCategory.GetDataPaged("ExpenseSubCategory ", sortExpression, startRowIndex, maximumRows, "ExpenseCategoryID=" + ExpenseCategoryID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertExpenseSubCategory(int ExpenseCategoryID, string ExpenseSubCategoryName, int PageType, bool IsActive) {

            MasterData.ExpenseSubCategoryDataTable ExpenseSubCategoryTab = new MasterData.ExpenseSubCategoryDataTable();
            MasterData.ExpenseSubCategoryRow ExpenseSubCategoryRow = ExpenseSubCategoryTab.NewExpenseSubCategoryRow();

            ExpenseSubCategoryRow.ExpenseCategoryID = ExpenseCategoryID;
            ExpenseSubCategoryRow.ExpenseSubCategoryName = ExpenseSubCategoryName;
            ExpenseSubCategoryRow.PageType = PageType;
            ExpenseSubCategoryRow.IsActive = IsActive;

            ExpenseSubCategoryTab.AddExpenseSubCategoryRow(ExpenseSubCategoryRow);
            this.TAExpenseSubCategory.Update(ExpenseSubCategoryTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateExpenseSubCategory(int ExpenseSubCategoryID, string ExpenseSubCategoryName, int PageType, bool IsActive) {

            MasterData.ExpenseSubCategoryDataTable ExpenseSubCategoryTab = this.TAExpenseSubCategory.GetDataByID(ExpenseSubCategoryID);
            MasterData.ExpenseSubCategoryRow ExpenseSubCategoryRow = ExpenseSubCategoryTab[0];

            ExpenseSubCategoryRow.ExpenseSubCategoryName = ExpenseSubCategoryName;
            ExpenseSubCategoryRow.PageType = PageType;
            ExpenseSubCategoryRow.IsActive = IsActive;

            this.TAExpenseSubCategory.Update(ExpenseSubCategoryRow);
        }

        public int ExpenseSubCategoryTotalCount(int ExpenseCategoryID) {
            return (int)this.TAExpenseSubCategory.QueryDataCount("ExpenseSubCategory", "ExpenseCategoryID=" + ExpenseCategoryID);
        }

        #endregion

        #region ExpenseItem Operate
        public MasterData.ExpenseItemRow GetExpenseItemById(int Id) {
            return this.TAExpenseItem.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public MasterData.ExpenseItemDataTable GetPagedExpenseItem(int startRowIndex, int maximumRows, string sortExpression, String queryExpression) {
            return this.TAExpenseItem.GetDataPaged("ExpenseItem", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertExpenseItem(int ExpenseSubCategoryID, string AccountingCode, String ExpenseItemName,
            bool NeedShopInfo, bool IsPriceDiscount, bool IsActive, string LastAccountingCode, string AccrualAccountingCode) {

            MasterData.ExpenseItemDataTable ExpenseItemTab = new MasterData.ExpenseItemDataTable();
            MasterData.ExpenseItemRow ExpenseItemRow = ExpenseItemTab.NewExpenseItemRow();

            ExpenseItemRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
            ExpenseItemRow.AccountingCode = AccountingCode;
            ExpenseItemRow.ExpenseItemName = ExpenseItemName;
            ExpenseItemRow.NeedShopInfo = NeedShopInfo;
            ExpenseItemRow.IsPriceDiscount = IsPriceDiscount;
            ExpenseItemRow.IsActive = IsActive;
            ExpenseItemRow.LastAccountingCode = LastAccountingCode;
            ExpenseItemRow.AccrualAccountingCode = AccrualAccountingCode;

            ExpenseItemTab.AddExpenseItemRow(ExpenseItemRow);
            this.TAExpenseItem.Update(ExpenseItemTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateExpenseItem(int ExpenseItemID, int ExpenseSubCategoryID, string AccountingCode, String ExpenseItemName,
            bool NeedShopInfo, bool IsPriceDiscount, bool IsActive, string LastAccountingCode, string AccrualAccountingCode) {

            MasterData.ExpenseItemDataTable ExpenseItemTab = this.TAExpenseItem.GetDataByID(ExpenseItemID);
            MasterData.ExpenseItemRow ExpenseItemRow = ExpenseItemTab[0];

            ExpenseItemRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
            ExpenseItemRow.AccountingCode = AccountingCode;
            ExpenseItemRow.ExpenseItemName = ExpenseItemName;
            ExpenseItemRow.NeedShopInfo = NeedShopInfo;
            ExpenseItemRow.IsPriceDiscount = IsPriceDiscount;
            ExpenseItemRow.IsActive = IsActive;
            ExpenseItemRow.LastAccountingCode = LastAccountingCode;
            ExpenseItemRow.AccrualAccountingCode = AccrualAccountingCode;

            this.TAExpenseItem.Update(ExpenseItemRow);
        }

        public int ExpenseItemTotalCount(string queryExpression) {
            return (int)this.TAExpenseItem.QueryDataCount("ExpenseItem", queryExpression);
        }
        #endregion

        #region MarketingProject Operate
        public MasterData.MarketingProjectRow GetMarketingProjectById(int Id) {
            return this.TAMarketingProject.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public MasterData.MarketingProjectDataTable GetPagedMarketingProject(int startRowIndex, int maximumRows, string sortExpression, String queryExpression) {
            return this.TAMarketingProject.GetDataPaged("MarketingProject", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertMarketingProject(string MarketingProjectName, DateTime BeginDate, DateTime EndDate, bool IsActive) {

            MasterData.MarketingProjectDataTable MarketingProjectTab = new MasterData.MarketingProjectDataTable();
            MasterData.MarketingProjectRow MarketingProjectRow = MarketingProjectTab.NewMarketingProjectRow();

            MarketingProjectRow.MarketingProjectName = MarketingProjectName;
            MarketingProjectRow.BeginDate = BeginDate;
            MarketingProjectRow.EndDate = EndDate;
            MarketingProjectRow.IsActive = IsActive;

            MarketingProjectTab.AddMarketingProjectRow(MarketingProjectRow);
            this.TAMarketingProject.Update(MarketingProjectTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateMarketingProject(int MarketingProjectID, string MarketingProjectName, DateTime BeginDate, DateTime EndDate, bool IsActive) {

            MasterData.MarketingProjectDataTable MarketingProjectTab = this.TAMarketingProject.GetDataByID(MarketingProjectID);
            MasterData.MarketingProjectRow MarketingProjectRow = MarketingProjectTab[0];

            MarketingProjectRow.MarketingProjectName = MarketingProjectName;
            MarketingProjectRow.BeginDate = BeginDate;
            MarketingProjectRow.EndDate = EndDate;
            MarketingProjectRow.IsActive = IsActive;

            this.TAMarketingProject.Update(MarketingProjectRow);
        }

        public int MarketingProjectTotalCount(string queryExpression) {
            return (int)this.TAMarketingProject.QueryDataCount("MarketingProject", queryExpression);
        }
        #endregion

        #region DisplayType Operate
        public MasterData.DisplayTypeRow GetDisplayTypeById(int Id) {
            return this.TADisplayType.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.DisplayTypeDataTable GetPagedDisplayType(int startRowIndex, int maximumRows, string sortExpression, String queryExpression) {
            return this.TADisplayType.GetDataPaged("DisplayType", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertDisplayType(String DisplayTypeName, bool IsActive) {

            MasterData.DisplayTypeDataTable DisplayTypeTab = new MasterData.DisplayTypeDataTable();
            MasterData.DisplayTypeRow DisplayTypeRow = DisplayTypeTab.NewDisplayTypeRow();
            DisplayTypeRow.DisplayTypeName = DisplayTypeName;
            DisplayTypeRow.IsActive = IsActive;
            DisplayTypeTab.AddDisplayTypeRow(DisplayTypeRow);
            this.TADisplayType.Update(DisplayTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateDisplayType(int DisplayTypeID, String DisplayTypeName, bool IsActive) {

            MasterData.DisplayTypeDataTable DisplayTypeTab = this.TADisplayType.GetDataByID(DisplayTypeID);
            MasterData.DisplayTypeRow DisplayTypeRow = DisplayTypeTab[0];
            DisplayTypeRow.DisplayTypeName = DisplayTypeName;
            DisplayTypeRow.IsActive = IsActive;
            this.TADisplayType.Update(DisplayTypeRow);
        }

        public int DisplayTypeTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "DisplayTypeID is not null";
            }
            return (int)this.TADisplayType.QueryDataCount("DisplayType", queryExpression);
        }
        #endregion

        #region DiscountType Operate
        public MasterData.DiscountTypeRow GetDiscountTypeById(int Id) {
            return this.TADiscountType.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.DiscountTypeDataTable GetPagedDiscountType(int startRowIndex, int maximumRows, string sortExpression, String queryExpression) {
            return this.TADiscountType.GetDataPaged("DiscountType", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertDiscountType(String DiscountTypeName, bool IsActive) {

            MasterData.DiscountTypeDataTable DiscountTypeTab = new MasterData.DiscountTypeDataTable();
            MasterData.DiscountTypeRow DiscountTypeRow = DiscountTypeTab.NewDiscountTypeRow();

            DiscountTypeRow.DiscountTypeName = DiscountTypeName;
            DiscountTypeRow.IsActive = IsActive;

            DiscountTypeTab.AddDiscountTypeRow(DiscountTypeRow);
            this.TADiscountType.Update(DiscountTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateDiscountType(int DiscountTypeID, String DiscountTypeName, bool IsActive) {

            MasterData.DiscountTypeDataTable DiscountTypeTab = this.TADiscountType.GetDataByID(DiscountTypeID);
            MasterData.DiscountTypeRow DiscountTypeRow = DiscountTypeTab[0];

            DiscountTypeRow.DiscountTypeName = DiscountTypeName;
            DiscountTypeRow.IsActive = IsActive;

            this.TADiscountType.Update(DiscountTypeRow);
        }

        public int DiscountTypeTotalCount(string queryExpression) {
            return (int)this.TADiscountType.QueryDataCount("DiscountType", queryExpression);
        }
        #endregion

        #region PeriodSale Operate

        public bool PeriodSaleExists() {
            MasterData.PeriodSaleDataTable tbPeriodSale = this.TAPeriodSale.GetData();
            if (tbPeriodSale != null && tbPeriodSale.Count > 0) {
                return true;
            } else {
                return false;
            }
        }

        public MasterData.PeriodSaleRow GetPeriodSaleById(int Id) {
            return this.TAPeriodSale.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.PeriodSaleDataTable GetPeriodSale() {
            return this.TAPeriodSale.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPeriodSale(String PeriodSale) {

            DateTime dtPeriod = DateTime.Parse(PeriodSale.Substring(0, 4) + "-" + PeriodSale.Substring(4, 2) + "-01");

            //验证是否该月份的汇率，若不存在，不能添加
            int count = (int)this.TAExchangeRate.SearchByPeriod(dtPeriod);
            if (0 > count) {
                throw new MyException("该月份的汇率不存在，不能添加月份！", "Exchange rate of this period not exists, Insert falied!");
            }
            MasterData.PeriodSaleDataTable PeriodSaleTab = new MasterData.PeriodSaleDataTable();
            MasterData.PeriodSaleRow PeriodSaleRow = PeriodSaleTab.NewPeriodSaleRow();
            PeriodSaleRow.PeriodSale = dtPeriod;
            PeriodSaleTab.AddPeriodSaleRow(PeriodSaleRow);
            this.TAPeriodSale.Update(PeriodSaleTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdatePeriodSale(int PeriodSaleID, String PeriodSale) {

            DateTime dtPeriod = DateTime.Parse(PeriodSale.Substring(0, 4) + "-" + PeriodSale.Substring(4, 2) + "-01");

            //验证是否该月份的汇率，若不存在，不能修改
            int count = (int)this.TAExchangeRate.SearchByPeriod(dtPeriod);
            if (0 >= count) {
                throw new MyException("该月份的汇率不完整，不能修改月份！", "Exchange rate of this period not exists, Modify falied!");
            }
            MasterData.PeriodSaleDataTable PeriodSaleTab = this.TAPeriodSale.GetDataByID(PeriodSaleID);
            MasterData.PeriodSaleRow PeriodSaleRow = PeriodSaleTab[0];

            PeriodSaleRow.PeriodSale = dtPeriod;

            this.TAPeriodSale.Update(PeriodSaleRow);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
        public void DeletePeriodSaleByID(int PeriodSaleID) {
            this.TAPeriodSale.DeleteByID(PeriodSaleID);
        }

        public bool IsValidPeriodSale(DateTime Period) {
            int count = (int)this.TAPeriodSale.QueryCountByPeriod(Period);
            if (count == 0) {
                return false;
            } else {
                return true;
            }
        }

        #endregion

        #region PeriodReimburse Operate

        public MasterData.PeriodReimburseRow GetPeriodReimburseById(int PeriodReimburseID) {
            return TAPeriodReimburse.GetDataByID(PeriodReimburseID)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.PeriodReimburseDataTable GetPeriodReimburse() {
            return this.TAPeriodReimburse.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPeriodReimburse(DateTime PeriodReimburse) {
            //验证是否该月份的汇率，若不存在，不能添加
            int count = (int)this.TAExchangeRate.SearchByPeriod(PeriodReimburse);
            if (0 > count) {
                throw new MyException("该月份的汇率不存在，不能添加月份！", "Exchange rate of this period not exists, Insert falied!");
            }
            this.TAPeriodReimburse.Insert(PeriodReimburse);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool DeletePeriodReimburseById(int PeriodReimburseId) {

            int rowsAffected = 0;
            try {
                rowsAffected = this.TAPeriodReimburse.DeleteByID(PeriodReimburseId);
            } catch (Exception e) {
                throw e;
            }
            return rowsAffected == 1;
        }

        #endregion

        #region PeriodPurchase Operate

        public MasterData.PeriodPurchaseRow GetPeriodPurchaseById(int Id) {
            return this.TAPeriodPurchase.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.PeriodPurchaseDataTable GetPeriodPurchase() {
            return this.TAPeriodPurchase.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPeriodPurchase(DateTime PeriodPurchase) {
            //验证是否该月份的汇率，若不存在，不能添加
            int count = (int)this.TAExchangeRate.SearchByPeriod(PeriodPurchase);
            if (0 > count) {
                throw new MyException("该月份的汇率不存在，不能添加月份！", "Exchange rate of this period not exists, Insert falied!");
            }
            this.TAPeriodPurchase.Insert(PeriodPurchase);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool DeletePeriodPurchaseById(int PeriodPurchaseId) {
            int rowsAffected = 0;
            try {
                rowsAffected = this.TAPeriodPurchase.DeleteByID(PeriodPurchaseId);
            } catch (Exception e) {
                throw e;
            }
            return rowsAffected == 1;
        }

        public bool IsValidPeriodPurchase(DateTime Period) {
            int count = (int)this.TAPeriodPurchase.QueryCountByPeriod(Period);
            if (count == 0) {
                return false;
            } else {
                return true;
            }
        }

        #endregion

        #region ExchangeRate  Operate

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.ExchangeRateDataTable GetPagedExchangeRate(int startRowIndex, int maximumRows, string sortExpression, int CurrencyID) {
            return this.TAExchangeRate.GetDataPaged("ExchangeRate", sortExpression, startRowIndex, maximumRows, "CurrencyID=" + CurrencyID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertExchangeRate(int CurrencyID, string Period, decimal Value) {

            MasterData.ExchangeRateDataTable tbExchangeRate = new MasterData.ExchangeRateDataTable();
            MasterData.ExchangeRateRow rowExchangeRate = tbExchangeRate.NewExchangeRateRow();

            rowExchangeRate.CurrencyID = CurrencyID;
            rowExchangeRate.Period = DateTime.Parse(Period.Substring(0, 4) + "-" + Period.Substring(4, 2) + "-01");
            rowExchangeRate.Value = Value;

            tbExchangeRate.AddExchangeRateRow(rowExchangeRate);
            this.TAExchangeRate.Update(rowExchangeRate);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateExchangeRate(int ExchangeRateID, int CurrencyID, string Period, decimal Value) {

            MasterData.ExchangeRateDataTable ExchangeRateTab = this.TAExchangeRate.GetDataByID(ExchangeRateID);
            MasterData.ExchangeRateRow ExchangeRateRow = ExchangeRateTab[0];

            ExchangeRateRow.CurrencyID = CurrencyID;
            ExchangeRateRow.Period = DateTime.Parse(Period.Substring(0, 4) + "-" + Period.Substring(4, 2) + "-01");
            ExchangeRateRow.Value = Value;

            this.TAExchangeRate.Update(ExchangeRateRow);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public void DeleteByID(int ExchangeRateID) {
            this.TAExchangeRate.DeleteByID(ExchangeRateID);
        }

        public int ExchangeRateTotalCount(int CurrencyID) {
            return (int)this.TADiscountType.QueryDataCount("ExchangeRate", "CurrencyID=" + CurrencyID);
        }

        #endregion

        #region ItemCategory Operate

        public MasterData.ItemCategoryRow GetItemCategoryById(int Id) {
            return this.TAItemCategory.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.ItemCategoryDataTable GetItemCategory() {
            return this.TAItemCategory.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertItemCategory(string ItemCategoryCode, string ItemCategoryName, string Remark, string AccountingCode, string AccountingName) {
            this.TAItemCategory.Insert(ItemCategoryCode, ItemCategoryName, AccountingCode, AccountingName, Remark);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateItemCategory(int ItemCategoryID, string ItemCategoryCode, string ItemCategoryName, string Remark, string AccountingCode, string AccountingName) {
            MasterData.ItemCategoryDataTable ItemCategoryTab = this.TAItemCategory.GetDataByID(ItemCategoryID);
            MasterData.ItemCategoryRow ItemCategoryRow = ItemCategoryTab[0];
            ItemCategoryRow.ItemCategoryCode = ItemCategoryCode;
            ItemCategoryRow.ItemCategoryName = ItemCategoryName;
            ItemCategoryRow.Remark = Remark;
            ItemCategoryRow.AccountingCode = AccountingCode;
            ItemCategoryRow.AccountingName = AccountingName;
            this.TAItemCategory.Update(ItemCategoryRow);
        }

        public MasterData.ItemCategoryDataTable GetItemCategoryByCode(string ItemCategoryCode) {
            return this.TAItemCategory.GetDataByCode(ItemCategoryCode);
        }

        public MasterData.ItemCategoryDataTable GetItemCategoryByName(string ItemCategoryName) {
            return this.TAItemCategory.GetDataByName(ItemCategoryName);
        }

        #endregion

        #region Item Operate

        public MasterData.ItemRow GetItemById(int Id) {
            return this.TAItem.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.ItemDataTable GetItemPaged(int startRowIndex, int maximumRows, string sortExpression, string queryExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ItemID Desc";
            }
            return this.TAItem.GetDataPaged("Item", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertItem(string ItemCode, string ItemName, int ItemCategoryID, string UOM, string Description, bool IsActive) {
            this.TAItem.Insert(ItemCode, ItemName, ItemCategoryID, UOM, Description, "", 0, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateItem(int ItemID, string ItemCode, string ItemName, int ItemCategoryID, string UOM, string Description, bool IsActive) {
            MasterData.ItemDataTable ItemTab = this.TAItem.GetDataByID(ItemID);
            MasterData.ItemRow ItemRow = ItemTab[0];
            ItemRow.ItemCode = ItemCode;
            ItemRow.ItemName = ItemName;
            ItemRow.ItemCategoryID = ItemCategoryID;
            ItemRow.UOM = UOM;
            ItemRow.Description = Description;
            ItemRow.IsActive = IsActive;
            this.TAItem.Update(ItemRow);
        }

        public int ItemCount(string queryExpression) {
            return (int)this.TAItem.QueryDataCount("Item", queryExpression);
        }
        public MasterData.ItemDataTable GetItemByCode(string Code) {
            return this.TAItem.GetDataByCode(Code);
        }
        #endregion

        #region GLAccount Operate

        public MasterData.GLAccountRow GetGLAccountById(int Id) {
            return this.TAGLAccount.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.GLAccountDataTable GetGLAccount() {
            return this.TAGLAccount.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertGLAccount(string GLAccountCode, string GLAccountName) {
            this.TAGLAccount.Insert(GLAccountCode, GLAccountName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateGLAccount(int GLAccountID, string GLAccountCode, string GLAccountName) {
            MasterData.GLAccountDataTable GLAccountTab = this.TAGLAccount.GetDataByID(GLAccountID);
            MasterData.GLAccountRow GLAccountRow = GLAccountTab[0];
            GLAccountRow.GLAccountCode = GLAccountCode;
            GLAccountRow.GLAccountName = GLAccountName;

            this.TAGLAccount.Update(GLAccountRow);
        }

        #endregion

        #region VendorType Operate

        public MasterData.VendorTypeRow GetVendorTypeById(int Id) {
            return this.TAVendorType.GetDataByID(Id)[0];
        }

        public MasterData.VendorTypeDataTable GetVendorTypePaged(int startRowIndex, int maximumRows, string sortExpression, string queryExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "VendorTypeID Desc";
            }
            return this.TAVendorType.GetDataPaged("VendorType", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertVendorType(string VendorTypeName, int CompanyID, int CurrencyID, string Description, bool IsActive) {
            this.TAVendorType.Insert(VendorTypeName, CurrencyID, Description, IsActive, CompanyID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateVendorType(int VendorTypeID, string VendorTypeName, int CompanyID, int CurrencyID, bool IsActive) {
            MasterData.VendorTypeDataTable VendorTypeTab = this.TAVendorType.GetDataByID(VendorTypeID);
            MasterData.VendorTypeRow VendorTypeRow = VendorTypeTab[0];
            VendorTypeRow.VendorTypeName = VendorTypeName;
            VendorTypeRow.CurrencyID = CurrencyID;
            VendorTypeRow.CompanyID = CompanyID;
            VendorTypeRow.IsActive = IsActive;

            this.TAVendorType.Update(VendorTypeRow);
        }

        public int VendorTypeCount(string queryExpression) {
            return (int)this.TAVendorType.QueryDataCount("VendorType", queryExpression);
        }

        #endregion

        #region ShippingTerm Operate

        public MasterData.ShippingTermRow GetShippingTermById(int Id) {
            return this.TAShippingTerm.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.ShippingTermDataTable GetShippingTerm() {

            return this.TAShippingTerm.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertShippingTerm(string ShippingTermCode, string ShippingTermName, bool IsActive) {
            this.TAShippingTerm.Insert(ShippingTermCode, ShippingTermName, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateShippingTerm(int ShippingTermID, string ShippingTermCode, string ShippingTermName, bool IsActive) {
            MasterData.ShippingTermDataTable ShippingTermTab = this.TAShippingTerm.GetDataByID(ShippingTermID);
            MasterData.ShippingTermRow ShippingTermeRow = ShippingTermTab[0];
            ShippingTermeRow.ShippingTermCode = ShippingTermCode;
            ShippingTermeRow.ShippingTermName = ShippingTermName;

            ShippingTermeRow.IsActive = IsActive;

            this.TAShippingTerm.Update(ShippingTermeRow);
        }
        #endregion

        #region PurchaseBudgetType Operate

        public MasterData.PurchaseBudgetTypeRow GetPurchaseBudgetTypeById(int Id) {
            return this.TAPurchaseBudgetType.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.PurchaseBudgetTypeDataTable GetPurchaseBudgetType() {
            return this.TAPurchaseBudgetType.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPurchaseBudgetType(string PurchaseBudgetTypeName) {
            this.TAPurchaseBudgetType.Insert(PurchaseBudgetTypeName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdatePurchaseBudgetType(int PurchaseBudgetTypeID, string PurchaseBudgetTypeName) {
            MasterData.PurchaseBudgetTypeDataTable PurchaseBudgetTypeTab = this.TAPurchaseBudgetType.GetDataByID(PurchaseBudgetTypeID);
            MasterData.PurchaseBudgetTypeRow PurchaseBudgetTypeRow = PurchaseBudgetTypeTab[0];
            PurchaseBudgetTypeRow.PurchaseBudgetTypeName = PurchaseBudgetTypeName;
            this.TAPurchaseBudgetType.Update(PurchaseBudgetTypeRow);
        }

        #endregion

        #region PurchaseType Operate

        public MasterData.PurchaseTypeRow GetPurchaseTypeById(int Id) {
            return this.TAPurchaseType.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.PurchaseTypeDataTable GetPurchaseType() {
            return this.TAPurchaseType.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPurchaseType(string PurchaseTypeName) {
            this.TAPurchaseType.Insert(PurchaseTypeName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdatePurchaseType(int PurchaseTypeID, string PurchaseTypeName) {
            MasterData.PurchaseTypeDataTable PurchaseTypeTab = this.TAPurchaseType.GetDataByID(PurchaseTypeID);
            MasterData.PurchaseTypeRow PurchaseTypeRow = PurchaseTypeTab[0];
            PurchaseTypeRow.PurchaseTypeName = PurchaseTypeName;
            this.TAPurchaseType.Update(PurchaseTypeRow);
        }

        #endregion

        #region InvoiceStatus Operate

        public MasterData.InvoiceStatusRow GetInvoiceStatusById(int Id) {
            return this.TAInvoiceStatus.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.InvoiceStatusDataTable GetInvoiceStatus() {
            return this.TAInvoiceStatus.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertInvoiceStatus(string Name, bool NeedInvoice) {
            this.TAInvoiceStatus.Insert(Name, NeedInvoice);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateInvoiceStatus(int InvoiceStatusID, string Name, bool NeedInvoice) {
            MasterData.InvoiceStatusDataTable InvoiceStatusTab = this.TAInvoiceStatus.GetDataByID(InvoiceStatusID);
            MasterData.InvoiceStatusRow InvoiceStatusRow = InvoiceStatusTab[0];
            InvoiceStatusRow.Name = Name;
            InvoiceStatusRow.NeedInvoice = NeedInvoice;
            this.TAInvoiceStatus.Update(InvoiceStatusRow);
        }

        #endregion

        #region Company Operate

        public MasterData.CompanyRow GetCompanyById(int Id) {
            return this.TACompany.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.CompanyDataTable GetCompany() {

            return this.TACompany.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCompany(string CompanyCode, string CompanyName, string CompanyShortName, int BeginSequenceNo, int EndSequenceNo, string CompanyAddress) {
            this.TACompany.Insert(CompanyCode, CompanyName, CompanyShortName, BeginSequenceNo, EndSequenceNo, 1, CompanyAddress);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCompany(int CompanyID, string CompanyCode, string CompanyName, string CompanyShortName, int BeginSequenceNo, int EndSequenceNo, string CompanyAddress) {
            MasterData.CompanyDataTable CompanyTab = this.TACompany.GetDataByID(CompanyID);
            MasterData.CompanyRow CompanyRow = CompanyTab[0];
            CompanyRow.CompanyCode = CompanyCode;
            CompanyRow.CompanyName = CompanyName;
            CompanyRow.CompanyShortName = CompanyShortName;
            CompanyRow.BeginSequenceNo = BeginSequenceNo;
            CompanyRow.EndSequenceNo = EndSequenceNo;
            CompanyRow.CompanyAddress = CompanyAddress;
            this.TACompany.Update(CompanyRow);
        }

        #endregion

        #region CustomerType Operate

        public MasterData.CustomerTypeRow GetCustomerTypeById(int Id) {
            return this.TACustomerType.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.CustomerTypeDataTable GetCustomerType() {

            return this.TACustomerType.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCustomerType(string CustomerTypeName, bool IsActive) {
            this.TACustomerType.Insert(CustomerTypeName, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCustomerType(int CustomerTypeID, string CustomerTypeName, bool IsActive) {
            MasterData.CustomerTypeDataTable CustomerTypeTab = this.TACustomerType.GetDataByID(CustomerTypeID);
            MasterData.CustomerTypeRow CustomerTypeRow = CustomerTypeTab[0];
            CustomerTypeRow.CustomerTypeName = CustomerTypeName;
            CustomerTypeRow.IsActive = IsActive;
            this.TACustomerType.Update(CustomerTypeRow);
        }
        public MasterData.CustomerTypeDataTable GetCustomerTypeByName(string Name) {
            return this.TACustomerType.GetDataByName(Name);
        }
        #endregion

        #region CustomerChannel Operate

        public MasterData.CustomerChannelDataTable GetCustomerChannelById(int CustomerChannelID) {
            return this.TACustomerChannel.GetDataByID(CustomerChannelID);
        }

        public MasterData.CustomerChannelDataTable GetCustomerChannel() {
            return this.TACustomerChannel.GetData();
        }

        public void InsertCustomerChannel(string CustomerChannelCode, string CustomerChannelName, bool IsActive) {
            this.TACustomerChannel.Insert(CustomerChannelName, IsActive, CustomerChannelCode);
        }

        public void UpdateCustomerChannel(int CustomerChannelID, string CustomerChannelCode, string CustomerChannelName, bool IsActive) {
            MasterData.CustomerChannelDataTable CustomerChannelTab = this.TACustomerChannel.GetDataByID(CustomerChannelID);
            MasterData.CustomerChannelRow CustomerChannelRow = CustomerChannelTab[0];
            CustomerChannelRow.CustomerChannelName = CustomerChannelName;
            CustomerChannelRow.IsActive = IsActive;
            CustomerChannelRow.CustomerChannelCode = CustomerChannelCode;
            this.TACustomerChannel.Update(CustomerChannelRow);
        }

        public MasterData.CustomerChannelDataTable GetCustomerChanneByCode(string Code) {
            return this.TACustomerChannel.GetDataByCode(Code);
        }

        #endregion

        #region CustomerRegion Operate

        public MasterData.CustomerRegionRow GetCustomerRegionById(int Id) {
            return this.TACustomerRegion.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.CustomerRegionDataTable GetCustomerRegion() {

            return this.TACustomerRegion.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCustomerRegion(string CustomerRegionName) {
            this.TACustomerRegion.Insert(CustomerRegionName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCustomerRegion(int CustomerRegionID, string CustomerRegionName) {
            MasterData.CustomerRegionDataTable CustomerRegionTab = this.TACustomerRegion.GetDataByID(CustomerRegionID);
            MasterData.CustomerRegionRow CustomerRegionRow = CustomerRegionTab[0];
            CustomerRegionRow.CustomerRegionName = CustomerRegionName;
            this.TACustomerRegion.Update(CustomerRegionRow);
        }
        public MasterData.CustomerRegionDataTable GetCustomerRegionByName(string Name) {
            return this.TACustomerRegion.GetDataByName(Name);
        }

        #endregion

        #region Customer Operate

        public MasterData.CustomerDataTable GetCustomerById(int CustomerID) {
            return this.TACustomer.GetDataByID(CustomerID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.CustomerDataTable GetCustomerPaged(int startRowIndex, int maximumRows, string sortExpression, string queryExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CustomerID Desc";
            }
            return this.TACustomer.GetDataPaged("Customer", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCustomer(string CustomerNo, string CustomerName, string Address, string Tel, string ResponsiblePerson, string Contactor, int CustomerTypeID, int CustomerChannelID, string City, int ProvinceID, int CustomerRegionID, string PostCode, string AccountCode, string DealerName, string Remark1, string Remark2, string Remark3, bool IsActive, string KaType) {

            int iCount = (int)this.TACustomer.CheckCustomerNo(CustomerNo, 0);
            if (iCount > 0) {
                throw new ApplicationException("客户编号重复");
            }
            this.TACustomer.Insert(CustomerNo, CustomerName, Address, Tel, ResponsiblePerson, Contactor, CustomerTypeID, CustomerChannelID, City, ProvinceID, CustomerRegionID, PostCode, AccountCode, DealerName, Remark1, Remark2, Remark3, IsActive, KaType);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCustomer(int CustomerID, string CustomerNo, string CustomerName, string Address, string Tel, string ResponsiblePerson, string Contactor, int CustomerTypeID, int CustomerChannelID, string City, int ProvinceID, int CustomerRegionID, string PostCode, string AccountCode, string DealerName, string Remark1, string Remark2, string Remark3, bool IsActive, string KaType) {
            int iCount = (int)this.TACustomer.CheckCustomerNo(CustomerNo, CustomerID);
            if (iCount > 0) {
                throw new ApplicationException("客户编号重复");
            }

            MasterData.CustomerDataTable CustomerTab = this.TACustomer.GetDataByID(CustomerID);
            MasterData.CustomerRow CustomerRow = CustomerTab[0];
            CustomerRow.CustomerNo = CustomerNo;
            CustomerRow.CustomerName = CustomerName;
            CustomerRow.Address = Address;
            CustomerRow.Tel = Tel;
            CustomerRow.ResponsiblePerson = ResponsiblePerson;
            CustomerRow.Contactor = Contactor;
            CustomerRow.CustomerTypeID = CustomerTypeID;
            CustomerRow.City = City;
            CustomerRow.ProvinceID = ProvinceID;
            CustomerRow.CustomerRegionID = CustomerRegionID;
            CustomerRow.PostCode = PostCode;
            CustomerRow.AccountCode = AccountCode;
            CustomerRow.DealerName = DealerName;
            CustomerRow.Remark1 = Remark1;
            CustomerRow.Remark2 = Remark2;
            CustomerRow.Remark3 = Remark3;
            CustomerRow.IsActive = IsActive;
            CustomerRow.KaType = KaType;
            this.TACustomer.Update(CustomerRow);
        }

        public int CustomerCount(string queryExpression) {
            return (int)this.TACustomer.QueryDataCount("Customer", queryExpression);
        }

        public MasterData.CustomerDataTable GetCustomerByNo(string No) {
            return this.TACustomer.GetDataByNo(No);
        }
        #endregion

        #region Brand Operate

        public MasterData.BrandDataTable GetBrandById(int BrandID) {
            return this.TABrand.GetDataByID(BrandID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.BrandDataTable GetBrand() {

            return this.TABrand.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertBrand(string BrandNo, string BrandName) {
            this.TABrand.Insert(BrandNo, BrandName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateBrand(int BrandID, string BrandNo, string BrandName) {
            MasterData.BrandDataTable BrandTab = this.TABrand.GetDataByID(BrandID);
            MasterData.BrandRow BrandRow = BrandTab[0];
            BrandRow.BrandNo = BrandNo;
            BrandRow.BrandName = BrandName;
            this.TABrand.Update(BrandRow);
        }
        public MasterData.BrandDataTable GetBrandByName(string Name) {
            return this.TABrand.GetDataByName(Name);
        }
        #endregion

        #region SKUCategory Operate

        public MasterData.SKUCategoryDataTable GetSKUCategoryById(int BrandID) {
            return this.TASKUCategory.GetDataByID(BrandID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.SKUCategoryDataTable GetSKUCategory() {
            return this.TASKUCategory.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertSKUCategory(string SKUCategoryName) {
            this.TASKUCategory.Insert(SKUCategoryName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateSKUCategory(int SKUCategoryID, string SKUCategoryName) {
            MasterData.SKUCategoryDataTable SKUCategoryTab = this.TASKUCategory.GetDataByID(SKUCategoryID);
            MasterData.SKUCategoryRow SKUCategoryRow = SKUCategoryTab[0];
            SKUCategoryRow.SKUCategoryName = SKUCategoryName;
            this.TASKUCategory.Update(SKUCategoryRow);
        }
        public MasterData.SKUCategoryDataTable GetSKUCategoryByName(string Name) {
            return this.TASKUCategory.GetDataByName(Name);
        }
        #endregion

        #region SKUType Operate

        public MasterData.SKUTypeDataTable GetSKUTypeById(int BrandID) {
            return this.TASKUType.GetDataByID(BrandID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.SKUTypeDataTable GetSKUType() {

            return this.TASKUType.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertSKUType(string SKUTypeName) {
            this.TASKUType.Insert(SKUTypeName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateSKUType(int SKUTypeID, string SKUTypeName) {
            MasterData.SKUTypeDataTable SKUTypeTab = this.TASKUType.GetDataByID(SKUTypeID);
            MasterData.SKUTypeRow SKUTypeRow = SKUTypeTab[0];
            SKUTypeRow.SKUTypeName = SKUTypeName;
            this.TASKUType.Update(SKUTypeRow);
        }
        public MasterData.SKUTypeDataTable GetSKUTypeByName(string Name) {
            return this.TASKUType.GetDataByName(Name);
        }
        #endregion

        #region SKU Operate

        public MasterData.SKUDataTable GetSKUById(int SKUID) {
            return this.TASKU.GetDataByID(SKUID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.SKUDataTable GetSKUPaged(int startRowIndex, int maximumRows, string sortExpression, string queryExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SKUID Desc";
            }
            return this.TASKU.GetDataPaged("SKU", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertSKU(string SKUNo, string SKUName, int BrandID, int SKUTypeID, int SKUCategoryID, string PackPerCase, string Pallet, string Weight, string Volume, string Barcode, decimal StandardCost, string ShelfLife, bool IsActive) {
            MasterData.SKUDataTable tbSKU = new MasterData.SKUDataTable();
            MasterData.SKURow rowSKU = tbSKU.NewSKURow();

            rowSKU.SKUNo = SKUNo;
            rowSKU.SKUName = SKUName;
            rowSKU.BrandID = BrandID;
            if (SKUTypeID > 0) {
                rowSKU.SKUTypeID = SKUTypeID;
            } else {
                rowSKU.SetSKUTypeIDNull();
            }
            rowSKU.SKUCategoryID = SKUCategoryID;
            rowSKU.PackPerCase = PackPerCase;
            rowSKU.Pallet = Pallet;
            rowSKU.Weight = Weight;
            rowSKU.Volume = Volume;
            rowSKU.Barcode = Barcode;
            rowSKU.StandardCost = StandardCost;
            rowSKU.ShelfLife = ShelfLife;
            rowSKU.IsActive = IsActive;

            tbSKU.AddSKURow(rowSKU);
            this.TASKU.Update(rowSKU);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateSKU(int SKUID, string SKUNo, string SKUName, int BrandID, int SKUTypeID, int SKUCategoryID, string PackPerCase, string Pallet, string Weight, string Volume, string Barcode, decimal StandardCost, string ShelfLife, bool IsActive) {
            MasterData.SKUDataTable SKUTab = this.TASKU.GetDataByID(SKUID);
            MasterData.SKURow SKURow = SKUTab[0];
            SKURow.SKUNo = SKUNo;
            SKURow.SKUName = SKUName;
            SKURow.BrandID = BrandID;
            if (SKUTypeID > 0) {
                SKURow.SKUTypeID = SKUTypeID;
            } else {
                SKURow.SetSKUTypeIDNull();
            }
            SKURow.SKUCategoryID = SKUCategoryID;
            SKURow.PackPerCase = PackPerCase;
            SKURow.Pallet = Pallet;
            SKURow.Weight = Weight;
            SKURow.Volume = Volume;
            SKURow.Barcode = Barcode;
            SKURow.StandardCost = StandardCost;
            SKURow.ShelfLife = ShelfLife;
            SKURow.IsActive = IsActive;
            this.TASKU.Update(SKURow);
        }

        public int QuerySKUCount(string queryExpression) {
            return (int)this.TASKU.QueryDataCount("SKU", queryExpression);
        }

        public decimal GetSKUPriceByParameter(int SKUID, int CustomerTypeID, int CustomerChannelID) {
            if (this.TASKU.GetSKUPriceByParameter(SKUID, CustomerTypeID, CustomerChannelID) == null) {
                return 0;
            } else {
                return (decimal)this.TASKU.GetSKUPriceByParameter(SKUID, CustomerTypeID, CustomerChannelID);
            }
        }
        public MasterData.SKUDataTable GetSKUBySKUNo(string SKUNo) {
            return this.TASKU.GetDataBySKUNo(SKUNo);
        }
        #endregion

        #region SKUPrice Operate

        public MasterData.SKUPriceDataTable GetSKUPriceBySKUID(int SKUID) {
            return this.TASKUPrice.GetDataBySKUID(SKUID);
        }

        public MasterData.SKUPriceDataTable GetSKUPriceById(int SKUPriceID) {
            return this.TASKUPrice.GetDataByID(SKUPriceID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.SKUPriceDataTable GetSKUPricePaged(int startRowIndex, int maximumRows, string sortExpression, string queryExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SKUPriceID Desc";
            }
            return this.TASKUPrice.GetDataPaged("SKUPrice", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertSKUPrice(int SKUID, int CustomerTypeID, int CustomerChannelID, decimal DeliveryPrice) {
            this.TASKUPrice.Insert(SKUID, CustomerTypeID, CustomerChannelID, DeliveryPrice);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateSKUPrice(int SKUPriceID, int SKUID, int CustomerTypeID, int CustomerChannelID, decimal DeliveryPrice) {
            MasterData.SKUPriceDataTable SKUPriceTab = this.TASKUPrice.GetDataByID(SKUPriceID);
            MasterData.SKUPriceRow SKUPriceRow = SKUPriceTab[0];
            SKUPriceRow.SKUID = SKUID;
            SKUPriceRow.CustomerTypeID = CustomerTypeID;
            SKUPriceRow.CustomerChannelID = CustomerChannelID;
            SKUPriceRow.DeliveryPrice = DeliveryPrice;

            this.TASKUPrice.Update(SKUPriceRow);
        }

        public int SKUPriceCount(string queryExpression) {
            return (int)this.TASKUPrice.QueryDataCount("SKUPrice", queryExpression);
        }

        #endregion

        #region Vendor

        public MasterData.VendorRow GetVendorByID(int VendorID) {
            return this.TAVendor.GetDataByID(VendorID)[0];
        }

        public MasterData.VendorDataTable GetAllVendor() {
            return this.TAVendor.GetData();
        }

        public MasterData.VendorDataTable GetEnabledVendor() {
            return this.TAVendor.GetEnabledVendor();
        }

        public MasterData.VendorDataTable GetVendorToExport() {
            return this.TAVendor.GetVendorToExport();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.VendorDataTable GetVendorPaged(int startRowIndex, int maximumRows, string sortExpression, string queryExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "VendorID Desc";
            }
            return this.TAVendor.GetDataPaged("Vendor", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public void InsertVendor(int FormVendorID) {

            PurchaseDS.FormVendorRow FormVendorRow = new FormVendorBLL().GetFormVendorByID(FormVendorID)[0];
            MasterData.VendorDataTable tb = new MasterData.VendorDataTable();
            MasterData.VendorRow newVendorRow = tb.NewVendorRow();
            MasterData.VendorTypeRow vendorTypeRow = new MasterDataBLL().GetVendorTypeById(FormVendorRow.VendorTypeID);
            int? Seq = 0;
            TAVendor.GenerateVenderCode(new MasterDataBLL().GetVendorTypeById(FormVendorRow.VendorTypeID).CompanyID, ref Seq);
            newVendorRow.VendorCode = Seq.ToString();
            newVendorRow.VendorName = FormVendorRow.VendorName;
            newVendorRow.VendorAddress = FormVendorRow.VendorAddress;
            newVendorRow.City = FormVendorRow.City;

            newVendorRow.Postal = FormVendorRow.Postal;
            newVendorRow.ContactName = FormVendorRow.ContactName;
            newVendorRow.VendorTypeID = FormVendorRow.VendorTypeID;
            newVendorRow.CompanyID = vendorTypeRow.CompanyID;

            newVendorRow.PhoneNumber = FormVendorRow.PhoneNumber;
            newVendorRow.OneTimeVendor = FormVendorRow.OneTimeVendor;
            newVendorRow.HoldVendor = FormVendorRow.HoldVendor;
            newVendorRow.PurchaseingPostalCode = FormVendorRow.PurchaseingPostalCode;
            newVendorRow.AlphaSearchKey = FormVendorRow.AlphaSearchKey;
            newVendorRow.PurchasingContact = FormVendorRow.PurchasingContact;
            newVendorRow.PurchasingAddress = FormVendorRow.PurchasingAddress;
            newVendorRow.PurchasingCity = FormVendorRow.PurchasingCity;
            newVendorRow.PurchasePhoneNumber = FormVendorRow.PurchasePhoneNumber;
            newVendorRow.BankCode = FormVendorRow.BankCode;
            newVendorRow.MethodPaymentID = FormVendorRow.MethodPaymentID;
            newVendorRow.PaymentTermID = FormVendorRow.PaymentTermID;
            newVendorRow.TransTypeID = FormVendorRow.TransTypeID;
            newVendorRow.VATTypeID = FormVendorRow.VATTypeID;
            newVendorRow.BankName = FormVendorRow.BankName;
            newVendorRow.AccountNo = FormVendorRow.AccountNo;
            newVendorRow.BankNo = FormVendorRow.BankNo;
            newVendorRow.ACTypeID = FormVendorRow.ACTypeID;
            newVendorRow.BankCodeID = FormVendorRow.BankCodeID;
            newVendorRow.IsExport = false;
            if (!FormVendorRow.IsRemarkNull()) {
                newVendorRow.Remark = FormVendorRow.Remark;
            }
            newVendorRow.IsActive = true;
            newVendorRow.ActionType = (int)SystemEnums.VendorActionType.Add;
            tb.AddVendorRow(newVendorRow);
            this.TAVendor.Update(tb);

            FormVendorRow.VendorID = newVendorRow.VendorID;

            new PurchaseDSTableAdapters.FormVendorTableAdapter().Update(FormVendorRow);
        }

        public void UpdateVendor(int FormVendorID) {

            PurchaseDS.FormVendorRow FormVendorRow = new FormVendorBLL().GetFormVendorByID(FormVendorID)[0];
            MasterData.VendorRow VendorRow = this.TAVendor.GetDataByID(FormVendorRow.VendorID)[0];
            MasterData.VendorTypeRow vendorTypeRow = new MasterDataBLL().GetVendorTypeById(FormVendorRow.VendorTypeID);
            VendorRow.VendorName = FormVendorRow.VendorName;
            VendorRow.VendorAddress = FormVendorRow.VendorAddress;
            VendorRow.City = FormVendorRow.City;

            VendorRow.Postal = FormVendorRow.Postal;
            VendorRow.ContactName = FormVendorRow.ContactName;
            VendorRow.VendorTypeID = FormVendorRow.VendorTypeID;
            VendorRow.CompanyID = vendorTypeRow.CompanyID;

            VendorRow.PhoneNumber = FormVendorRow.PhoneNumber;
            VendorRow.OneTimeVendor = FormVendorRow.OneTimeVendor;
            VendorRow.HoldVendor = FormVendorRow.HoldVendor;
            VendorRow.PurchaseingPostalCode = FormVendorRow.PurchaseingPostalCode;
            VendorRow.AlphaSearchKey = FormVendorRow.AlphaSearchKey;
            VendorRow.PurchasingContact = FormVendorRow.PurchasingContact;
            VendorRow.PurchasingAddress = FormVendorRow.PurchasingAddress;
            VendorRow.PurchasingCity = FormVendorRow.PurchasingCity;
            VendorRow.PurchasePhoneNumber = FormVendorRow.PurchasePhoneNumber;
            VendorRow.BankCode = FormVendorRow.BankCode;
            VendorRow.MethodPaymentID = FormVendorRow.MethodPaymentID;
            VendorRow.PaymentTermID = FormVendorRow.PaymentTermID;
            VendorRow.TransTypeID = FormVendorRow.TransTypeID;
            VendorRow.VATTypeID = FormVendorRow.VATTypeID;
            VendorRow.BankName = FormVendorRow.BankName;
            VendorRow.AccountNo = FormVendorRow.AccountNo;
            VendorRow.BankNo = FormVendorRow.BankNo;
            VendorRow.ACTypeID = FormVendorRow.ACTypeID;
            if (!FormVendorRow.IsRemarkNull()) {
                VendorRow.Remark = FormVendorRow.Remark;
            }
            VendorRow.BankCodeID = FormVendorRow.BankCodeID;

            if (FormVendorRow.ActionType == (int)SystemEnums.VendorActionType.Delete) {
                VendorRow.IsActive = false;
            }

            if (FormVendorRow.ActionType == (int)SystemEnums.VendorActionType.Reactive) {
                VendorRow.IsActive = true;
            }
            VendorRow.ActionType = FormVendorRow.ActionType;
            VendorRow.IsExport = false;
            this.TAVendor.Update(VendorRow);

            FormVendorRow.VendorID = VendorRow.VendorID;

            new PurchaseDSTableAdapters.FormVendorTableAdapter().Update(FormVendorRow);
        }

        //待改动
        public void DeleteVendorByID(int VendorID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TAVendor);
                this.TAVendor.DeleteByID(VendorID);
                transaction.Commit();
            } catch (Exception) {
                throw new ApplicationException();

            }
        }

        public int QueryVendorCount(string queryExpression) {
            return (int)this.TAVendor.QueryDataCount("Vendor", queryExpression);
        }

        #endregion

        #region VendorView

        public QueryDS.Export_VendorDataTable GetVendorViewByExport(string QueryExpression) {
            return this.TAVendorView.GetData(QueryExpression);
        }

        #endregion

        #region PaymentType Operate

        public MasterData.PaymentTypeRow GetPaymentTypeById(int Id) {
            return this.TAPaymentType.GetDataByID(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.PaymentTypeDataTable GetPaymentType() {
            return this.TAPaymentType.GetData();
        }

        public MasterData.PaymentTypeDataTable GetPaymentTypeForDDL() {
            return this.TAPaymentType.GetDataForDDL();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdatePaymentType(int PaymentTypeID, string PaymentTypeName) {
            MasterData.PaymentTypeDataTable PaymentTypeTab = this.TAPaymentType.GetDataByID(PaymentTypeID);
            MasterData.PaymentTypeRow PaymentTypeRow = PaymentTypeTab[0];
            PaymentTypeRow.PaymentTypeName = PaymentTypeName;
            this.TAPaymentType.Update(PaymentTypeRow);
        }

        #endregion

        #region ACType Operate

        public MasterData.ACTypeDataTable GetACTypeById(int ACTypeID) {
            return this.TAACType.GetDataByID(ACTypeID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.ACTypeDataTable GetACType() {
            return this.TAACType.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertACType(string ACTypeName, string Description, bool IsActive) {
            this.TAACType.Insert(ACTypeName, Description, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateACType(int ACTypeID, string ACTypeName, string Description, bool IsActive) {
            MasterData.ACTypeDataTable ACTypeTab = this.TAACType.GetDataByID(ACTypeID);
            MasterData.ACTypeRow ACTypeRow = ACTypeTab[0];
            ACTypeRow.ACTypeName = ACTypeName;
            ACTypeRow.Description = Description;
            ACTypeRow.IsActive = IsActive;
            this.TAACType.Update(ACTypeRow);
        }

        #endregion

        #region BankCode Operate

        public MasterData.BankCodeDataTable GetBankCodeById(int BankCodeID) {
            return this.TABankCode.GetDataByID(BankCodeID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.BankCodeDataTable GetBankCode() {
            return this.TABankCode.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertBankCode(string BankCode, string Description, bool IsActive) {
            this.TABankCode.Insert(BankCode, Description, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateBankCode(int BankCodeID, string BankCode, string Description, bool IsActive) {
            MasterData.BankCodeDataTable BankCodeTab = this.TABankCode.GetDataByID(BankCodeID);
            MasterData.BankCodeRow BankCodeRow = BankCodeTab[0];
            BankCodeRow.BankCode = BankCode;
            BankCodeRow.Description = Description;
            BankCodeRow.IsActive = IsActive;
            this.TABankCode.Update(BankCodeRow);
        }

        #endregion

        #region MethodPayment Operate

        public MasterData.MethodPaymentDataTable GetMethodPaymentById(int MethodPaymentID) {
            return this.TAMethodPayment.GetDataByID(MethodPaymentID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.MethodPaymentDataTable GetMethodPayment() {
            return this.TAMethodPayment.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertMethodPayment(string MethodPaymentName, string Description, bool IsActive) {
            this.TAMethodPayment.Insert(MethodPaymentName, Description, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateMethodPayment(int MethodPaymentID, string MethodPaymentName, string Description, bool IsActive) {
            MasterData.MethodPaymentDataTable MethodPaymentTab = this.TAMethodPayment.GetDataByID(MethodPaymentID);
            MasterData.MethodPaymentRow MethodPaymentRow = MethodPaymentTab[0];
            MethodPaymentRow.MethodPaymentName = MethodPaymentName;
            MethodPaymentRow.Description = Description;
            MethodPaymentRow.IsActive = IsActive;
            this.TAMethodPayment.Update(MethodPaymentRow);
        }

        #endregion

        #region PaymentTerm Operate

        public MasterData.PaymentTermDataTable GetPaymentTermById(int PaymentTermID) {
            return this.TAPaymentTerm.GetDataByID(PaymentTermID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.PaymentTermDataTable GetPaymentTerm() {
            return this.TAPaymentTerm.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPaymentTerm(string PaymentTermName, string Description, bool IsActive) {
            this.TAPaymentTerm.Insert(PaymentTermName, Description, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdatePaymentTerm(int PaymentTermID, string PaymentTermName, string Description, bool IsActive) {
            MasterData.PaymentTermDataTable PaymentTermTab = this.TAPaymentTerm.GetDataByID(PaymentTermID);
            MasterData.PaymentTermRow PaymentTermRow = PaymentTermTab[0];
            PaymentTermRow.PaymentTermName = PaymentTermName;
            PaymentTermRow.Description = Description;
            PaymentTermRow.IsActive = IsActive;
            this.TAPaymentTerm.Update(PaymentTermRow);
        }

        #endregion

        #region TransType Operate

        public MasterData.TransTypeDataTable GetTransTypeById(int TransTypeID) {
            return this.TATransType.GetDataByID(TransTypeID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.TransTypeDataTable GetTransType() {
            return this.TATransType.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertTransType(string TransTypeName, string Description, bool IsActive) {
            this.TATransType.Insert(TransTypeName, Description, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateTransType(int TransTypeID, string TransTypeName, string Description, bool IsActive) {
            MasterData.TransTypeDataTable TransTypeTab = this.TATransType.GetDataByID(TransTypeID);
            MasterData.TransTypeRow TransTypeRow = TransTypeTab[0];
            TransTypeRow.TransTypeName = TransTypeName;
            TransTypeRow.Description = Description;
            TransTypeRow.IsActive = IsActive;
            this.TATransType.Update(TransTypeRow);
        }

        #endregion

        #region VatType Operate

        public MasterData.VatTypeDataTable GetVatTypeById(int VatTypeID) {
            return this.TAVatType.GetDataByID(VatTypeID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.VatTypeDataTable GetVatType() {
            return this.TAVatType.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertVatType(string VatTypeName, string Description, bool HasTax, bool IsActive) {
            this.TAVatType.Insert(VatTypeName, Description, HasTax, IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateVatType(int VatTypeID, string VatTypeName, string Description, bool HasTax, bool IsActive) {
            MasterData.VatTypeDataTable VatTypeTab = this.TAVatType.GetDataByID(VatTypeID);
            MasterData.VatTypeRow VatTypeRow = VatTypeTab[0];
            VatTypeRow.VatTypeName = VatTypeName;
            VatTypeRow.Description = Description;
            VatTypeRow.HasTax = HasTax;
            VatTypeRow.IsActive = IsActive;
            this.TAVatType.Update(VatTypeRow);
        }

        #endregion

        #region UserAndRegion Operate

        public MasterData.UserAndRegionDataTable GetUserAndRegionById(int UserAndRegionID) {
            return this.TAUserAndRegion.GetDataByID(UserAndRegionID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public MasterData.UserAndRegionDataTable GetUserAndRegion() {
            return this.TAUserAndRegion.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertUserAndRegion(int StuffUserID, int CustomerRegionID, string Description) {
            this.TAUserAndRegion.Insert(StuffUserID, CustomerRegionID, Description);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateUserAndRegion(int UserAndRegionID, int StuffUserID, int CustomerRegionID, string Description) {
            MasterData.UserAndRegionRow row = TAUserAndRegion.GetDataByID(UserAndRegionID)[0];
            row.StuffUserID = StuffUserID;
            row.CustomerRegionID = CustomerRegionID;
            row.Description = Description;
            this.TAUserAndRegion.Update(row);
        }

        public void DeleteUserAndRegionByID(int UserAndRegionID) {
            this.TAUserAndRegion.DeleteByID(UserAndRegionID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public MasterData.UserAndRegionDataTable GetPagedUserAndRegion(int startRowIndex, int maximumRows, string sortExpression, String queryExpression) {
            return this.TAUserAndRegion.GetUserAndRegionPaged("UserAndRegion", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int UserAndRegionTotalCount(string queryExpression) {
            return (int)this.TAUserAndRegion.QueryDataCount("UserAndRegion", queryExpression);
        }
        #endregion
    }
}
