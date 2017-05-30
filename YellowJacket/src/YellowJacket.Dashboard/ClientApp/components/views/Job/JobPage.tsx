import * as React from 'react';
import { Dispatch } from 'redux';

import JobForm, { IJobFormData, IJobFormProps } from './JobForm';

export interface ContactPageProps {
}

class ContactPage extends React.Component<IJobFormProps, {}> {

    // redux-form
    // SubmitHandler<FormData extends DataShape, P, S>
    save(values: IJobFormData, dispatch: Dispatch<{}>, props: IJobFormProps) {
        // tslint:disable-next-line:no-console
        console.log('JobPage.save');
        // tslint:disable-next-line:no-console
        console.log(values);
    }

    render() {
        return (
            // Property name should be "onSubmit", by default
            // The redux-form "handleSubmit" method will invoke "onSubmit", by default
            <JobForm onSubmit={this.save} />
        );
    }
}

export default ContactPage;