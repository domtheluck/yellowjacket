import * as React from 'react';

export class NotFound extends React.Component<void, void> {
    render() {
        return (
            <div className="wrapper wrapper-content animated fadeInRight">
                <div className="row">
                    <div className="col-lg-12">
                        <div className="text-center m-t-lg">
                            <h1>
                                Error 404
                            </h1>
                            <small>
                                Error 404
                            </small>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default NotFound