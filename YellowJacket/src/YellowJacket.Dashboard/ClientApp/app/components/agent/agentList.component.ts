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
