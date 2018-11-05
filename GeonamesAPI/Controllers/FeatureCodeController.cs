using GeonamesAPI.Domain;
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
    [ApiVersion(version: "2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FeatureCodeController : Controller
    {
        private IFeatureCodeRepository repository;

        public FeatureCodeController(IFeatureCodeRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type=typeof(List<FeatureCode>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetFeatureCodes(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                                (pageSize == null && pageNumber == null))
                {
                    try
                    {
                        IEnumerable<FeatureCode> result = repository.GetFeatureCodes(featureCodeId: null, pageNumber: pageNumber, pageSize: pageSize);
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

        //[Authorize]
        [HttpPut]
        [Route("")]
        [ProducesResponseType(200, Type=typeof(List<FeatureCode>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateFeatureCodes(IEnumerable<Upd_VM.FeatureCode> featureCodes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<FeatureCode> result = repository.UpdateFeatureCodes(featureCodes);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featureCodes
                                        .Select(inputFeatureCode => inputFeatureCode.FeatureCodeId)
                                        .FirstOrDefault();

                        byte[] inputRowId = featureCodes
                                        .Where(inputFeatureCode => inputFeatureCode.FeatureCodeId == primaryKey)
                                        .Select(inputFeatureCode => inputFeatureCode.RowId)
                                        .FirstOrDefault();

                        byte[] outputRowId = result
                                        .Where(outputFeatureCode => outputFeatureCode.FeatureCodeId == primaryKey)
                                        .Select(outputFeatureCode => outputFeatureCode.RowId)
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

        //[Authorize]
        [HttpPut]
        [Route("{featureCode:minlength(4)}")]
        [ProducesResponseType(200, Type=typeof(FeatureCode))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSingleFeatureCode(Upd_VM.FeatureCode featureCode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Upd_VM.FeatureCode> inputFeatureCodes = new List<Upd_VM.FeatureCode>();
                    inputFeatureCodes.Add(featureCode);
                    IEnumerable<FeatureCode> result = repository.UpdateFeatureCodes(inputFeatureCodes);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featureCode.FeatureCodeId;

                        byte[] inputRowId = featureCode.RowId;

                        byte[] outputRowId = result
                                        .Where(outputFeatureCode => outputFeatureCode.FeatureCodeId == primaryKey)
                                        .Select(outputFeatureCode => outputFeatureCode.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(ErrMsgs.ErrorMessages_US_en.NotUpdated_SingleEntry);

                        }
                        else
                        {
                            FeatureCode outputFeatureCode = new FeatureCode();
                            outputFeatureCode = result.FirstOrDefault();
                            return Ok(outputFeatureCode);
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

        //[Authorize]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(200, Type=typeof(List<FeatureCode>))]
        public IActionResult InsertFeatureCodes(List<Ins_VM.FeatureCode> featureCodes)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        //[Authorize]
        [HttpPost]
        [Route("featureCode")]
        [ProducesResponseType(200, Type=typeof(FeatureCode))]
        public IActionResult InsertSingleFeatureCode(Ins_VM.FeatureCode featureCode)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        //[Authorize]
        [HttpDelete]
        [Route("{featureCode:minlength(4)}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFeatureCode(string featureCodeId)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }
    }
}
