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

import { Component, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { IAgentService, AgentService } from '../../services/agent.service'

import IAgent from '../../models/agent.model'

@Component({
    selector: 'agentList',
    templateUrl: './agentList.component.html',
    providers: [{ provide: 'IAgentService', useClass: AgentService }]

})
export class AgentListComponent {
    public agentService: IAgentService;
    public agents: Observable<IAgent[]>;

    /**
     * Initialize a new instance of AgentListComponent.
     * @param {IAgentService} An instance of the IAgentService.
     */
    constructor( @Inject('IAgentService') agentService: IAgentService) {
        this.agentService = agentService;
    }

    /**
    * {Agular} Lifecycle hook that is called after data-bound properties of a directive are initialized.
    */
    private ngOnInit() {
        this.agents = this.agentService.getAll();
    }

    /**
     * Used to refresh the agents from the service.
    */
    private refreshData() {
        this.agents = this.agentService.getAll();
    }
}
