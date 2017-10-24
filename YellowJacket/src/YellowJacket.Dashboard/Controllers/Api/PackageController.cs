// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class PackageController : BaseController
    {
        #region Private Members

        private readonly IPackageRepository _packageRepository;

        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="packageRepository">The package repository.</param>
        public PackageController(IMapper mapper, IPackageRepository packageRepository)
        {
            _mapper = mapper;
            _packageRepository = packageRepository;
        }

        #endregion

        #region Public Methods

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                PackageEntity entity = await _packageRepository.Find(id);

                if (entity == null)
                    return StatusCode(404);

                return Ok(_mapper.Map<PackageEntity, PackageModel>(entity));
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500, id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<PackageEntity>, IEnumerable<PackageModel>>(await _packageRepository.GetAll()));
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500);
            }
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download()
        {
            string fileName = "SimpleFeature.zip";

            string filepath = $@"D:\Projects\DEV\yellowjacket\YellowJacket\samples\Packages\Test/{fileName}";
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filepath);

            return File(fileBytes, "application/x-msdownload", fileName);
        }

        #endregion
    }
}
