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
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { IAgentService } from './agent.service.interface';
import IAgent from '../models/agent.model'

@Injectable()
export class AgentService implements IAgentService {
    private readonly http: Http;
    private readonly baseUrl: string;    

    /**
     * Initialize a new instance of AgentService.
     * @param {Http} http Angular http component.
     * @param {string} baseUrl The base url.
     */
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    /**
     * Gets all agents from the repository.
     * @returns {Observable<IAgent[]>} An Angular Observable who contains an array of agent.
     */
    public getAll(): Observable<IAgent[]> {
        const agent$ = this.http
            .get(`${this.baseUrl}api/v1/agent`, { headers: this.getHeaders() })
            .map(this.mapAgents);

        return agent$;
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

    /**
     * Maps the Api response to an array of IAgent.
     * @param {Response} response The Api response.
     * @returns {IAgent[]} An array of IAgent.
     */
    private mapAgents = (response: Response): IAgent[] => {
        if (response.json().length === 0)
            return [];

        return response.json().map(this.toAgent);
    }

    /**
     * Maps a single item to a IAgent instance.
     * @param {any} item The item to map.
     * @returns {IAgent} The mapped IAgent instance.
     */
    private toAgent = (item: any): IAgent => {
        const model = ({
            id: item.id,
            name: item.name,
            status: item.status
        }) as IAgent;

        return model;
    }
}
