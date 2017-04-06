import * as React from "react";

import "animate";

export interface View1Props {
    body: React.ReactElement<any>;
}

export class View1 extends React.Component<View1Props, void> {
    render() {
        return (
            <div className="wrapper wrapper-content animated fadeInRight">
                <div className="row">
                    <div className="col-lg-12">
                        <div className="text-center m-t-lg">
                            <h1>
                                View1
                            </h1>
                            <small>
                                It is an application skeleton for a typical web app. You can use it to quickly bootstrap your webapp projects.
                            </small>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default View1