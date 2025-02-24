﻿#pragma checksum "..\..\AnalyticsMenu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "15F0B24EAA14A01F26882EF4C1A6D87C1BBE32FAFCC477D033BD22B184091F58"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TripMaintanance;


namespace TripMaintanance {
    
    
    /// <summary>
    /// AnalyticsMenu
    /// </summary>
    public partial class AnalyticsMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\AnalyticsMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBookingNumber;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\AnalyticsMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtClassID;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\AnalyticsMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBusName;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\AnalyticsMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDateOfTrip;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\AnalyticsMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTimeOfTrip;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\AnalyticsMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView BookingList;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\AnalyticsMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView CommentsList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TripMaintanance;component/analyticsmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AnalyticsMenu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtBookingNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtClassID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtBusName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtDateOfTrip = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtTimeOfTrip = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.BookingList = ((System.Windows.Controls.ListView)(target));
            
            #line 88 "..\..\AnalyticsMenu.xaml"
            this.BookingList.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.BookingList_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 113 "..\..\AnalyticsMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnViewBook);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 116 "..\..\AnalyticsMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnApproveBook);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 119 "..\..\AnalyticsMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnUpdateBook);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 122 "..\..\AnalyticsMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnDeleteBook);
            
            #line default
            #line hidden
            return;
            case 11:
            this.CommentsList = ((System.Windows.Controls.ListView)(target));
            return;
            case 12:
            
            #line 167 "..\..\AnalyticsMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 169 "..\..\AnalyticsMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Bus);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 172 "..\..\AnalyticsMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

