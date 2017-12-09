using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;

namespace WPFSerialAssistant
{
    public partial class MainWindow : Window
    {
        // 保存面板的显示状态
        private Stack<Visibility> panelVisibilityStack = new Stack<Visibility>(3);

        /// <summary>
        /// 判断是否处于简洁视图模式
        /// </summary>
        /// <returns></returns>
        private bool IsCompactViewMode()
        {
            if (compactViewMenuItem.IsChecked == true
                //serialCommunicationConfigPanel.Visibility == Visibility.Collapsed 
                //&& serialSettingViewMenuItem.Visibility == Visibility.Collapsed
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 进入简洁视图模式
        /// </summary>
        private void EnterCompactViewMode()
        {
            // 首先需要保持panel的显示状态
            panelVisibilityStack.Push(serialPortConfigPanel.Visibility);
            panelVisibilityStack.Push(serialCommunicationConfigPanel.Visibility);

            // 进入简洁视图模式
            serialPortConfigPanel.Visibility = Visibility.Collapsed;
            serialCommunicationConfigPanel.Visibility = Visibility.Collapsed;

            // 把对应的菜单项取消选中
            serialSettingViewMenuItem.IsChecked = false;
            serialCommunicationSettingViewMenuItem.IsChecked = false;

            // 此时无法视图模式，必须恢复到原先的视图模式才可以
            serialSettingViewMenuItem.IsEnabled = false;
            serialCommunicationSettingViewMenuItem.IsEnabled = false;

            // 切换至简洁视图模式，菜单项选中
            compactViewMenuItem.IsChecked = true;

            // 
            Information("进入简洁视图模式。");
        }

        /// <summary>
        /// 恢复到原来的视图模式
        /// </summary>
        private void RestoreViewMode()
        {
            // 恢复面板显示状态
            serialCommunicationConfigPanel.Visibility = Visibility.Visible;
            serialPortConfigPanel.Visibility = Visibility.Visible;

            // 恢复菜单选中状态
            if (serialPortConfigPanel.Visibility == Visibility.Visible)
            {
                serialSettingViewMenuItem.IsChecked = true;
            }
            
            if (serialCommunicationConfigPanel.Visibility == Visibility.Visible)
            {
                serialCommunicationSettingViewMenuItem.IsChecked = true;
            }

            serialSettingViewMenuItem.IsEnabled = true;
            serialCommunicationSettingViewMenuItem.IsEnabled = true;

            compactViewMenuItem.IsChecked = false;

            // 
            Information("恢复原来的视图模式。");
        }
    }
}
