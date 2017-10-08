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

