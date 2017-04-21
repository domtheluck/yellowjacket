import * as React from "react";
import { connect } from 'react-redux';

//import "animate.css";

import { IApplicationState } from "../../store";
import * as IAgentState from "../../store/AgentStore";

import IboxTools from "../common/IboxTools";

// At runtime, Redux will merge together...
type AgentsProps =
    IAgentState.IAgentState // ... state we've requested from the Redux store
    & typeof IAgentState.actionCreators;   // ... plus action creators we've requested

export class Agents extends React.Component<AgentsProps, void> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestAgents();
    }

    componentWillReceiveProps(nextProps: AgentsProps) {
        // This method runs when incoming props (e.g., route params) change
        //this.props.requestAgents();
    }

    render() {
        return (
            <div className="wrapper wrapper-content animated fadeInRight">
                <div className="row">
                    <div className="col-lg-12">

                        <div className="ibox">
                            <div className="ibox-title">
                                <h5>
                                    Resizable
                                </h5>
                                <IboxTools collapseEnabled={false} wrenchEnabled={false} closeEnabled={false}></IboxTools>
                            </div>
                            <div className="ibox-content">
                                <h2>
                                    This is simple box container nr. 1
                                </h2>
                                <p>
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown
                                    printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged.
                                </p>
                            </div>
                        </div>
                        <div className="text-center m-t-lg">
                            <h1>
                                Agents
                            </h1>
                            <small>
                                It is an application skeleton for a typical web app. You can use it to quickly bootstrap your webapp projects.
                            </small>
                            
                            <button onClick={() => { this.props.requestAgents() }}>Reload</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default connect(
    (state: IApplicationState) => state.agents, // Selects which state properties are merged into the component's props
    IAgentState.actionCreators                 // Selects which action creators are merged into the component's props
)(Agents);