using GeonamesAPI.Domain;
using GeonamesAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeonamesAPI.Service.Controllers
{
    [FormatFilter]
    [ApiVersion(version:"2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ContinentController : Controller
    {
        private IContinentRepository repository;

        public ContinentController(IContinentRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type=typeof(List<Continent>))]
        public IActionResult GetAllContinents()
        {
            try
            {
                return Ok(repository.GetContinentInfo(continentCodeId: null, geonameId: null, continentName: null));
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw;
            }


        }

        [HttpGet]
        [Route("keyvalue")]
        [ProducesResponseType(200, Type=typeof(KeyValuePair<long, string>))]
        public IActionResult GetContinentsAsDictionary()
        {
            try
            {
                var result = repository.GetContinentInfo(continentCodeId: null, geonameId: null, continentName: null)
                                            .ToDictionary(item => item.GeonameId, item => item.ContinentName)
                                            .OrderBy(item => item.Value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("{continentCodeId:length(2):alpha}")]
        [Route("{geonameId:long}")]
        [Route("{continentName:minlength(4):alpha}")]
        [ProducesResponseType(200, Type=typeof(Continent))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetContinentInfo(string continentCodeId = null, int? geonameId = null, string continentName = null)
        {
            try
            {
                if ((!string.IsNullOrWhiteSpace(continentCodeId) && !string.IsNullOrEmpty(continentCodeId)) ||
                (!string.IsNullOrWhiteSpace(continentName) && !string.IsNullOrEmpty(continentName)) ||
                (geonameId.HasValue == true && geonameId.Value > 0))
                {
                    try
                    {
                        Continent result = repository.GetContinentInfo(continentCodeId, geonameId, continentName).FirstOrDefault<Continent>();
                        if (result.RowId != null)
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
                        //Debug.WriteLine(ex);
                        throw;
                    }
                }
                else
                {
                    return BadRequest("Please provide valid value of a continent code or continent name or geoname id.");
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("{continentCodeId:length(2):alpha}/countries")]
        [Route("{geonameId:long}/countries")]
        [Route("{continentName:minlength(4):alpha}/countries")]
        [ProducesResponseType(200, Type=typeof(List<Country>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetCountriesInAContinent(string continentName = null, string continentCodeId = null, int? geonameId = null,
int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(continentName) || !string.IsNullOrWhiteSpace(continentCodeId) || geonameId > 0)
                {
                    if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) || (pageSize == null && pageNumber == null))
                    {
                        try
                        {
                            IEnumerable<Country> result = repository.GetCountriesInAContinent(continentName, continentCodeId, geonameId, pageNumber, pageSize);
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
                            //Debug.WriteLine(ex);
                            throw;
                        }
                    }
                    else
                    {
                        return BadRequest("Both pageSize and pageNumber properties need to have valid values.");
                    }
                }
                else
                {
                    return BadRequest("Please provide valid value of a continent code or continent name or geoname id.");
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("{continentCodeId:length(2):alpha}/countries/keyvalue")]
        [Route("{geonameId:long}/countries/keyvalue")]
        [Route("{continentName:minlength(4):alpha}/countries/keyvalue")]
        [ProducesResponseType(200, Type=typeof(KeyValuePair<long?, string>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetCountriesInAContinentAsDictionary(string continentName = null, string continentCodeId = null, int? geonameId = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(continentName) || !string.IsNullOrWhiteSpace(continentCodeId) || geonameId > 0)
                {
                    try
                    {
                        var result = repository.GetCountriesInAContinent(continentName, continentCodeId, geonameId, pageNumber: null, pageSize: null).ToDictionary(item => item.GeonameId, item => item.CountryName).OrderBy(item => item.Value);

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
                        //Debug.WriteLine(ex);
                        throw;
                    }
                }
                else
                {
                    return BadRequest("Please provide valid value of a continent code or continent name or geoname id.");
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw;
            }
        }
    }
}