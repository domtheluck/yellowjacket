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

import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import {
    AlertModule, AccordionModule, ButtonsModule, CarouselModule, CollapseModule, BsDatepickerModule,
    BsDropdownModule, ModalModule, PaginationModule, ProgressbarModule, SortableModule, TabsModule,
    TimepickerModule, TooltipModule, TypeaheadModule
} from 'ngx-bootstrap';

import { NotificationModule } from './components/common/notification/notification.module';

import { AgentModule } from './components/agent/agent.module';
import { JobModule } from './components/job/job.module';

import { NotificationService } from './services/notification.service';
import { ResourceService } from './services/resource.service';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';

import { AgentListComponent } from './components/agent/agentList.component';

import { JobListComponent } from './components/job/jobList.component';
import { JobAddComponent } from './components/job/jobAdd.component';

import { BasicLayoutComponent } from './components/layout/layouts/basicLayout.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        ReactiveFormsModule,
        FormsModule,
        RouterModule.forRoot([
            {
                path: '', component: BasicLayoutComponent,
                children: [
                    { path: 'home', component: HomeComponent },
                    { path: 'agent', component: AgentListComponent },
                    { path: 'job', component: JobListComponent },
                    { path: 'job/add', component: JobAddComponent },
                    { path: '**', redirectTo: 'home' }
                ]
            }
        ]),
        AlertModule.forRoot(),
        AccordionModule.forRoot(),
        ButtonsModule.forRoot(),
        CarouselModule.forRoot(),
        CollapseModule.forRoot(),
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        ModalModule.forRoot(),
        PaginationModule.forRoot(),
        ProgressbarModule.forRoot(),
        SortableModule.forRoot(),
        TabsModule.forRoot(),
        TimepickerModule.forRoot(),
        TooltipModule.forRoot(),
        TypeaheadModule.forRoot(),
        NotificationModule,
        AgentModule,
        JobModule
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule
    ],
    providers: [
        NotificationService, ResourceService
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export class AppModuleShared { }
