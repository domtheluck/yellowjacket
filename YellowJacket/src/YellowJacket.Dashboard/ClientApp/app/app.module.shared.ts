import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AgentModule } from './components/agent/agent.module';
import { JobModule } from './components/job/job.module';

import { NotificationService } from './services/notification.service';
import { ResourceService } from './services/resource.service';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';

import { AgentListComponent } from './components/agent/agentList.component';

import { JobListComponent } from './components/job/jobList.component';
import { JobAddComponent } from './components/job/jobAdd.component';

import { BasicLayoutComponent } from './components/common/layouts/basicLayout.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
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
