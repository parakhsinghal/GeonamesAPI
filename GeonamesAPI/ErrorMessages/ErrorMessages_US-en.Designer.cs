﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeonamesAPI.Service.ErrorMessages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages_US_en {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages_US_en() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GeonamesAPI.Service.ErrorMessages.ErrorMessages_US-en", typeof(ErrorMessages_US_en).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The data was not inserted. Please make sure that the data is free of any corruption..
        /// </summary>
        internal static string NotCreated_MultipleEntries {
            get {
                return ResourceManager.GetString("NotCreated_MultipleEntries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The data was not inserted. Please make sure that the data is free of any corruption..
        /// </summary>
        internal static string NotCreated_SingleEntry {
            get {
                return ResourceManager.GetString("NotCreated_SingleEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The resource was not deleted. Please make sure that the resource exists before trying to delete it..
        /// </summary>
        internal static string NotDeleted_SingleEntry {
            get {
                return ResourceManager.GetString("NotDeleted_SingleEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The data was not updated as one or more entries in the input data were stale or corrupt. Please refresh your input data by calling the appropriate link..
        /// </summary>
        internal static string NotUpdated_MultipleEntries {
            get {
                return ResourceManager.GetString("NotUpdated_MultipleEntries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The update was not performed as the supplied data was stale. Please refresh your input data by calling the appropriate link..
        /// </summary>
        internal static string NotUpdated_SingleEntry {
            get {
                return ResourceManager.GetString("NotUpdated_SingleEntry", resourceCulture);
            }
        }
    }
}