﻿// ***********************************************************************
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

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;
using YellowJacket.Dashboard.Services.Interfaces;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Services
{
    public class PackageService : IPackageService
    {
        #region Private Members

        private readonly IPackageRepository _packageRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public PackageService(IPackageRepository repository, IMapper mapper)
        {
            _packageRepository = repository;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods
        
        /// <inheritdoc />
        /// <summary>
        /// Finds a package by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <see cref="T:YellowJacket.Models.PackageModel" />.
        /// </returns>
        public async Task<PackageModel> Find(string id)
        {
            return _mapper.Map<PackageEntity, PackageModel>(
                await _packageRepository.Find(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all packages from the repository.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Collections.Generic.List`1" />.
        /// </returns>
        public async Task<List<PackageModel>> GetAll()
        {
            List<PackageEntity> entities = await _packageRepository.GetAll();

            return _mapper.Map<List<PackageEntity>, List<PackageModel>>(entities);
        }

        #endregion
    }
}
