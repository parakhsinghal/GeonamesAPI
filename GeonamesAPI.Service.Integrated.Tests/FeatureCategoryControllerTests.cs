using GeonamesAPI.Domain;
using GeonamesAPI.Service.Controllers;
using GeonamesAPI.SQLRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GeonamesAPI.Service.Integrated.Tests.Controllers
{
    [TestClass]
    public class FeatureCategoryControllerTests
    {
        #region Local fields, test initialization and test clean up setup

        private readonly IConfiguration generalSection, featurecategorySection, config;

        public FeatureCategoryControllerTests()
        {
            var configuration = new ConfigurationBuilder()
                           .AddJsonFile("testsettings.json")
                           .Build();

            generalSection = configuration.GetSection("General");
            featurecategorySection = configuration.GetSection("FeatureCategory");
            config = configuration;
        }

        private string serverURL = string.Empty;
        private string requestURL = string.Empty;
        private string jsonMediaType = string.Empty;
        private string xmlMediaType = string.Empty;
        private string jsonFormatParameter = string.Empty;
        private string xmlFormatParameter = string.Empty;
        private HttpClientHandler clientHandler;
        private HttpClient client;

        private string featureCategoryControllerSegment = string.Empty;
        private string expectedFeatureCategoryId = string.Empty;

        private FeatureCategoryController featureCategoryControllerObject;

        [TestInitialize]
        public void InitializelocalFields()
        {
            serverURL = generalSection["ServerURL"];
            jsonMediaType = generalSection["JSONMediaType"];
            xmlMediaType = generalSection["XMLMediaType"];
            jsonFormatParameter = generalSection["JSONFormatParameter"];
            xmlFormatParameter = generalSection["XMLFormatParameter"];
            clientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            client = new HttpClient(clientHandler);

            featureCategoryControllerSegment = featurecategorySection["FeatureCategoryControllerSegment"];
            expectedFeatureCategoryId = featurecategorySection["FeatureCategoryId"];
            featureCategoryControllerObject = new FeatureCategoryController(new FeatureCategorySQLRepository(config));
        }

        [TestCleanup]
        public void CleanUpLocalFields()
        {
            serverURL = string.Empty;
            jsonMediaType = string.Empty;
            xmlMediaType = string.Empty;
            jsonFormatParameter = string.Empty;
            xmlFormatParameter = string.Empty;
            clientHandler = null;
            client = null;

            featureCategoryControllerSegment = string.Empty;
            expectedFeatureCategoryId = string.Empty;
            featureCategoryControllerObject = null;
        }

        #endregion

        #region Integrated tests

        #region Tests for constructor of FeatureCategoryController class

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnNonNullControllerObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(featureCategoryControllerObject);
        }

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnAnInstanceOfFeatureCategoryController()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsInstanceOfType(featureCategoryControllerObject, typeof(FeatureCategoryController));
        }

        #endregion

        #region Tests for the GetFeatureCategories method

        [TestMethod]
        public void GetFeatureCategories_Executed_ReturnsNonNullResult()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFeatureCategories_Executed_ReturnsAValidListOfFeatureCategories()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<FeatureCategory> result = JsonConvert.DeserializeObject<List<FeatureCategory>>(response);
            string expected = expectedFeatureCategoryId;
            string actual = result.Find(featureCategory => featureCategory.FeatureCategoryId == expectedFeatureCategoryId).FeatureCategoryId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCategories_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCategories_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCategories_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCategories_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCategories_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCategories_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCategories_PassedAValidFeatureCategoryId_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + featureCategoryControllerSegment + @"/" + expectedFeatureCategoryId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            FeatureCategory result = JsonConvert.DeserializeObject<FeatureCategory>(response);
            string actualFeatureCategoryId = result.FeatureCategoryId;

            //Assert
            Assert.AreEqual<string>(expectedFeatureCategoryId, actualFeatureCategoryId);

        }

        [TestMethod]
        public void GetFeatureCategories_PassedAnInValidFeatureCategoryId_ReturnsNotFoundResponse()
        {
            //Arrange
            expectedFeatureCategoryId = "Z";
            requestURL = serverURL + featureCategoryControllerSegment + @"/" + expectedFeatureCategoryId;

            //Act
            HttpStatusCode actualStatusCode = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expectedStatusCode = HttpStatusCode.NotFound;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expectedStatusCode, actualStatusCode);
        }

        #endregion

        #endregion
    }
}
