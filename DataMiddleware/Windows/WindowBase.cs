/*****************************************************

** 作者：董旭阳
** 日期：2019/7/3 11:59:17
** 描述：文件内容或所属项目

** 非文件作者禁止修改或删除文件内容

******************************************************/

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace DataMiddleware.Windows
{
    public class WindowBase : Window
    {
        /// <summary>
        /// 渐显动画
        /// </summary>
        private DoubleAnimation m_FadeInAnimation;

        /// <summary>
        /// 构造
        /// </summary>
        public WindowBase()
        {
            this.IsVisibleChanged += WindowsBase_IsVisibleChanged;
        }

        /// <summary>
        /// 显示属性变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowsBase_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                m_FadeInAnimation = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
                BeginAnimation(OpacityProperty, m_FadeInAnimation);
            }
        }
    }
}
