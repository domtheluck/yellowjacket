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

import * as React from "react";
import { connect } from "react-redux";

import { IApplicationState } from "../../../stores";
import * as IAgentState from "../../../stores/AgentStore";

import IboxTools from "../../common/IboxTools";

// at runtime, Redux will merge together
type AgentsProps =
    IAgentState.IAgentState // state we've requested from the Redux store
    & typeof IAgentState.actionCreators;   // plus action creators we've requested

export class Agents extends React.Component<AgentsProps, void> {
    constructor(props) {
        super(props);

        this.handleRefreshButtonClick = this.handleRefreshButtonClick.bind(this);
    }

    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestAgents();
    }

    componentWillReceiveProps(nextProps: AgentsProps) {
        // This method runs when incoming props (e.g., route params) change
    }

    private handleRefreshButtonClick() {
        this.props.requestAgents();
    }

    private renderAgentCardList() {
        return <div className="row">
            {this.props.payload.map(agent =>
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

    render() {
        return (
            <div className="wrapper wrapper-content animated fadeInRight">
                <div className="row">
                    <div className="col-lg-12">
                        <div className="ibox">
                            <div className="ibox-title">
                                <div className="form-group">
                                    <a className="btn btn-primary" onClick={this.handleRefreshButtonClick}>
                                        <i className="fa fa-refresh"></i>&nbsp;Refresh</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                {this.renderAgentCardList()}
                <br />
            </div>
        );
    }


}

export default connect(
    (state: IApplicationState) => state.agent, // selects which state properties are merged into the component's props
    IAgentState.actionCreators                 // selects which action creators are merged into the component's props
)(Agents);