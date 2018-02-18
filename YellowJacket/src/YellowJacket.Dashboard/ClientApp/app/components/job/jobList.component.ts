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

import { Component, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { NotificationService } from '../../services/notification.service';

import { IJobService } from '../../services/job.service.interface'
import { JobService } from '../../services/job.service'

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
    private readonly jobService: IJobService;

    public jobs: Observable<IJob[]>;

    /**
     * Initialize a new instance of JobListComponent.
     * @param {IJobService} jobService An instance of the IJobService.
     * @param {NotificationService} notificationService An instance of the notification service.
     */
    constructor( @Inject('IJobService') jobService: IJobService, private readonly notificationService: NotificationService) {
        this.jobService = jobService;
    }

    /**
     * Used to refresh the jobs from the service.
     */
    public refreshData() {
        this.jobs = this.jobService.getAll();
    }

    /**
     * {Agular} Lifecycle hook that is called after data-bound properties of a directive are initialized.
     */
    private ngOnInit() {
        this.jobs = this.jobService.getAll();
    }
}
