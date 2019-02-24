using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;

namespace AzureDemoTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Get_Number1andNumber_2NotDefined()
        {
            var client = MakeClient();
            var request = MakeRequest();
            var response = client.Get(request);
            Assert.AreEqual("\"Number 1 not passed\"", response.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [TestMethod]
        public void Get_Number2_NotDefined()
        {
            var client = MakeClient();
            var request = MakeRequest();
            request.AddQueryParameter("Number1", "4");
            var response = client.Get(request);
            Assert.AreEqual("\"Number 2 not passed\"", response.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void Get_Number1_NotDefined()
        {
            var client = MakeClient();
            var request = MakeRequest();
            request.AddQueryParameter("Number2", "3");
            var response = client.Get(request);
            Assert.AreEqual("\"Number 1 not passed\"", response.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [TestMethod]
        public void Get_Number1andNumber2_Defined()
        {
            var client = MakeClient();
            var request = MakeRequest();
            request.AddQueryParameter("Number1", "5");
            request.AddQueryParameter("Number2", "6");
            var response = client.Get(request);
            Assert.AreEqual("11", response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        private IRestClient MakeClient()
        {
            var url = ConfigurationManager.AppSettings["Url"];
            var client = new RestClient(url);

            return client;
        }

        private IRestRequest MakeRequest()
        {
            var appKey = ConfigurationManager.AppSettings["AzureAppKey"];
            var SimpleFunctionName = ConfigurationManager.AppSettings["functionName"];
            var Request = new RestRequest(SimpleFunctionName, Method.GET);
            Request.AddQueryParameter("Code", appKey);
            return Request;

        }
    }
}
