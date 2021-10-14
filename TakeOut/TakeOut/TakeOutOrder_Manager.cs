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
    public partial class TakeOutOrder_Manager : Form
    {
        public TakeOutOrder_Manager()
        {
            InitializeComponent();
        }

        private void TakeOutOrder_Student_Load(object sender, EventArgs e)
        {
            DataGridviewload();
        }

        public void DataGridviewload()
        {
            //url WebApi 
            Uri uri = new Uri("http://localhost:5001/api/TakeOut/DateGetTakeOutOrder?Date=" + dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            //WebClient:获取webapi中的数据。
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
                    if (es.Result != null)
                    {
                        //把webApi之中获取的json数据 序列化成dataTable
                        var test = JsonConvert.DeserializeObject<DataTable>(es.Result);
                        //GV绑定dataTable数据
                        dataGridView1.DataSource = test;
                    }
                };
                //把当前webApi地址释放掉。
                c.DownloadStringAsync(uri);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddTakeOutOrder form = new AddTakeOutOrder();
            form.ShowDialog();
            DataGridviewload();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;//获得选中行的索引
            string takeoutorderid = dataGridView1.Rows[rowindex].Cells["TakeOutOrderID"].Value.ToString();

            DialogResult result = MessageBox.Show("确定删除该订单信息？", "删除", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Uri uri = new Uri("http://localhost:5001/api/TakeOut/DeleteOrder?TakeOutOrderID= " + takeoutorderid);

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
                                MessageBox.Show("删除成功");
                            }
                            else
                            {
                                MessageBox.Show("删除失败");
                            }
                        }
                    };
                    //把当前webApi地址释放掉。
                    c.DownloadStringAsync(uri);
                }
            }
            DataGridviewload();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;//获得选中行的索引
            string studentid = dataGridView1.Rows[rowindex].Cells["StudentID"].Value.ToString();
            string name = dataGridView1.Rows[rowindex].Cells["StudentName"].Value.ToString();
            string phonenumber = dataGridView1.Rows[rowindex].Cells["PhoneNumber"].Value.ToString();
            string takeoutorderid = dataGridView1.Rows[rowindex].Cells["TakeOutOrderID"].Value.ToString();
            string date = dataGridView1.Rows[rowindex].Cells["Date"].Value.ToString();
            string state = dataGridView1.Rows[rowindex].Cells["State"].Value.ToString();

            if (state == "已取")
            {
                MessageBox.Show("外卖状态为已取，无法修改");
            }
            else
            {
                UpdateTakeOutOrder form = new UpdateTakeOutOrder(studentid, name, phonenumber, takeoutorderid, date);
                form.ShowDialog();
                DataGridviewload();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataGridviewload();
        }
    }
}
