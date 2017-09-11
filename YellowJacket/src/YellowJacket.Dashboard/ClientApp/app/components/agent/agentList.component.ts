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

    constructor( @Inject('IAgentService') agentService: IAgentService) {
        this.agentService = agentService;
    }

    private ngOnInit() {
        this.agents = this.agentService.getAll();
    }

    private refreshData() {
        this.agents = this.agentService.getAll();
    }
}
