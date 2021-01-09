/*****************************************************

** 作者：董旭阳
** 日期：2019/6/26 14:48:28
** 描述：文件内容或所属项目

** 非文件作者禁止修改或删除文件内容

******************************************************/

using System;
using System.Threading;
using System.Windows;

namespace DataMiddleware.Windows
{
    /// <summary>
    /// 确认窗口
    /// </summary>
    public partial class ConfirmWindow : WindowBase
    {
        /// <summary>
        /// 倒计时时间
        /// </summary>
        private readonly int m_Second = 5;

        /// <summary>
        /// 当前值
        /// </summary>
        private int m_CurrentSecond = 5;

        /// <summary>
        /// 计时器
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// 构造
        /// </summary>
        public ConfirmWindow()
        {
            InitializeComponent();

            m_Timer = new Timer(TimerCallback, null, 1000, 1000);
        }

        /// <summary>
        /// 定时器回调
        /// </summary>
        /// <param name="obj"></param>
        private void TimerCallback(object obj)
        {
            if (m_CurrentSecond <= 1)
            {
                if (m_Timer != null)
                {
                    m_Timer.Dispose();
                    m_Timer = null;
                }

                Dispatcher.Invoke(() =>
                {
                    this.DialogResult = true;
                });
            }
            else
            {
                m_CurrentSecond--;
                Dispatcher.Invoke(() =>
                {
                    TimerBlock.Text = "(" + m_CurrentSecond + ")";
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            m_Timer.Dispose();
            m_Timer = null;

            this.DialogResult = false;
        }
    }
}
