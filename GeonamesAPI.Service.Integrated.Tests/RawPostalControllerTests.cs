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
    public class RawPostalControllerTests
    {
        #region Local fields, test initialization and test clean up setup

        private readonly IConfiguration generalSection, countrySection, postalcodeSection, config;

        public RawPostalControllerTests()
        {
            var configuration = new ConfigurationBuilder()
                           .AddJsonFile("testsettings.json")
                           .Build();

            generalSection = configuration.GetSection("General");
            countrySection = configuration.GetSection("Country");
            postalcodeSection = configuration.GetSection("PostalCode");
            config = configuration;
        }

        private string serverURL = string.Empty;
        private string requestURL = string.Empty;
        private string jsonMediaType = string.Empty;
        private string xmlMediaType = string.Empty;
        private string jsonFormatParameter = string.Empty;
        private string xmlFormatParameter = string.Empty;
        private string pageSizeSegment = string.Empty;
        private string pageNumberSegment = string.Empty;
        private int expectedResultCount = int.MinValue;
        private HttpClientHandler clientHandler;
        private HttpClient client;

        private string countrySegment = string.Empty;
        private string expectedCountryName = string.Empty;
        private string expectedISOCountryCode = string.Empty;
        private string expectedPostalCode = string.Empty;
        private string rawPostalCodeControllerSegment = string.Empty;
        private RawPostalController rawPostalControllerObject;

        [TestInitialize]
        public void InitializelocalFields()
        {
            serverURL = generalSection["ServerURL"];
            jsonMediaType = generalSection["JSONMediaType"];
            xmlMediaType = generalSection["XMLMediaType"];
            jsonFormatParameter = generalSection["JSONFormatParameter"];
            xmlFormatParameter = generalSection["XMLFormatParameter"];
            pageSizeSegment = generalSection["PageSizeSegment"];
            pageNumberSegment = generalSection["PageNumberSegment"];
            expectedResultCount = int.Parse(generalSection["ExpectedResultCount"]);
            countrySegment = generalSection["CountrySegment"];
            expectedCountryName = countrySection["CountryName"];
            clientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            client = new HttpClient(clientHandler);

            expectedPostalCode = postalcodeSection["PostalCode"];
            expectedISOCountryCode = countrySection["ISOCountryCode"];
            rawPostalCodeControllerSegment = postalcodeSection["RawPostalCodeControllerSegment"];
            rawPostalControllerObject = new RawPostalController(new RawPostalSQLRepository(config));
        }

        [TestCleanup]
        public void CleanUpLocalFields()
        {
            serverURL = string.Empty;
            jsonMediaType = string.Empty;
            xmlMediaType = string.Empty;
            jsonFormatParameter = string.Empty;
            xmlFormatParameter = string.Empty;
            pageNumberSegment = string.Empty;
            pageSizeSegment = string.Empty;
            expectedResultCount = int.MinValue;
            countrySegment = string.Empty;
            expectedCountryName = string.Empty;
            clientHandler = null;
            client = null;

            expectedPostalCode = string.Empty;
            expectedISOCountryCode = string.Empty;
            rawPostalCodeControllerSegment = string.Empty;
            rawPostalControllerObject = null;
        }

        #endregion

        #region Integrated tests

        #region Tests for constructor of RawPostalController class

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnNonNullControllerObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(rawPostalControllerObject);
        }

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnAnInstanceOfContinentController()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsInstanceOfType(rawPostalControllerObject, typeof(RawPostalController));
        }

        #endregion

        #region Tests for GetPostalCodesInfo method

        [TestMethod]
        public void GetPostalCodesInfo_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPostalCodesInfo_Executed_ReturnsAValidListOfPostalCodes()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawPostal> result = JsonConvert.DeserializeObject<List<RawPostal>>(response);
            string expected = expectedPostalCode;
            string actual = result.Find(postalCode => postalCode.PostalCode == expectedPostalCode).PostalCode;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName + @"?" + pageNumberSegment + @"&" + pageSizeSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName + @"?" + pageNumberSegment + @"&" + pageSizeSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_PassedAValidCountryName_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedCountryName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawPostal> result = JsonConvert.DeserializeObject<List<RawPostal>>(response);
            string expected = expectedPostalCode;
            string actual = result.Find(postalCode => postalCode.PostalCode == expectedPostalCode).PostalCode;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_PassedAValidISOCountryCode_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedISOCountryCode;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawPostal> result = JsonConvert.DeserializeObject<List<RawPostal>>(response);
            string expected = expectedPostalCode;
            string actual = result.Find(postalCode => postalCode.PostalCode == expectedPostalCode).PostalCode;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetPostalCodesInfo_PassedAPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + rawPostalCodeControllerSegment + @"/" + expectedISOCountryCode + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawPostal> result = JsonConvert.DeserializeObject<List<RawPostal>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual<int>(expectedResultCount, actualResultCount);
        }

        #endregion

        #endregion
    }
}
