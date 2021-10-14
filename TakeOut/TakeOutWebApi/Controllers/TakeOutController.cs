using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;





namespace DormManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakeOutController : ControllerBase
    {
        private readonly TakeOutOrderContext TakeOutDb;

        //构造函数把TakeOutContext 作为参数，Asp.net core 框架可以自动注入TakeOutContext对象
        public TakeOutController(TakeOutOrderContext context)
        {
            this.TakeOutDb = context;
        }

        // GET: api/TakeOut/SelectTakeOutOrder
        [Route("SelectTakeOutOrder")]
        [HttpGet]
        public List<TakeOutOrder> Tables()
        {

            var row = (from i in TakeOutDb.TakeOutOrders
                       select i).ToList();

            return row;
        } //输出所有外卖订单信息

        // GET: api/TakeOut/StuIDGetTakeOutOrder？StudentID=1
        [Route("StuIDGetTakeOutOrder")]
        [HttpGet]
        public List<TakeOutOrder> StuIDGetTakeOutOrder(string studentid)
        {
            IQueryable<TakeOutOrder> query = TakeOutDb.TakeOutOrders;

            query = query.Where(t => t.StudentID == Convert.ToInt32(studentid));

            return query.ToList();

        } //根据学号查询外卖信息

        // GET: api/TakeOut/DateGetTakeOutOrder？Date=2020-01-01
        [Route("DateGetTakeOutOrder")]
        [HttpGet]
        public List<TakeOutOrder> DateGetTakeOutOrder(string date)
        {
            IQueryable<TakeOutOrder> query = TakeOutDb.TakeOutOrders;

            query = query.Where(t => t.Date == date);

            return query.ToList();

        } //根据日期查询外卖信息

        //Post: api/TakeOut/AddOrder?PhoneNumber=1234567&TakeOutOrderID=1
        [Route("AddOrder")]
        [HttpPost]       
        public string AddOrder(string phonenumber,string takeoutorderid)
        {
            var student = TakeOutDb.Students.FirstOrDefault(t => t.PhoneNumber == Convert.ToInt32(phonenumber));
            if (student != null)
            {
                TakeOutOrder t = new TakeOutOrder();
                t.StudentID = student.StudentID;
                t.Name = student.Name;
                t.PhoneNumber = student.PhoneNumber;
                t.TakeOutOrderID = Convert.ToInt32(takeoutorderid);
                t.Date = DateTime.Now.ToString("yyyy-MM-dd");
                t.State = "已达";
                TakeOutDb.TakeOutOrders.Add(t);
                TakeOutDb.SaveChanges();
                return "OK";
            }
            else return "False";
        } //录入外卖订单信息

        // PUT: api/TakeOut/UpdateOrder?TakeOutOrderID=1
        [Route("UpdateOrder")]
        [HttpPut]
        public string UpdateOrder(string takeoutorderid)
        {
           var takeoutorder = TakeOutDb.TakeOutOrders.FirstOrDefault(t => t.TakeOutOrderID == Convert.ToInt32(takeoutorderid));
           TakeOutOrder t = new TakeOutOrder();
            t.TakeOutOrderID = takeoutorder.TakeOutOrderID;
            t.StudentID = takeoutorder.StudentID;
            t.Name = takeoutorder.Name;
            t.PhoneNumber = takeoutorder.PhoneNumber;
            t.Date = takeoutorder.Date;
            t.State = "已取";
            TakeOutDb.Entry(t).State = EntityState.Modified;
            TakeOutDb.SaveChanges();
            return "OK";
        }

        // DELETE: api/TakeOut/DeleteOrder?TakeOutOrderID=1
        [Route("DeleteOrder")]
        [HttpDelete]
        public string DeleteOrder(string takeoutorderid)
        {
           var takeoutorder = TakeOutDb.TakeOutOrders.FirstOrDefault(t => t.TakeOutOrderID == Convert.ToInt32(takeoutorderid));
            if (takeoutorder != null)
            {
                TakeOutDb.TakeOutOrders.Remove(takeoutorder);
                TakeOutDb.SaveChanges();
                return "OK";
            }
           else return "False";
        } //删除外卖订单信息

    }
}
