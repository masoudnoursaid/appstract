﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Appstract.Front.Application.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class PaymentMethodResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public PaymentMethodResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Appstract.Front.Application.Common.Resources.PaymentMethodResource", typeof(PaymentMethodResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Back.
        /// </summary>
        public static string Back {
            get {
                return ResourceManager.GetString("Back", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change Method.
        /// </summary>
        public static string ChangeMethod {
            get {
                return ResourceManager.GetString("ChangeMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checkout.
        /// </summary>
        public static string Checkout {
            get {
                return ResourceManager.GetString("Checkout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Choose the Payment Method.
        /// </summary>
        public static string ChoosePaymentMethod {
            get {
                return ResourceManager.GetString("ChoosePaymentMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  Please enter the credit amount .
        /// </summary>
        public static string EnterCreditAmount {
            get {
                return ResourceManager.GetString("EnterCreditAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Payment Method.
        /// </summary>
        public static string PaymentMethod {
            get {
                return ResourceManager.GetString("PaymentMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please choose a payment method first.
        /// </summary>
        public static string PleaseChoosePaymentMethod {
            get {
                return ResourceManager.GetString("PleaseChoosePaymentMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  Powered By .
        /// </summary>
        public static string PoweredBy {
            get {
                return ResourceManager.GetString("PoweredBy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are connected with IP address from a sanctioned country, we are not authorized to allow payment on the platform from your IP Address.
        /// </summary>
        public static string SanctionedCountryError {
            get {
                return ResourceManager.GetString("SanctionedCountryError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to value should be in {0} range.
        /// </summary>
        public static string ValueRangeMessage {
            get {
                return ResourceManager.GetString("ValueRangeMessage", resourceCulture);
            }
        }
    }
}
