 using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace Authorization_Api;

[TestClass]
public class LoginWithModel_Tests
{
    // Model Class
    public class LoginRequestModel
    {
        public string username { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public int expiresInMins { get; set; }
    }

    [TestMethod]
    public void LoginWithModel()
    {
        // Arrange
        RestClient client = new RestClient("https://dummyjson.com");

        RestRequest request = new RestRequest("/auth/login", Method.Post);

        request.AddHeader("Content-Type", "application/json");

        LoginRequestModel loginRequest = new LoginRequestModel()
        {
            username = "emilys",
            password = "emilyspass",
            expiresInMins = 30
        };

        request.AddJsonBody(loginRequest);

        // Act
        RestResponse response = client.Execute(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        JObject jsonResponse = JObject.Parse(response.Content!);

        Assert.IsNotNull(jsonResponse["accessToken"]);

        Assert.AreEqual("emilys",
            jsonResponse["username"]?.ToString());
    }
}
