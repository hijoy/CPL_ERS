using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;

namespace BusinessObjects {
    public class MyException : Exception {
        private string _errorMsgEN;
        private string _errorMsgCN;

        public String ErrorMsg {
            get {
                Console.WriteLine(Thread.CurrentThread.CurrentUICulture.ToString());
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US") {
                    return _errorMsgEN;
                } else {
                    return _errorMsgCN;
                }
            }
        }

        public MyException() {
        }

        public MyException(string errorMsgCN, string errorMsgEN) {
            
            this._errorMsgCN = errorMsgCN;
            this._errorMsgEN = errorMsgEN;
        }
    }
}