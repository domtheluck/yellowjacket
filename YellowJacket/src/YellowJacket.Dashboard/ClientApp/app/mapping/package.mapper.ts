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

