import { Component, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { IJobService, JobService } from '../../services/job.service'

import IJob from '../../models/job.model'

@Component({
    selector: 'jobList',
    templateUrl: './jobList.component.html',
    providers: [{ provide: 'IJobService', useClass: JobService }]
})
export class JobListComponent {
    public jobService: IJobService;
    public jobs: Observable<IJob[]>;

    constructor( @Inject('IJobService') jobService: IJobService) {
        this.jobService = jobService;
    }

    private ngOnInit() {
        this.jobs = this.jobService.getAll();
    }

    private refreshData() {
        this.jobs = this.jobService.getAll();
    }
}
