import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AgentListComponent } from './agentList.component';

import { IboxtoolsModule } from '../../components/common/iboxtools/iboxtools.module';

@NgModule({
    declarations: [AgentListComponent],
    imports: [BrowserModule, IboxtoolsModule],
    exports: [AgentListComponent]
})

export class AgentModule { }
