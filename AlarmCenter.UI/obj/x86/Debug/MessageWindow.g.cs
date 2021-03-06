﻿#pragma checksum "..\..\..\MessageWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C209C90CF4A26F4EE2631601F6375BF4"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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
using System.Windows.Forms.Integration;
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


namespace AlarmCenter.UI {
    
    
    /// <summary>
    /// MessageWindow
    /// </summary>
    public partial class MessageWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbUserName;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbAddress;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbEventTpye;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbEventInfomation;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbZoneNumber;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMarkEvent;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStopSound;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnToMap;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbEventTime;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbIsNoMarkEventCount;
        
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
            System.Uri resourceLocater = new System.Uri("/AlarmCenter;component/messagewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MessageWindow.xaml"
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
            this.tbUserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.tbAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tbEventTpye = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbEventInfomation = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbZoneNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnMarkEvent = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\MessageWindow.xaml"
            this.btnMarkEvent.Click += new System.Windows.RoutedEventHandler(this.btnMarkEvent_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnStopSound = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\MessageWindow.xaml"
            this.btnStopSound.Click += new System.Windows.RoutedEventHandler(this.btnStopSound_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnToMap = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\MessageWindow.xaml"
            this.btnToMap.Click += new System.Windows.RoutedEventHandler(this.btnToMap_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.tbEventTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.tbIsNoMarkEventCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

