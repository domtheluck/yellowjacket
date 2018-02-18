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

import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { NotificationService } from '../../services/notification.service';

import { IResourceService } from '../../services/resource.service.interface';
import { MessageId } from '../../services/resource.service.messageId';
import { ResourceService } from '../../services/resource.service';

import { IJobService } from '../../services/job.service.interface'
import { JobService } from '../../services/job.service'

import { IPackageService } from '../../services/package.service.interface';
import { PackageService } from '../../services/package.service';

import IJob from '../../models/job.model';
import IPackage from '../../models/package.model';
import IFeature from '../../models/feature.model';

@Component({
    selector: 'jobAdd',
    templateUrl: './jobAdd.component.html',
    providers: [
        { provide: 'IJobService', useClass: JobService },
        { provide: 'IResourceService', useClass: ResourceService },
        { provide: 'INotificationService', useClass: NotificationService },
        { provide: 'IPackageService', useClass: PackageService }]
})
export class JobAddComponent implements OnInit {
    private readonly resourceService: IResourceService;

    private readonly jobService: IJobService;
    private readonly packageService: IPackageService;

    private job: IJob;

    public jobAddForm: FormGroup;
    public name: FormControl;
    public selectedPackage: FormControl;
    public selectedFeatures: FormControl;

    public packages: IPackage[];
    public features: IFeature[];

    public validationMessages: any;

    /**
     * Initialize a new instance of JobAddComponent.
     * @param {IJobService} jobService An instance of the job service.
     * @param {NotificationService} notificationService An instance of the notification service.
     * @param {IPackageService} packageService An instance of the package service.
     */
    constructor(
        @Inject('IResourceService') resourceService: IResourceService,
        @Inject('IJobService') jobService: IJobService,
        @Inject('IPackageService') packageService: IPackageService,

        private readonly notificationService: NotificationService) {
        this.resourceService = resourceService;

        this.jobService = jobService;
        this.packageService = packageService;
    }

    /**
     * {Agular} Lifecycle hook that is called after data-bound properties of a directive are initialized.
     */
    public ngOnInit(): void {
        this.createFormControls();
        this.createForm();

        this.packageService.getAll().subscribe(res => {
            this.packages = res;
        });

        this.validationMessages = {
            name: {
                fieldRequired: this.resourceService.getMessageWithArguments(MessageId.FieldIsRequired, 'Name'),
                fieldMaxCharacters: this.resourceService.getMessageWithArguments(MessageId.FieldMaxCharacters, 'Name', '25')
            },
            selectedPackage: {
                fieldRequired: this.resourceService.getMessageWithArguments(MessageId.FieldIsRequired, 'Package')
        }}
    }

    /**
     * {Angular} Lifecycle hook that is called when any data-bound property of a directive changes.
     */
    public ngOnChanges() {

    }

    /**
     * Invoked when the selected package changed.
     * @param {any} item The item.
     */
    public selectedPackageChanged(item: any) {
        if (this.packages) {
            const selectedPackage = this.packages.filter(x => x.name === item.value)[0] as IPackage;

            if (!selectedPackage) {
                this.features = [];
            }

            this.features = selectedPackage.features;
        }
    }

    /**
     * Invoked when submitting the form.
     */
    public onSubmit() {
        this.notificationService.clear();

        this.job = this.prepareJob();

        this.jobService.add(this.job)
            .subscribe(result => {
                this.notificationService.addSuccess(this.resourceService.getMessage(MessageId.OperationSuccessfullyCompleted), true);
            },
            error => {
                this.notificationService.addError(this.resourceService.getMessage(MessageId.ErrorHappened), true);
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

        this.selectedPackage = new FormControl('', [
            Validators.required
        ]);

        this.selectedFeatures = new FormControl('', [

        ]);
    }

    /**
     * Creates the form.
     */
    private createForm() {
        this.jobAddForm = new FormGroup({
            name: this.name,
            selectedPackage: this.selectedPackage,
            selectedFeatures: this.selectedFeatures
        });
    }

    /**
     * Prepare the job for the Api call.
     * @returns {IJob} An instance of IJob.
     */
    private prepareJob() {
        const formModel = this.jobAddForm.value;

        const job: IJob = {
            name: formModel.name as string,
            "packageName": formModel.selectedPackage as string,
            features: formModel.selectedFeatures as IFeature[]
        };

        return job;
    }
}
