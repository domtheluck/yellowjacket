import * as React from "react";
import * as $ from "jquery";

export default class Blank extends React.Component<any, void> {
    render() {
        return (
            <div>
                {this.props.children}
            </div>
        );
    }

    componentDidMount(){
        $("body").addClass("gray-bg");
    }

    componentWillUnmount(){
        $("body").removeClass("gray-bg");
    }
}