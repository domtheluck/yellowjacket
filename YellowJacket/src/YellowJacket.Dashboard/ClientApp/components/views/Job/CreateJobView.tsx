import * as React from "react"
import { reduxForm, getFormValues, FormProps, FieldArray } from "redux-form"
import { connect } from "react-redux"
import { AppState, Job, DispatchProps } from "./JobTypes"

interface IStateProps {
    formValues: any | undefined,
}

interface IOwnProps extends FormProps<Job, AppState, {}> {
    handleSave: (job: Job) => void,
}

// notice that this.props.handleSubmit! and props.form! need a bang "!" when using strictNullChecks
// reason is complicated, you can have a look here: https://github.com/erikras/redux-form/pull/1318#issuecomment-231590672
class CreateJobView extends React.PureComponent<IOwnProps, any> {
    render() {
        // this.props.handleSubmit => redux form's handler
        // this.props.handleSave => our controller's handler
        return <form onSubmit={this.context.handleSubmit!(this.props.handleSave)}>
            <legend>Job</legend>
            <div className="btn-toolbar">
                <button type="submit" className="btn btn-primary" /*disabled={this.props.submitting || this.props.pristine}*/>
                    Process
                </button>
            </div>
        </form>;
    }
}

// if you need additional stuff from app's state, you totally can connect your form
const mapStateToProps = (state: AppState, props: IOwnProps): IStateProps => ({
    formValues: getFormValues(props.form!)(state)
});

export default connect<IStateProps, DispatchProps, IOwnProps>(mapStateToProps)(reduxForm({})(CreateJobView))