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

import { Response } from '@angular/http';

import FeatureMapper from '../mapping/feature.mapper';

import IPackage from '../models/package.model';

export class PackageMapper {
    private readonly featureMapper: FeatureMapper;

    /**
     * Initialize a new instance of PackageMapper.
     */
    constructor() {
        this.featureMapper = new FeatureMapper();
    }

    /**
     * Maps an Angular Response to an array of package.
     * @param {Response} data The data to map.
     * @returns {IFeature[]} An array of IPackage.
     */
    public mapPackages = (response: Response): IPackage[] => {
        if (response.json().length === 0)
            return [];

        return response.json().map(this.toPackage);
    }

    /**
     * Maps an item to a package.
     * @param {any} item The item to map.
     * @returns {IFeature} A IPackage instance.
     */
    public toPackage = (item: any): IPackage => {
        const model = ({
            name: item.name,
            testAssemblyName: item.testAssemblyName,
            features: this.featureMapper.mapFeatures(item.features)
        }) as IPackage;

        return model;
    }
}

export default PackageMapper;

