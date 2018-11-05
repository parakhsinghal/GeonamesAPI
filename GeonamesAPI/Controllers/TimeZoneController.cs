using GeonamesAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ErrMsgs = GeonamesAPI.Service.ErrorMessages;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;

namespace GeonamesAPI.Service.Controllers
{
    [FormatFilter]
    [ApiVersion(version: "2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TimeZoneController : Controller
    {
        private ITimeZoneRepository repository;

        public TimeZoneController(ITimeZoneRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(200, Type=typeof(List<string>))]
        [ProducesResponseType(404)]
        public IActionResult GetDistinctTimeZones()
        {
            try
            {
                IEnumerable<string> result = repository.GetDistinctTimeZones();

                if (result != null && result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

        }

        [HttpGet]
        [Route("{isoCountryCode:alpha:length(2)}")]
        [Route("{iso3Code:alpha:length(3)}")]
        [Route("{isoNumeric:int}")]
        [Route("{latitude:double}/{longitude:double}")]
        [Route("{countryName:alpha:length(4,50)}")]
        [Route("")]
        [ProducesResponseType(200, Type=typeof(List<GeonamesAPI.Domain.TimeZone>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetTimeZoneDetails(string continent = null, string country = null, string state = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) || (pageSize == null && pageNumber == null))
                {
                    try
                    {
                        string timeZoneId = null;

                        if ((!string.IsNullOrEmpty(continent) && !string.IsNullOrWhiteSpace(continent)) &&
                            (!string.IsNullOrEmpty(country) && !string.IsNullOrWhiteSpace(country)))
                        {
                            timeZoneId = continent + "/" + country;
                            if (!string.IsNullOrEmpty(state) && !string.IsNullOrWhiteSpace(state))
                            {
                                timeZoneId += "/" + state;
                            }
                        }
                        IEnumerable<GeonamesAPI.Domain.TimeZone> result = repository.GetTimeZoneDetails(timeZoneId, isoCountryCode, iso3Code, isoNumeric, countryName, latitude, longitude, pageNumber, pageSize);
                        if (result != null && result.Count() > 0)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        throw;
                    }

                }
                else
                {
                    return BadRequest("Both pageSize and pageNumber properties need to have valid values.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

        }


        [HttpGet]
        [Route("{continent:regex([[a-z]][[a-z0-9_]])}/{country:regex([[a-z]][[a-z0-9_]])}/{state:regex([[a-z]][[a-z0-9_]])?}")]
        [ProducesResponseType(200, Type=typeof(GeonamesAPI.Domain.TimeZone))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetSingleTimeZoneDetails(string continent = null, string country = null, string state = null)
        {
            try
            {
                string timeZoneId = null;

                if ((!string.IsNullOrEmpty(continent) && !string.IsNullOrWhiteSpace(continent)) &&
                    (!string.IsNullOrEmpty(country) && !string.IsNullOrWhiteSpace(country)))
                {
                    timeZoneId = continent + "/" + country;
                    if (!string.IsNullOrEmpty(state) && !string.IsNullOrWhiteSpace(state))
                    {
                        timeZoneId += "/" + state;
                    }
                }
                IEnumerable<GeonamesAPI.Domain.TimeZone> result = repository.GetTimeZoneDetails(timeZoneId, null, null, null, null, null, null, null, null);
                if (result != null && result.Count() > 0)
                {
                    return Ok(result.FirstOrDefault<GeonamesAPI.Domain.TimeZone>());
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
                throw;
            }
        }

        [HttpGet]
        [Route("place/{placeName:regex([[a-zA-Z]]+[[ a-zA-Z-_]]*)}")]
        [ProducesResponseType(200, Type= typeof(List<GeonamesAPI.Domain.TimeZone>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetTimeZoneDetailsByPlaceName(string placeName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(placeName) && !string.IsNullOrEmpty(placeName))
                {
                    try
                    {
                        IEnumerable<GeonamesAPI.Domain.TimeZone> result = repository.GetTimeZoneDetailsByPlaceName(placeName);
                        if (result != null || result.Count() > 0)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        throw;
                    }
                }
                else
                {
                    return BadRequest("Please provide a valid value for name of the place of which the time zone is required.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(200, Type=typeof(List<GeonamesAPI.Domain.TimeZone>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public IActionResult UpdateTimeZones(IEnumerable<Upd_VM.TimeZone> timeZones)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<GeonamesAPI.Domain.TimeZone> result = repository.UpdateTimeZones(timeZones);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = timeZones
                                        .Select(inputTimeZone => inputTimeZone.TimeZoneId)
                                        .FirstOrDefault();

                        byte[] inputRowId = timeZones
                                        .Where(inputTimeZone => inputTimeZone.TimeZoneId == primaryKey)
                                        .Select(inputTimeZone => inputTimeZone.RowId)
                                        .FirstOrDefault();

                        byte[] outputRowId = result
                                        .Where(outputTimeZone => outputTimeZone.TimeZoneId == primaryKey)
                                        .Select(outputTimeZone => outputTimeZone.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(ErrMsgs.ErrorMessages_US_en.NotUpdated_MultipleEntries);
                           
                        }
                        else
                        {
                            return Ok(result);
                        }

                    }
                    else
                    {
                        return StatusCode(500);
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpPut]
        [Route("timezone/{continent:regex([[a-z]][[a-z0-9_]])}/{country:regex([[a-z]][[a-z0-9_]])}/{state:regex([[a-z]][[a-z0-9_]])?}")]
        [ProducesResponseType(200, Type=typeof(GeonamesAPI.Domain.TimeZone))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSingleTimeZone(Upd_VM.TimeZone timeZone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Upd_VM.TimeZone> inputTimeZones = new List<Upd_VM.TimeZone>();
                    inputTimeZones.Add(timeZone);
                    IEnumerable<GeonamesAPI.Domain.TimeZone> result = repository.UpdateTimeZones(inputTimeZones);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = timeZone.TimeZoneId;

                        byte[] inputRowId = timeZone.RowId;

                        byte[] outputRowId = result
                                        .Where(outputTimeZone => outputTimeZone.TimeZoneId == primaryKey)
                                        .Select(outputTimeZone => outputTimeZone.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(ErrMsgs.ErrorMessages_US_en.NotUpdated_SingleEntry);

                        }
                        else
                        {
                            GeonamesAPI.Domain.TimeZone outputTimeZone = new GeonamesAPI.Domain.TimeZone();
                            outputTimeZone = result.FirstOrDefault();
                            return Ok(outputTimeZone);
                        }

                    }
                    else
                    {
                        return StatusCode(500);
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(200, Type=typeof(List<GeonamesAPI.Domain.TimeZone>))]
        public ActionResult InsertTimeZones(List<Ins_VM.TimeZone> timeZones)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("timeZone")]
        [ProducesResponseType(200, Type=typeof(GeonamesAPI.Domain.TimeZone))]
        public IActionResult InsertSingleTimeZone(Ins_VM.TimeZone timeZone)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("timezone/{continent:regex([[a-z]][[a-z0-9_]])}/{country:regex([[a-z]][[a-z0-9_]])}/{state:regex([[a-z]][[a-z0-9_]])?}")]
        public IActionResult DeleteTimeZone(string timeZoneId)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }
    }
}