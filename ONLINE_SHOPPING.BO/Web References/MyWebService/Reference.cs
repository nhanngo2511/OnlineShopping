﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace ONLINE_SHOPPING.BO.MyWebService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="MyWebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class MyWebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ChangeMoneyToUSDOperationCompleted;
        
        private System.Threading.SendOrPostCallback ChangeMoneyToVNDOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetBrandsOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateBrandOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public MyWebService() {
            this.Url = global::ONLINE_SHOPPING.BO.Properties.Settings.Default.ONLINE_SHOPPING_BO_MyWebService_MyWebService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ChangeMoneyToUSDCompletedEventHandler ChangeMoneyToUSDCompleted;
        
        /// <remarks/>
        public event ChangeMoneyToVNDCompletedEventHandler ChangeMoneyToVNDCompleted;
        
        /// <remarks/>
        public event GetBrandsCompletedEventHandler GetBrandsCompleted;
        
        /// <remarks/>
        public event CreateBrandCompletedEventHandler CreateBrandCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ChangeMoneyToUSD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public float ChangeMoneyToUSD(float CurrentMoney) {
            object[] results = this.Invoke("ChangeMoneyToUSD", new object[] {
                        CurrentMoney});
            return ((float)(results[0]));
        }
        
        /// <remarks/>
        public void ChangeMoneyToUSDAsync(float CurrentMoney) {
            this.ChangeMoneyToUSDAsync(CurrentMoney, null);
        }
        
        /// <remarks/>
        public void ChangeMoneyToUSDAsync(float CurrentMoney, object userState) {
            if ((this.ChangeMoneyToUSDOperationCompleted == null)) {
                this.ChangeMoneyToUSDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnChangeMoneyToUSDOperationCompleted);
            }
            this.InvokeAsync("ChangeMoneyToUSD", new object[] {
                        CurrentMoney}, this.ChangeMoneyToUSDOperationCompleted, userState);
        }
        
        private void OnChangeMoneyToUSDOperationCompleted(object arg) {
            if ((this.ChangeMoneyToUSDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ChangeMoneyToUSDCompleted(this, new ChangeMoneyToUSDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ChangeMoneyToVND", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public float ChangeMoneyToVND(float CurrentMoney) {
            object[] results = this.Invoke("ChangeMoneyToVND", new object[] {
                        CurrentMoney});
            return ((float)(results[0]));
        }
        
        /// <remarks/>
        public void ChangeMoneyToVNDAsync(float CurrentMoney) {
            this.ChangeMoneyToVNDAsync(CurrentMoney, null);
        }
        
        /// <remarks/>
        public void ChangeMoneyToVNDAsync(float CurrentMoney, object userState) {
            if ((this.ChangeMoneyToVNDOperationCompleted == null)) {
                this.ChangeMoneyToVNDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnChangeMoneyToVNDOperationCompleted);
            }
            this.InvokeAsync("ChangeMoneyToVND", new object[] {
                        CurrentMoney}, this.ChangeMoneyToVNDOperationCompleted, userState);
        }
        
        private void OnChangeMoneyToVNDOperationCompleted(object arg) {
            if ((this.ChangeMoneyToVNDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ChangeMoneyToVNDCompleted(this, new ChangeMoneyToVNDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetBrands", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetBrands() {
            object[] results = this.Invoke("GetBrands", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetBrandsAsync() {
            this.GetBrandsAsync(null);
        }
        
        /// <remarks/>
        public void GetBrandsAsync(object userState) {
            if ((this.GetBrandsOperationCompleted == null)) {
                this.GetBrandsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetBrandsOperationCompleted);
            }
            this.InvokeAsync("GetBrands", new object[0], this.GetBrandsOperationCompleted, userState);
        }
        
        private void OnGetBrandsOperationCompleted(object arg) {
            if ((this.GetBrandsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetBrandsCompleted(this, new GetBrandsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateBrand", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void CreateBrand(string Name) {
            this.Invoke("CreateBrand", new object[] {
                        Name});
        }
        
        /// <remarks/>
        public void CreateBrandAsync(string Name) {
            this.CreateBrandAsync(Name, null);
        }
        
        /// <remarks/>
        public void CreateBrandAsync(string Name, object userState) {
            if ((this.CreateBrandOperationCompleted == null)) {
                this.CreateBrandOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateBrandOperationCompleted);
            }
            this.InvokeAsync("CreateBrand", new object[] {
                        Name}, this.CreateBrandOperationCompleted, userState);
        }
        
        private void OnCreateBrandOperationCompleted(object arg) {
            if ((this.CreateBrandCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateBrandCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void ChangeMoneyToUSDCompletedEventHandler(object sender, ChangeMoneyToUSDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ChangeMoneyToUSDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ChangeMoneyToUSDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public float Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((float)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void ChangeMoneyToVNDCompletedEventHandler(object sender, ChangeMoneyToVNDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ChangeMoneyToVNDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ChangeMoneyToVNDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public float Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((float)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void GetBrandsCompletedEventHandler(object sender, GetBrandsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetBrandsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetBrandsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void CreateBrandCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591