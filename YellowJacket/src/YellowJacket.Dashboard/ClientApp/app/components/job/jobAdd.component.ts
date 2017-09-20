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
    //private readonly notificationService: INotificationService;

    private job: IJob;

    private jobAddForm: FormGroup;
    private name: FormControl;

    constructor( @Inject('IJobService') agentService: IJobService, private notificationService: NotificationService) {
        this.jobService = agentService;
        //this.notificationService = notificationService;
    }

    public ngOnInit(): void {
        this.createFormControls();
        this.createForm();
    }

    public ngOnChanges() {

    }

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

    private createFormControls() {
        this.name = new FormControl('', [
            Validators.required,
            Validators.maxLength(25)
        ]);
    }

    private createForm() {
        this.jobAddForm = new FormGroup({
            name: this.name
        });
    }

    private prepareJob() {
        const formModel = this.jobAddForm.value;

        const job: IJob = {
            name: formModel.name as string
        };

        return job;
    }
}
