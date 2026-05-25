using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Authorization_Api;

[TestClass]
public class Authorization_Tests
{
    [TestMethod]
    public void Login_With_Valid_Credentials_Should_Return_Access_Token()
    {
        // Arrange
        RestClient client = new RestClient("https://dummyjson.com");

        RestRequest request = new RestRequest("/auth/login", Method.Post);

        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(new
        {
            username = "emilys",
            password = "emilyspass",
            expiresInMins = 30
        });

        // Act
        RestResponse response = client.Execute(request);

           // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        Assert.IsFalse(string.IsNullOrEmpty(response.Content));

        JObject jsonResponse = JObject.Parse(response.Content);

        Assert.IsNotNull(jsonResponse["accessToken"]);
        Assert.AreEqual("emilys", jsonResponse["username"]?.ToString());
        Assert.AreEqual("emily.johnson@x.dummyjson.com", jsonResponse["email"]?.ToString());
    }
}

