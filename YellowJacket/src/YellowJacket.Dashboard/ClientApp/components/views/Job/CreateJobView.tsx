import * as React from "react"
import { reduxForm, getFormValues, FormProps, FieldArray } from "redux-form"
import { connect } from "react-redux"
import { AppState, Job, DispatchProps } from "./JobTypes"

interface StateProps {
    formValues: Job | undefined,
}

interface OwnProps extends FormProps<Job, AppState, any> {
    handleSave: (job: Job) => void,
}

interface P extends OwnProps, StateProps, DispatchProps { }

// notice that this.props.handleSubmit! and props.form! need a bang "!" when using strictNullChecks
// reason is complicated, you can have a look here: https://github.com/erikras/redux-form/pull/1318#issuecomment-231590672
class CreateJobView extends React.PureComponent<P, {}> {

    render() {
        // this.props.handleSubmit => redux form's handler
        // this.props.handleSave => our controller's handler
        return <form onSubmit={this.props.handleSubmit!(this.props.handleSave)}>
            <legend>Job</legend>
            <div className="btn-toolbar">
                <button type="submit" className="btn btn-primary" disabled={this.props.submitting || this.props.pristine}>
                    Process
                </button>
            </div>
        </form>
    }
}

// if you need additional stuff from app's state, you totally can connect your form
const mapStateToProps = (state: AppState, props: OwnProps): StateProps => ({
    formValues: getFormValues<Job>(props.form!)(state),
})

export default connect<StateProps, DispatchProps, OwnProps>(mapStateToProps)(reduxForm({})(CreateJobView))