using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects {
    public class SystemEnums {

        public enum FlowRuleType {
            AmountJudge = 1 //����ж�
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public enum FlowNodeStatus {
            Wait = 0,//��δִ��
            InTurn = 1,//��ǰ��ִ��
            Pass = 2,//ͨ��
            Reject = 3 //���
        }

        /// <summary>
        /// ���̽ڵ��������
        /// </summary>
        public enum FlowNodeOperateType {
            Apply = 1,//����
            Approval = 2,//����
            Report = 3,//����
            Modify = 4 //�˻ش��޸�
        }

        public enum RejectActionType {
            GoOn = 1,//������ת���˻�
            Reject = 2 //�˻ص�������
        }

        public enum BusinessUseCase {
            FormPersonalReimburse = 11,
            MarketingApply = 81,
            MarketingPayment = 82,
            SalesApply = 51,
            SaleSettlement = 52,
            SalesPayment = 53,
            ViewBudget=54,
            Vendor = 61,
            PR = 62,
            PO = 63,
            PV = 64,
            RDApply = 71,
            RDPayment = 72,
            DeliveryComplete=91,
            ImportSettlementPayment=92,
            BudgetManageFee = 111,
            BudgetSale = 112,
            BudgetMarketing = 113,
            BudgetPurchase = 114,
            BudgetRD = 115,
            CostCenter = 304,
            RejectReason = 307,
            Bulletin = 308,
            PositionType = 506,
            FlowParticipant = 507,
            CustomerType = 361,
            CustomerChannel = 362,
            CustomerRegion = 363,
            Customer = 364,
            Brand = 365,
            SKUCategory = 366,
            SKUType = 367,
            SKU = 368,
            SKUPrice = 369,
            CityType = 371,
            Currency = 372,
            ExpenseManageType = 373,
            CostLimit = 374,
            PeriodReimburse = 375,
            ExpenseCategory = 376,
            ExpenseItem = 377,
            ItemCategory = 378,
            Item = 379,
            GLAccount = 380,
            ShippingTerm = 381,
            VendorType = 382,
            PurchaseBudgetType = 383,
            PurchaseType = 384,
            PeriodPurchase = 385,
            Company = 386,
            InvoiceStatus = 387,
            MarketingProject = 388,
            DisplayType = 389,
            DiscountType = 390,
            PeriodSale = 391,
            PaymentType = 393,
            ACType = 394,//add xjGuo
            BankCode = 395,//add xjGuo
            MethodPayment = 396,//add xjGuo
            PaymentTerm = 397,//add xjGuo
            TransType = 398,//add xjGuo
            VatType = 399,//add xjGuo
            StaffLevel = 508,
            ProxyReimburse = 509,
            ProxyBusiness = 510,
            DemoReport = 601,
            ReportHQ_ByExpenseType = 602,//add xjGuo
            ReportHQ_ByRegionbyCity = 603,//add xjGuo
            ReportRegion_ByExpenseType = 604,//add xjGuo
            ReportTEBudget = 605,//add xjGuo
            VendorReport = 606,//add xjGuo
            RDMAAReport = 607,//add xjGuo
            FormPRPOReport = 608,//add xjGuo
            FormPVReport = 609,//add xjGuo
            FormExpenseSummaryReport = 610,//add xjGuo
            FormReportPL = 611,//add xjGuo
            ReportSaleApplyDetail = 612,//add xjGuo
            ReportMarketingApplyDetail = 613,//add xjGuo
            ReportRDApplyDetail = 614,//add xjGuo
            ReportMAAPayment = 615,//add xjGuo
            ReportMAAging = 616,//add xjGuo
            FormPVPaidReport = 618,//add xjGuo
            FormPVStatisticsReport = 619,//add xjGuo
            FormPVDelayReport = 620,//add xjGuo

            InvoiceReverse = 511,
            DataImportLog = 512,//add xjGuo
            FinanceRemark = 513,
            ExportLog=514,
            UserAndRegion=515,
            InvoiceReturn= 517,
            PeriodModify=518,
            VendorTypeModify=519
        }

        public enum UseCase {
            UserManage = 502,
            SystemRoleManage = 503,
            OrganizationManage = 501,
            PositionAuthorization = 504,
            OperateScope = 505
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public enum FormStatus {
            Draft = 0,//�ݸ�
            Awaiting = 1,//������
            ApproveCompleted = 2,//�������
            Rejected = 3,//�˻ش��޸�            
            Scrap = 4 //����
        }

        /// <summary>
        /// ��֯��������
        /// </summary>
        public enum OrganizationUnitType {
            Filiale = 1, //�ֹ�˾
            Department = 6, //����
            Office = 3 //����
        }

        /// <summary>
        /// ҵ�����
        /// </summary>
        public enum OperateEnum {
            FillForm = 1000, //�
            Approval = 2000, //����
            View = 3000, //�鿴
            Print = 4000, //��ӡƾ֤
            Query = 5000, //��ѯ
            Manage = 6000, //ά��
            Import = 7000,//����
            Scrap = 8000,//����
            Other=9000
        }

        /// <summary>
        /// ������
        /// </summary>
        public enum PageType {
            TravelReimburseApply = 1,
            FormInvitationApply = 2,
            FormInvitationReimburse = 3,
            FormPersonalReimburse = 4,
            ActivityApply = 11,
            NoActivityApply = 12,
            ActivitySettlementApply = 14,
            NoActivitySettlementApply = 15,
            ActivityAdvancedPayment = 16,
            PaymentCash = 17,
            NoActivityAdvancedPayment = 18,
            PaymentFreeGoods = 19,
            VendorApply = 21,
            PRApply = 22,
            POApply = 23,
            PVApply = 24,
            NormalPV = 24,
            SpecialPV = 25,
            SpecialPOApply = 26,
            RDApply = 31,
            RDPayment = 32,
            FormMarketingApply = 41,
            FormMArketingPayment = 42,

            SaleSampleRequest=27,
            MarketingSampleRequest=43,
            RDSampleRequest = 33
        }

        public enum FormType {
            TravelReimburseApply = 1,
            FormInvitationApply = 2,
            FormInvitationReimburse = 3,
            PersonalReimburseApply = 4,
            SaleApply = 11,
            SaleSettlement = 12,
            SaleAdvancedPayment = 13,
            SalePayment = 14,
            VendorApply = 21,
            PRApply = 22,
            POApply = 23,
            PVApply = 24,
            RDApply = 31,
            RDPayment = 32,
            FormMarketingApply = 41,
            FormMarketingPayment = 42
        }

        public enum BusinessType {
            Sales = 1,
            Marketing = 2,
            RD = 3
        }

        public enum PaymentType {
            Cash = 1,
            PiaoKou = 2,
            Transfer = 3,
            FreeGoods = 4
        }

        public enum POType {
            Purchase = 1,
            Sale = 2,
            Marketing = 3,
            RD = 4
        }

        public enum PVType {
            PR = 1,
            PO = 2,
            None = 3
        }

        public enum VendorActionType {
            Delete = 0,
            Add = 1,
            Edit = 2,
            Reactive=3
        }

        public enum InvoiceStatus {
            Yes = 1,
            No = 2,
            Waiting = 3
        }

        public enum DiscountType {
            AdjustmentFactor=8
        }

        public enum PurchaseBudgetType {
            Capital=3,
            Non_Capital = 4
        }

    }
}
