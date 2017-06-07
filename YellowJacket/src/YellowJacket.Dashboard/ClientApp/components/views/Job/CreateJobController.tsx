import * as React from "react"
import { connect } from "react-redux"
import { AppState, Job, DispatchProps } from "./jobTypes"
import CreateJobView from "./CreateJobView"

// whatever props you may need from owner
interface OwnProps {
}

// whatever props you may need from state
interface StateProps {
    job?: Job
}

interface P extends OwnProps, StateProps, DispatchProps { }

class CreateJobController extends React.PureComponent<P, {}> {
    handleSave = (job: Job) => {
        console.log("handleSave called", job);
        this.props.dispatch(/.../) // trigger things in your state
        
    }

    render() {
        return <CreateJobView form="createJob" handleSave={this.handleSave} />
    }
}

const mapStateToProps = (state: AppState): StateProps => ({
    job: state.job,
})

export default connect<OwnProps, StateProps, DispatchProps>(mapStateToProps)(CreateJobController)