import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { JobListComponent } from './jobList.component';
import { JobAddComponent } from './jobAdd.component';

import { IboxtoolsModule } from '../../components/common/iboxtools/iboxtools.module';

@NgModule({
    declarations: [JobListComponent, JobAddComponent],
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        IboxtoolsModule]
    ,
    exports: [
        JobListComponent,
        JobAddComponent]
})

export class JobModule { }
