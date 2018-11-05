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
    public class LanguageControllerTests
    {
        #region Local fields, test initialization and test clean up setup

        private readonly IConfiguration generalSection, languageSection, config;

        public LanguageControllerTests()
        {
            var configuration = new ConfigurationBuilder()
                           .AddJsonFile("testsettings.json")
                           .Build();

            generalSection = configuration.GetSection("General");
            languageSection = configuration.GetSection("Language");
            config = configuration;
        }

        private string serverURL = string.Empty;
        private string requestURL = string.Empty;
        private string jsonMediaType = string.Empty;
        private string xmlMediaType = string.Empty;
        private string jsonFormatParameter = string.Empty;
        private string xmlFormatParameter = string.Empty;
        private string keyValueSegment = string.Empty;
        private string pageSizeSegment = string.Empty;
        private string pageNumberSegment = string.Empty;
        private int expectedResultCount = int.MinValue;
        private HttpClientHandler clientHandler;
        private HttpClient client;

        private string languageControllerSegment = string.Empty;
        private string expectedLanguageName = string.Empty;
        private string expectedISO6393Code = string.Empty;

        private LanguageController languageControllerObject;

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
            keyValueSegment = generalSection["KeyValueSegment"];

            clientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            client = new HttpClient(clientHandler);

            languageControllerSegment = languageSection["LanguageControllerSegment"];
            expectedLanguageName = languageSection["LanguageName"];
            expectedISO6393Code = languageSection["ISO6393Code"];
            languageControllerObject = new LanguageController(new LanguageCodeSQLRepository(config));
        }

        [TestCleanup]
        public void CleanUpLocalFields()
        {
            serverURL = string.Empty;
            jsonMediaType = string.Empty;
            xmlMediaType = string.Empty;
            jsonFormatParameter = string.Empty;
            xmlFormatParameter = string.Empty;
            keyValueSegment = string.Empty;
            pageNumberSegment = string.Empty;
            pageSizeSegment = string.Empty;
            expectedResultCount = int.MinValue;
            clientHandler = null;
            client = null;

            languageControllerSegment = string.Empty;
            expectedLanguageName = string.Empty;
            expectedISO6393Code = string.Empty;
            languageControllerObject = null;
        }

        #endregion

        #region Integrated tests

        #region Tests for construction of LanguageController class

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnNonNullControllerObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(languageControllerObject);
        }

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnAnInstanceOfContinentController()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsInstanceOfType(languageControllerObject, typeof(LanguageController));
        }

        #endregion

        #region Tests for GetAllLanguageCodes method

        [TestMethod]
        public void GetAllLanguageCodes_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllLanguageCodes_Executed_ReturnsAValidListOfLanguages()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<LanguageCode> result = JsonConvert.DeserializeObject<List<LanguageCode>>(response);
            string actualLanguageName = result.Find(language => language.Language == expectedLanguageName).Language;

            //Assert
            Assert.AreEqual<string>(expectedLanguageName, actualLanguageName);
        }

        [TestMethod]
        public void GetAllLanguageCodes_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetAllLanguageCodes_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllLanguageCodes_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllLanguageCodes_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllLanguageCodes_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllLanguageCodes_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllLanguageCodes_PassedValidPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            var response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            int actualResultCount = JsonConvert.DeserializeObject<List<LanguageCode>>(response).Count;

            //Assert
            Assert.AreEqual<int>(expectedResultCount, actualResultCount);
        }

        #endregion

        #region Tests for GetLanguageCode method

        [TestMethod]
        public void GetLanguageCode_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code;

            //Act
            string result = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLanguageCode_Executed_ReturnsAValidLanguage()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            string actualLanguageName = JsonConvert.DeserializeObject<LanguageCode>(response).Language;

            //Assert
            Assert.AreEqual<string>(expectedLanguageName, actualLanguageName);
        }

        [TestMethod]
        public void GetLanguageCode_Executed_ReturnsValidHttpStatusCode()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCode_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCode_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCode_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCode_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCode_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCode_PassedAValidLanguageISO6393Code_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedISO6393Code;

            //Act
            HttpResponseMessage response = client.GetAsync(requestURL).Result;
            HttpStatusCode actualHttpStatusCode = response.StatusCode;
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK;

            string actualResultSet = response.Content.ReadAsStringAsync().Result;
            string actualLanguageName = JsonConvert.DeserializeObject<LanguageCode>(actualResultSet).Language;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expectedHttpStatusCode, actualHttpStatusCode);
            Assert.AreEqual<string>(expectedLanguageName, actualLanguageName);
        }

        [TestMethod]
        public void GetLanguageCode_PassedAValidLanguageName_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + @"/" + expectedLanguageName;

            //Act
            HttpResponseMessage response = client.GetAsync(requestURL).Result;
            HttpStatusCode actualHttpStatusCode = response.StatusCode;
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK;

            string actualResult = response.Content.ReadAsStringAsync().Result;
            string actualLanguageName = JsonConvert.DeserializeObject<LanguageCode>(actualResult).Language;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expectedHttpStatusCode, actualHttpStatusCode);
            Assert.AreEqual<string>(expectedLanguageName, actualLanguageName);
        }

        #endregion

        #region Tests for GetLanguageCodesAsDictionary method

        [TestMethod]
        public void GetLanguageCodesAsDictionary_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + keyValueSegment;

            //Act
            string result = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLanguageCodesAsDictionary_Executed_ReturnsAValidKeyValuePairOfLanguages()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + keyValueSegment;

            //Act
            var response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            var actual = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(response);
            string actualKey = actual.Find(item => item.Key == expectedISO6393Code).Key;
            string actualValue = actual.Find(item => item.Value == expectedLanguageName).Value;
            string expectedKey = expectedISO6393Code;
            string expectedValue = expectedLanguageName;

            Assert.AreEqual<string>(expectedKey, actualKey);
            Assert.AreEqual<string>(expectedValue, actualValue);
        }

        [TestMethod]
        public void GetLanguageCodesAsDictionary_Executed_ReturnsValidHttpStatusCode()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + keyValueSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCodesAsDictionary_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + keyValueSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCodesAsDictionary_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + keyValueSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCodesAsDictionary_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + keyValueSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCodesAsDictionary_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + keyValueSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetLanguageCodesAsDictionary_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + languageControllerSegment + keyValueSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        #endregion

        #endregion
    }
}
