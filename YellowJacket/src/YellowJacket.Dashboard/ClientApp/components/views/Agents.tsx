import * as React from "react";
import { connect } from "react-redux";

import { IApplicationState } from "../../store";
import * as IAgentState from "../../store/AgentStore";

import IboxTools from "../common/IboxTools";

// at runtime, Redux will merge together
type AgentsProps =
    IAgentState.IAgentState // state we've requested from the Redux store
    & typeof IAgentState.actionCreators;   // plus action creators we've requested

export class Agents extends React.Component<AgentsProps, void> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestAgents();
    }

    componentWillReceiveProps(nextProps: AgentsProps) {
        // This method runs when incoming props (e.g., route params) change
    }

    render() {
        return (
            <div className="wrapper wrapper-content animated fadeInRight">
                {this.renderAgentCardList()}
                <br />
            </div>
        );
    }

    private renderAgentCardList() {
        return <div className="row">
            {this.props.agents.map(agent =>
                <div className="col-lg-2 col-md-4 col-sm-6 col-xs-6" key={agent.id}>
                    <div className="widget-head-color-box navy-bg p-xs text-center">
                        <div>
                            <h2 className="font-bold no-margins">{agent.name}</h2>
                        </div>
                    </div>
                    <div className="widget-text-box">
                        <div className="text-center">
                            <form role="form">
                                <div className="form-group">
                                    <label>Id</label>
                                    <p>
                                        {agent.id}
                                    </p>
                                </div>
                                <div className="form-group">
                                    <label>Status</label>
                                    <p>
                                        {agent.status}
                                    </p>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            )}
        </div>;
    }
}

// ReSharper disable once TsResolvedFromInaccessibleModule
export default connect(
    (state: IApplicationState) => state.agents, // selects which state properties are merged into the component's props
    IAgentState.actionCreators                 // selects which action creators are merged into the component's props
)(Agents);