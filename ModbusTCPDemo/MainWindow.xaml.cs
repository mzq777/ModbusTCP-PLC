using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HslCommunication;
using HslCommunication.ModBus;

namespace ModbusTCPDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModbusTcpNet busTcpClient;
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            busTcpClient = new ModbusTcpNet(this.txtIP.Text, Int32.Parse(this.txtPort.Text));
            //在Modbus服务器的设备里，大部分的设备都是从地址0开始的，有些特殊的设备是从地址1开始的，所以本组件里面，默认从地址0开始，如果想要从地址1开始，那么就需要如下的配置：
            busTcpClient.AddressStartWithZero = false;
            busTcpClient.ConnectServer();
            MessageBox.Show("连接成功！");
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            busTcpClient.ConnectClose();
            MessageBox.Show("断开连接成功！");
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSet_Click(object sender, RoutedEventArgs e)
        {
            busTcpClient.Write(this.txtSetAddress.Text, Int32.Parse(this.txtSetVal.Text));
            MessageBox.Show("设置成功！");
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            this.txtReadVal.Text = busTcpClient.ReadInt32Async(this.txtReadAddress.Text).Result.Content.ToString();            
        }

    }
}
