import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AgentModule } from './components/agent/agent.module';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { AgentListComponent } from './components/agent/agentList.component';
import { BasicLayoutComponent } from './components/common/layouts/basicLayout.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            {
                path: '', component: BasicLayoutComponent,
                children: [
                    { path: 'home', component: HomeComponent },
                    { path: 'agents', component: AgentListComponent },
                    { path: '**', redirectTo: 'home' }
                ]
            }
        ]),
        AgentModule
    ]
})

export class AppModuleShared { }
