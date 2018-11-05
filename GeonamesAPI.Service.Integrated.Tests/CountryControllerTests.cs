using GeonamesAPI.Domain;
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
    public class CountryControllerTests
    {
        private readonly IConfiguration generalSection, continentSection, countrySection, featurecategorySection, featurecodeSection;

        public CountryControllerTests()
        {
            var configuration = new ConfigurationBuilder()
                           .AddJsonFile("testsettings.json")
                           .Build();

            generalSection = configuration.GetSection("General");
            continentSection = configuration.GetSection("Continent");
            countrySection = configuration.GetSection("Country");
            featurecategorySection = configuration.GetSection("FeatureCategory");
            featurecodeSection = configuration.GetSection("FeatureCode");
        }

        #region Local fields, test initialization and test clean up setup

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

        private string expectedCountryName = string.Empty;
        private long? expectedCountryGeonameId = null;
        private string expectedISOCountryCode = string.Empty;
        private string expectedStateName = string.Empty;
        private long expectedStateGeonameId = long.MinValue;
        private string expectedCityName = string.Empty;
        private long expectedCityGeonameId = long.MinValue;
        private string expectedFeatureCategoryId = string.Empty;
        private string expectedFeatureCode = string.Empty;
        private string countryControllerSegment = string.Empty;
        private string stateSegment = string.Empty;
        private string citySegment = string.Empty;

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

            expectedCountryGeonameId = Convert.ToInt64(countrySection["CountryGeonameId"]);
            expectedISOCountryCode = countrySection["ISOCountryCode"];
            expectedStateName = countrySection["StateName"];
            expectedStateGeonameId = long.Parse(countrySection["StateGeonameId"]);
            expectedCityName = countrySection["CityName"];
            expectedCityGeonameId = long.Parse(countrySection["CityGeonameId"]);
            clientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            client = new HttpClient(clientHandler);

            countryControllerSegment = countrySection["CountryController"];
            expectedFeatureCategoryId = featurecategorySection["FeatureCategoryId"].ToString();
            expectedFeatureCode = featurecodeSection["FeatureCode"].ToString();
            stateSegment = generalSection["StateSegment"];
            citySegment = generalSection["CitySegment"];
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
            expectedCountryName = string.Empty;
            expectedISOCountryCode = string.Empty;
            expectedCountryGeonameId = null;
            expectedStateName = string.Empty;
            expectedStateGeonameId = long.MinValue;
            expectedCityName = string.Empty;
            expectedCityGeonameId = long.MinValue;
            clientHandler = null;
            client = null;

            countryControllerSegment = string.Empty;
            expectedFeatureCategoryId = string.Empty;
            expectedFeatureCode = string.Empty;
            stateSegment = string.Empty;
            citySegment = string.Empty;
        }

        #endregion

        #region Tests for GetCountries method

        [TestMethod]
        public void GetCountries_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCountries_Executed_ReturnsAValidListOfCountries()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> result = JsonConvert.DeserializeObject<List<Country>>(response);
            string expected = expectedCountryName;
            string actual = result.Find(country => country.CountryName == expectedCountryName).CountryName;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountries_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCountries_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountries_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountries_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountries_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountries_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountries_PassedAPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> result = JsonConvert.DeserializeObject<List<Country>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual<int>(expectedResultCount, actualResultCount);
        }

        #endregion

        #region Tests for GetCountryInfo methods

        [TestMethod]
        public void GetCountryInfo_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCountryInfo_Executed_ReturnsSomeData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            Country result = JsonConvert.DeserializeObject<Country>(response);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCountryInfo_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCountryInfo_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryInfo_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryInfo_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryInfo_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryInfo_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryInfo_PassedValidISOCountryCode_ReturnsValidCountryData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            Country result = JsonConvert.DeserializeObject<Country>(response);
            string actualCountryname = result.CountryName;

            //Assert
            Assert.AreEqual<string>(expectedCountryName, actualCountryname);
        }

        [TestMethod]
        public void GetCountryInfo_PassedValidCountryName_ReturnsValidCountryData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            Country result = JsonConvert.DeserializeObject<Country>(response);
            string actualCountryname = result.CountryName;

            //Assert
            Assert.AreEqual<string>(expectedCountryName, actualCountryname);
        }

        #endregion

        #region Tests for GetCountryFeatureCategoryFeatureCode method

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_Executed_ReturnsSomeData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_PassedValidISOCountryCodeAndFeatureCategoryId_ReturnsValidRawData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string actualCityName = result.Find(rawData => rawData.ASCIIName == expectedCityName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_PassedValidISOCountryCodeAndFeatureCategoryIdAndFeatureCode_ReturnsValidRawData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId + @"/" + expectedFeatureCode.Split(new char[] { '.' })[1];

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string actualStateName = result.Find(rawData => rawData.ASCIIName == expectedStateName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedStateName, actualStateName);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_PassedValidCountryNameAndFeatureCategoryId_ReturnsValidRawData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + expectedFeatureCategoryId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string actualCityName = result.Find(rawData => rawData.ASCIIName == expectedCityName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_PassedValidCountryNameAndFeatureCategoryIdAndFeatureCode_ReturnsValidRawData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + expectedFeatureCategoryId + @"/" + expectedFeatureCode.Split(new char[] { '.' })[1];

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string actualStateName = result.Find(rawData => rawData.ASCIIName == expectedStateName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedStateName, actualStateName);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_PassedValidCountryNameAndPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + expectedFeatureCategoryId + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual<int>(expectedResultCount, actualResultCount);
        }

        [TestMethod]
        public void GetCountryFeatureCategoryFeatureCode_PassedValidISOCountryCodeAndPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + expectedFeatureCategoryId + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual<int>(expectedResultCount, actualResultCount);
        }
        #endregion

        #region Tests for GetStates method

        [TestMethod]
        public void GetStates_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetStates_Executed_ReturnsAValidListOfStates()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string expected = expectedStateName;
            string actual = result.Find(rawData => rawData.ASCIIName == expectedStateName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStates_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetStates_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStates_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStates_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStates_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStates_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStates_PassedValidCountryNameAndPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> result = JsonConvert.DeserializeObject<List<Country>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual<int>(expectedResultCount, actualResultCount);
        }

        [TestMethod]
        public void GetStates_PassedValidSOCoutnryCodeAndPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + stateSegment + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Country> result = JsonConvert.DeserializeObject<List<Country>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual<int>(expectedResultCount, actualResultCount);
        }

        [TestMethod]
        public void GetStates_PassedValidCountryName_ReturnsAValidListOfStates()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + @"/" + stateSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string expected = expectedStateName;
            string actual = result.Find(rawData => rawData.ASCIIName == expectedStateName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStates_PassedValidISOCountryCode_ReturnsAValidListOfStates()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + @"/" + stateSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string expected = expectedStateName;
            string actual = result.Find(rawData => rawData.ASCIIName == expectedStateName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        #endregion

        #region Tests for GetStateInfo method

        [TestMethod]
        public void GetStateInfo_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetStateInfo_Executed_ReturnsSomeData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetStateInfo_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetStateInfo_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStateInfo_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStateInfo_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStateInfo_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStateInfo_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetStateInfo_PassedValidISOCountryCodeAndStateName_ReturnsValidStateData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualStateName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedStateName, actualStateName);
        }

        [TestMethod]
        public void GetStateInfo_PassedValidISOCountryCodeAndStateGeonameId_ReturnsValidStateData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateGeonameId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualStateName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedStateName, actualStateName);
        }

        [TestMethod]
        public void GetStateInfo_PassedValidCountryNameAndStateName_ReturnsValidStateData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + stateSegment + @"/" + expectedStateName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualStateName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedStateName, actualStateName);
        }

        [TestMethod]
        public void GetStateInfo_PassedValidCountryNameAndStateGeonameId_ReturnsValidStateData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + stateSegment + @"/" + expectedStateGeonameId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualStateName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedStateName, actualStateName);
        }

        #endregion

        #region Tests for GetCitiesInState method

        [TestMethod]
        public void GetCitiesInState_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCitiesInState_Executed_ReturnsSomeData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCitiesInState_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCitiesInState_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCitiesInState_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCitiesInState_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCitiesInState_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCitiesInState_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCitiesInState_PassedValidISOCountryCodeAndStateName_ReturnsValidListOfCities()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string actualCityName = result.Find(city => city.ASCIIName == expectedCityName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCitiesInState_PassedValidISOCountryCodeAndStateGeonameId_ReturnsValidListOfCities()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateGeonameId + citySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string actualCityName = result.Find(city => city.ASCIIName == expectedCityName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCitiesInState_PassedValidCountryNameAndStateName_ReturnsValidListOfCities()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + stateSegment + @"/" + expectedStateName + citySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string actualCityName = result.Find(city => city.ASCIIName == expectedCityName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCitiesInState_PassedValidCountryNameAndStateGeonameId_ReturnsValidListOfCities()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + stateSegment + @"/" + expectedStateGeonameId + citySegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<RawData> result = JsonConvert.DeserializeObject<List<RawData>>(response);
            string actualCityName = result.Find(city => city.ASCIIName == expectedCityName).ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        #endregion

        #region Tests for GetCityInState method

        [TestMethod]
        public void GetCityInState_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCityInState_Executed_ReturnsSomeData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCityInState_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetCityInState_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCityInState_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCityInState_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCityInState_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCityInState_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetCityInState_PassedValidISOCountryCodeAndStateNameAndCityName_ReturnsValidCityData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualCityName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCityInState_PassedValidISOCountryCodeAndStateNameAndCityGeonameId_ReturnsValidCityData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityGeonameId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualCityName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCityInState_PassedValidISOCountryCodeAndStateGeonameIdAndCityName_ReturnsValidCityData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateGeonameId + citySegment + @"/" + expectedCityName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualCityName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCityInState_PassedValidISOCountryCodeAndStateGeonameIdAndCityGeonameId_ReturnsValidCityData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedISOCountryCode + stateSegment + @"/" + expectedStateGeonameId + citySegment + @"/" + expectedCityGeonameId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualCityName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCityInState_PassedValidCountryNameAndStateNameAndCityName_ReturnsValidCityData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualCityName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCityInState_PassedValidCountryNameAndStateNameAndCityGeonameId_ReturnsValidCityData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + stateSegment + @"/" + expectedStateName + citySegment + @"/" + expectedCityGeonameId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualCityName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCityInState_PassedValidCountryNameAndStateGeonameIdAndCityName_ReturnsValidCityData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + stateSegment + @"/" + expectedStateGeonameId + citySegment + @"/" + expectedCityName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualCityName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        [TestMethod]
        public void GetCityInState_PassedValidCountryNameAndStateGeonameIdAndCityGeonameId_ReturnsValidCityData()
        {
            //Arrange
            requestURL = serverURL + countryControllerSegment + @"/" + expectedCountryName + stateSegment + @"/" + expectedStateGeonameId + citySegment + @"/" + expectedCityGeonameId;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            RawData result = JsonConvert.DeserializeObject<RawData>(response);
            string actualCityName = result.ASCIIName;

            //Assert
            Assert.AreEqual<string>(expectedCityName, actualCityName);
        }

        #endregion
    }
}
