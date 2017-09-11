import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
//import { ReactiveFormsModule, FormsModule, FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { IJobService, JobService } from '../../services/job.service'

import IJob from '../../models/job.model'

@Component({
    selector: 'jobAdd',
    templateUrl: './jobAdd.component.html',
    providers: [{ provide: 'IJobService', useClass: JobService }]

})
export class JobAddComponent implements OnInit {
    public jobService: IJobService;

    public jobAddForm: FormGroup;
    public name: FormControl;

    constructor( @Inject('IJobService') agentService: IJobService) {
        this.jobService = agentService;
    }

    public ngOnInit(): void {
        this.createFormControls();
        this.createForm();
    }

    public onSubmit() {
        console.log(JSON.stringify(this.jobAddForm.value));

        //this.hero = this.prepareSaveHero();
        //this.heroService.updateHero(this.hero).subscribe(/* error handling */);
        //this.ngOnChanges();
    }

    public createFormControls() {
        this.name = new FormControl('', Validators.required);
    }

    public createForm() {
        this.jobAddForm = new FormGroup({
            name: this.name
        });
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
