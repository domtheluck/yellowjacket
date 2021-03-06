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
using YellowJacket.Dashboard.Entities;

namespace YellowJacket.Dashboard.Repositories.Interfaces
{
    /// <summary>
    /// PAckage repository interface definition.
    /// </summary>
    public interface IPackageRepository
    {
        /// <summary>
        /// Finds a package by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><see cref="PackageEntity"/>.</returns>
        Task<PackageEntity> Find(string id);

        /// <summary>
        /// Gets all packages from the repository.
        /// </summary>
        /// <returns>
        ///   <see cref="List{PackageEntity}" />.
        /// </returns>
        Task<List<PackageEntity>> GetAll();

        /// <summary>
        /// Downloads the specified package.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><see cref="T:byte[]"/>.</returns>
        Task<byte[]> Download(string id);
    }
}
