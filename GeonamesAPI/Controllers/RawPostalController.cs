using GeonamesAPI.Domain;
using GeonamesAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeoDataAPI.Service.Controllers
{
    [Route("api/v2/[controller]")]
    public class RawPostalController : Controller
    {
        private IRawPostalRepository repository;

        public RawPostalController(IRawPostalRepository _repository)
        {
            this.repository = _repository;
        }

        [Route("{isoCountryCode:alpha:length(2)}/{postalCode?}")]
        [Route("{countryName:alpha:minlength(3)}/{postalCode?}")]
        [ProducesResponseType(200, Type=typeof(List<RawPostal>))]
        public IActionResult GetPostalCodesInfo(string isoCountryCode = null, string countryName = null, string postalCode = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(isoCountryCode) && !string.IsNullOrWhiteSpace(isoCountryCode)) ||
                               (!string.IsNullOrEmpty(countryName) && (!string.IsNullOrWhiteSpace(countryName))))
                {
                    if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                        (pageSize == null && pageNumber == null))
                    {
                        try
                        {
                            var result = repository.GetPostalInfo(isoCountryCode, countryName, postalCode, pageNumber, pageSize);

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
                else
                {
                    return BadRequest("Please provide a valid value of ISO country code or country name.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

        }
    }
}