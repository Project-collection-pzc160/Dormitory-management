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

namespace DormManagementApp
{
    public partial class UpdateTakeOutOrder : Form
    {
        private string studentid = "";
        private string name = "";
        private string phonenumber = "";
        private string takeoutorderid = "";
        private string date = "";

        public UpdateTakeOutOrder()
        {
            InitializeComponent();
        }

        public UpdateTakeOutOrder(string studentid, string name, string phonenumber, string takeoutorderid, string date) : this()
        {
            this.studentid = studentid;
            this.name = name;
            this.takeoutorderid = takeoutorderid;
            this.phonenumber = phonenumber;
            this.date = date;
        }


        private void UpdateTakeOutOrder_Load(object sender, EventArgs e)
        {
            label6.Text = studentid;
            label7.Text = name;
            label8.Text = takeoutorderid;
            label9.Text = phonenumber;
            label11.Text = date;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false)
            {
                MessageBox.Show("订单状态未修改");
            }
            else
            {
                Uri uri = new Uri("http://localhost5001/api/TakeOut/UpdateOrder?TakeOutOrderID= " + takeoutorderid);

                using (WebClient c = new WebClient())
                {
                    //c.Headers["Type"]:获取数据的方式
                    c.Headers["Type"] = "Get";
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
                                MessageBox.Show("修改成功");
                                this.Close();
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                    };
                    //把当前webApi地址释放掉。
                    c.DownloadStringAsync(uri);
                }
            }
        }
    }
}
