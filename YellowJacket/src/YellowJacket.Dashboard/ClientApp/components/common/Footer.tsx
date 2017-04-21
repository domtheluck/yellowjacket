import * as React from "react";

class Footer extends React.Component<void, any> {
    render() {
        return (
            <div className="footer">
                <div className="pull-right">
                    10GB of <strong>250GB</strong> Free.
                </div>
                <div>
                    <strong>Copyright</strong> Example Company &copy; 2015-2017
                </div>
            </div>
        );
    }
}

export default Footer