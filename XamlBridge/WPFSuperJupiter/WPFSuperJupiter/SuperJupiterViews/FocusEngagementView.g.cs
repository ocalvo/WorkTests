﻿#pragma checksum "C:\projects\WPFSuperJupiter\SuperJupiterViews\FocusEngagementView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BC6CDFFCF5299F4DBBAB199716CD562B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SuperJupiter.Views
{
    partial class FocusEngagementView : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // FocusEngagementView.xaml line 41
                {
                    this.gv1 = (global::Windows.UI.Xaml.Controls.GridView)(target);
                }
                break;
            case 2: // FocusEngagementView.xaml line 47
                {
                    this.A = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 3: // FocusEngagementView.xaml line 36
                {
                    this.Slider2 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 4: // FocusEngagementView.xaml line 13
                {
                    this.Slider1 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 5: // FocusEngagementView.xaml line 14
                {
                    this.combobox1 = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 6: // FocusEngagementView.xaml line 15
                {
                    this.Item1 = (global::Windows.UI.Xaml.Controls.ComboBoxItem)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBoxItem)this.Item1).GotFocus += this.GiveCombobox2Focus;
                }
                break;
            case 7: // FocusEngagementView.xaml line 16
                {
                    this.combobox2 = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

