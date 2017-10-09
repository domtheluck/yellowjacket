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

import IFeature from '../models/feature.model'

export class FeatureMapper {
    /**
     * Maps a list of data to an array of feature.
     * @param {any} data The data to map.
     * @returns {IFeature[]} An array of IFeature.
     */
    public mapFeatures = (data: any): IFeature[] => {
        return data.map(this.toFeature);
    }

    /**
     * Maps an item to a feature.
     * @param {any} item The item to map.
     * @returns {IFeature} A IFeature instance.
     */
    public toFeature = (item: any): IFeature => {
        const model = ({
            id: item.id,
            name: item.name
        }) as IFeature;

        return model;
    }
}

export default FeatureMapper;

