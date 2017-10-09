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

import { Injectable, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import IPackage from '../models/package.model'

import PackageMapper from '../mapping/package.mapper'

export interface IPackageService {
    getAll(): Observable<IPackage[]>
}

@Injectable()
export class PackageService implements IPackageService {
    private readonly http: Http;
    private readonly baseUrl: string;

    private readonly packageMapper: PackageMapper;

    /**
     * Initialize a new instance of PackageService.
     * @param {Http} http Angular http component.
     * @param {string} baseUrl The base url.
     */
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;

        this.packageMapper = new PackageMapper();
    }

    /**
     * Gets all packages from the repository.
     * @returns {Observable<IJob[]>} An Angular Observable who contains an array of package.
     */
    public getAll(): Observable<IPackage[]> {
        const package$ = this.http
            .get(`${this.baseUrl}api/v1/package`, { headers: this.getHeaders() })
            .map(this.packageMapper.mapPackages);

        return package$;
    }

    /**
     * Get the request headers.
     * @returns {Headers} An Augular Headers instance.
     */
    private getHeaders() {
        const headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }
}
