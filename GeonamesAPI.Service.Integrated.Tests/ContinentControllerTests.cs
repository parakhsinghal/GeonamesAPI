using GeonamesAPI.Domain;
using GeonamesAPI.Service.Controllers;
using GeonamesAPI.SQLRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GeonamesAPI.Service.Integrated.Tests.Controllers
{
    [TestClass]
    public class ContinentControllerTests
    {
        #region Local fields, test initialization and test clean up setup

        private readonly IConfiguration generalSection, continentSection, countrySection, featurecategorySection, featurecodeSection, config;

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

        private string countrySegment = string.Empty;
        private string expectedCountryName = string.Empty;
        private long? expectedCountryGeonameId = null;
        private string continentControllerSegment = string.Empty;
        private string expectedContinentName = string.Empty;
        private long? expectedContinentGeonameId = null;
        private string expectedContinentCodeId = string.Empty;
        private ContinentController continentControllerObject;

        public ContinentControllerTests()
        {
            var configuration = new ConfigurationBuilder()
                            .AddJsonFile("testsettings.json")
                            .Build();

            generalSection = configuration.GetSection("General");
            continentSection = configuration.GetSection("Continent");
            countrySection = configuration.GetSection("Country");
            featurecategorySection = configuration.GetSection("FeatureCategory");
            featurecodeSection = configuration.GetSection("FeatureCode");
            config = configuration;
        }

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
            countrySegment = generalSection["CountrySegment"];
            expectedCountryName = countrySection["CountryName"];
            expectedCountryGeonameId = Convert.ToInt64(countrySection["CountryGeonameId"]);
            clientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            client = new HttpClient(clientHandler);

            continentControllerSegment = continentSection["ContinentControllerSegment"];
            expectedContinentName = continentSection["ContinentName"];
            expectedContinentGeonameId = long.Parse(continentSection["ContinentGeonameId"]);
            expectedContinentCodeId = continentSection["ContinentCodeId"];
            continentControllerObject = new ContinentController(new ContinentSQLRepository(config));
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
            countrySegment = string.Empty;
            expectedCountryName = string.Empty;
            expectedCountryGeonameId = null;
            clientHandler = null;
            client = null;

            continentControllerSegment = string.Empty;
            expectedContinentName = string.Empty;
            expectedContinentGeonameId = null;
            expectedContinentCodeId = string.Empty;
            continentControllerObject = null;
        }

        #endregion

        #region Integrated tests

        #region Tests for constructor of ContinentController class

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnNonNullControllerObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(continentControllerObject);
        }

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnAnInstanceOfContinentController()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsInstanceOfType(continentControllerObject, typeof(ContinentController));
        }

        #endregion

        #region Tests for GetAllContinents method

        [TestMethod]
        public void GetAllContinents_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllContinents_Executed_ReturnsAValidListOfContinents()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Continent> result = JsonConvert.DeserializeObject<List<Continent>>(response);
            string expected = expectedContinentName;
            string actual = result.Find(continent => continent.ASCIIName == expectedContinentName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GeoAllContinents_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetAllContinents_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllContinents_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllContinents_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllContinents_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetAllContinents_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        #endregion

        #region Tests for GetContinentsAsDictionary method

        [TestMethod]
        public void GetContinentsAsDictionary_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + keyValueSegment;

            //Act
            string result = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetContinentsAsDictionary_Executed_ReturnsAValidKeyValuePairOfContinents()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + keyValueSegment;

            //Act
            var response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            var arrayObject = JsonConvert.DeserializeObject<object[]>(response);
            var actual = JsonConvert.DeserializeObject<List<KeyValuePair<long, string>>>(response);
            long actualKey = actual.Find(item => item.Key == expectedContinentGeonameId).Key;
            string actualValue = actual.Find(item => item.Value == expectedContinentName).Value;
            long? expectedKey = expectedContinentGeonameId.Value;
            string expectedValue = expectedContinentName;

            Assert.AreEqual(expectedKey, actualKey);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void GetContinentsAsDictionary_Executed_ReturnsValidHttpStatusCode()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + keyValueSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetContinentsAsDictionary_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + keyValueSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentsAsDictionary_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + keyValueSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentsAsDictionary_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + keyValueSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentsAsDictionary_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + keyValueSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentsAsDictionary_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + keyValueSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        #endregion

        #region Tests for GetContinentInfo method

        [TestMethod]
        public void GetContinentInfo_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId;

            //Act
            string result = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetContinentInfo_Executed_ReturnsAValidContinent()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            string actual = JsonConvert.DeserializeObject<Continent>(response).ASCIIName;
            string expected = expectedContinentName;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentInfo_Executed_ReturnsValidHttpStatusCode()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetContinentInfo_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentInfo_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentInfo_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentInfo_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentInfo_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetContinentInfo_PassedANegativeContinentGeonameId_ReturnsBadRequestAsResponse()
        {
            //Arrange
            expectedContinentGeonameId = -1;
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentGeonameId;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.BadRequest;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetContinentInfo_PassedAValidContinentName_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentName;

            //Act
            HttpResponseMessage response = client.GetAsync(requestURL).Result;
            HttpStatusCode actualHttpStatusCode = response.StatusCode;
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK;

            string responseContents = response.Content.ReadAsStringAsync().Result;
            string actualContinentName = JsonConvert.DeserializeObject<Continent>(responseContents).ASCIIName;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expectedHttpStatusCode, actualHttpStatusCode);
            Assert.AreEqual<string>(expectedContinentName, actualContinentName);
        }

        [TestMethod]
        public void GetContinentInfo_PassedAValidContinentCodeId_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId;

            //Act
            HttpResponseMessage response = client.GetAsync(requestURL).Result;
            HttpStatusCode actualHttpStatusCode = response.StatusCode;
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK;

            string responseContents = response.Content.ReadAsStringAsync().Result;
            string actualContinentName = JsonConvert.DeserializeObject<Continent>(responseContents).ASCIIName;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expectedHttpStatusCode, actualHttpStatusCode);
            Assert.AreEqual<string>(expectedContinentName, actualContinentName);
        }

        [TestMethod]
        public void GetContinentInfo_PassedAValidContinentGeonameId_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentGeonameId;

            //Act
            HttpResponseMessage response = client.GetAsync(requestURL).Result;
            HttpStatusCode actualHttpStatusCode = response.StatusCode;
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK;

            string responseContents = response.Content.ReadAsStringAsync().Result;
            string actualContinentName = JsonConvert.DeserializeObject<Continent>(responseContents).ASCIIName;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expectedHttpStatusCode, actualHttpStatusCode);
            Assert.AreEqual<string>(expectedContinentName, actualContinentName);
        }

        // We can still make some tests to test for invalid values of continent name, 
        // continent code ids, continent geoname ids
        // to provide more code coverage.

        #endregion

        #region Tests for GetCountriesInAContinent method

        [TestMethod]
        public void GetCountriesInAContinent_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment;

            //Act
            string result = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCountriesInAContinent_Executed_ReturnsAValidListOfCountries()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> countryCollection = JsonConvert.DeserializeObject<List<Country>>(response);
            string actual = countryCollection.Find(country => country.CountryName == expectedCountryName).CountryName;
            string expected = expectedCountryName;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinent_Executed_ReturnsValidHttpStatusCode()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinent_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinent_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinent_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinent_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinent_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinent_PassedANegativeContinentGeonameId_ReturnsBadRequestAsResponse()
        {
            //Arrange
            expectedContinentGeonameId = -1;
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentGeonameId + @"/" + countrySegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.BadRequest;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinent_PassedAValidContinentName_ReturnsValidResponse()
        {
            //Arrange            
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentName + @"/" + countrySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> result = JsonConvert.DeserializeObject<List<Country>>(response);
            string actualCountry = result.Find(country => country.CountryName == expectedCountryName).CountryName;

            //Assert
            Assert.AreEqual<string>(expectedCountryName, actualCountry);
        }

        [TestMethod]
        public void GetCountriesInAContinent_PassedAValidContinentCodeId_ReturnsValidResponse()
        {
            //Arrange            
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> result = JsonConvert.DeserializeObject<List<Country>>(response);
            string actualCountry = result.Find(country => country.CountryName == expectedCountryName).CountryName;

            //Assert
            Assert.AreEqual<string>(expectedCountryName, actualCountry);
        }

        [TestMethod]
        public void GetCountriesInAContinent_PassedAValidContinentGeonameId_ReturnsValidResponse()
        {
            //Arrange            
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentGeonameId + @"/" + countrySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> result = JsonConvert.DeserializeObject<List<Country>>(response);
            string actualCountry = result.Find(country => country.CountryName == expectedCountryName).CountryName;

            //Assert
            Assert.AreEqual<string>(expectedCountryName, actualCountry);
        }

        [TestMethod]
        public void GetCountriesInAContinent_PassedAPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentGeonameId + @"/" + countrySegment + @"?" + pageSizeSegment + @"&" + pageNumberSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> result = JsonConvert.DeserializeObject<List<Country>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual(expectedResultCount, actualResultCount);
        }

        // We can still make some tests to test for invalid values of continent name, 
        // continent code ids, continent geoname ids, page number and page size
        // to provide more code coverage.

        #endregion

        #region Tests for GetCountriesInAContinentAsDictionary method

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + countrySegment + keyValueSegment;

            //Act
            string result = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_Executed_ReturnsAValidKeyValuePairOfContinents()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + countrySegment + keyValueSegment;

            //Act
            var response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            var arrayObject = JsonConvert.DeserializeObject<object[]>(response);
            var actual = JsonConvert.DeserializeObject<List<KeyValuePair<long, string>>>(response);
            long actualKey = actual.Find(item => item.Key == expectedCountryGeonameId.Value).Key;
            string actualValue = actual.Find(item => item.Value == expectedCountryName).Value;
            long? expectedKey = expectedCountryGeonameId.Value;
            string expectedValue = expectedCountryName;

            Assert.AreEqual(expectedKey, actualKey);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_Executed_ReturnsValidHttpStatusCode()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + countrySegment + keyValueSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + countrySegment + keyValueSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + countrySegment + keyValueSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + countrySegment + keyValueSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + countrySegment + keyValueSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + countrySegment + keyValueSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_PassedANegativeContinentGeonameId_ReturnsBadRequestAsResponse()
        {
            //Arrange
            expectedContinentGeonameId = -1;
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentGeonameId + @"/" + countrySegment + keyValueSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.BadRequest;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_PassedAValidContinentName_ReturnsValidResponse()
        {
            //Arrange            
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentName + @"/" + countrySegment + keyValueSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            var actualResultSet = JsonConvert.DeserializeObject<List<KeyValuePair<long?, string>>>(response);
            string actualCountryName = actualResultSet.Find(country => country.Key == expectedCountryGeonameId).Value;

            //Assert
            Assert.AreEqual<string>(expectedCountryName, actualCountryName);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_PassedAValidContinentCodeId_ReturnsValidResponse()
        {
            //Arrange            
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentCodeId + @"/" + countrySegment + keyValueSegment;

            //Act            
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            var actualResultSet = JsonConvert.DeserializeObject<List<KeyValuePair<long?, string>>>(response);
            string actualCountryName = actualResultSet.Find(country => country.Key == expectedCountryGeonameId).Value;

            //Assert
            Assert.AreEqual<string>(expectedCountryName, actualCountryName);
        }

        [TestMethod]
        public void GetCountriesInAContinentAsDictionary_PassedAValidContinentGeonameId_ReturnsValidResponse()
        {
            //Arrange            
            requestURL = serverURL + continentControllerSegment + @"/" + expectedContinentGeonameId + @"/" + countrySegment + keyValueSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            var actualResultSet = JsonConvert.DeserializeObject<List<KeyValuePair<long?, string>>>(response);
            string actualCountryName = actualResultSet.Find(country => country.Key == expectedCountryGeonameId).Value;

            //Assert
            Assert.AreEqual<string>(expectedCountryName, actualCountryName);
        }

        // We can still make some tests to test for invalid values of continent name, 
        // continent code ids, continent geoname ids
        // to provide more code coverage.

        #endregion

        #endregion
    }
}
