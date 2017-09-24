import { Component, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { NotificationService } from '../../services/notification.service';

import { IJobService, JobService } from '../../services/job.service'

import IJob from '../../models/job.model'

@Component({
    selector: 'jobList',
    templateUrl: './jobList.component.html',
    providers: [
        {
            provide: 'IJobService',
            useClass: JobService
        },
        {
            provide: 'INotificationService',
            useClass: NotificationService
        }
    ]
})
export class JobListComponent {
    public jobService: IJobService;
    public jobs: Observable<IJob[]>;

    /**
     * Initialize a new instance of JobListComponent.
     * @param {IJobService} jobService An instance of the job service.
     * @param {NotificationService} notificationService An instance of the notification service.
     */
    constructor( @Inject('IJobService') jobService: IJobService, private readonly notificationService: NotificationService) {
        this.jobService = jobService;
    }

    /**
     * Angular ngOnInit function.
    */
    private ngOnInit() {
        this.jobs = this.jobService.getAll();
    }

    /**
     * Used to refresh the job job data.
     */
    private refreshData() {
        this.jobs = this.jobService.getAll();
    }
}
