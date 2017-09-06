import { Injectable, Inject } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import IAgent from '../models/agent.model'

export interface IAgentService {
    getAll(): Observable<IAgent[]>
}

@Injectable()
export class AgentService implements IAgentService {
    private readonly http: Http;
    private readonly baseUrl: string;    

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }
    
    public getAll(): Observable<IAgent[]> {
        const agent$ = this.http
            .get(`${this.baseUrl}api/v1/agent`, { headers: this.getHeaders() })
            .map(this.mapAgents);

        return agent$;
    }

    private getHeaders() {
        const headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }

    private mapAgents = (response: Response): IAgent[] => {
        if (response.json().length === 0)
            return [];

        return response.json().map(this.toAgent);
    }

    private toAgent = (item: any): IAgent => {
        const agent = ({
            id: item.id,
            name: item.name,
            status: item.status
        }) as IAgent;

        return agent;
    }
}
