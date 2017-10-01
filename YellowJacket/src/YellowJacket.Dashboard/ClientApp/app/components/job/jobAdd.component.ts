import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { NotificationService } from '../../services/notification.service';
import { IResourceService, ResourceService, MessageId } from '../../services/resource.service';

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
            provide: 'IResourceService',
            useClass: ResourceService
        },
        {
            provide: 'INotificationService',
            useClass: NotificationService
        }]
})
export class JobAddComponent implements OnInit {
    private readonly jobService: IJobService;
    private readonly resourceService: IResourceService;

    private job: IJob;

    private jobAddForm: FormGroup;
    private name: FormControl;

    /**
     * Initialize a new instance of JobAddComponent.
     * @param {IJobService} jobService An instance of the job service.
     * @param {NotificationService} notificationService An instance of the notification service.
     */
    constructor(
        @Inject('IJobService') jobService: IJobService,
        @Inject('IResourceService') resourceService: IResourceService,
        private readonly notificationService: NotificationService) {
        this.jobService = jobService;
        this.resourceService = resourceService;
    }

    /**
     * {Agular} Lifecycle hook that is called after data-bound properties of a directive are initialized.
     */
    public ngOnInit(): void {
        this.createFormControls();
        this.createForm();
    }

    /**
     * {Angular} Lifecycle hook that is called when any data-bound property of a directive changes..
     */
    public ngOnChanges() {

    }

    /**
     * Invoked when submitting the form.
     */
    private onSubmit() {
        console.log(JSON.stringify(this.jobAddForm.value));

        this.notificationService.clear();

        this.job = this.prepareJob();

        this.jobService.add(this.job)
            .subscribe(result => {
                console.log(JSON.stringify(result));
                this.notificationService.success(this.resourceService.getMessage(MessageId.OperationSuccessfullyCompleted), true);
            },
            error => {
                console.log(JSON.stringify(error));
                this.notificationService.error(this.resourceService.getMessage(MessageId.ErrorHappened), true);
            });

        this.ngOnChanges();
    }

    /**
     * Creates the form controls.
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
     * Prepare the job for sending to the Api.
     * @returns {IJob} An instance of IJob.
     */
    private prepareJob() {
        const formModel = this.jobAddForm.value;

        const job: IJob = {
            name: formModel.name as string
        };

        return job;
    }
}
