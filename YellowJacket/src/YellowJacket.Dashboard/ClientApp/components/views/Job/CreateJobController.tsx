import * as React from "react"
import { connect } from "react-redux"
import { AppState, Job, DispatchProps } from "./jobTypes"
import CreateJobView from "./CreateJobView"
import * as IJobState from "../../../stores/JobStore";

// whatever props you may need from owner
interface IOwnProps {
}

// whatever props you may need from state
interface IStateProps {
    job?: Job
}

// at runtime, Redux will merge together
type JobProps =
    IJobState.IJobState // state we've requested from the Redux store
    & typeof IJobState.actionCreators;   // plus action creators we've requested

interface ICommonProps extends IOwnProps, IStateProps, JobProps { }

class CreateJobController extends React.PureComponent<ICommonProps, {}> {
    handleSave = (job: Job) => {
        console.log("handleSave called", job);
        this.props.createJob(job); // trigger things in your state    
    };

    render() {
        return <CreateJobView form="createJob" handleSave={this.handleSave} />;
    }
}

const mapStateToProps = (state: AppState): IStateProps => ({
    job: state.job
});
export default connect<IOwnProps, IStateProps, DispatchProps>(mapStateToProps)(CreateJobController)