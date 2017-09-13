import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { IJobService, JobService } from '../../services/job.service'

import IJob from '../../models/job.model'

@Component({
    selector: 'jobAdd',
    templateUrl: './jobAdd.component.html',
    providers: [{ provide: 'IJobService', useClass: JobService }]

})
export class JobAddComponent implements OnInit {
    private readonly jobService: IJobService;

    private job: IJob;

    private jobAddForm: FormGroup;
    private name: FormControl;

    constructor( @Inject('IJobService') agentService: IJobService) {
        this.jobService = agentService;
    }

    public ngOnInit(): void {
        this.createFormControls();
        this.createForm();
    }

    public ngOnChanges() {
        
    }

    private onSubmit() {
        console.log(JSON.stringify(this.jobAddForm.value));

        this.job = this.prepareJob();

        this.jobService.add(this.job).subscribe();

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
            id: 'add',
            name: formModel.name as string
        };

        return job;
    }

    //public ngOnInit() {
    //    const myform = new FormGroup({
    //        name: new FormGroup({
    //            name: new FormControl('', [
    //                Validators.required,
    //                Validators.minLength(1),
    //                Validators.maxLength(25)])
    //        }),
    //        email: new FormControl('', [
    //            Validators.required,
    //            Validators.pattern("[^ @]*@[^ @]*")
    //        ]),
    //        password: new FormControl('', [
    //            Validators.minLength(8),
    //            Validators.required
    //        ]),
    //        language: new FormControl()
    //    });
    //}
}
