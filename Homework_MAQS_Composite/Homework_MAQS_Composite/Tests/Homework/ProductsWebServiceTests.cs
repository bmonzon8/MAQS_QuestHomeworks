using CognizantSoftvision.Maqs.BaseWebServiceTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebService;
using System;
using System.Collections.Generic;

namespace Tests
{
    /// <summary>
    /// Sample test class
    /// </summary>
    [TestClass]
    public class ProductsWebServiceTests : BaseWebServiceTest
    {
        

        /// <summary>
        /// Get single product as Json
        /// </summary>
        [TestCategory("Product WebService Tests")]
        [TestMethod]
        public void GetProductsAsJsonDeserialized()
        {
            List<ProductJson> result = WebServiceDriver.Get<List<ProductJson>>("/api/XML_JSON/GetAllProducts", "application/json", false);

            foreach(ProductJson prod in result)
            {
                Console.WriteLine("The prod id is: " + prod.Id.ToString());
                Console.WriteLine("The prod name is: " + prod.Name.ToString());
            }
            Assert.AreEqual(1, 2, "Forced Failure");
        }

        /// <summary>
        /// Get single product as Json
        /// </summary>
        /// 
        [TestCategory("Product WebService Tests")]
        [TestMethod]
        public void GetSingleProductAsJson()
        {
            ProductJson result = this.WebServiceDriver.Get<ProductJson>("/api/XML_JSON/GetProduct/1", "application/json", false);

            Assert.AreEqual(1, result.Id, "Expected to get product 1");
        }

    }
}