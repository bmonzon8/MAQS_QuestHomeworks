using CognizantSoftvision.Maqs.BaseWebServiceTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models.WebService;
// TODO: Add reference to object model
// using Model;

namespace Tests.Homework
{
    /// <summary>
    /// Test1 test class
    /// </summary>
    [TestClass]
    public class TripsWebServiceTests : BaseTripTests
    {
        /// <summary>
        /// Sample test
        /// </summary>
        [TestCategory("Trip WebService Tests")]
        [TestMethod]
        public void GetUsersAsString()
        {
            try
            {
                var response = this.WebServiceDriver.Get("authTripsAPI/users", "application/json");
                Console.WriteLine("The Response Data is: " + response.ToString());
                Assert.IsTrue(response.Contains("userId"), "Body should contain the userId");

            }
            catch (Exception e)
            {
                Console.WriteLine("The Exception Details are: " + e.Message);
            }
            finally
            {

            }
        }

       
        [TestCategory("Trip WebService Tests")]
        [TestMethod]
        public void GetCurrentUserInfo()
        {
            try
            {
                TripUser result = this.WebServiceDriver.Get<TripUser>("authTripsAPI/users", "application/json",false);

                Console.WriteLine("The Result data is: " + result.ToString());


                //Console.WriteLine("The userId is: " + result.UserId.ToString());

               // Assert.IsTrue(result.Contains("8"), "userid must match who I am");
               


                Assert.IsNotNull(result);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

        }


        [TestCategory("Trip WebService Tests")]
        [TestMethod]
        public void GetTripsAsObjects()
        {
            try
            {
                var response = this.WebServiceDriver.Get<TripJson[]>("authTripsAPI/6/trips", "application/json");
                Console.WriteLine("The response.name value is:  " + response[0].name.ToString());
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {

            }

        }

        [TestCategory("Trip WebService Tests")]
        [TestMethod]
        public void GetTripWithResponse()
        {
            try
            {
                var response = this.WebServiceDriver.GetWithResponse("authTripsAPI/6/trips/1", "application/json", System.Net.HttpStatusCode.OK);
                TripJson result = WebServiceUtils.DeserializeJson<TripJson>(response);
                Console.WriteLine("The response id value is:  " + result.name);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {

            }
        }

        [TestCategory("Trip WebService Tests")]
        [TestMethod]
        public void NotWorking_GetUserWithResponse()
        {
            try
            {
                var response = this.WebServiceDriver.GetWithResponse("authTripsAPI/users", "application/json", System.Net.HttpStatusCode.OK);
                TripUser result = WebServiceUtils.DeserializeJson<TripUser>(response);
                Console.WriteLine("The response id value is:  " + result.NumberOfTrips.ToString());
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {

            }
        }

        [TestCategory("Trip WebService Tests")]
        [TestMethod]
        public void GetUserAsObject()
        {
            try
            {
                var response = this.WebServiceDriver.Get<TripUser>("authTripsAPI/users", "application/json");
                Console.WriteLine("The response user value is:  " + response.UserId);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {

            }

        }

        [TestCategory("Trip WebService Tests")]
        [TestMethod]
        public void GetCurrentUserTrips()
        {
            try
            {

                TripJson result = this.WebServiceDriver.Get<TripJson>("/authTripsAPI/authTripsAPI/3/trips", "application/json", false);

                Console.WriteLine("The Result data is: " + result.ToString());

                Assert.IsNotNull(result);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

        }
        }
    }

