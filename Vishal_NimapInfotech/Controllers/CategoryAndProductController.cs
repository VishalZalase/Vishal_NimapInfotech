using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vishal_NimapInfotech.Models;

namespace Vishal_NimapInfotech.Controllers
{
    public class CategoryAndProductController : Controller
    {

        CFADBContext objContext;
        public CategoryAndProductController()
        {
            objContext = new CFADBContext();
        }

        /// <summary>
        /// method for add view
        /// </summary>
        /// <returns>return view</returns>
        public ActionResult Index()
        {           
            return View();
        }

        /// <summary>
        /// method for get Category data and Product data list
        /// </summary>
        /// <returns>return Category data and Product data list</returns>
        public ActionResult GetCategory()
        {
            var categoryList = objContext.categoryMst.ToList();
           
            var result = new { categoryList= categoryList };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for Add Category data
        /// </summary>
        /// <param name="categoryMst">this contains category data</param>
        /// <returns>return message</returns>
        public ActionResult AddCategory(CategoryMst categoryMst)
        {
            if (categoryMst.CategoryId>0)
            {
                var checkCategory = objContext.categoryMst.Where(a => a.CategoryId == categoryMst.CategoryId).FirstOrDefault();

                if (checkCategory!=null)
                {
                    checkCategory.CategoryName = Convert.ToString(categoryMst.CategoryName).Trim();
                    objContext.SaveChanges();
                    return Json("Saved successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Already exists", JsonRequestBehavior.AllowGet);
                }
               
            }
            else{
                var checkCategory = objContext.categoryMst.Where(a => a.CategoryName == categoryMst.CategoryName.Trim()).FirstOrDefault();

                if (checkCategory != null)
                {
                    return Json("Already exists", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    objContext.categoryMst.Add(categoryMst);
                    objContext.SaveChanges();
                    //var categoryList = objContext.categoryMst.ToList();
                    return Json("Saved successfully", JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// method for Add Product data
        /// </summary>
        /// <param name="productMst">this contains Product data</param>
        /// <returns>return message</returns>
        public ActionResult AddProduct(ProductMst productMst)
        {
            var checkProduct = objContext.productMst.Where(a => a.ProductName == productMst.ProductName.Trim() && a.CategoryId== productMst.CategoryId).FirstOrDefault();

            if (checkProduct != null)
            {
                return Json("Already exists", JsonRequestBehavior.AllowGet);
            }
            else
            {
                objContext.productMst.Add(productMst);
                objContext.SaveChanges();
                //var productList = objContext.productMst.ToList();
                return Json("Saved successfully", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// method for Update Product data
        /// </summary>
        /// <param name="productMst">this contains Product data</param>
        /// <returns>return message</returns>
        public ActionResult UpdateProduct(ProductMst productMst)
        {
            var checkProduct = objContext.productMst.Where(a => a.ProductId == productMst.ProductId).FirstOrDefault();

            if (checkProduct != null)
            {
                checkProduct.ProductName = productMst.ProductName.Trim();
                objContext.SaveChanges();
                return Json("Saved successfully", JsonRequestBehavior.AllowGet);
            }
            else
            {
               
                return Json("Saved successfully", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// method for delete Category data
        /// </summary>
        /// <param name="id">delete this data</param>
        /// <returns>return if delete successfully then true otherwise 'No data'</returns>
        public ActionResult DeleteCategory(int id)
        {
            var categoryData= objContext.categoryMst.Where(a => a.CategoryId == id).FirstOrDefault();
            if (categoryData!=null)
            {
                objContext.categoryMst.Remove(categoryData);
                objContext.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json("No data", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for delete Product data
        /// </summary>
        /// <param name="id">delete this data</param>
        /// <returns>return if delete successfully then true otherwise 'No data'</returns>
        public ActionResult DeleteProduct(int id)
        {
            var cproductData = objContext.productMst.Where(a => a.ProductId == id).FirstOrDefault();
            if (cproductData != null)
            {
                objContext.productMst.Remove(cproductData);
                objContext.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json("No data", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for load Category grid data
        /// </summary>
        /// <returns>Category data list</returns>
        [HttpPost]
        public JsonResult loadCategorydata()
        {
            List<CategoryMst> categories = (from customer in objContext.categoryMst
                                            select customer).ToList();
            return Json(categories);
        }

        /// <summary>
        /// method for load Product grid data
        /// </summary>
        /// <returns>product data list</returns>
        [HttpPost]
        public JsonResult loadProductdata()
        {
            //List<ProductMst> products = (from customer in objContext.productMst
            //                                select customer).ToList();

            var productAndCategoryList = (from product in objContext.productMst
                                          join category in objContext.categoryMst
                                          on product.CategoryId equals category.CategoryId
                                          select new
                                          {
                                              ProductId = product.ProductId,
                                              ProductName = product.ProductName,
                                              CategoryId = product.CategoryId,
                                              CategoryName = category.CategoryName,
                                          }).ToList();

            return Json(productAndCategoryList);
        }
    }
}