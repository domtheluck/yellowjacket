import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NotificationComponent } from '../common/notification/index';

import { JobListComponent } from './jobList.component';
import { JobAddComponent } from './jobAdd.component';

import { IboxtoolsModule } from '../../components/common/iboxtools/iboxtools.module';

@NgModule({
    declarations: [JobListComponent, JobAddComponent, NotificationComponent],
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        IboxtoolsModule]
    ,
    exports: [
        JobListComponent,
        JobAddComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
})

export class JobModule { }
