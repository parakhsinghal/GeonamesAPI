﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GeonamesAPI.SQLRepository;
using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Service.Controllers
{
    
    [Route("api/[controller]")]
    public class ContinentController : Controller
    {
        private IContinentRepository repository;

        public ContinentController(IContinentRepository _repository)
        {
            this.repository = _repository;
        }

        [Route("")]
        //[ProducesResponseType(typeof(List<Continent>))]
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

        [Route("keyvalue")]
        //[ProducesResponseType(typeof(Dictionary<long, string>))]
        public IActionResult GetContinentsAsDictionary()
        {
            try
            {
                return Ok(repository.GetContinentInfo(continentCodeId: null, geonameId: null, continentName: null)
                                            .ToDictionary(item => item.GeonameId, item => item.ContinentName)
                                            .OrderBy(item => item.Value));
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw;
            }

        }

        [Route("{continentCodeId:length(2):alpha}")]
        [Route("{geonameId:long}")]
        [Route("{continentName:minlength(4):alpha}")]
        //[ProducesResponseType(typeof(Continent))]
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

        [Route("{continentCodeId:length(2):alpha}/countries")]
        [Route("{geonameId:long}/countries")]
        [Route("{continentName:minlength(4):alpha}/countries")]
        //[ProducesResponseType(typeof(List<Country>))]
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

        [Route("{continentCodeId:length(2):alpha}/countries/keyvalue")]
        [Route("{geonameId:long}/countries/keyvalue")]
        [Route("{continentName:minlength(4):alpha}/countries/keyvalue")]
        //[ProducesResponseType(typeof(KeyValuePair<long?, string>))]
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