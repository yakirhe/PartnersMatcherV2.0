﻿#pragma checksum "..\..\..\View\SignUpW.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "73157E8747B9E9EC8FEDE72080E64777"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PartnersMatcher.View;
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


namespace PartnersMatcher.View {
    
    
    /// <summary>
    /// SignUpW
    /// </summary>
    public partial class SignUpW : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 67 "..\..\..\View\SignUpW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox fullNameTxt;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\View\SignUpW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailTxt;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\View\SignUpW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox passwordTxt;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\View\SignUpW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox dobTxt;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\View\SignUpW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cityTxt;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\View\SignUpW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phoneTxt;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\View\SignUpW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox smoking;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\View\SignUpW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox pet;
        
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
            System.Uri resourceLocater = new System.Uri("/PartnersMatcher;component/view/signupw.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\SignUpW.xaml"
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
            this.fullNameTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.emailTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.passwordTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.dobTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cityTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.phoneTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 100 "..\..\..\View\SignUpW.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.smoking = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.pet = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

