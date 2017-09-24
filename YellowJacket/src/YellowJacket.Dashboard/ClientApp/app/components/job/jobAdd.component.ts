import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { NotificationService } from '../../services/notification.service';

import { IJobService, JobService } from '../../services/job.service';

import IJob from '../../models/job.model';

@Component({
    selector: 'jobAdd',
    templateUrl: './jobAdd.component.html',
    providers: [
        {
            provide: 'IJobService',
            useClass: JobService
        },
        {
            provide: 'INotificationService',
            useClass: NotificationService
        }]
})
export class JobAddComponent implements OnInit {
    private readonly jobService: IJobService;

    private job: IJob;

    private jobAddForm: FormGroup;
    private name: FormControl;

    /**
     * Initialize a new instance of JobAddComponent.
     * @param {IJobService} jobService An instance of the job service.
     * @param {NotificationService} notificationService An instance of the notification service.
     */
    constructor( @Inject('IJobService') jobService: IJobService, private readonly notificationService: NotificationService) {
        this.jobService = jobService;
    }

    /**
     * Angular nbOnInit function.
     */
    public ngOnInit(): void {
        this.createFormControls();
        this.createForm();
    }

    /**
     * Angular nbOnInit function.
     */
    public ngOnChanges() {

    }

    /**
     * Used when submitting the form.
     */
    private onSubmit() {
        console.log(JSON.stringify(this.jobAddForm.value));

        this.notificationService.clear();

        this.job = this.prepareJob();

        this.jobService.add(this.job)
            .subscribe(result => {
                console.log(JSON.stringify(result));
                this.notificationService.success('TEST SUCCESS', true);
            },
            error => {
                console.log(JSON.stringify(error));
                this.notificationService.error('TEST ERROR', true);
            });

        this.ngOnChanges();
    }

    /**
     * Creates the form different controls.
     */
    private createFormControls() {
        this.name = new FormControl('', [
            Validators.required,
            Validators.maxLength(25)
        ]);
    }

    /**
     * Creates the form.
     */
    private createForm() {
        this.jobAddForm = new FormGroup({
            name: this.name
        });
    }

    /**
     * Prepare the job for sending to the API.
     * @returns [IJob] An instance of IJob.
     */
    private prepareJob() {
        const formModel = this.jobAddForm.value;

        const job: IJob = {
            name: formModel.name as string
        };

        return job;
    }
}
