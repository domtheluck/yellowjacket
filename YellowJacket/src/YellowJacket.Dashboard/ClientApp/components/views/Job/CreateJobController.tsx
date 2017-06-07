import * as React from "react"
import { connect } from "react-redux"
import { AppState, Job, DispatchProps } from "./jobTypes"
import CreateJobView from "./CreateJobView"
import * as IJobState from "../../../store/JobStore";

// whatever props you may need from owner
interface OwnProps {
}

// whatever props you may need from state
interface StateProps {
    job?: Job
}

// at runtime, Redux will merge together
type JobProps =
    IJobState.IJobState // state we've requested from the Redux store
    & typeof IJobState.actionCreators;   // plus action creators we've requested

interface P extends OwnProps, StateProps, DispatchProps, JobProps { }

class CreateJobController extends React.PureComponent<P, {}> {
    handleSave = (job: Job) => {
        console.log("handleSave called", job);
        this.props.dispatch(this.props.createJob) // trigger things in your state    
    }

    render() {
        return <CreateJobView form="createJob" handleSave={this.handleSave} />
    }
}

const mapStateToProps = (state: AppState): StateProps => ({
    job: state.job,
})

export default connect<OwnProps, StateProps, DispatchProps>(mapStateToProps)(CreateJobController)