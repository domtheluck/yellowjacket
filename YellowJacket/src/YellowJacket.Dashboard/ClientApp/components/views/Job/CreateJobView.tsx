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
import * as IJobState from "../../../stores/JobStore";
import { form, from, DisabledFormSubmit, FeedbackFormSubmit, ResetFormButton } from "react-inform";

// at runtime, Redux will merge together
type JobProps =
    IJobState.IJobState // state we've requested from the Redux store
    & typeof IJobState.actionCreators;   // plus action creators we've requested

interface IMyProps extends JobProps {
    fields: any,
    form: any;
    
}

const fields = ["name"];

const isRequired = value => value;

const rulesMap = {
    name: {
        "Name is required": isRequired
    }
};

function FormGroup({type, id, text, placeHolder, error, props}){
    let control = {};
    if (type === "text")
        control = (
            <input type="text" className="form-control" id={id} placeholder={placeHolder} {...props}/>
        );

    if (error) {
        return (
            <div className="form-group has-error has-feedback">
                <label htmlFor={id} className="control-label">{text}</label>
                { control }
                <span className="glyphicon glyphicon-xbt form-control-feedback" aria-hidden="true"></span>
                <small className="form-text text-muted">{error}</small>
            </div>
        );
    } else {
        return (
            <div className="form-group has-feedback">
                <label htmlFor={id} className="control-label">{text}</label>
                { control }
                <small className="form-text text-muted">{error}</small>
            </div>    
        );    
    }     
};

function LabeledInput({ text, error, id, props }) {
    return (
        <div>
            <label htmlFor={id}>{text}</label>
            <input id={id} placeholder={text} type="text" {...props} />
            <small className="form-text text-muted">{error}</small>
        </div>
    );
}

@form(from(rulesMap))
export class CreateJobView extends React.Component<IMyProps, void> {
    constructor(props) {
        super(props);
    }

    handleSubmit(e) {
        e.preventDefault();

        this.props.form.forceValidate();

        if (!this.props.form.isValid())
            return;

        // TODO: create the object to send with the action
        const newJob = {
            name: this.props.fields.name.value
        };

        this.props.createJob(newJob);
    };

    render() {
        const { name } = this.props.fields;

        // TODO: add missing fields and styles.
        return (
            <div className="wrapper wrapper-content animated fadeInRight">
                <div className="row">
                    <div className="col-lg-6">
                        <div className="ibox">
                            <div className="ibox-title">
                                <form onSubmit={e => this.handleSubmit(e)}>
                                    <div className="form-group">
                                        <label htmlFor="name" className="control-label">Name</label>
                                        <input type="text" className="form-control" id="name" placeholder="Enter name..." {...name.props} />
                                        <small className="form-text text-muted">{name.error}</small>
                                    </div>
                                    <div className="form-group">
                                        <button type="submit" className="btn btn-primary">Submit</button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default connect(
    (state: IApplicationState) => state.job, // selects which state properties are merged into the component's props
    IJobState.actionCreators                 // selects which action creators are merged into the component's props
)(CreateJobView);