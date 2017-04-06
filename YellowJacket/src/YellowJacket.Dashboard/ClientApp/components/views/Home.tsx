import * as React from "react";

import "animate";

export interface HomeProps {
    body: React.ReactElement<any>;
}

export class Home extends React.Component<HomeProps, void> {
    render() {
        return (
            <div className="wrapper wrapper-content animated fadeInRight">
                <div className="row">
                    <div className="col-lg-12">
                        <div className="text-center m-t-lg">
                            <h1>
                                Home
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

export default Home