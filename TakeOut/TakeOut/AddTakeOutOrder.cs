using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DormManagementApp
{
    public partial class AddTakeOutOrder : Form
    {
        public AddTakeOutOrder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string takeoutorderid = textBox1.Text;
            string phonenumber = textBox2.Text;

            if (String.IsNullOrEmpty(takeoutorderid))
            {
                MessageBox.Show("订单号不能为空！");
            }
            if (String.IsNullOrEmpty(phonenumber))
            {
                MessageBox.Show("手机号不能为空！");
            }

            //AddTakeOutOrder
            //获取webApi数据的地址。
            Uri uri = new Uri("http://localhost:5001/api/TakeOut/AddOrder?PhoneNumber=" + phonenumber
                              + "&TakeOutOrderID=" + takeoutorderid);
            //WebClient:获取webaapi中的数据。
            using (WebClient c = new WebClient())
            {
                //c.Headers["Type"]:获取数据的方式
                c.Headers["Type"] = "GET";
                //c.Headers["Accept"]:获取数据的格式
                c.Headers["Accept"] = "application/json";
                //c.Encoding：数据的编码格式。
                c.Encoding = Encoding.UTF8;
                //DownloadDataCompleted:在多线程情况下，能进行webApi数据传输。
                c.DownloadStringCompleted += (senderobj, es) =>
                {
                    //es.Result:如果获取的webapi之中的数据不为空，则绑定gv
                    if (es.Result != null)
                    {
                        if (es.Result == "\"OK\"")
                        {
                            MessageBox.Show("录入成功");
                            this.Close();
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("手机号输入错误，无法查找到对应学生，录入失败");
                        }
                    }
                };
                //把当前webApi地址释放掉。
                c.DownloadStringAsync(uri);
            }
        }

    }
}

