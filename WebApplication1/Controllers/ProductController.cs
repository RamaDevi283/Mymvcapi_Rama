using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        string ConnectionString = @"Data Source =DELL\SQLEXPRESS; Initial Catalog = sakethDB;User ID=sa;Password=123456 ; Integrated Security = true";
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            GetProductsModel Productmodel = new GetProductsModel();
            List<UGCountriesModel> contrieslistOBJ = new List<UGCountriesModel>();
            List<UGProductsModel> productlistsobj = new List<UGProductsModel>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetInitialDetails", con);
                cmd.CommandType =CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    productlistsobj.Add(new UGProductsModel
                    {
                        ProductName = Convert.ToString(row["ProductName"]),
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        Count = Convert.ToInt32(row["Count"]),
                        Price = Convert.ToDecimal(row["Price"]),

                    });
                }
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    contrieslistOBJ.Add(new UGCountriesModel
                    {
                        Country = Convert.ToString(row["Country"]),
                        ID = Convert.ToInt32(row["ID"]),

                    });
                }
             
            }
            Productmodel.Countries = contrieslistOBJ;
            Productmodel.Products = productlistsobj;



            return View(Productmodel);
        }


        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            ProductModel Productmodel = new ProductModel();


            List<UGCountriesModel1> contrieslistOBJ1 = new List<UGCountriesModel1>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetInitialDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    contrieslistOBJ1.Add(new UGCountriesModel1
                    {
                        Country = Convert.ToString(row["Country"]),
                        ID = Convert.ToInt32(row["ID"]),

                    });
                }

            }
            Productmodel.Countries = contrieslistOBJ1;
            ViewBag.Countriesbag = contrieslistOBJ1;

            return View(Productmodel);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productmodel)
        { 

            if(ModelState.IsValid==true)
            {
                return View(productmodel);
             }
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "INSERT INTO Product VALUES(@ProductName,@Price,@Count)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductName", productmodel.ProductName);
                cmd.Parameters.AddWithValue("@Price", productmodel.Price);
                cmd.Parameters.AddWithValue("@Count", productmodel.Count);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel product = new ProductModel();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Productid=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                product.ProductId = Convert.ToInt32(dt.Rows[0][0].ToString());
                product.ProductName = dt.Rows[0][1].ToString();
                product.Price = Convert.ToDecimal(dt.Rows[0][2].ToString());
                product.Count = Convert.ToInt32(dt.Rows[0][3].ToString());
                return View(product);
            }
            else
            {

                return RedirectToAction("Index");
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productmodel)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "UPDATE Product SET ProductName=@ProductName,Price=@Price,Count=@Count WHERE ProductId=@ProductID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", productmodel.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", productmodel.ProductName);
                cmd.Parameters.AddWithValue("@Price", productmodel.Price);
                cmd.Parameters.AddWithValue("@Count", productmodel.Count);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult ViewAlltModel(ProductModel productmodel)
        {
            ViewBag.ProductId = productmodel.ProductId;
            ViewBag.ProductName = productmodel.ProductName;
            ViewBag.Price = productmodel.Price;
            ViewBag.Count = productmodel.Count;
            return View();
        }

    }
}
