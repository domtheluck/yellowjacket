import * as React from "react";
import { Dispatch } from "redux";
import { connect } from "react-redux"
import { Field, reduxForm, FormProps, FormErrors } from "redux-form";
import * as classNames from "classnames";

import { IApplicationState } from "../../../store";
import * as IJobState from "../../../store/JobStore";

export interface IJobFormData {
    firstName?: string;
    lastName?: string;
    email?: string;
}

export interface IJobFormProps extends FormProps<IJobFormData, {}, {}> {
    dispatch?: Dispatch<{}>;

    //onSubmit: (values: IJobFormData, dispatch: Dispatch<{}>, props: IJobFormProps) => void | FormErrors<IJobFormData> | Promise<any>;
}

const renderInput = (field: any) => {
    console.log("renderInput");
    const hasError = !!field.meta.error && !!field.meta.touched;
    
    let classNameArray = ["form-group", { "has-error": hasError }];
    const classNamesResult = classNames(classNameArray);

    return (
        <div className={classNamesResult}>
            <label className="col-sm-2 control-label" htmlFor="firstName">{field.placeholder}</label>
            <div className="col-sm-10">
                <input {...field.input} type={field.type} placeholder={field.placeholder} className="form-control" />
                {hasError && <span className="help-block">{field.meta.error}</span>}
            </div>
        </div>
    );
};

class JobForm extends React.Component<FormProps<FormData, {}, {}> & IJobFormProps, {}> {
    render() {
        const { handleSubmit, onSubmit } = this.props;

        return (
            <div className="container">
                <form className="form-horizontal">
                    <Field name="firstName" component={renderInput} placeholder="First Name" type="text" />
                    <Field name="lastName" component={renderInput} placeholder="Last Name" type="text" />
                    <Field name="email" component={renderInput} placeholder="Email" type="email" />

                    <button className="btn btn-default" type="submit" disabled={this.props.submitting || this.props.pristine} onClick={this.props.handleSubmit}>Submit</button>
                </form>
            </div>
        );
    }
}

const validate = (values: Readonly<IJobFormData>): FormErrors<IJobFormData> => { 
   
    console.log("validate");
    const { firstName, lastName, email } = values;

    const errors: FormErrors<IJobFormData> = {};

    if (!firstName) {
        errors.firstName = "The First Name is required";
    }

    if (!lastName) {
        errors.lastName = "The Last Name is required";
    }

    if (!email) {
        errors.email = "The Email is required";
    }

    return errors;
};

//export default reduxForm({
export connect(
    (state: IApplicationState) => state.job, // selects which state properties are merged into the component's props
    IJobState.actionCreators                 // selects which action creators are merged into the component's props
)(JobForm);

export default 
    reduxForm<Readonly<IJobFormData>, IJobFormProps, {}>({
        form: "JobForm",
        validate: validate,
        onSubmit: (values, dispatch, props) => {

            console.log('submit is being handled...');
        }
})(JobForm);