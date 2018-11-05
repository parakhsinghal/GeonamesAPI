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
    public class LanguageController : Controller
    {
        private ILanguageCodeRepository repository;

        public LanguageController(ILanguageCodeRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LanguageCode>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetAllLanguageCodes(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) || (pageSize == null && pageNumber == null))
                {
                    try
                    {
                        var result = repository.GetLanguageInfo(iso6393Code: null, language: null, pageNumber: pageNumber, pageSize: pageSize);

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
        [Route("{iso6393Code:length(3):alpha}")]
        [Route("{language:minlength(4):regex([[a-zA-Z]]+[[ a-zA-Z-_]]*)}")]
        [ProducesResponseType(200, Type = typeof(LanguageCode))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetLanguageCode(string iso6393Code = null, string language = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(iso6393Code) || !string.IsNullOrWhiteSpace(language))
                {
                    try
                    {
                        LanguageCode result = repository.GetLanguageInfo(iso6393Code: iso6393Code, language: language, pageNumber: null, pageSize: null)
                                                     .FirstOrDefault<LanguageCode>();
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
                        Debug.WriteLine(ex);
                        throw;
                    }
                }
                else
                {
                    return BadRequest("Please provide a valid value of ISO6393 code or language name.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

        }

        [HttpGet]
        [Route("keyvalue")]
        [ProducesResponseType(200, Type = typeof(KeyValuePair<string, string>))]
        [ProducesResponseType(404)]
        public IActionResult GetLanguageCodesAsDictionary()
        {
            try
            {
                var result = repository.GetLanguageInfo(iso6393Code: null, language: null, pageNumber: null, pageSize: null)
                                            .ToDictionary(item => item.ISO6393, item => item.Language).OrderBy(item => item.Value);
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

        //[Authorize]
        [HttpPut]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(List<Upd_VM.LanguageCode>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public IActionResult UpdateLanguageCodes(IEnumerable<Upd_VM.LanguageCode> languageCodes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<LanguageCode> result = repository.UpdateLanguages(languageCodes);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = languageCodes
                                        .Select(inputLanguageCode => inputLanguageCode.ISO6393)
                                        .FirstOrDefault();

                        byte[] inputRowId = languageCodes
                                        .Where(inputLanguageCode => inputLanguageCode.ISO6393 == primaryKey)
                                        .Select(inputLanguageCode => inputLanguageCode.RowId)
                                        .FirstOrDefault();

                        byte[] outputRowId = result
                                        .Where(outputLanguageCode => outputLanguageCode.ISO6393 == primaryKey)
                                        .Select(outputLanguageCode => outputLanguageCode.RowId)
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
        [Route("{iso6393Code:length(3):alpha}")]
        [Route("{language:minlength(4):regex([[a-zA-Z]]+[[ a-zA-Z-_]]*)}")]
        [ProducesResponseType(200, Type=typeof(Upd_VM.LanguageCode))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public IActionResult UpdateSingleLanguageCode(Upd_VM.LanguageCode languageCode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Upd_VM.LanguageCode> inputLanguageCodes = new List<Upd_VM.LanguageCode>();
                    inputLanguageCodes.Add(languageCode);
                    IEnumerable<LanguageCode> result = repository.UpdateLanguages(inputLanguageCodes);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = languageCode.ISO6393;

                        byte[] inputRowId = languageCode.RowId;

                        byte[] outputRowId = result
                                            .Where(outputLanguageCode => outputLanguageCode.ISO6393 == primaryKey)
                                            .Select(outputLanguageCode => outputLanguageCode.RowId)
                                            .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(ErrMsgs.ErrorMessages_US_en.NotUpdated_SingleEntry);
                        }
                        else
                        {
                            LanguageCode outputLanguageCode = new LanguageCode();
                            outputLanguageCode = result.FirstOrDefault();
                            return Ok(outputLanguageCode);
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
        [ProducesResponseType(200, Type= typeof(List<LanguageCode>))]
        [ProducesResponseType(400)]
        public IActionResult InsertLanguageCodes(IEnumerable<Ins_VM.LanguageCode> languageCodes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<LanguageCode> result = new List<LanguageCode>();
                    result = repository.InsertLanguages(languageCodes);

                    if (result != null && result.Count() > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(ErrMsgs.ErrorMessages_US_en.NotCreated_MultipleEntries);
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
        [Route("language")]
        [ProducesResponseType(200, Type=typeof(LanguageCode))]
        [ProducesResponseType(400)]
        public IActionResult InsertSingleLanguageCode(Ins_VM.LanguageCode languageCode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Ins_VM.LanguageCode> languageCodes = new List<Ins_VM.LanguageCode>();
                    languageCodes.Add(languageCode);
                    IEnumerable<LanguageCode> result = repository.InsertLanguages(languageCodes);

                    if (result != null && result.Count() > 0)
                    {
                        return Ok(result.FirstOrDefault<LanguageCode>());
                    }
                    else
                    {
                        return BadRequest(ErrMsgs.ErrorMessages_US_en.NotCreated_MultipleEntries);
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
        [HttpDelete]
        [Route("{iso6393Code:length(3):alpha}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteLanguageCode(string iso6393Code)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = repository.DeleteLanguageCode(iso6393Code);
                    if (result > 0)
                    {
                        return Ok("The resource has been deleted.");
                    }
                    else
                    {
                        return NotFound();
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
    }
}
