import { Injectable, Inject } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import IJob from '../models/job.model'

export interface IJobService {
    getAll(): Observable<IJob[]>,
    add(job: IJob): Observable<IJob>;
}

@Injectable()
export class JobService implements IJobService {
    private readonly http: Http;
    private readonly baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    public add(job: IJob): Observable<IJob> {
        const agent$ = this.http
            .post(`${this.baseUrl}api/v1/job`, job, { headers: this.getHeaders() })
            .map(this.toJob);

        return agent$;
    }

    public getAll(): Observable<IJob[]> {
        const agents$ = this.http
            .get(`${this.baseUrl}api/v1/job`, { headers: this.getHeaders() })
            .map(this.mapJobs);

        return agents$;
    }

    private getHeaders() {
        const headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }

    private mapJobs = (response: Response): IJob[] => {
        if (response.json().length === 0)
            return [];

        return response.json().map(this.toJob);
    }

    private toJob = (item: any): IJob => {
        const job = ({
            id: item.id,
            name: item.name
        }) as IJob;

        return job;
    }
}
