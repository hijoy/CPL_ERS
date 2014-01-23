using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.LogDSTableAdapters;

namespace BusinessObjects {
    [System.ComponentModel.DataObject]
    public class LogBLL {
        private LogInActionLogTableAdapter m_LogInActionLogTA;
        public LogInActionLogTableAdapter LogInActionLogTA {
            get {
                if (m_LogInActionLogTA == null) {
                    m_LogInActionLogTA = new LogInActionLogTableAdapter();
                }
                return m_LogInActionLogTA;
            }
        }

        private ExportLogTableAdapter m_ExportLogTableAdapter;
        public ExportLogTableAdapter ExportLogTA {
            get {
                if (m_ExportLogTableAdapter == null) {
                    m_ExportLogTableAdapter = new ExportLogTableAdapter();
                }
                return m_ExportLogTableAdapter;
            }
        }

        private ExportLogDetailTableAdapter m_ExportLogDetailTableAdapter;
        public ExportLogDetailTableAdapter ExportLogDetailTA {
            get {
                if (m_ExportLogDetailTableAdapter == null) {
                    m_ExportLogDetailTableAdapter = new ExportLogDetailTableAdapter();
                }
                return m_ExportLogDetailTableAdapter;
            }
        }

        private CommonDataEditActionLogTableAdapter m_CommonDataEditActionLogTA;
        public CommonDataEditActionLogTableAdapter CommonDataEditActionLogTA {
            get {
                if (m_CommonDataEditActionLogTA == null) {
                    m_CommonDataEditActionLogTA = new CommonDataEditActionLogTableAdapter();
                }
                return this.m_CommonDataEditActionLogTA;
            }
        }

        private AuthorizationConfigureLogTableAdapter m_AuthorizationConfigureLogTA;
        public AuthorizationConfigureLogTableAdapter AuthorizationConfigureLogTA {
            get {
                if (this.m_AuthorizationConfigureLogTA == null) {
                    this.m_AuthorizationConfigureLogTA = new AuthorizationConfigureLogTableAdapter();
                }
                return this.m_AuthorizationConfigureLogTA;
            }
        }

        private ImportLogTableAdapter m_ImportLogTableAdapter;
        public ImportLogTableAdapter ImportLogTA {
            get {
                if (m_ImportLogTableAdapter == null) {
                    m_ImportLogTableAdapter = new ImportLogTableAdapter();
                }
                return m_ImportLogTableAdapter;
            }
        }

        private ImportLogDetailTableAdapter m_ImportLogDetailTableAdapter;
        public ImportLogDetailTableAdapter ImportLogDetailTA {
            get {
                if (m_ImportLogDetailTableAdapter == null) {
                    m_ImportLogDetailTableAdapter = new ImportLogDetailTableAdapter();
                }
                return m_ImportLogDetailTableAdapter;
            }
        }

        private ImportFormLogTableAdapter _TAImportFormLog;
        public ImportFormLogTableAdapter TAImportFormLog {
            get {
                if (_TAImportFormLog == null) {
                    _TAImportFormLog = new ImportFormLogTableAdapter();
                }
                return _TAImportFormLog;
            }
        }

        private ImportFormLogDetailTableAdapter _TAImportFormLogDetail;
        public ImportFormLogDetailTableAdapter TAImportFormLogDetail {
            get {
                if (_TAImportFormLogDetail == null) {
                    _TAImportFormLogDetail = new ImportFormLogDetailTableAdapter();
                }
                return _TAImportFormLogDetail;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public LogDS.LogInActionLogDataTable GetLogInActionLog(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "LogInTime DESC";
            }
            return this.LogInActionLogTA.GetPage("LogInActionLog", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int TotalCount(string queryExpression) {
            return this.LogInActionLogTA.QueryDataCount("LogInActionLog", queryExpression).GetValueOrDefault();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public LogDS.CommonDataEditActionLogDataTable GetCommonDataEditActionLog(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ActionTime DESC";
            }
            return this.CommonDataEditActionLogTA.GetPage("CommonDataEditActionLog", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int CommonDataEditActionLogTotalCount(string queryExpression) {
            return this.CommonDataEditActionLogTA.QueryDataCount("CommonDataEditActionLog", queryExpression).GetValueOrDefault();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public LogDS.AuthorizationConfigureLogDataTable GetAuthorizationConfigureLog(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ConfigureTime DESC";
            }
            return this.AuthorizationConfigureLogTA.GetPage("AuthorizationConfigureLog", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int AuthorizationConfigureLogTotalCount(string queryExpression) {
            return this.AuthorizationConfigureLogTA.QueryDataCount("AuthorizationConfigureLog", queryExpression).GetValueOrDefault();
        }

        #region ImportLog WindowsService

        public int InsertImportLog(int ImportType, string FileName, int ImportUserID, int TotalCount, int SuccessCount, int FailCount) {
            LogDS.ImportLogDataTable ImportLogDataTable = new LogDS.ImportLogDataTable();
            LogDS.ImportLogRow ImportLogRow = ImportLogDataTable.NewImportLogRow();
            ImportLogRow.ImportUserID = ImportUserID;
            ImportLogRow.ImportType = ImportType;
            ImportLogRow.FileName = FileName;
            ImportLogRow.ImportDate = DateTime.Now;
            ImportLogRow.TotalCount = TotalCount;
            ImportLogRow.SuccessCount = SuccessCount;
            ImportLogRow.FailCount = FailCount;
            ImportLogDataTable.AddImportLogRow(ImportLogRow);
            this.ImportLogTA.Update(ImportLogDataTable);
            return ImportLogRow.LogID;
        }

        public int UpdateImportLog(int LogID, int ImportType, string FileName, int ImportUserID, int TotalCount, int SuccessCount, int FailCount) {
            LogDS.ImportLogDataTable ImportLogDataTable = this.ImportLogTA.GetDataByID(LogID);
            LogDS.ImportLogRow ImportLogRow = ImportLogDataTable[0];
            ImportLogRow.ImportUserID = ImportUserID;
            ImportLogRow.ImportType = ImportType;
            ImportLogRow.FileName = FileName;
            ImportLogRow.ImportDate = DateTime.Now;
            ImportLogRow.TotalCount = TotalCount;
            ImportLogRow.SuccessCount = SuccessCount;
            ImportLogRow.FailCount = FailCount;
            return this.ImportLogTA.Update(ImportLogRow);

        }

        public int InsertImportLogDetail(int LogID, int line, string Error) {
            LogDS.ImportLogDetailDataTable ImportLogDetailDataTable = new LogDS.ImportLogDetailDataTable();
            LogDS.ImportLogDetailRow ImportLogDetailRow = ImportLogDetailDataTable.NewImportLogDetailRow();

            ImportLogDetailRow.LogID = LogID;
            ImportLogDetailRow.Line = line;
            ImportLogDetailRow.Error = Error;
            ImportLogDetailDataTable.AddImportLogDetailRow(ImportLogDetailRow);
            return this.ImportLogDetailTA.Update(ImportLogDetailDataTable);
        }

        public LogDS.ImportLogDetailDataTable GetLogDetailByLogId(int logId) {
            return this.ImportLogDetailTA.GetDataByLogID(logId);
        }

        public LogDS.ImportLogDataTable GetPagedFromImport(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            return this.ImportLogTA.GetPagedFromImport(sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int GetImportDataCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.ImportLogTA.FillDataCount(queryExpression);
        }

        #endregion

        #region ExportLog WindowsService
        public int InsertExportLog(int ExportType, string FileName, int TotalCount, int SuccessCount, int FailCount, int StuffUserID) {
            LogDS.ExportLogDataTable ExportDataTable = new LogDS.ExportLogDataTable();
            LogDS.ExportLogRow ExportLogRow = ExportDataTable.NewExportLogRow();
            ExportLogRow.ExportType = ExportType;
            ExportLogRow.FileName = FileName;
            ExportLogRow.ExportDate = DateTime.Now;
            ExportLogRow.TotalCount = TotalCount;
            ExportLogRow.SuccessCount = SuccessCount;
            ExportLogRow.FailCount = FailCount;
            if (StuffUserID > 0) {
                ExportLogRow.StuffUserID = StuffUserID;
            }
            ExportDataTable.AddExportLogRow(ExportLogRow);
            this.ExportLogTA.Update(ExportDataTable);
            return ExportLogRow.LogId;
        }
        public int UpdateExportLog(int LogID, int TotalCount, int SuccessCount, int FailCount) {
            LogDS.ExportLogDataTable ExportDataTable = this.ExportLogTA.GetDataByID(LogID);
            LogDS.ExportLogRow ExportLogRow = ExportDataTable[0];
            ExportLogRow.TotalCount = TotalCount;
            ExportLogRow.SuccessCount = SuccessCount;
            ExportLogRow.FailCount = FailCount;
            return this.ExportLogTA.Update(ExportLogRow);
        }
        public int InsertExportLogDetail(int LogID, string Error) {
            LogDS.ExportLogDetailDataTable ExportLogDetailDataTable = new LogDS.ExportLogDetailDataTable();
            LogDS.ExportLogDetailRow ExportLogDetailRow = ExportLogDetailDataTable.NewExportLogDetailRow();
            ExportLogDetailRow.LogID = LogID;
            ExportLogDetailRow.Error = Error;
            ExportLogDetailDataTable.AddExportLogDetailRow(ExportLogDetailRow);
            return this.ExportLogDetailTA.Update(ExportLogDetailDataTable);
        }
        public LogDS.ExportLogDetailDataTable GetExportLogDetailByLogId(int logId) {
            return this.ExportLogDetailTA.GetDataByLogID(logId);
        }

        public LogDS.ExportLogDataTable GetPagedFromExport(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            return this.ExportLogTA.GetPagedFromExport(sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int GetExportDataCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.ExportLogTA.FillDataCount(queryExpression);
        }
        #endregion

        #region 自动生成报销单

        public LogDS.ImportFormLogDataTable GetPagedImportFormLog(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ImportDate DESC";
            }
            return this.TAImportFormLog.GetPagedImportFormLog("ImportFormLog", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryImportFormLogCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAImportFormLog.QueryDataCount("ImportFormLog", queryExpression);
        }

        public LogDS.ImportFormLogDetailDataTable GetPagedImportFormLogDetail(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Line";
            }
            return this.TAImportFormLogDetail.GetPagedImportFormLog("ImportFormLogDetail", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryImportFormLogDetailCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAImportFormLogDetail.QueryDataCount("ImportFormLogDetail", queryExpression);
        }

        #endregion

    }
}
