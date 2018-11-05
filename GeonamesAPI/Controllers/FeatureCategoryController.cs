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
    [FormatFilter]
    [ApiVersion(version: "2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FeatureCategoryController : Controller
    {
        private IFeatureCategoryRepository repository;

        public FeatureCategoryController(IFeatureCategoryRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        [Route("")]
        [Route("{featureCategoryId:alpha:length(1)}")]
        [ProducesResponseType(200, Type = typeof(List<FeatureCategory>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFeatureCategories(string featureCategoryId = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(featureCategoryId) && featureCategoryId.Length == 1) || string.IsNullOrEmpty(featureCategoryId))
                {
                    try
                    {
                        IEnumerable<FeatureCategory> result = repository.GetFeatureCategories(featureCategoryId);
                        if (result != null && result.Count() > 0)
                        {
                            if (!string.IsNullOrEmpty(featureCategoryId))
                            {
                                return Ok(result.FirstOrDefault<FeatureCategory>());
                            }
                            else
                            {
                                return Ok(result);
                            }

                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
                else
                {
                    return BadRequest("If you are passing a featureCategoryId the ensure that the length of the string parameter featureCategoryId does not exceed one english alphabet");
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
        [ProducesResponseType(200, Type = typeof(List<FeatureCategory>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFeatureCategories(IEnumerable<Upd_VM.FeatureCategory> featureCategories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<FeatureCategory> result = repository.UpdateFeatureCategories(featureCategories);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featureCategories
                                        .Select(inputFeatureCategory => inputFeatureCategory.FeatureCategoryId)
                                        .FirstOrDefault();

                        byte[] inputRowId = featureCategories
                                        .Where(inputFeatureCategory => inputFeatureCategory.FeatureCategoryId == primaryKey)
                                        .Select(inputFeatureCategory => inputFeatureCategory.RowId)
                                        .FirstOrDefault();

                        byte[] outputRowId = result
                                        .Where(outputFeatureCategory => outputFeatureCategory.FeatureCategoryId == primaryKey)
                                        .Select(outputFeatureCategory => outputFeatureCategory.RowId)
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
        [Route("{featureCategoryId:alpha:length(1)}")]
        [ProducesResponseType(200, Type = typeof(FeatureCategory))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public IActionResult UpdateSingleFeatureCategory(Upd_VM.FeatureCategory featureCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Upd_VM.FeatureCategory> inputFeatureCategories = new List<Upd_VM.FeatureCategory>();
                    inputFeatureCategories.Add(featureCategory);
                    IEnumerable<FeatureCategory> result = repository.UpdateFeatureCategories(inputFeatureCategories);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featureCategory.FeatureCategoryId;

                        byte[] inputRowId = featureCategory.RowId;

                        byte[] outputRowId = result
                                        .Where(outputFeatureCategory => outputFeatureCategory.FeatureCategoryId == primaryKey)
                                        .Select(outputFeatureCategory => outputFeatureCategory.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(ErrMsgs.ErrorMessages_US_en.NotUpdated_SingleEntry);

                        }
                        else
                        {
                            FeatureCategory outputFeatureCategory = new FeatureCategory();
                            outputFeatureCategory = result.FirstOrDefault();
                            return Ok(outputFeatureCategory);
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
        [ProducesResponseType(200, Type = typeof(List<FeatureCategory>))]
        public IActionResult InsertFeatureCategories(List<Ins_VM.FeatureCategory> featureCategories)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("featureCategory")]
        [ProducesResponseType(200, Type=typeof(FeatureCategory))]
        public IActionResult InsertSingleFeatuerCategory(Ins_VM.FeatureCategory featureCategory)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("")]
        [ProducesResponseType(200)]
        public IActionResult DeleteFeatureCategory(string featureCategoryId)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }
    }
}
