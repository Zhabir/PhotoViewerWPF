#pragma checksum "..\..\Рисование.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0D8FDDA81760C7A5B1C4E85C1D666317601C096E4B0E73BD8A384924BC93D9E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using ВПФ_Фоторедактор;


namespace ВПФ_Фоторедактор {
    
    
    /// <summary>
    /// Рисование
    /// </summary>
    public partial class Рисование : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\Рисование.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.InkCanvas inkcanvas1;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Рисование.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image1;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Рисование.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Erase;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Рисование.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Draw;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Рисование.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Save;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Рисование.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RedButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Рисование.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton GreenButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Рисование.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton BlueButton;
        
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
            System.Uri resourceLocater = new System.Uri("/ВПФ Фоторедактор;component/%d0%a0%d0%b8%d1%81%d0%be%d0%b2%d0%b0%d0%bd%d0%b8%d0%b" +
                    "5.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Рисование.xaml"
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
            this.inkcanvas1 = ((System.Windows.Controls.InkCanvas)(target));
            return;
            case 2:
            this.image1 = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.Erase = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\Рисование.xaml"
            this.Erase.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Draw = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Рисование.xaml"
            this.Draw.Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Save = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\Рисование.xaml"
            this.Save.Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RedButton = ((System.Windows.Controls.RadioButton)(target));
            
            #line 21 "..\..\Рисование.xaml"
            this.RedButton.Checked += new System.Windows.RoutedEventHandler(this.RedButton_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.GreenButton = ((System.Windows.Controls.RadioButton)(target));
            
            #line 22 "..\..\Рисование.xaml"
            this.GreenButton.Checked += new System.Windows.RoutedEventHandler(this.GreenButton_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BlueButton = ((System.Windows.Controls.RadioButton)(target));
            
            #line 23 "..\..\Рисование.xaml"
            this.BlueButton.Checked += new System.Windows.RoutedEventHandler(this.BlueButton_Checked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

