﻿#pragma checksum "..\..\ClassMaintain.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B9DCFB1EC96F0855D9B90EA89D8C8A0790F77F90F7381F368DA5D5EE64AECA44"
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
    /// ClassMaintain
    /// </summary>
    public partial class ClassMaintain : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtClassID;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTeacherID;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtClassName;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtClassType;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearch;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ClassList;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TTeach;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse ITeach;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\ClassMaintain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bTeach;
        
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
            System.Uri resourceLocater = new System.Uri("/TripMaintanance;component/classmaintain.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ClassMaintain.xaml"
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
            
            #line 7 "..\..\ClassMaintain.xaml"
            ((TripMaintanance.ClassMaintain)(target)).Loaded += new System.Windows.RoutedEventHandler(this.OnLoad);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtClassID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtTeacherID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtClassName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtClassType = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtSearch = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 77 "..\..\ClassMaintain.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSearch);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 81 "..\..\ClassMaintain.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSort);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ClassList = ((System.Windows.Controls.ListView)(target));
            
            #line 91 "..\..\ClassMaintain.xaml"
            this.ClassList.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ClassList_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 125 "..\..\ClassMaintain.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnView);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 128 "..\..\ClassMaintain.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnInsert);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 131 "..\..\ClassMaintain.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnUpdate);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 134 "..\..\ClassMaintain.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnDelete);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 137 "..\..\ClassMaintain.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnExit);
            
            #line default
            #line hidden
            return;
            case 15:
            this.TTeach = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.ITeach = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 17:
            
            #line 178 "..\..\ClassMaintain.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.StudentButton);
            
            #line default
            #line hidden
            return;
            case 18:
            this.bTeach = ((System.Windows.Controls.Button)(target));
            
            #line 183 "..\..\ClassMaintain.xaml"
            this.bTeach.Click += new System.Windows.RoutedEventHandler(this.TeacherButton);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

