import * as React from "react";

export class Home extends React.Component<void, void> {
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
                                It is an application skeleton for a typical web app. You can use it to quickly bootstrap your webapp projects. (UPDATED)
                            </small>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Home