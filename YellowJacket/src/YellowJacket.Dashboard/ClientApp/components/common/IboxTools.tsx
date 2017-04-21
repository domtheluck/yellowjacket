import * as React from "react";
import * as $ from "jquery";

//import "./css/site.css";
//import "./css/inspinia.css";

interface IIboxToolsProps {
    collapseEnabled: boolean;
    wrenchEnabled: boolean;
    closeEnabled: boolean;
}

class IboxTools extends React.Component<IIboxToolsProps, void> {
    collapsePanel(e) {
        e.preventDefault();
        const element = $(e.target);
        var ibox = element.closest("div.ibox");
        const button = element.closest("i");
        const content = ibox.find("div.ibox-content");
        content.slideToggle(200);
        button.toggleClass("fa-chevron-up").toggleClass("fa-chevron-down");
        ibox.toggleClass("").toggleClass("border-bottom");
        setTimeout(() => {
            ibox.resize();
            ibox.find("[id^=map-]").resize();
        }, 50);
    };

    closePanel(e) {
        e.preventDefault();
        const element = $(e.target);
        const content = element.closest("div.ibox");
        content.remove();
    };

    render() {
        let collapseButton = null;

        if (this.props.collapseEnabled) {
            collapseButton = <a className="collapse-link" onClick={this.collapsePanel}><i className="fa fa-chevron-up"></i></a>;
        }

        return (
            <div className="ibox-tools">
                {collapseButton}
                <a className="dropdown-toggle" data-toggle="dropdown" href="#">
                    <i className="fa fa-wrench"></i>
                </a>
                <ul className="dropdown-menu dropdown-user">
                    <li><a href="#">Config option 1</a>
                    </li>
                    <li><a href="#">Config option 2</a>
                    </li>
                </ul>
                <a className="close-link" onClick={this.closePanel}>
                    <i className="fa fa-times"></i>
                </a>
            </div >
        );
    }
}

export default IboxTools