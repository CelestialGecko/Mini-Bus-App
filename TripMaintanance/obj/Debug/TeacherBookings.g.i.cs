﻿#pragma checksum "..\..\TeacherBookings.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4124E8F89B7F7F94906CB38B5B79C6DBE163EA4EE324053E56370A5EFBC8430E"
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
    /// TeacherBookings
    /// </summary>
    public partial class TeacherBookings : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\TeacherBookings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBookingNumber;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\TeacherBookings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtClassID;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\TeacherBookings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBusName;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\TeacherBookings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDateOfTrip;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\TeacherBookings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTimeOfTrip;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\TeacherBookings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView BookingList;
        
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
            System.Uri resourceLocater = new System.Uri("/TripMaintanance;component/teacherbookings.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TeacherBookings.xaml"
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
            
            #line 81 "..\..\TeacherBookings.xaml"
            this.BookingList.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.BookingList_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 106 "..\..\TeacherBookings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnViewBook);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 109 "..\..\TeacherBookings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnUpdateBook);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 112 "..\..\TeacherBookings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnDeleteBook);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 115 "..\..\TeacherBookings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnExit);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

